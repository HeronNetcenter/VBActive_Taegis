<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogsMesGeral
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lvwAlertasMes = New System.Windows.Forms.ListView()
        Me.Dia = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Cliente = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TipoLog = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Quantidade = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Acumulado = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.chkAlerts = New System.Windows.Forms.CheckBox()
        Me.chkAssets = New System.Windows.Forms.CheckBox()
        Me.chkInvestigations = New System.Windows.Forms.CheckBox()
        Me.chkCollectors = New System.Windows.Forms.CheckBox()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvwAlertasMes
        '
        Me.lvwAlertasMes.BackColor = System.Drawing.SystemColors.Info
        Me.lvwAlertasMes.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Dia, Me.Cliente, Me.TipoLog, Me.Quantidade, Me.Acumulado})
        Me.lvwAlertasMes.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwAlertasMes.FullRowSelect = True
        Me.lvwAlertasMes.GridLines = True
        Me.lvwAlertasMes.Location = New System.Drawing.Point(6, 53)
        Me.lvwAlertasMes.MultiSelect = False
        Me.lvwAlertasMes.Name = "lvwAlertasMes"
        Me.lvwAlertasMes.Size = New System.Drawing.Size(481, 565)
        Me.lvwAlertasMes.TabIndex = 2
        Me.lvwAlertasMes.UseCompatibleStateImageBehavior = False
        Me.lvwAlertasMes.View = System.Windows.Forms.View.Details
        '
        'Dia
        '
        Me.Dia.Text = "Dia"
        '
        'Cliente
        '
        Me.Cliente.Text = "Cliente"
        Me.Cliente.Width = 100
        '
        'TipoLog
        '
        Me.TipoLog.Text = "Tipo"
        Me.TipoLog.Width = 100
        '
        'Quantidade
        '
        Me.Quantidade.Text = "Quantidade"
        Me.Quantidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Quantidade.Width = 100
        '
        'Acumulado
        '
        Me.Acumulado.Text = "Acumulado"
        Me.Acumulado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Acumulado.Width = 100
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 621)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(499, 22)
        Me.StatusStrip1.TabIndex = 22
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(128, 17)
        Me.ToolStripStatusLabel1.Text = "Nostromo C 2022-2023"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(120, 17)
        Me.ToolStripStatusLabel2.Text = "ToolStripStatusLabel2"
        '
        'chkAlerts
        '
        Me.chkAlerts.AutoSize = True
        Me.chkAlerts.Checked = True
        Me.chkAlerts.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAlerts.Location = New System.Drawing.Point(6, 12)
        Me.chkAlerts.Name = "chkAlerts"
        Me.chkAlerts.Size = New System.Drawing.Size(52, 17)
        Me.chkAlerts.TabIndex = 23
        Me.chkAlerts.Text = "Alerts"
        Me.chkAlerts.UseVisualStyleBackColor = True
        '
        'chkAssets
        '
        Me.chkAssets.AutoSize = True
        Me.chkAssets.Checked = True
        Me.chkAssets.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAssets.Location = New System.Drawing.Point(84, 12)
        Me.chkAssets.Name = "chkAssets"
        Me.chkAssets.Size = New System.Drawing.Size(57, 17)
        Me.chkAssets.TabIndex = 24
        Me.chkAssets.Text = "Assets"
        Me.chkAssets.UseVisualStyleBackColor = True
        '
        'chkInvestigations
        '
        Me.chkInvestigations.AutoSize = True
        Me.chkInvestigations.Checked = True
        Me.chkInvestigations.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkInvestigations.Location = New System.Drawing.Point(265, 12)
        Me.chkInvestigations.Name = "chkInvestigations"
        Me.chkInvestigations.Size = New System.Drawing.Size(91, 17)
        Me.chkInvestigations.TabIndex = 26
        Me.chkInvestigations.Text = "Investigations"
        Me.chkInvestigations.UseVisualStyleBackColor = True
        '
        'chkCollectors
        '
        Me.chkCollectors.AutoSize = True
        Me.chkCollectors.Checked = True
        Me.chkCollectors.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCollectors.Location = New System.Drawing.Point(167, 12)
        Me.chkCollectors.Name = "chkCollectors"
        Me.chkCollectors.Size = New System.Drawing.Size(72, 17)
        Me.chkCollectors.TabIndex = 25
        Me.chkCollectors.Text = "Collectors"
        Me.chkCollectors.UseVisualStyleBackColor = True
        '
        'frmLogsMesGeral
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(499, 643)
        Me.Controls.Add(Me.chkInvestigations)
        Me.Controls.Add(Me.chkCollectors)
        Me.Controls.Add(Me.chkAssets)
        Me.Controls.Add(Me.chkAlerts)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.lvwAlertasMes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmLogsMesGeral"
        Me.Text = "frmLogsMesGeral"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvwAlertasMes As ListView
    Friend WithEvents Dia As ColumnHeader
    Friend WithEvents Cliente As ColumnHeader
    Friend WithEvents Quantidade As ColumnHeader
    Friend WithEvents Acumulado As ColumnHeader
    Friend WithEvents TipoLog As ColumnHeader
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents chkAlerts As CheckBox
    Friend WithEvents chkAssets As CheckBox
    Friend WithEvents chkInvestigations As CheckBox
    Friend WithEvents chkCollectors As CheckBox
End Class
