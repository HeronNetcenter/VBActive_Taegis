<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAlertasMesGeral
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
        Me.Alertas = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Acumulado = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvwAlertasMes
        '
        Me.lvwAlertasMes.BackColor = System.Drawing.SystemColors.Info
        Me.lvwAlertasMes.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Dia, Me.Cliente, Me.Alertas, Me.Acumulado})
        Me.lvwAlertasMes.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwAlertasMes.FullRowSelect = True
        Me.lvwAlertasMes.GridLines = True
        Me.lvwAlertasMes.Location = New System.Drawing.Point(6, 12)
        Me.lvwAlertasMes.MultiSelect = False
        Me.lvwAlertasMes.Name = "lvwAlertasMes"
        Me.lvwAlertasMes.Size = New System.Drawing.Size(384, 496)
        Me.lvwAlertasMes.TabIndex = 1
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
        'Alertas
        '
        Me.Alertas.Text = "Alertas"
        Me.Alertas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Alertas.Width = 100
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
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 522)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(396, 22)
        Me.StatusStrip1.TabIndex = 21
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
        'frmAlertasMesGeral
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 544)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.lvwAlertasMes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmAlertasMesGeral"
        Me.Text = "Alertas no Mês: fevereiro/4444"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvwAlertasMes As ListView
    Friend WithEvents Dia As ColumnHeader
    Friend WithEvents Cliente As ColumnHeader
    Friend WithEvents Alertas As ColumnHeader
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents Acumulado As ColumnHeader
End Class
