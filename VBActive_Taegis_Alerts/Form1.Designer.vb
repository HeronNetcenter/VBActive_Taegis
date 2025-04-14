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
        Me.btnAlerts1 = New System.Windows.Forms.Button()
        Me.txtPastaPython = New System.Windows.Forms.TextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMensagens = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPastaProj = New System.Windows.Forms.TextBox()
        Me.btnAlerts2 = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArquivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfiguraçãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabelaDeClientesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CriaTabelaAlertsIdToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportaTechniquesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportaPlanilhaDeTechniquesGenéricaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FechaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AlertasMensaisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AlertasMensaisToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogMensalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TacticsTechniquesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DebugToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSQL = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnFecha = New System.Windows.Forms.Button()
        Me.dtpDataExtracao = New System.Windows.Forms.DateTimePicker()
        Me.cboClientes = New System.Windows.Forms.ComboBox()
        Me.btnAtualizaPastas = New System.Windows.Forms.Button()
        Me.btnReinicio = New System.Windows.Forms.Button()
        Me.btnSQLTechTact = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.grpMesAnterior = New System.Windows.Forms.GroupBox()
        Me.opt_21_a_fim = New System.Windows.Forms.RadioButton()
        Me.opt_11_a_20 = New System.Windows.Forms.RadioButton()
        Me.opt_1_a_10 = New System.Windows.Forms.RadioButton()
        Me.opt_16_fim = New System.Windows.Forms.RadioButton()
        Me.opt_1_a_15 = New System.Windows.Forms.RadioButton()
        Me.optMes = New System.Windows.Forms.RadioButton()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMesAnterior.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnAlerts1
        '
        Me.btnAlerts1.Enabled = False
        Me.btnAlerts1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAlerts1.Location = New System.Drawing.Point(683, 149)
        Me.btnAlerts1.Name = "btnAlerts1"
        Me.btnAlerts1.Size = New System.Drawing.Size(115, 23)
        Me.btnAlerts1.TabIndex = 0
        Me.btnAlerts1.Text = "Alerts1 => JSON"
        Me.btnAlerts1.UseVisualStyleBackColor = True
        '
        'txtPastaPython
        '
        Me.txtPastaPython.Enabled = False
        Me.txtPastaPython.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPastaPython.Location = New System.Drawing.Point(44, 52)
        Me.txtPastaPython.Name = "txtPastaPython"
        Me.txtPastaPython.Size = New System.Drawing.Size(633, 20)
        Me.txtPastaPython.TabIndex = 1
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 496)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(985, 22)
        Me.StatusStrip1.TabIndex = 3
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(41, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Pasta Python.exe"
        '
        'txtMensagens
        '
        Me.txtMensagens.BackColor = System.Drawing.SystemColors.Info
        Me.txtMensagens.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMensagens.Location = New System.Drawing.Point(44, 123)
        Me.txtMensagens.Multiline = True
        Me.txtMensagens.Name = "txtMensagens"
        Me.txtMensagens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMensagens.Size = New System.Drawing.Size(633, 359)
        Me.txtMensagens.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(41, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Pasta Projeto"
        '
        'txtPastaProj
        '
        Me.txtPastaProj.Enabled = False
        Me.txtPastaProj.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPastaProj.Location = New System.Drawing.Point(44, 89)
        Me.txtPastaProj.Name = "txtPastaProj"
        Me.txtPastaProj.Size = New System.Drawing.Size(633, 20)
        Me.txtPastaProj.TabIndex = 6
        '
        'btnAlerts2
        '
        Me.btnAlerts2.Enabled = False
        Me.btnAlerts2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAlerts2.Location = New System.Drawing.Point(683, 182)
        Me.btnAlerts2.Name = "btnAlerts2"
        Me.btnAlerts2.Size = New System.Drawing.Size(115, 23)
        Me.btnAlerts2.TabIndex = 9
        Me.btnAlerts2.Text = "Alerts2 => CSV"
        Me.btnAlerts2.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArquivoToolStripMenuItem, Me.ListasToolStripMenuItem, Me.DebugToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(985, 24)
        Me.MenuStrip1.TabIndex = 10
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArquivoToolStripMenuItem
        '
        Me.ArquivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfiguraçãoToolStripMenuItem, Me.DataSelecionadaToolStripMenuItem, Me.TabelaDeClientesToolStripMenuItem, Me.CriaTabelaAlertsIdToolStripMenuItem, Me.ImportaTechniquesToolStripMenuItem, Me.ImportaPlanilhaDeTechniquesGenéricaToolStripMenuItem, Me.FechaToolStripMenuItem})
        Me.ArquivoToolStripMenuItem.Name = "ArquivoToolStripMenuItem"
        Me.ArquivoToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.ArquivoToolStripMenuItem.Text = "Arquivo"
        '
        'ConfiguraçãoToolStripMenuItem
        '
        Me.ConfiguraçãoToolStripMenuItem.Name = "ConfiguraçãoToolStripMenuItem"
        Me.ConfiguraçãoToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.ConfiguraçãoToolStripMenuItem.Size = New System.Drawing.Size(417, 22)
        Me.ConfiguraçãoToolStripMenuItem.Text = "Configuração"
        '
        'DataSelecionadaToolStripMenuItem
        '
        Me.DataSelecionadaToolStripMenuItem.Name = "DataSelecionadaToolStripMenuItem"
        Me.DataSelecionadaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.DataSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(417, 22)
        Me.DataSelecionadaToolStripMenuItem.Text = "Data selecionada"
        '
        'TabelaDeClientesToolStripMenuItem
        '
        Me.TabelaDeClientesToolStripMenuItem.Name = "TabelaDeClientesToolStripMenuItem"
        Me.TabelaDeClientesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4
        Me.TabelaDeClientesToolStripMenuItem.Size = New System.Drawing.Size(417, 22)
        Me.TabelaDeClientesToolStripMenuItem.Text = "Tabela de clientes"
        '
        'CriaTabelaAlertsIdToolStripMenuItem
        '
        Me.CriaTabelaAlertsIdToolStripMenuItem.Name = "CriaTabelaAlertsIdToolStripMenuItem"
        Me.CriaTabelaAlertsIdToolStripMenuItem.Size = New System.Drawing.Size(417, 22)
        Me.CriaTabelaAlertsIdToolStripMenuItem.Text = "Cria tabela Alerts_Id"
        '
        'ImportaTechniquesToolStripMenuItem
        '
        Me.ImportaTechniquesToolStripMenuItem.Name = "ImportaTechniquesToolStripMenuItem"
        Me.ImportaTechniquesToolStripMenuItem.Size = New System.Drawing.Size(417, 22)
        Me.ImportaTechniquesToolStripMenuItem.Text = "Importa planilha de techniques (Manual)"
        '
        'ImportaPlanilhaDeTechniquesGenéricaToolStripMenuItem
        '
        Me.ImportaPlanilhaDeTechniquesGenéricaToolStripMenuItem.Name = "ImportaPlanilhaDeTechniquesGenéricaToolStripMenuItem"
        Me.ImportaPlanilhaDeTechniquesGenéricaToolStripMenuItem.Size = New System.Drawing.Size(417, 22)
        Me.ImportaPlanilhaDeTechniquesGenéricaToolStripMenuItem.Text = "Download da planilha de techniques (Direto do site Mitre-Attack)"
        '
        'FechaToolStripMenuItem
        '
        Me.FechaToolStripMenuItem.Name = "FechaToolStripMenuItem"
        Me.FechaToolStripMenuItem.Size = New System.Drawing.Size(417, 22)
        Me.FechaToolStripMenuItem.Text = "Fecha"
        '
        'ListasToolStripMenuItem
        '
        Me.ListasToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AlertasMensaisToolStripMenuItem, Me.AlertasMensaisToolStripMenuItem1, Me.LogMensalToolStripMenuItem, Me.TacticsTechniquesToolStripMenuItem})
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
        'TacticsTechniquesToolStripMenuItem
        '
        Me.TacticsTechniquesToolStripMenuItem.Name = "TacticsTechniquesToolStripMenuItem"
        Me.TacticsTechniquesToolStripMenuItem.Size = New System.Drawing.Size(237, 22)
        Me.TacticsTechniquesToolStripMenuItem.Text = "Tactics / Techniques"
        '
        'DebugToolStripMenuItem
        '
        Me.DebugToolStripMenuItem.Name = "DebugToolStripMenuItem"
        Me.DebugToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
        Me.DebugToolStripMenuItem.Text = "Debug"
        '
        'btnSQL
        '
        Me.btnSQL.Enabled = False
        Me.btnSQL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSQL.Location = New System.Drawing.Point(683, 215)
        Me.btnSQL.Name = "btnSQL"
        Me.btnSQL.Size = New System.Drawing.Size(115, 23)
        Me.btnSQL.TabIndex = 11
        Me.btnSQL.Text = "SQL"
        Me.btnSQL.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(683, 293)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(115, 115)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 12
        Me.PictureBox1.TabStop = False
        '
        'btnFecha
        '
        Me.btnFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFecha.ForeColor = System.Drawing.Color.Red
        Me.btnFecha.Location = New System.Drawing.Point(682, 455)
        Me.btnFecha.Name = "btnFecha"
        Me.btnFecha.Size = New System.Drawing.Size(115, 23)
        Me.btnFecha.TabIndex = 13
        Me.btnFecha.Text = "Fecha"
        Me.btnFecha.UseVisualStyleBackColor = True
        '
        'dtpDataExtracao
        '
        Me.dtpDataExtracao.Enabled = False
        Me.dtpDataExtracao.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDataExtracao.Location = New System.Drawing.Point(682, 119)
        Me.dtpDataExtracao.Name = "dtpDataExtracao"
        Me.dtpDataExtracao.Size = New System.Drawing.Size(115, 20)
        Me.dtpDataExtracao.TabIndex = 14
        '
        'cboClientes
        '
        Me.cboClientes.FormattingEnabled = True
        Me.cboClientes.Location = New System.Drawing.Point(682, 51)
        Me.cboClientes.Name = "cboClientes"
        Me.cboClientes.Size = New System.Drawing.Size(115, 21)
        Me.cboClientes.TabIndex = 15
        '
        'btnAtualizaPastas
        '
        Me.btnAtualizaPastas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAtualizaPastas.Location = New System.Drawing.Point(682, 86)
        Me.btnAtualizaPastas.Name = "btnAtualizaPastas"
        Me.btnAtualizaPastas.Size = New System.Drawing.Size(115, 23)
        Me.btnAtualizaPastas.TabIndex = 16
        Me.btnAtualizaPastas.Text = "Atualiza Pastas"
        Me.btnAtualizaPastas.UseVisualStyleBackColor = True
        Me.btnAtualizaPastas.Visible = False
        '
        'btnReinicio
        '
        Me.btnReinicio.Enabled = False
        Me.btnReinicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReinicio.Location = New System.Drawing.Point(683, 248)
        Me.btnReinicio.Name = "btnReinicio"
        Me.btnReinicio.Size = New System.Drawing.Size(115, 23)
        Me.btnReinicio.TabIndex = 17
        Me.btnReinicio.Text = "Reinício"
        Me.btnReinicio.UseVisualStyleBackColor = True
        '
        'btnSQLTechTact
        '
        Me.btnSQLTechTact.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSQLTechTact.Location = New System.Drawing.Point(682, 426)
        Me.btnSQLTechTact.Name = "btnSQLTechTact"
        Me.btnSQLTechTact.Size = New System.Drawing.Size(115, 23)
        Me.btnSQLTechTact.TabIndex = 18
        Me.btnSQLTechTact.Text = "SQL TechTact"
        Me.btnSQLTechTact.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'grpMesAnterior
        '
        Me.grpMesAnterior.Controls.Add(Me.opt_21_a_fim)
        Me.grpMesAnterior.Controls.Add(Me.opt_11_a_20)
        Me.grpMesAnterior.Controls.Add(Me.opt_1_a_10)
        Me.grpMesAnterior.Controls.Add(Me.opt_16_fim)
        Me.grpMesAnterior.Controls.Add(Me.opt_1_a_15)
        Me.grpMesAnterior.Controls.Add(Me.optMes)
        Me.grpMesAnterior.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpMesAnterior.Location = New System.Drawing.Point(804, 51)
        Me.grpMesAnterior.Name = "grpMesAnterior"
        Me.grpMesAnterior.Size = New System.Drawing.Size(169, 205)
        Me.grpMesAnterior.TabIndex = 19
        Me.grpMesAnterior.TabStop = False
        Me.grpMesAnterior.Text = "Opções para o mês"
        '
        'opt_21_a_fim
        '
        Me.opt_21_a_fim.AutoSize = True
        Me.opt_21_a_fim.Location = New System.Drawing.Point(6, 173)
        Me.opt_21_a_fim.Name = "opt_21_a_fim"
        Me.opt_21_a_fim.Size = New System.Drawing.Size(118, 17)
        Me.opt_21_a_fim.TabIndex = 5
        Me.opt_21_a_fim.TabStop = True
        Me.opt_21_a_fim.Text = "Do dia 21 ao fim"
        Me.opt_21_a_fim.UseVisualStyleBackColor = True
        '
        'opt_11_a_20
        '
        Me.opt_11_a_20.AutoSize = True
        Me.opt_11_a_20.Location = New System.Drawing.Point(6, 150)
        Me.opt_11_a_20.Name = "opt_11_a_20"
        Me.opt_11_a_20.Size = New System.Drawing.Size(137, 17)
        Me.opt_11_a_20.TabIndex = 4
        Me.opt_11_a_20.TabStop = True
        Me.opt_11_a_20.Text = "Do dia 11 ao dia 20"
        Me.opt_11_a_20.UseVisualStyleBackColor = True
        '
        'opt_1_a_10
        '
        Me.opt_1_a_10.AutoSize = True
        Me.opt_1_a_10.Location = New System.Drawing.Point(6, 127)
        Me.opt_1_a_10.Name = "opt_1_a_10"
        Me.opt_1_a_10.Size = New System.Drawing.Size(130, 17)
        Me.opt_1_a_10.TabIndex = 3
        Me.opt_1_a_10.TabStop = True
        Me.opt_1_a_10.Text = "Do dia 1 ao dia 10"
        Me.opt_1_a_10.UseVisualStyleBackColor = True
        '
        'opt_16_fim
        '
        Me.opt_16_fim.AutoSize = True
        Me.opt_16_fim.Location = New System.Drawing.Point(6, 85)
        Me.opt_16_fim.Name = "opt_16_fim"
        Me.opt_16_fim.Size = New System.Drawing.Size(118, 17)
        Me.opt_16_fim.TabIndex = 2
        Me.opt_16_fim.TabStop = True
        Me.opt_16_fim.Text = "Do dia 16 ao fim"
        Me.opt_16_fim.UseVisualStyleBackColor = True
        '
        'opt_1_a_15
        '
        Me.opt_1_a_15.AutoSize = True
        Me.opt_1_a_15.Location = New System.Drawing.Point(6, 62)
        Me.opt_1_a_15.Name = "opt_1_a_15"
        Me.opt_1_a_15.Size = New System.Drawing.Size(130, 17)
        Me.opt_1_a_15.TabIndex = 1
        Me.opt_1_a_15.TabStop = True
        Me.opt_1_a_15.Text = "Do dia 1 ao dia 15"
        Me.opt_1_a_15.UseVisualStyleBackColor = True
        '
        'optMes
        '
        Me.optMes.AutoSize = True
        Me.optMes.Checked = True
        Me.optMes.Location = New System.Drawing.Point(6, 22)
        Me.optMes.Name = "optMes"
        Me.optMes.Size = New System.Drawing.Size(87, 17)
        Me.optMes.TabIndex = 0
        Me.optMes.TabStop = True
        Me.optMes.Text = "Mês inteiro"
        Me.optMes.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(985, 518)
        Me.Controls.Add(Me.grpMesAnterior)
        Me.Controls.Add(Me.btnSQLTechTact)
        Me.Controls.Add(Me.btnReinicio)
        Me.Controls.Add(Me.btnAtualizaPastas)
        Me.Controls.Add(Me.cboClientes)
        Me.Controls.Add(Me.dtpDataExtracao)
        Me.Controls.Add(Me.btnFecha)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnSQL)
        Me.Controls.Add(Me.btnAlerts2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPastaProj)
        Me.Controls.Add(Me.txtMensagens)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.txtPastaPython)
        Me.Controls.Add(Me.btnAlerts1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "VBActive_Taegis_Alerts"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMesAnterior.ResumeLayout(False)
        Me.grpMesAnterior.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnAlerts1 As Button
    Friend WithEvents txtPastaPython As TextBox
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents Label1 As Label
    Friend WithEvents txtMensagens As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPastaProj As TextBox
    Friend WithEvents btnAlerts2 As Button
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
    Friend WithEvents btnReinicio As Button
    Friend WithEvents btnSQLTechTact As Button
    Friend WithEvents CriaTabelaAlertsIdToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TacticsTechniquesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents ImportaTechniquesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ImportaPlanilhaDeTechniquesGenéricaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DebugToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents grpMesAnterior As GroupBox
    Friend WithEvents optMes As RadioButton
    Friend WithEvents opt_1_a_10 As RadioButton
    Friend WithEvents opt_16_fim As RadioButton
    Friend WithEvents opt_1_a_15 As RadioButton
    Friend WithEvents opt_21_a_fim As RadioButton
    Friend WithEvents opt_11_a_20 As RadioButton
End Class
