' The Sam Fanclub is a real place.
Imports System.Net.Http
Imports System.Text.Json
Imports MySql.Data.MySqlClient

Public Class Form1

    Private Shared ReadOnly client As New HttpClient()
    Private Shared ReadOnly weights As Integer() = {8, 7, 6, 5, 4, 3, 2, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2}
    Private Shared ReadOnly charMap As New Dictionary(Of Char, Integer) From {
        {"0"c, 0}, {"1"c, 1}, {"2"c, 2}, {"3"c, 3}, {"4"c, 4}, {"5"c, 5}, {"6"c, 6}, {"7"c, 7}, {"8"c, 8}, {"9"c, 9},
        {"A"c, 1}, {"B"c, 2}, {"C"c, 3}, {"D"c, 4}, {"E"c, 5}, {"F"c, 6}, {"G"c, 7}, {"H"c, 8},
        {"J"c, 1}, {"K"c, 2}, {"L"c, 3}, {"M"c, 4}, {"N"c, 5},
        {"P"c, 7}, {"R"c, 9}, {"S"c, 2}, {"T"c, 3}, {"U"c, 4}, {"V"c, 5}, {"W"c, 6}, {"X"c, 7}, {"Y"c, 8}, {"Z"c, 9}
    }

    Private Shared ReadOnly yearToVinChar As New Dictionary(Of Integer, String) From {
        {1980, "A"}, {1981, "B"}, {1982, "C"}, {1983, "D"}, {1984, "E"},
        {1985, "F"}, {1986, "G"}, {1987, "H"}, {1988, "J"}, {1989, "K"},
        {1990, "L"}, {1991, "M"}, {1992, "N"}, {1993, "P"}, {1994, "R"},
        {1995, "S"}, {1996, "T"}, {1997, "V"}, {1998, "W"}, {1999, "X"},
        {2000, "Y"}, {2001, "1"}, {2002, "2"}, {2003, "3"}, {2004, "4"},
        {2005, "5"}, {2006, "6"}, {2007, "7"}, {2008, "8"}, {2009, "9"},
        {2010, "A"}, {2011, "B"}, {2012, "C"}, {2013, "D"}, {2014, "E"},
        {2015, "F"}, {2016, "G"}, {2017, "H"}, {2018, "J"}, {2019, "K"},
        {2020, "L"}, {2021, "M"}, {2022, "N"}, {2023, "P"}, {2024, "R"},
        {2025, "S"}, {2026, "T"}, {2027, "V"}, {2028, "W"}, {2029, "X"},
        {2030, "Y"}
    }

    Private ReadOnly Property ConnectionString As String
        Get
            Return My.Settings.ConnectionString
        End Get
    End Property

    Private Shared ReadOnly fieldMap As New Dictionary(Of String, String) From {
        {"ModelYear", "Model Year"},
        {"Make", "Make"},
        {"Model", "Model"},
        {"BodyType", "Body Class"},
        {"Country", "Plant Country"},
        {"DriveLineType", "Drive Type"},
        {"Series", "Series"},
        {"VehicleClass", "Vehicle Type"}
    }

    Private discrepantRowIndices As New List(Of Integer)
    Private _lastNhtsaFields As Dictionary(Of String, String)
    Private _lastInputVin As String = ""

    ' -----------------------------------------------------------------------
    '  FORM LOAD
    ' -----------------------------------------------------------------------
    'Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    SetupDGV(dgv1)
    '    SetupDGV(dgv2)

    '    AddHandler dgv1.Scroll, Sub(s, e2) pnlConnector.Invalidate()
    '    AddHandler dgv2.Scroll, Sub(s, e2) pnlConnector.Invalidate()

    '    pnlConnector.BackColor = Color.FromArgb(245, 245, 245)

    '    txtYear.Visible = False
    '    Label5.Visible = False
    '    btnCheckCorrectedVin.Visible = False
    '    btnCheckYear.Visible = False
    'End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Explicitly rewire Tab 1 button handlers
        AddHandler btnCheck.Click, AddressOf btnCheck_Click
        AddHandler btnCheckYear.Click, AddressOf btnCheckYear_Click
        AddHandler btnCheckCorrectedVin.Click, AddressOf btnCheckCorrectedVin_Click

        ' Rest of your existing load code
        SetupDGV(dgv1)
        SetupDGV(dgv2)

        AddHandler dgv1.Scroll, Sub(s, e2) pnlConnector.Invalidate()
        AddHandler dgv2.Scroll, Sub(s, e2) pnlConnector.Invalidate()

        pnlConnector.BackColor = Color.FromArgb(245, 245, 245)

        txtYear.Visible = False
        Label5.Visible = False
        btnCheckCorrectedVin.Visible = False
        btnCheckYear.Visible = False

    End Sub
    ' -----------------------------------------------------------------------
    '  GRID SETUP
    ' -----------------------------------------------------------------------
    Private Sub SetupDGV(dgv As DataGridView)
        dgv.Columns.Clear()
        dgv.Columns.Add("colField", "Field")
        dgv.Columns.Add("colValue", "Value")
        dgv.Columns(0).Width = 140
        dgv.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgv.Columns(1).Width = 250
        dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgv.AllowUserToAddRows = False
        dgv.ReadOnly = True
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    ' -----------------------------------------------------------------------
    '  PARTIAL VIN (positions 1-8 + 10-11, i.e. indices 0-7 + 9 + 10)
    ' -----------------------------------------------------------------------
    Private Function MakePartialVin(vin As String) As String
        Return vin.Substring(0, 8) & vin(9) & vin(10)
    End Function

    ' -----------------------------------------------------------------------
    '  SHOW YEAR CONTROLS
    '  Always called after a successful VIN check. We never try to guess
    '  whether the year or VDS is wrong — we ask the user to confirm the
    '  model year first, then btnCheckYear does the real analysis.
    ' -----------------------------------------------------------------------
    Private Sub ShowYearControls()
        Label5.Visible = True
        txtYear.Visible = True
        btnCheckYear.Visible = True
    End Sub

    ' -----------------------------------------------------------------------
    '  MAIN CHECK BUTTON
    ' -----------------------------------------------------------------------
    Private Async Sub btnCheck_Click(sender As Object, e As EventArgs)
        ' Reset UI
        dgv1.Rows.Clear()
        dgv2.Rows.Clear()
        discrepantRowIndices.Clear()
        txtPartialVin.Text = ""
        txtYear.Text = ""
        pnlConnector.Invalidate()
        lblStatus.Text = "Working..."
        lblStatus.ForeColor = Color.DimGray
        lblStatus.Visible = True
        btnCheckCorrectedVin.Visible = False
        txtYear.Visible = False
        Label5.Visible = False
        btnCheckYear.Visible = False
        _lastNhtsaFields = Nothing
        _lastInputVin = ""

        Dim vin = txtVIN.Text.Trim.ToUpper

        If vin.Length <> 17 Then
            lblStatus.Text = "VIN must be exactly 17 characters."
            lblStatus.ForeColor = Color.Crimson
            Return
        End If

        Dim checkResult = VinCheckDigit(vin)

        Dim partialVIN = MakePartialVin(vin)
        txtPartialVin.Text = partialVIN

        ' Fetch NHTSA
        Dim nhtsaFields As Dictionary(Of String, String)
        Try
            nhtsaFields = Await FetchNHTSA(vin)
        Catch ex As Exception
            lblStatus.Text = $"NHTSA API error: {ex.Message}"
            lblStatus.ForeColor = Color.Crimson
            Return
        End Try

        ' Fetch database
        Dim dbFields As Dictionary(Of String, String)
        Try
            dbFields = QueryDatabase(partialVIN)
        Catch ex As Exception
            lblStatus.Text = $"Database error: {ex.Message}"
            lblStatus.ForeColor = Color.Crimson
            Return
        End Try

        _lastNhtsaFields = nhtsaFields
        _lastInputVin = vin

        ' No DB match — show NHTSA data and ask for year
        If dbFields Is Nothing OrElse dbFields.Count = 0 Then
            PopulateNhtsaGridFull(nhtsaFields)
            lblStatus.Text = "No matching partial VIN found in database. Please confirm the model year below."
            lblStatus.ForeColor = Color.DimGray
            ShowYearControls()
            Return
        End If

        ' Determine discrepancies
        Dim discrepantFields As New HashSet(Of String)
        For Each kvp In fieldMap
            Dim dbKey = kvp.Key
            Dim nhtsaKey = kvp.Value
            If Not dbFields.ContainsKey(dbKey) OrElse Not nhtsaFields.ContainsKey(nhtsaKey) Then Continue For
            Dim dbVal = dbFields(dbKey)
            Dim nhtsaVal = nhtsaFields(nhtsaKey)
            If String.IsNullOrWhiteSpace(dbVal) OrElse String.IsNullOrWhiteSpace(nhtsaVal) Then Continue For
            If Not NormalizeValue(dbVal).Equals(NormalizeValue(nhtsaVal), StringComparison.OrdinalIgnoreCase) Then
                discrepantFields.Add(dbKey)
            End If
        Next

        ' Build ordered key list: discrepant rows first
        Dim orderedKeys As New List(Of String)
        For Each kvp In fieldMap
            If discrepantFields.Contains(kvp.Key) Then orderedKeys.Add(kvp.Key)
        Next
        For Each kvp In fieldMap
            If Not discrepantFields.Contains(kvp.Key) Then orderedKeys.Add(kvp.Key)
        Next

        ' Populate both grids
        For i = 0 To orderedKeys.Count - 1
            Dim dbKey = orderedKeys(i)
            Dim nhtsaKey = fieldMap(dbKey)
            Dim dbVal = If(dbFields.ContainsKey(dbKey), dbFields(dbKey), "—")
            Dim nhtsaVal = If(nhtsaFields.ContainsKey(nhtsaKey), nhtsaFields(nhtsaKey), "—")
            Dim isDiscrepant = discrepantFields.Contains(dbKey)
            Dim bg = If(isDiscrepant, Color.FromArgb(255, 200, 200), Color.White)

            dgv1.Rows.Add(nhtsaKey, nhtsaVal)
            dgv1.Rows(i).DefaultCellStyle.BackColor = bg
            dgv2.Rows.Add(dbKey, dbVal)
            dgv2.Rows(i).DefaultCellStyle.BackColor = bg
            If isDiscrepant Then discrepantRowIndices.Add(i)
        Next

        ' Add check digit row to dgv1 only
        Dim checkBg = If(checkResult.isValid, Color.FromArgb(220, 255, 220), Color.FromArgb(255, 200, 200))
        dgv1.Rows.Add("Check Digit", If(checkResult.isValid,
            $"✓ Valid ({checkResult.expectedCheckDigit})",
            $"✗ Invalid — expected '{checkResult.expectedCheckDigit}', found '{vin(8)}'"))
        dgv1.Rows(dgv1.Rows.Count - 1).DefaultCellStyle.BackColor = checkBg

        If discrepantFields.Count > 0 Then
            lblStatus.Text = $"⚠ {discrepantFields.Count} discrepancy(ies) found between NHTSA and database. Please confirm the model year below."
            lblStatus.ForeColor = Color.Crimson
        Else
            lblStatus.Text = "✓ No discrepancies found! Please confirm the model year below."
            lblStatus.ForeColor = Color.ForestGreen
        End If

        dgv1.ClearSelection()
        dgv2.ClearSelection()
        pnlConnector.Invalidate()

        ' Always ask the user to confirm the year — never try to guess
        ShowYearControls()
    End Sub

    ' -----------------------------------------------------------------------
    '  CHECK YEAR BUTTON
    '  Now does the real analysis. With the user's year as ground truth:
    '    - If position 10 matches the stated year → year is fine, VDS is suspect
    '    - If position 10 does not match          → year digit is wrong, suggest fix
    ' -----------------------------------------------------------------------
    Private Sub btnCheckYear_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(_lastInputVin) OrElse _lastInputVin.Length <> 17 Then
            MessageBox.Show("Please run a VIN check first.", "No VIN loaded",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim yearText = txtYear.Text.Trim
        Dim modelYear As Integer
        If Not Integer.TryParse(yearText, modelYear) OrElse Not yearToVinChar.ContainsKey(modelYear) Then
            MessageBox.Show("Please enter a valid model year between 1980 and 2030.",
                            "Invalid Year", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim expectedChar = yearToVinChar(modelYear)   ' what position 10 should be for this year
        Dim actualChar = _lastInputVin(9).ToString  ' what position 10 actually is

        If expectedChar = actualChar Then
            ' Year digit is correct — the problem must be in the VDS (positions 4-8)
            ' Now we can safely use NHTSA's VDS correction signals
            Dim nhtsaSuggestedVin = ""
            If _lastNhtsaFields IsNot Nothing AndAlso _lastNhtsaFields.ContainsKey("Suggested VIN") Then
                nhtsaSuggestedVin = _lastNhtsaFields("Suggested VIN").Trim.ToUpper
            End If

            If Not String.IsNullOrWhiteSpace(nhtsaSuggestedVin) AndAlso nhtsaSuggestedVin.Length = 17 Then
                lblStatus.Text = $"✓ Year digit '{actualChar}' confirmed correct for {modelYear}. " &
                                 $"⚠ VDS issue detected — suggested corrected VIN: {nhtsaSuggestedVin}"
                lblStatus.ForeColor = Color.DarkOrange
                btnCheckCorrectedVin.Tag = nhtsaSuggestedVin
                btnCheckCorrectedVin.Visible = True

            ElseIf _lastNhtsaFields IsNot Nothing AndAlso _lastNhtsaFields.ContainsKey("Possible Values") Then
                Dim possibleValues = _lastNhtsaFields("Possible Values").Trim
                Dim suggestedVin = ApplyPossibleValues(_lastInputVin, possibleValues)
                If suggestedVin <> _lastInputVin Then
                    lblStatus.Text = $"✓ Year digit '{actualChar}' confirmed correct for {modelYear}. " &
                                     $"⚠ VDS issue — possible values: {possibleValues} → Suggested VIN: {suggestedVin}"
                    lblStatus.ForeColor = Color.DarkOrange
                    btnCheckCorrectedVin.Tag = suggestedVin
                    btnCheckCorrectedVin.Visible = True
                Else
                    lblStatus.Text = $"✓ Year digit '{actualChar}' confirmed correct for {modelYear}. " &
                                     $"⚠ VDS issue detected but could not auto-build a corrected VIN."
                    lblStatus.ForeColor = Color.DarkOrange
                End If

            Else
                lblStatus.Text = $"✓ Year digit '{actualChar}' confirmed correct for {modelYear}. " &
                                 $"⚠ VDS (digits 4-8) may be suspect — check positions 4-8 for typos."
                lblStatus.ForeColor = Color.DarkOrange
            End If

        Else
            ' Year digit is wrong — build a corrected VIN with the right year char
            ' and recalculate the check digit
            Dim correctedChars = _lastInputVin.ToCharArray
            correctedChars(9) = expectedChar(0)
            Dim tempVin = New String(correctedChars)
            Dim cr = VinCheckDigit(tempVin)
            If cr.expectedCheckDigit.Length > 0 Then
                correctedChars(8) = cr.expectedCheckDigit(0)
            End If
            Dim correctedVin = New String(correctedChars)

            lblStatus.Text = $"✗ Position 10 mismatch: found '{actualChar}', expected '{expectedChar}' for {modelYear}. " &
                             $"Suggested corrected VIN: {correctedVin}"
            lblStatus.ForeColor = Color.Crimson
            btnCheckCorrectedVin.Tag = correctedVin
            btnCheckCorrectedVin.Visible = True
        End If
    End Sub

    ' -----------------------------------------------------------------------
    '  APPLY NHTSA POSSIBLE VALUES TO VIN
    ' -----------------------------------------------------------------------
    Private Function ApplyPossibleValues(vin As String, possibleValues As String) As String
        Dim chars() As Char = vin.ToCharArray()

        Dim matches = System.Text.RegularExpressions.Regex.Matches(
            possibleValues, "\((\d+):([A-Z0-9])\)")

        For Each m As System.Text.RegularExpressions.Match In matches
            Dim pos As Integer
            If Integer.TryParse(m.Groups(1).Value, pos) Then
                Dim zeroIdx As Integer = pos - 1
                If zeroIdx >= 0 AndAlso zeroIdx < chars.Length Then
                    chars(zeroIdx) = m.Groups(2).Value(0)
                End If
            End If
        Next

        Dim tempVin As String = New String(chars)
        Dim cr = VinCheckDigit(tempVin)
        If cr.expectedCheckDigit.Length > 0 Then
            chars(8) = cr.expectedCheckDigit(0)
        End If

        Return New String(chars)
    End Function

    ' -----------------------------------------------------------------------
    '  CHECK CORRECTED VIN BUTTON
    ' -----------------------------------------------------------------------
    Private Sub btnCheckCorrectedVin_Click(sender As Object, e As EventArgs)
        Dim suggested = If(btnCheckCorrectedVin.Tag?.ToString, "").Trim.ToUpper
        If suggested.Length <> 17 Then
            MessageBox.Show("Corrected VIN is not valid (not 17 characters).", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        txtVIN.Text = suggested
        btnCheck_Click(btnCheck, EventArgs.Empty)
    End Sub

    ' -----------------------------------------------------------------------
    '  POPULATE DGV1 WITH ALL NHTSA FIELDS (no-DB-match fallback)
    ' -----------------------------------------------------------------------
    Private Sub PopulateNhtsaGridFull(nhtsaFields As Dictionary(Of String, String))
        Dim priorityKeys As New HashSet(Of String) From {
            "Suggested VIN", "Error Code", "Possible Values",
            "Additional Error Text", "Error Text", "Vehicle Descriptor",
            "Make", "Manufacturer Name", "Model", "Model Year",
            "Plant City", "Vehicle Type", "Plant Country", "Plant State"
        }

        For Each key In priorityKeys
            If nhtsaFields.ContainsKey(key) Then
                dgv1.Rows.Add(key, nhtsaFields(key))
            End If
        Next

        For Each kvp In nhtsaFields
            If Not priorityKeys.Contains(kvp.Key) Then
                dgv1.Rows.Add(kvp.Key, kvp.Value)
            End If
        Next
    End Sub

    ' -----------------------------------------------------------------------
    '  CONNECTOR PANEL PAINT
    ' -----------------------------------------------------------------------
    Private Sub pnlConnector_Paint(sender As Object, e As PaintEventArgs)
        If discrepantRowIndices.Count = 0 Then Return

        Dim g = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim arrowPen As New Pen(Color.Crimson, 2)
        arrowPen.CustomEndCap = New Drawing2D.AdjustableArrowCap(5, 5)
        Dim matchPen As New Pen(Color.FromArgb(100, 180, 180, 180), 1)
        matchPen.DashStyle = Drawing2D.DashStyle.Dot

        Dim panelWidth = pnlConnector.Width

        Dim GetPanelY = Function(dgv As DataGridView, rowIdx As Integer) As Integer
                            Dim rect = dgv.GetRowDisplayRectangle(rowIdx, True)
                            Dim midInDgv As New Point(0, rect.Top + rect.Height \ 2)
                            Dim screenPt = dgv.PointToScreen(midInDgv)
                            Return pnlConnector.PointToClient(screenPt).Y
                        End Function

        For i = 0 To Math.Min(dgv1.Rows.Count, dgv2.Rows.Count) - 1
            Dim rect1 = dgv1.GetRowDisplayRectangle(i, True)
            Dim rect2 = dgv2.GetRowDisplayRectangle(i, True)
            If rect1.Height = 0 OrElse rect2.Height = 0 Then Continue For
            Dim y1 = GetPanelY(dgv1, i)
            Dim y2 = GetPanelY(dgv2, i)
            g.DrawBezier(matchPen, New Point(0, y1), New Point(panelWidth \ 2, y1),
                         New Point(panelWidth \ 2, y2), New Point(panelWidth, y2))
        Next

        For Each rowIdx In discrepantRowIndices
            If rowIdx >= dgv1.Rows.Count OrElse rowIdx >= dgv2.Rows.Count Then Continue For
            Dim rect1 = dgv1.GetRowDisplayRectangle(rowIdx, True)
            Dim rect2 = dgv2.GetRowDisplayRectangle(rowIdx, True)
            If rect1.Height = 0 OrElse rect2.Height = 0 Then Continue For
            Dim y1 = GetPanelY(dgv1, rowIdx)
            Dim y2 = GetPanelY(dgv2, rowIdx)
            g.DrawBezier(arrowPen, New Point(0, y1), New Point(panelWidth \ 2, y1),
                         New Point(panelWidth \ 2, y2), New Point(panelWidth, y2))
        Next

        arrowPen.Dispose()
        matchPen.Dispose()
    End Sub

    ' -----------------------------------------------------------------------
    '  DATABASE QUERY
    ' -----------------------------------------------------------------------
    Private Function QueryDatabase(partialVIN As String) As Dictionary(Of String, String)
        Dim dbFields As New Dictionary(Of String, String)
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Dim sql As String =
                "SELECT ModelYear, Make, Model, BodyType, Country, DriveLineType, Series, VehicleClass " &
                "FROM partials WHERE vehVinPartial = @pvin LIMIT 1"
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@pvin", partialVIN)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        For i As Integer = 0 To reader.FieldCount - 1
                            Dim colName As String = reader.GetName(i)
                            Dim val As String = If(reader.IsDBNull(i), "", reader.GetValue(i).ToString().Trim())
                            If Not String.IsNullOrWhiteSpace(val) Then dbFields(colName) = val
                        Next
                    End If
                End Using
            End Using
        End Using
        Return dbFields
    End Function

    ' -----------------------------------------------------------------------
    '  NORMALIZE VALUE (for comparison)
    ' -----------------------------------------------------------------------
    Private Function NormalizeValue(val As String) As String
        Dim result As String = val.Trim()
        result = System.Text.RegularExpressions.Regex.Replace(result, "\s*\([^)]*\)\s*$", "").Trim()
        result = result.Replace("U.S.A.", "UNITED STATES").Replace("USA", "UNITED STATES")
        result = System.Text.RegularExpressions.Regex.Replace(result, "\s+", " ")
        Return result.ToUpper()
    End Function

    ' -----------------------------------------------------------------------
    '  NHTSA API FETCH
    ' -----------------------------------------------------------------------
    Private Async Function FetchNHTSA(vin As String) As Task(Of Dictionary(Of String, String))
        Dim url As String = $"https://vpic.nhtsa.dot.gov/api/vehicles/decodevin/{vin}?format=json"
        Dim response As String = Await client.GetStringAsync(url)
        Dim doc As JsonDocument = JsonDocument.Parse(response)
        Dim results As JsonElement = doc.RootElement.GetProperty("Results")
        Dim fields As New Dictionary(Of String, String)
        For Each item As JsonElement In results.EnumerateArray()
            Dim variable As String = If(item.GetProperty("Variable").GetString(), "")
            Dim value As String = If(item.GetProperty("Value").GetString(), "")
            If Not String.IsNullOrWhiteSpace(value) AndAlso value <> "Not Applicable" Then
                fields(variable) = value
            End If
        Next
        Return fields
    End Function

    ' -----------------------------------------------------------------------
    '  CHECK DIGIT CALCULATION
    ' -----------------------------------------------------------------------
    Private Function VinCheckDigit(vin As String) As (isValid As Boolean, message As String, expectedCheckDigit As String)
        Dim invalidChars As New HashSet(Of Char) From {"I"c, "O"c, "Q"c}
        Dim invalidFound As New List(Of Char)
        For Each c As Char In vin
            If invalidChars.Contains(c) Then invalidFound.Add(c)
        Next
        If invalidFound.Count > 0 Then
            Dim chars = String.Join("', '", invalidFound.Distinct())
            Return (False, $"Invalid character(s) '{chars}' found. I, O, and Q are not allowed.", "")
        End If

        Dim total As Integer = 0
        For i As Integer = 0 To vin.Length - 1
            Dim c As Char = vin(i)
            If Not Char.IsLetterOrDigit(c) Then
                Return (False, $"Unexpected character '{c}' at position {i + 1}.", "")
            End If
            Dim value As Integer = If(Char.IsLetter(c), charMap(c), Integer.Parse(c.ToString()))
            total += value * weights(i)
        Next

        Dim checkDigit As Integer = total Mod 11
        Dim expectedCheckDigit As String = If(checkDigit = 10, "X", checkDigit.ToString())

        If expectedCheckDigit = vin(8).ToString() Then
            Return (True, $"✓ Valid ({expectedCheckDigit}). Total: {total}, mod 11 = {checkDigit}", expectedCheckDigit)
        Else
            Return (False, $"✗ Invalid — expected '{expectedCheckDigit}', found '{vin(8)}'. Total: {total}, mod 11 = {checkDigit}", expectedCheckDigit)
        End If
    End Function

    ' -----------------------------------------------------------------------
    '  STUB HANDLERS
    ' -----------------------------------------------------------------------
    Private Sub txtVIN_TextChanged(sender As Object, e As EventArgs)
    End Sub

    Private Sub dgv1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
    Private Sub btnSqlQuery_Click(sender As Object, e As EventArgs) Handles btnSqlQuery.Click
        Dim sql = txtSql.Text.Trim()

        'If Not sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase) Then
        '    MessageBox.Show("Only SELECT queries are allowed.", "Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Return
        'End If

        'If String.IsNullOrWhiteSpace(sql) Then
        '    MessageBox.Show("Please enter a SQL query.", "Empty Query", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Return
        'End If

        Try
            DataGridView1.Columns.Clear()
            DataGridView1.Rows.Clear()

            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Using cmd As New MySqlCommand(sql, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()

                        ' Build columns from result schema
                        For i As Integer = 0 To reader.FieldCount - 1
                            DataGridView1.Columns.Add(reader.GetName(i), reader.GetName(i))
                        Next

                        ' Populate rows
                        Dim rowCount As Integer = 0
                        While reader.Read()
                            Dim row(reader.FieldCount - 1) As Object
                            For i As Integer = 0 To reader.FieldCount - 1
                                row(i) = If(reader.IsDBNull(i), "", reader.GetValue(i).ToString())
                            Next
                            DataGridView1.Rows.Add(row)
                            rowCount += 1
                        End While

                        MessageBox.Show($"Query returned {rowCount} row(s).", "Done",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show($"MySQL error:{Environment.NewLine}{ex.Message}",
                            "Query Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"Unexpected error:{Environment.NewLine}{ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class

