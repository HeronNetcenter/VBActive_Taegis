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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArquivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfiguraçãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabelaDeClientesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FechaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnSQL = New System.Windows.Forms.Button()
        Me.btnAssets2 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPastaProj = New System.Windows.Forms.TextBox()
        Me.txtMensagens = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPastaPython = New System.Windows.Forms.TextBox()
        Me.btnAssets1 = New System.Windows.Forms.Button()
        Me.btnParseJson = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnFecha = New System.Windows.Forms.Button()
        Me.cboClientes = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnReiniciar = New System.Windows.Forms.Button()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArquivoToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(818, 24)
        Me.MenuStrip1.TabIndex = 11
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArquivoToolStripMenuItem
        '
        Me.ArquivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfiguraçãoToolStripMenuItem, Me.TabelaDeClientesToolStripMenuItem, Me.FechaToolStripMenuItem})
        Me.ArquivoToolStripMenuItem.Name = "ArquivoToolStripMenuItem"
        Me.ArquivoToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.ArquivoToolStripMenuItem.Text = "Arquivo"
        '
        'ConfiguraçãoToolStripMenuItem
        '
        Me.ConfiguraçãoToolStripMenuItem.Name = "ConfiguraçãoToolStripMenuItem"
        Me.ConfiguraçãoToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.ConfiguraçãoToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ConfiguraçãoToolStripMenuItem.Text = "Configuração"
        '
        'TabelaDeClientesToolStripMenuItem
        '
        Me.TabelaDeClientesToolStripMenuItem.Name = "TabelaDeClientesToolStripMenuItem"
        Me.TabelaDeClientesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4
        Me.TabelaDeClientesToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.TabelaDeClientesToolStripMenuItem.Text = "Tabela de Clientes"
        '
        'FechaToolStripMenuItem
        '
        Me.FechaToolStripMenuItem.Name = "FechaToolStripMenuItem"
        Me.FechaToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.FechaToolStripMenuItem.Text = "Fecha"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 428)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(818, 22)
        Me.StatusStrip1.TabIndex = 12
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(128, 17)
        Me.ToolStripStatusLabel1.Text = "Nostromo C 2022-2023"
        '
        'btnSQL
        '
        Me.btnSQL.Enabled = False
        Me.btnSQL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSQL.Location = New System.Drawing.Point(679, 193)
        Me.btnSQL.Name = "btnSQL"
        Me.btnSQL.Size = New System.Drawing.Size(128, 23)
        Me.btnSQL.TabIndex = 20
        Me.btnSQL.Text = "SQL"
        Me.btnSQL.UseVisualStyleBackColor = True
        '
        'btnAssets2
        '
        Me.btnAssets2.Enabled = False
        Me.btnAssets2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAssets2.Location = New System.Drawing.Point(679, 150)
        Me.btnAssets2.Name = "btnAssets2"
        Me.btnAssets2.Size = New System.Drawing.Size(128, 23)
        Me.btnAssets2.TabIndex = 19
        Me.btnAssets2.Text = "Assets2 => CSV"
        Me.btnAssets2.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(30, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Pasta Projeto"
        '
        'txtPastaProj
        '
        Me.txtPastaProj.Enabled = False
        Me.txtPastaProj.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPastaProj.Location = New System.Drawing.Point(33, 93)
        Me.txtPastaProj.Name = "txtPastaProj"
        Me.txtPastaProj.Size = New System.Drawing.Size(633, 20)
        Me.txtPastaProj.TabIndex = 17
        '
        'txtMensagens
        '
        Me.txtMensagens.BackColor = System.Drawing.SystemColors.Info
        Me.txtMensagens.Location = New System.Drawing.Point(33, 121)
        Me.txtMensagens.Multiline = True
        Me.txtMensagens.Name = "txtMensagens"
        Me.txtMensagens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMensagens.Size = New System.Drawing.Size(633, 295)
        Me.txtMensagens.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(30, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Pasta Python.exe"
        '
        'txtPastaPython
        '
        Me.txtPastaPython.Enabled = False
        Me.txtPastaPython.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPastaPython.Location = New System.Drawing.Point(33, 50)
        Me.txtPastaPython.Name = "txtPastaPython"
        Me.txtPastaPython.Size = New System.Drawing.Size(633, 20)
        Me.txtPastaPython.TabIndex = 14
        '
        'btnAssets1
        '
        Me.btnAssets1.Enabled = False
        Me.btnAssets1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAssets1.Location = New System.Drawing.Point(679, 92)
        Me.btnAssets1.Name = "btnAssets1"
        Me.btnAssets1.Size = New System.Drawing.Size(128, 23)
        Me.btnAssets1.TabIndex = 13
        Me.btnAssets1.Text = "Assets1 => JSON"
        Me.btnAssets1.UseVisualStyleBackColor = True
        '
        'btnParseJson
        '
        Me.btnParseJson.Enabled = False
        Me.btnParseJson.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnParseJson.Location = New System.Drawing.Point(679, 121)
        Me.btnParseJson.Name = "btnParseJson"
        Me.btnParseJson.Size = New System.Drawing.Size(128, 23)
        Me.btnParseJson.TabIndex = 21
        Me.btnParseJson.Text = "Parse => JSON"
        Me.btnParseJson.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(691, 282)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 22
        Me.PictureBox1.TabStop = False
        '
        'btnFecha
        '
        Me.btnFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFecha.Location = New System.Drawing.Point(679, 393)
        Me.btnFecha.Name = "btnFecha"
        Me.btnFecha.Size = New System.Drawing.Size(128, 23)
        Me.btnFecha.TabIndex = 23
        Me.btnFecha.Text = "Fecha"
        Me.btnFecha.UseVisualStyleBackColor = True
        '
        'cboClientes
        '
        Me.cboClientes.FormattingEnabled = True
        Me.cboClientes.Location = New System.Drawing.Point(679, 50)
        Me.cboClientes.Name = "cboClientes"
        Me.cboClientes.Size = New System.Drawing.Size(115, 21)
        Me.cboClientes.TabIndex = 24
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(676, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Cliente"
        '
        'btnReiniciar
        '
        Me.btnReiniciar.Enabled = False
        Me.btnReiniciar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReiniciar.Location = New System.Drawing.Point(679, 234)
        Me.btnReiniciar.Name = "btnReiniciar"
        Me.btnReiniciar.Size = New System.Drawing.Size(128, 23)
        Me.btnReiniciar.TabIndex = 26
        Me.btnReiniciar.Text = "Reiniciar"
        Me.btnReiniciar.UseVisualStyleBackColor = True
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.ForeColor = System.Drawing.Color.Blue
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(53, 17)
        Me.ToolStripStatusLabel2.Text = "|  VS2022"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(818, 450)
        Me.Controls.Add(Me.btnReiniciar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboClientes)
        Me.Controls.Add(Me.btnFecha)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnParseJson)
        Me.Controls.Add(Me.btnSQL)
        Me.Controls.Add(Me.btnAssets2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPastaProj)
        Me.Controls.Add(Me.txtMensagens)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPastaPython)
        Me.Controls.Add(Me.btnAssets1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "VBActive_Taegis_Assets"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArquivoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConfiguraçãoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FechaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents btnSQL As Button
    Friend WithEvents btnAssets2 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPastaProj As TextBox
    Friend WithEvents txtMensagens As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtPastaPython As TextBox
    Friend WithEvents btnAssets1 As Button
    Friend WithEvents btnParseJson As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnFecha As Button
    Friend WithEvents cboClientes As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TabelaDeClientesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnReiniciar As Button
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
End Class
