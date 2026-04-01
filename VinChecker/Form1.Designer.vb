<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        btnCheckYear = New Button()
        Label5 = New Label()
        txtYear = New TextBox()
        btnCheckCorrectedVin = New Button()
        Label4 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        txtPartialVin = New TextBox()
        pnlConnector = New Panel()
        dgv2 = New DataGridView()
        dgv1 = New DataGridView()
        Label1 = New Label()
        lblStatus = New Label()
        txtVIN = New TextBox()
        btnCheck = New Button()
        TabPage2 = New TabPage()
        chkSafeMode = New CheckBox()
        txtSql = New RichTextBox()
        DataGridView1 = New DataGridView()
        btnSqlQuery = New Button()
        Label6 = New Label()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        CType(dgv2, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgv1, ComponentModel.ISupportInitialize).BeginInit()
        TabPage2.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Location = New Point(-2, 2)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(922, 587)
        TabControl1.TabIndex = 15
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(btnCheckYear)
        TabPage1.Controls.Add(Label5)
        TabPage1.Controls.Add(txtYear)
        TabPage1.Controls.Add(btnCheckCorrectedVin)
        TabPage1.Controls.Add(Label4)
        TabPage1.Controls.Add(Label3)
        TabPage1.Controls.Add(Label2)
        TabPage1.Controls.Add(txtPartialVin)
        TabPage1.Controls.Add(pnlConnector)
        TabPage1.Controls.Add(dgv2)
        TabPage1.Controls.Add(dgv1)
        TabPage1.Controls.Add(Label1)
        TabPage1.Controls.Add(lblStatus)
        TabPage1.Controls.Add(txtVIN)
        TabPage1.Controls.Add(btnCheck)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(914, 559)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Vin Checker"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' btnCheckYear
        ' 
        btnCheckYear.Location = New Point(760, 73)
        btnCheckYear.Name = "btnCheckYear"
        btnCheckYear.Size = New Size(115, 23)
        btnCheckYear.TabIndex = 29
        btnCheckYear.Text = "Check Year Digit"
        btnCheckYear.UseVisualStyleBackColor = True
        btnCheckYear.Visible = False
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(791, 99)
        Label5.Name = "Label5"
        Label5.Size = New Size(66, 15)
        Label5.TabIndex = 28
        Label5.Text = "Model Year"
        Label5.Visible = False
        ' 
        ' txtYear
        ' 
        txtYear.Location = New Point(775, 119)
        txtYear.Name = "txtYear"
        txtYear.Size = New Size(100, 23)
        txtYear.TabIndex = 27
        txtYear.Visible = False
        ' 
        ' btnCheckCorrectedVin
        ' 
        btnCheckCorrectedVin.Location = New Point(540, 64)
        btnCheckCorrectedVin.Name = "btnCheckCorrectedVin"
        btnCheckCorrectedVin.Size = New Size(118, 40)
        btnCheckCorrectedVin.TabIndex = 26
        btnCheckCorrectedVin.Text = "Check Corrected VIN"
        btnCheckCorrectedVin.UseVisualStyleBackColor = True
        btnCheckCorrectedVin.Visible = False
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(662, 148)
        Label4.Name = "Label4"
        Label4.Size = New Size(117, 15)
        Label4.TabIndex = 25
        Label4.Text = "MySQL Partials Table"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(137, 148)
        Label3.Name = "Label3"
        Label3.Size = New Size(32, 15)
        Label3.TabIndex = 24
        Label3.Text = "VPIC"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(37, 97)
        Label2.Name = "Label2"
        Label2.Size = New Size(62, 15)
        Label2.TabIndex = 23
        Label2.Text = "Partial VIN"
        ' 
        ' txtPartialVin
        ' 
        txtPartialVin.Location = New Point(37, 119)
        txtPartialVin.Name = "txtPartialVin"
        txtPartialVin.Size = New Size(157, 23)
        txtPartialVin.TabIndex = 22
        ' 
        ' pnlConnector
        ' 
        pnlConnector.Location = New Point(341, 166)
        pnlConnector.Name = "pnlConnector"
        pnlConnector.Size = New Size(222, 370)
        pnlConnector.TabIndex = 21
        ' 
        ' dgv2
        ' 
        dgv2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv2.Location = New Point(569, 166)
        dgv2.Name = "dgv2"
        dgv2.Size = New Size(321, 370)
        dgv2.TabIndex = 20
        ' 
        ' dgv1
        ' 
        dgv1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv1.Location = New Point(14, 166)
        dgv1.Name = "dgv1"
        dgv1.Size = New Size(321, 370)
        dgv1.TabIndex = 19
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(37, 41)
        Label1.Name = "Label1"
        Label1.Size = New Size(56, 15)
        Label1.TabIndex = 18
        Label1.Text = "Enter VIN"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(245, 127)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(0, 15)
        lblStatus.TabIndex = 17
        lblStatus.TextAlign = ContentAlignment.MiddleCenter
        lblStatus.Visible = False
        ' 
        ' txtVIN
        ' 
        txtVIN.Location = New Point(37, 59)
        txtVIN.Name = "txtVIN"
        txtVIN.Size = New Size(173, 23)
        txtVIN.TabIndex = 16
        ' 
        ' btnCheck
        ' 
        btnCheck.Location = New Point(328, 59)
        btnCheck.Name = "btnCheck"
        btnCheck.Size = New Size(89, 45)
        btnCheck.TabIndex = 15
        btnCheck.Text = "Check VIN"
        btnCheck.UseVisualStyleBackColor = True
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(chkSafeMode)
        TabPage2.Controls.Add(txtSql)
        TabPage2.Controls.Add(DataGridView1)
        TabPage2.Controls.Add(btnSqlQuery)
        TabPage2.Controls.Add(Label6)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(914, 559)
        TabPage2.TabIndex = 1
        TabPage2.Text = "DataBase Connection"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' chkSafeMode
        ' 
        chkSafeMode.AutoSize = True
        chkSafeMode.Location = New Point(176, 73)
        chkSafeMode.Name = "chkSafeMode"
        chkSafeMode.Size = New Size(82, 19)
        chkSafeMode.TabIndex = 5
        chkSafeMode.Text = "Safe Mode"
        chkSafeMode.UseVisualStyleBackColor = True
        ' 
        ' txtSql
        ' 
        txtSql.Location = New Point(10, 178)
        txtSql.Name = "txtSql"
        txtSql.Size = New Size(415, 189)
        txtSql.TabIndex = 4
        txtSql.Text = ""
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(476, 38)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(411, 511)
        DataGridView1.TabIndex = 3
        ' 
        ' btnSqlQuery
        ' 
        btnSqlQuery.Location = New Point(165, 373)
        btnSqlQuery.Name = "btnSqlQuery"
        btnSqlQuery.Size = New Size(86, 34)
        btnSqlQuery.TabIndex = 2
        btnSqlQuery.Text = "Run Query"
        btnSqlQuery.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(165, 160)
        Label6.Name = "Label6"
        Label6.Size = New Size(93, 15)
        Label6.TabIndex = 1
        Label6.Text = "Enter SQL Query"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(918, 587)
        Controls.Add(TabControl1)
        Name = "Form1"
        Text = "Form1"
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        CType(dgv2, ComponentModel.ISupportInitialize).EndInit()
        CType(dgv1, ComponentModel.ISupportInitialize).EndInit()
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents btnCheckYear As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents txtYear As TextBox
    Friend WithEvents btnCheckCorrectedVin As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPartialVin As TextBox
    Friend WithEvents pnlConnector As Panel
    Friend WithEvents dgv2 As DataGridView
    Friend WithEvents dgv1 As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents txtVIN As TextBox
    Friend WithEvents btnCheck As Button
    Friend WithEvents btnSqlQuery As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents txtSql As RichTextBox
    Friend WithEvents chkSafeMode As CheckBox

End Class
