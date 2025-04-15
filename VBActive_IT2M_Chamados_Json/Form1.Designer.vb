<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArquivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FechaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboPeriodo = New System.Windows.Forms.ComboBox()
        Me.cboCliente = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblTotalChamados = New System.Windows.Forms.Label()
        Me.lblTotalEmAberto = New System.Windows.Forms.Label()
        Me.lblTotalPausado = New System.Windows.Forms.Label()
        Me.lblTotalEmAndamento = New System.Windows.Forms.Label()
        Me.lblTotalTerminado = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnFecha = New System.Windows.Forms.Button()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArquivoToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(765, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArquivoToolStripMenuItem
        '
        Me.ArquivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FechaToolStripMenuItem})
        Me.ArquivoToolStripMenuItem.Name = "ArquivoToolStripMenuItem"
        Me.ArquivoToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.ArquivoToolStripMenuItem.Text = "Arquivo"
        '
        'FechaToolStripMenuItem
        '
        Me.FechaToolStripMenuItem.Name = "FechaToolStripMenuItem"
        Me.FechaToolStripMenuItem.Size = New System.Drawing.Size(105, 22)
        Me.FechaToolStripMenuItem.Text = "Fecha"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Período:"
        '
        'cboPeriodo
        '
        Me.cboPeriodo.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPeriodo.FormattingEnabled = True
        Me.cboPeriodo.Items.AddRange(New Object() {"1 ano"})
        Me.cboPeriodo.Location = New System.Drawing.Point(15, 54)
        Me.cboPeriodo.Name = "cboPeriodo"
        Me.cboPeriodo.Size = New System.Drawing.Size(121, 24)
        Me.cboPeriodo.TabIndex = 2
        '
        'cboCliente
        '
        Me.cboCliente.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCliente.FormattingEnabled = True
        Me.cboCliente.Location = New System.Drawing.Point(166, 54)
        Me.cboCliente.Name = "cboCliente"
        Me.cboCliente.Size = New System.Drawing.Size(591, 24)
        Me.cboCliente.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(163, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Cliente:"
        '
        'lblTotalChamados
        '
        Me.lblTotalChamados.AutoSize = True
        Me.lblTotalChamados.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalChamados.Location = New System.Drawing.Point(20, 265)
        Me.lblTotalChamados.Name = "lblTotalChamados"
        Me.lblTotalChamados.Size = New System.Drawing.Size(159, 16)
        Me.lblTotalChamados.TabIndex = 7
        Me.lblTotalChamados.Text = "Total de chamados: "
        '
        'lblTotalEmAberto
        '
        Me.lblTotalEmAberto.AutoSize = True
        Me.lblTotalEmAberto.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalEmAberto.Location = New System.Drawing.Point(12, 131)
        Me.lblTotalEmAberto.Name = "lblTotalEmAberto"
        Me.lblTotalEmAberto.Size = New System.Drawing.Size(183, 16)
        Me.lblTotalEmAberto.TabIndex = 8
        Me.lblTotalEmAberto.Text = " - status ""em Aberto"":"
        '
        'lblTotalPausado
        '
        Me.lblTotalPausado.AutoSize = True
        Me.lblTotalPausado.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPausado.Location = New System.Drawing.Point(12, 183)
        Me.lblTotalPausado.Name = "lblTotalPausado"
        Me.lblTotalPausado.Size = New System.Drawing.Size(167, 16)
        Me.lblTotalPausado.TabIndex = 10
        Me.lblTotalPausado.Text = " - status ""Pausado"":"
        '
        'lblTotalEmAndamento
        '
        Me.lblTotalEmAndamento.AutoSize = True
        Me.lblTotalEmAndamento.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalEmAndamento.Location = New System.Drawing.Point(12, 157)
        Me.lblTotalEmAndamento.Name = "lblTotalEmAndamento"
        Me.lblTotalEmAndamento.Size = New System.Drawing.Size(207, 16)
        Me.lblTotalEmAndamento.TabIndex = 9
        Me.lblTotalEmAndamento.Text = " - status ""em Andamento"":"
        '
        'lblTotalTerminado
        '
        Me.lblTotalTerminado.AutoSize = True
        Me.lblTotalTerminado.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalTerminado.Location = New System.Drawing.Point(12, 209)
        Me.lblTotalTerminado.Name = "lblTotalTerminado"
        Me.lblTotalTerminado.Size = New System.Drawing.Size(167, 16)
        Me.lblTotalTerminado.TabIndex = 11
        Me.lblTotalTerminado.Text = " - status ""Fechada"":"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(167, 16)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Resumo de chamados: "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(20, 238)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(295, 16)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "===================================="
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 299)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(765, 22)
        Me.StatusStrip1.TabIndex = 14
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(120, 17)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.ForeColor = System.Drawing.Color.RoyalBlue
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(53, 17)
        Me.ToolStripStatusLabel2.Text = "|  VS2022"
        '
        'btnFecha
        '
        Me.btnFecha.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnFecha.Location = New System.Drawing.Point(682, 261)
        Me.btnFecha.Name = "btnFecha"
        Me.btnFecha.Size = New System.Drawing.Size(75, 23)
        Me.btnFecha.TabIndex = 15
        Me.btnFecha.Text = "Fecha"
        Me.btnFecha.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(765, 321)
        Me.Controls.Add(Me.btnFecha)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblTotalTerminado)
        Me.Controls.Add(Me.lblTotalPausado)
        Me.Controls.Add(Me.lblTotalEmAndamento)
        Me.Controls.Add(Me.lblTotalEmAberto)
        Me.Controls.Add(Me.lblTotalChamados)
        Me.Controls.Add(Me.cboCliente)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboPeriodo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Resumo de Chamados IT2M em 12 Meses"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArquivoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FechaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents cboPeriodo As ComboBox
    Friend WithEvents cboCliente As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lblTotalChamados As Label
    Friend WithEvents lblTotalEmAberto As Label
    Friend WithEvents lblTotalPausado As Label
    Friend WithEvents lblTotalEmAndamento As Label
    Friend WithEvents lblTotalTerminado As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents btnFecha As Button
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
End Class
