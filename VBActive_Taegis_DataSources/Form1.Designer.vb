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
        Me.ConfiguraToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabelaDeClientesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FechaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnAtualizaPastas = New System.Windows.Forms.Button()
        Me.cboClientes = New System.Windows.Forms.ComboBox()
        Me.dtpDataExtracao = New System.Windows.Forms.DateTimePicker()
        Me.btnFecha = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnSQL = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPastaProj = New System.Windows.Forms.TextBox()
        Me.txtMensagens = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtPastaPython = New System.Windows.Forms.TextBox()
        Me.btnContaDataSources = New System.Windows.Forms.Button()
        Me.btnCSV_SQL = New System.Windows.Forms.Button()
        Me.btnGeraCSVDataSources = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArquivoToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(804, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArquivoToolStripMenuItem
        '
        Me.ArquivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfiguraToolStripMenuItem, Me.TabelaDeClientesToolStripMenuItem, Me.FechaToolStripMenuItem})
        Me.ArquivoToolStripMenuItem.Name = "ArquivoToolStripMenuItem"
        Me.ArquivoToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.ArquivoToolStripMenuItem.Text = "Arquivo"
        '
        'ConfiguraToolStripMenuItem
        '
        Me.ConfiguraToolStripMenuItem.Name = "ConfiguraToolStripMenuItem"
        Me.ConfiguraToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.ConfiguraToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ConfiguraToolStripMenuItem.Text = "Configuração"
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
        'btnAtualizaPastas
        '
        Me.btnAtualizaPastas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAtualizaPastas.Location = New System.Drawing.Point(682, 94)
        Me.btnAtualizaPastas.Name = "btnAtualizaPastas"
        Me.btnAtualizaPastas.Size = New System.Drawing.Size(115, 23)
        Me.btnAtualizaPastas.TabIndex = 30
        Me.btnAtualizaPastas.Text = "Atualiza Pastas"
        Me.btnAtualizaPastas.UseVisualStyleBackColor = True
        Me.btnAtualizaPastas.Visible = False
        '
        'cboClientes
        '
        Me.cboClientes.FormattingEnabled = True
        Me.cboClientes.Location = New System.Drawing.Point(682, 52)
        Me.cboClientes.Name = "cboClientes"
        Me.cboClientes.Size = New System.Drawing.Size(115, 21)
        Me.cboClientes.TabIndex = 29
        '
        'dtpDataExtracao
        '
        Me.dtpDataExtracao.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDataExtracao.Location = New System.Drawing.Point(682, 145)
        Me.dtpDataExtracao.Name = "dtpDataExtracao"
        Me.dtpDataExtracao.Size = New System.Drawing.Size(115, 20)
        Me.dtpDataExtracao.TabIndex = 28
        '
        'btnFecha
        '
        Me.btnFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFecha.Location = New System.Drawing.Point(682, 458)
        Me.btnFecha.Name = "btnFecha"
        Me.btnFecha.Size = New System.Drawing.Size(115, 23)
        Me.btnFecha.TabIndex = 27
        Me.btnFecha.Text = "Fecha"
        Me.btnFecha.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(682, 335)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(115, 115)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 26
        Me.PictureBox1.TabStop = False
        '
        'btnSQL
        '
        Me.btnSQL.Enabled = False
        Me.btnSQL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSQL.Location = New System.Drawing.Point(682, 214)
        Me.btnSQL.Name = "btnSQL"
        Me.btnSQL.Size = New System.Drawing.Size(115, 23)
        Me.btnSQL.TabIndex = 25
        Me.btnSQL.Text = "SQL"
        Me.btnSQL.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(41, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Pasta Projeto"
        '
        'txtPastaProj
        '
        Me.txtPastaProj.Enabled = False
        Me.txtPastaProj.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPastaProj.Location = New System.Drawing.Point(44, 96)
        Me.txtPastaProj.Name = "txtPastaProj"
        Me.txtPastaProj.Size = New System.Drawing.Size(633, 20)
        Me.txtPastaProj.TabIndex = 22
        '
        'txtMensagens
        '
        Me.txtMensagens.BackColor = System.Drawing.SystemColors.Info
        Me.txtMensagens.Location = New System.Drawing.Point(44, 143)
        Me.txtMensagens.Multiline = True
        Me.txtMensagens.Name = "txtMensagens"
        Me.txtMensagens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMensagens.Size = New System.Drawing.Size(633, 338)
        Me.txtMensagens.TabIndex = 21
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(41, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Pasta Python.exe"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 500)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(804, 22)
        Me.StatusStrip1.TabIndex = 19
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
        Me.ToolStripStatusLabel2.ForeColor = System.Drawing.Color.Blue
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(53, 17)
        Me.ToolStripStatusLabel2.Text = "|  VS2022"
        '
        'txtPastaPython
        '
        Me.txtPastaPython.Enabled = False
        Me.txtPastaPython.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPastaPython.Location = New System.Drawing.Point(44, 53)
        Me.txtPastaPython.Name = "txtPastaPython"
        Me.txtPastaPython.Size = New System.Drawing.Size(633, 20)
        Me.txtPastaPython.TabIndex = 18
        '
        'btnContaDataSources
        '
        Me.btnContaDataSources.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContaDataSources.Location = New System.Drawing.Point(682, 170)
        Me.btnContaDataSources.Name = "btnContaDataSources"
        Me.btnContaDataSources.Size = New System.Drawing.Size(115, 39)
        Me.btnContaDataSources.TabIndex = 17
        Me.btnContaDataSources.Text = "Conta Data Sources"
        Me.btnContaDataSources.UseVisualStyleBackColor = True
        '
        'btnCSV_SQL
        '
        Me.btnCSV_SQL.Enabled = False
        Me.btnCSV_SQL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCSV_SQL.Location = New System.Drawing.Point(682, 304)
        Me.btnCSV_SQL.Name = "btnCSV_SQL"
        Me.btnCSV_SQL.Size = New System.Drawing.Size(115, 23)
        Me.btnCSV_SQL.TabIndex = 32
        Me.btnCSV_SQL.Text = "CSV -> SQL"
        Me.btnCSV_SQL.UseVisualStyleBackColor = True
        '
        'btnGeraCSVDataSources
        '
        Me.btnGeraCSVDataSources.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGeraCSVDataSources.Location = New System.Drawing.Point(682, 260)
        Me.btnGeraCSVDataSources.Name = "btnGeraCSVDataSources"
        Me.btnGeraCSVDataSources.Size = New System.Drawing.Size(115, 39)
        Me.btnGeraCSVDataSources.TabIndex = 31
        Me.btnGeraCSVDataSources.Text = "Gera CSV de Data Sources"
        Me.btnGeraCSVDataSources.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(729, 242)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 13)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "OU"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(804, 522)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnCSV_SQL)
        Me.Controls.Add(Me.btnGeraCSVDataSources)
        Me.Controls.Add(Me.btnAtualizaPastas)
        Me.Controls.Add(Me.cboClientes)
        Me.Controls.Add(Me.dtpDataExtracao)
        Me.Controls.Add(Me.btnFecha)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnSQL)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPastaProj)
        Me.Controls.Add(Me.txtMensagens)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.txtPastaPython)
        Me.Controls.Add(Me.btnContaDataSources)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Active Taegis Data Sources"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArquivoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabelaDeClientesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConfiguraToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnAtualizaPastas As Button
    Friend WithEvents cboClientes As ComboBox
    Friend WithEvents dtpDataExtracao As DateTimePicker
    Friend WithEvents btnFecha As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnSQL As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPastaProj As TextBox
    Friend WithEvents txtMensagens As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents txtPastaPython As TextBox
    Friend WithEvents btnContaDataSources As Button
    Friend WithEvents FechaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents btnCSV_SQL As Button
    Friend WithEvents btnGeraCSVDataSources As Button
    Friend WithEvents Label3 As Label
End Class
