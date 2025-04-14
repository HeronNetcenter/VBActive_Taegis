<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btnEvents = New System.Windows.Forms.Button()
        Me.txtPastaPython = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMensagens = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPastaProj = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArquivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfiguraçãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabelaDeClientesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FechaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AlertasMensaisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AlertasMensaisToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogMensalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSQL = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnFecha = New System.Windows.Forms.Button()
        Me.dtpDataExtracao = New System.Windows.Forms.DateTimePicker()
        Me.cboClientes = New System.Windows.Forms.ComboBox()
        Me.btnAtualizaPastas = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnEventsMesAnt = New System.Windows.Forms.Button()
        Me.btnSQLDiario = New System.Windows.Forms.Button()
        Me.btnEventsPeriodo = New System.Windows.Forms.Button()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnEvents
        '
        Me.btnEvents.Enabled = False
        Me.btnEvents.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEvents.Location = New System.Drawing.Point(682, 172)
        Me.btnEvents.Name = "btnEvents"
        Me.btnEvents.Size = New System.Drawing.Size(115, 41)
        Me.btnEvents.TabIndex = 0
        Me.btnEvents.Text = "Conta Eventos Diários"
        Me.btnEvents.UseVisualStyleBackColor = True
        '
        'txtPastaPython
        '
        Me.txtPastaPython.Enabled = False
        Me.txtPastaPython.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPastaPython.Location = New System.Drawing.Point(44, 54)
        Me.txtPastaPython.Name = "txtPastaPython"
        Me.txtPastaPython.Size = New System.Drawing.Size(633, 20)
        Me.txtPastaPython.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(41, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Pasta Python.exe"
        '
        'txtMensagens
        '
        Me.txtMensagens.BackColor = System.Drawing.SystemColors.Info
        Me.txtMensagens.Location = New System.Drawing.Point(44, 144)
        Me.txtMensagens.Multiline = True
        Me.txtMensagens.Name = "txtMensagens"
        Me.txtMensagens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMensagens.Size = New System.Drawing.Size(633, 398)
        Me.txtMensagens.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(41, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Pasta Projeto"
        '
        'txtPastaProj
        '
        Me.txtPastaProj.Enabled = False
        Me.txtPastaProj.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPastaProj.Location = New System.Drawing.Point(44, 97)
        Me.txtPastaProj.Name = "txtPastaProj"
        Me.txtPastaProj.Size = New System.Drawing.Size(633, 20)
        Me.txtPastaProj.TabIndex = 6
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArquivoToolStripMenuItem, Me.ListasToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(815, 24)
        Me.MenuStrip1.TabIndex = 10
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArquivoToolStripMenuItem
        '
        Me.ArquivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfiguraçãoToolStripMenuItem, Me.DataSelecionadaToolStripMenuItem, Me.TabelaDeClientesToolStripMenuItem, Me.FechaToolStripMenuItem})
        Me.ArquivoToolStripMenuItem.Name = "ArquivoToolStripMenuItem"
        Me.ArquivoToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.ArquivoToolStripMenuItem.Text = "Arquivo"
        '
        'ConfiguraçãoToolStripMenuItem
        '
        Me.ConfiguraçãoToolStripMenuItem.Name = "ConfiguraçãoToolStripMenuItem"
        Me.ConfiguraçãoToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.ConfiguraçãoToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.ConfiguraçãoToolStripMenuItem.Text = "Configuração"
        '
        'DataSelecionadaToolStripMenuItem
        '
        Me.DataSelecionadaToolStripMenuItem.Name = "DataSelecionadaToolStripMenuItem"
        Me.DataSelecionadaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.DataSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.DataSelecionadaToolStripMenuItem.Text = "Data selecionada"
        '
        'TabelaDeClientesToolStripMenuItem
        '
        Me.TabelaDeClientesToolStripMenuItem.Name = "TabelaDeClientesToolStripMenuItem"
        Me.TabelaDeClientesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4
        Me.TabelaDeClientesToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.TabelaDeClientesToolStripMenuItem.Text = "Tabela de clientes"
        '
        'FechaToolStripMenuItem
        '
        Me.FechaToolStripMenuItem.Name = "FechaToolStripMenuItem"
        Me.FechaToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.FechaToolStripMenuItem.Text = "Fecha"
        '
        'ListasToolStripMenuItem
        '
        Me.ListasToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AlertasMensaisToolStripMenuItem, Me.AlertasMensaisToolStripMenuItem1, Me.LogMensalToolStripMenuItem})
        Me.ListasToolStripMenuItem.Name = "ListasToolStripMenuItem"
        Me.ListasToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6
        Me.ListasToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.ListasToolStripMenuItem.Text = "Listas"
        '
        'AlertasMensaisToolStripMenuItem
        '
        Me.AlertasMensaisToolStripMenuItem.Name = "AlertasMensaisToolStripMenuItem"
        Me.AlertasMensaisToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.AlertasMensaisToolStripMenuItem.Size = New System.Drawing.Size(237, 22)
        Me.AlertasMensaisToolStripMenuItem.Text = "Alertas mensais  por cliente"
        '
        'AlertasMensaisToolStripMenuItem1
        '
        Me.AlertasMensaisToolStripMenuItem1.Name = "AlertasMensaisToolStripMenuItem1"
        Me.AlertasMensaisToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F6
        Me.AlertasMensaisToolStripMenuItem1.Size = New System.Drawing.Size(237, 22)
        Me.AlertasMensaisToolStripMenuItem1.Text = "Alertas mensais - geral"
        '
        'LogMensalToolStripMenuItem
        '
        Me.LogMensalToolStripMenuItem.Name = "LogMensalToolStripMenuItem"
        Me.LogMensalToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7
        Me.LogMensalToolStripMenuItem.Size = New System.Drawing.Size(237, 22)
        Me.LogMensalToolStripMenuItem.Text = "Log Mensal"
        '
        'btnSQL
        '
        Me.btnSQL.Enabled = False
        Me.btnSQL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSQL.Location = New System.Drawing.Point(682, 363)
        Me.btnSQL.Name = "btnSQL"
        Me.btnSQL.Size = New System.Drawing.Size(115, 23)
        Me.btnSQL.TabIndex = 11
        Me.btnSQL.Text = "SQL"
        Me.btnSQL.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(682, 392)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(115, 115)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 12
        Me.PictureBox1.TabStop = False
        '
        'btnFecha
        '
        Me.btnFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFecha.Location = New System.Drawing.Point(682, 519)
        Me.btnFecha.Name = "btnFecha"
        Me.btnFecha.Size = New System.Drawing.Size(115, 23)
        Me.btnFecha.TabIndex = 13
        Me.btnFecha.Text = "Fecha"
        Me.btnFecha.UseVisualStyleBackColor = True
        '
        'dtpDataExtracao
        '
        Me.dtpDataExtracao.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDataExtracao.Location = New System.Drawing.Point(682, 146)
        Me.dtpDataExtracao.Name = "dtpDataExtracao"
        Me.dtpDataExtracao.Size = New System.Drawing.Size(115, 20)
        Me.dtpDataExtracao.TabIndex = 14
        '
        'cboClientes
        '
        Me.cboClientes.FormattingEnabled = True
        Me.cboClientes.Location = New System.Drawing.Point(682, 53)
        Me.cboClientes.Name = "cboClientes"
        Me.cboClientes.Size = New System.Drawing.Size(115, 21)
        Me.cboClientes.TabIndex = 15
        '
        'btnAtualizaPastas
        '
        Me.btnAtualizaPastas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAtualizaPastas.Location = New System.Drawing.Point(682, 95)
        Me.btnAtualizaPastas.Name = "btnAtualizaPastas"
        Me.btnAtualizaPastas.Size = New System.Drawing.Size(115, 23)
        Me.btnAtualizaPastas.TabIndex = 16
        Me.btnAtualizaPastas.Text = "Atualiza Pastas"
        Me.btnAtualizaPastas.UseVisualStyleBackColor = True
        Me.btnAtualizaPastas.Visible = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 560)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(815, 22)
        Me.StatusStrip1.TabIndex = 17
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(60, 17)
        Me.ToolStripStatusLabel1.Text = "Copyright"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.ForeColor = System.Drawing.Color.Blue
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(53, 17)
        Me.ToolStripStatusLabel2.Text = "|  VS2022"
        '
        'btnEventsMesAnt
        '
        Me.btnEventsMesAnt.Enabled = False
        Me.btnEventsMesAnt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEventsMesAnt.Location = New System.Drawing.Point(682, 316)
        Me.btnEventsMesAnt.Name = "btnEventsMesAnt"
        Me.btnEventsMesAnt.Size = New System.Drawing.Size(115, 41)
        Me.btnEventsMesAnt.TabIndex = 18
        Me.btnEventsMesAnt.Text = "Conta Eventos do Mês Anterior"
        Me.btnEventsMesAnt.UseVisualStyleBackColor = True
        '
        'btnSQLDiario
        '
        Me.btnSQLDiario.Enabled = False
        Me.btnSQLDiario.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSQLDiario.Location = New System.Drawing.Point(682, 266)
        Me.btnSQLDiario.Name = "btnSQLDiario"
        Me.btnSQLDiario.Size = New System.Drawing.Size(115, 23)
        Me.btnSQLDiario.TabIndex = 19
        Me.btnSQLDiario.Text = "SQL"
        Me.btnSQLDiario.UseVisualStyleBackColor = True
        '
        'btnEventsPeriodo
        '
        Me.btnEventsPeriodo.Enabled = False
        Me.btnEventsPeriodo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEventsPeriodo.Location = New System.Drawing.Point(682, 219)
        Me.btnEventsPeriodo.Name = "btnEventsPeriodo"
        Me.btnEventsPeriodo.Size = New System.Drawing.Size(115, 41)
        Me.btnEventsPeriodo.TabIndex = 20
        Me.btnEventsPeriodo.Text = "Conta Eventos Diários/Período"
        Me.btnEventsPeriodo.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(815, 582)
        Me.Controls.Add(Me.btnEventsPeriodo)
        Me.Controls.Add(Me.btnSQLDiario)
        Me.Controls.Add(Me.btnEventsMesAnt)
        Me.Controls.Add(Me.StatusStrip1)
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
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.txtPastaPython)
        Me.Controls.Add(Me.btnEvents)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "VBActive_Taegis_Events"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnEvents As Button
    Friend WithEvents txtPastaPython As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtMensagens As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPastaProj As TextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArquivoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConfiguraçãoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FechaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnSQL As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnFecha As Button
    Friend WithEvents DataSelecionadaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents dtpDataExtracao As DateTimePicker
    Friend WithEvents ListasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AlertasMensaisToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents cboClientes As ComboBox
    Friend WithEvents TabelaDeClientesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnAtualizaPastas As Button
    Friend WithEvents AlertasMensaisToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents LogMensalToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents btnEventsMesAnt As Button
    Friend WithEvents btnSQLDiario As Button
    Friend WithEvents btnEventsPeriodo As Button
End Class
