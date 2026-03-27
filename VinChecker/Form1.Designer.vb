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
        btnCheck = New Button()
        txtVIN = New TextBox()
        lblStatus = New Label()
        Label1 = New Label()
        dgv1 = New DataGridView()
        dgv2 = New DataGridView()
        pnlConnector = New Panel()
        txtPartialVin = New TextBox()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        btnCheckCorrectedVin = New Button()
        txtYear = New TextBox()
        Label5 = New Label()
        btnCheckYear = New Button()
        CType(dgv1, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgv2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnCheck
        ' 
        btnCheck.Location = New Point(350, 27)
        btnCheck.Name = "btnCheck"
        btnCheck.Size = New Size(89, 45)
        btnCheck.TabIndex = 0
        btnCheck.Text = "Check VIN"
        btnCheck.UseVisualStyleBackColor = True
        ' 
        ' txtVIN
        ' 
        txtVIN.Location = New Point(59, 27)
        txtVIN.Name = "txtVIN"
        txtVIN.Size = New Size(173, 23)
        txtVIN.TabIndex = 1
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(267, 95)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(0, 15)
        lblStatus.TabIndex = 2
        lblStatus.TextAlign = ContentAlignment.MiddleCenter
        lblStatus.Visible = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(59, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(56, 15)
        Label1.TabIndex = 3
        Label1.Text = "Enter VIN"
        ' 
        ' dgv1
        ' 
        dgv1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv1.Location = New Point(36, 134)
        dgv1.Name = "dgv1"
        dgv1.Size = New Size(321, 370)
        dgv1.TabIndex = 4
        ' 
        ' dgv2
        ' 
        dgv2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv2.Location = New Point(591, 134)
        dgv2.Name = "dgv2"
        dgv2.Size = New Size(321, 370)
        dgv2.TabIndex = 5
        ' 
        ' pnlConnector
        ' 
        pnlConnector.Location = New Point(363, 134)
        pnlConnector.Name = "pnlConnector"
        pnlConnector.Size = New Size(222, 370)
        pnlConnector.TabIndex = 6
        ' 
        ' txtPartialVin
        ' 
        txtPartialVin.Location = New Point(59, 87)
        txtPartialVin.Name = "txtPartialVin"
        txtPartialVin.Size = New Size(157, 23)
        txtPartialVin.TabIndex = 7
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(59, 65)
        Label2.Name = "Label2"
        Label2.Size = New Size(62, 15)
        Label2.TabIndex = 8
        Label2.Text = "Partial VIN"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(159, 116)
        Label3.Name = "Label3"
        Label3.Size = New Size(32, 15)
        Label3.TabIndex = 9
        Label3.Text = "VPIC"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(684, 116)
        Label4.Name = "Label4"
        Label4.Size = New Size(117, 15)
        Label4.TabIndex = 10
        Label4.Text = "MySQL Partials Table"
        ' 
        ' btnCheckCorrectedVin
        ' 
        btnCheckCorrectedVin.Location = New Point(562, 32)
        btnCheckCorrectedVin.Name = "btnCheckCorrectedVin"
        btnCheckCorrectedVin.Size = New Size(118, 40)
        btnCheckCorrectedVin.TabIndex = 11
        btnCheckCorrectedVin.Text = "Check Corrected VIN"
        btnCheckCorrectedVin.UseVisualStyleBackColor = True
        btnCheckCorrectedVin.Visible = False
        ' 
        ' txtYear
        ' 
        txtYear.Location = New Point(838, 87)
        txtYear.Name = "txtYear"
        txtYear.Size = New Size(100, 23)
        txtYear.TabIndex = 12
        txtYear.Visible = False
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(852, 67)
        Label5.Name = "Label5"
        Label5.Size = New Size(66, 15)
        Label5.TabIndex = 13
        Label5.Text = "Model Year"
        Label5.Visible = False
        ' 
        ' btnCheckYear
        ' 
        btnCheckYear.Location = New Point(782, 41)
        btnCheckYear.Name = "btnCheckYear"
        btnCheckYear.Size = New Size(115, 23)
        btnCheckYear.TabIndex = 14
        btnCheckYear.Text = "Check Year Digit"
        btnCheckYear.UseVisualStyleBackColor = True
        btnCheckYear.Visible = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(952, 549)
        Controls.Add(btnCheckYear)
        Controls.Add(Label5)
        Controls.Add(txtYear)
        Controls.Add(btnCheckCorrectedVin)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(txtPartialVin)
        Controls.Add(pnlConnector)
        Controls.Add(dgv2)
        Controls.Add(dgv1)
        Controls.Add(Label1)
        Controls.Add(lblStatus)
        Controls.Add(txtVIN)
        Controls.Add(btnCheck)
        Name = "Form1"
        Text = "Form1"
        CType(dgv1, ComponentModel.ISupportInitialize).EndInit()
        CType(dgv2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnCheck As Button
    Friend WithEvents txtVIN As TextBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dgv1 As DataGridView
    Friend WithEvents dgv2 As DataGridView
    Friend WithEvents pnlConnector As Panel
    Friend WithEvents txtPartialVin As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnCheckCorrectedVin As Button
    Friend WithEvents txtYear As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents btnCheckYear As Button

End Class
