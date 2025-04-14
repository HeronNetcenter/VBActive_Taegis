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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArquivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfiguraçãoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabelaDeClientesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListaDeMutationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FechaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnParseJson = New System.Windows.Forms.Button()
        Me.btnSQL = New System.Windows.Forms.Button()
        Me.btnInvestigations2 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPastaProj = New System.Windows.Forms.TextBox()
        Me.txtMensagens = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPastaPython = New System.Windows.Forms.TextBox()
        Me.btnInvestigations1 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnFecha = New System.Windows.Forms.Button()
        Me.cboClientes = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtInvestigationId = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtServiceDeskId = New System.Windows.Forms.TextBox()
        Me.btnMutation = New System.Windows.Forms.Button()
        Me.btnRunMutation = New System.Windows.Forms.Button()
        Me.btnInestigationsList = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCSVManual = New System.Windows.Forms.Button()
        Me.chkProcessoManual = New System.Windows.Forms.CheckBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnSQLManual = New System.Windows.Forms.Button()
        Me.btnSelecioneCliente = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
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
        Me.MenuStrip1.Size = New System.Drawing.Size(959, 24)
        Me.MenuStrip1.TabIndex = 12
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArquivoToolStripMenuItem
        '
        Me.ArquivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfiguraçãoToolStripMenuItem, Me.TabelaDeClientesToolStripMenuItem, Me.ListaDeMutationsToolStripMenuItem, Me.FechaToolStripMenuItem})
        Me.ArquivoToolStripMenuItem.Name = "ArquivoToolStripMenuItem"
        Me.ArquivoToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.ArquivoToolStripMenuItem.Text = "Arquivo"
        '
        'ConfiguraçãoToolStripMenuItem
        '
        Me.ConfiguraçãoToolStripMenuItem.Name = "ConfiguraçãoToolStripMenuItem"
        Me.ConfiguraçãoToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.ConfiguraçãoToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.ConfiguraçãoToolStripMenuItem.Text = "Configuração"
        '
        'TabelaDeClientesToolStripMenuItem
        '
        Me.TabelaDeClientesToolStripMenuItem.Name = "TabelaDeClientesToolStripMenuItem"
        Me.TabelaDeClientesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4
        Me.TabelaDeClientesToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.TabelaDeClientesToolStripMenuItem.Text = "Tabela de Clientes"
        '
        'ListaDeMutationsToolStripMenuItem
        '
        Me.ListaDeMutationsToolStripMenuItem.Name = "ListaDeMutationsToolStripMenuItem"
        Me.ListaDeMutationsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.ListaDeMutationsToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.ListaDeMutationsToolStripMenuItem.Text = "Lista de Mutations"
        '
        'FechaToolStripMenuItem
        '
        Me.FechaToolStripMenuItem.Name = "FechaToolStripMenuItem"
        Me.FechaToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.FechaToolStripMenuItem.Text = "Fecha"
        '
        'btnParseJson
        '
        Me.btnParseJson.Enabled = False
        Me.btnParseJson.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnParseJson.ForeColor = System.Drawing.Color.Red
        Me.btnParseJson.Location = New System.Drawing.Point(814, 307)
        Me.btnParseJson.Name = "btnParseJson"
        Me.btnParseJson.Size = New System.Drawing.Size(128, 23)
        Me.btnParseJson.TabIndex = 30
        Me.btnParseJson.Text = "Parse => JSON"
        Me.btnParseJson.UseVisualStyleBackColor = True
        Me.btnParseJson.Visible = False
        '
        'btnSQL
        '
        Me.btnSQL.Enabled = False
        Me.btnSQL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSQL.Location = New System.Drawing.Point(660, 226)
        Me.btnSQL.Name = "btnSQL"
        Me.btnSQL.Size = New System.Drawing.Size(128, 23)
        Me.btnSQL.TabIndex = 29
        Me.btnSQL.Text = "SQL"
        Me.btnSQL.UseVisualStyleBackColor = True
        '
        'btnInvestigations2
        '
        Me.btnInvestigations2.Enabled = False
        Me.btnInvestigations2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInvestigations2.ForeColor = System.Drawing.Color.Red
        Me.btnInvestigations2.Location = New System.Drawing.Point(813, 339)
        Me.btnInvestigations2.Name = "btnInvestigations2"
        Me.btnInvestigations2.Size = New System.Drawing.Size(128, 23)
        Me.btnInvestigations2.TabIndex = 28
        Me.btnInvestigations2.Text = "Investig2 => CSV"
        Me.btnInvestigations2.UseVisualStyleBackColor = True
        Me.btnInvestigations2.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Pasta Projeto"
        '
        'txtPastaProj
        '
        Me.txtPastaProj.Enabled = False
        Me.txtPastaProj.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPastaProj.Location = New System.Drawing.Point(15, 93)
        Me.txtPastaProj.Name = "txtPastaProj"
        Me.txtPastaProj.Size = New System.Drawing.Size(633, 20)
        Me.txtPastaProj.TabIndex = 26
        '
        'txtMensagens
        '
        Me.txtMensagens.BackColor = System.Drawing.SystemColors.Info
        Me.txtMensagens.Location = New System.Drawing.Point(15, 217)
        Me.txtMensagens.Multiline = True
        Me.txtMensagens.Name = "txtMensagens"
        Me.txtMensagens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMensagens.Size = New System.Drawing.Size(633, 199)
        Me.txtMensagens.TabIndex = 25
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Pasta Python.exe"
        '
        'txtPastaPython
        '
        Me.txtPastaPython.Enabled = False
        Me.txtPastaPython.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPastaPython.Location = New System.Drawing.Point(15, 50)
        Me.txtPastaPython.Name = "txtPastaPython"
        Me.txtPastaPython.Size = New System.Drawing.Size(633, 20)
        Me.txtPastaPython.TabIndex = 23
        '
        'btnInvestigations1
        '
        Me.btnInvestigations1.Enabled = False
        Me.btnInvestigations1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInvestigations1.Location = New System.Drawing.Point(661, 182)
        Me.btnInvestigations1.Name = "btnInvestigations1"
        Me.btnInvestigations1.Size = New System.Drawing.Size(128, 23)
        Me.btnInvestigations1.TabIndex = 22
        Me.btnInvestigations1.Text = "Investig1 => JSON"
        Me.btnInvestigations1.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(675, 283)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 31
        Me.PictureBox1.TabStop = False
        '
        'btnFecha
        '
        Me.btnFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFecha.Location = New System.Drawing.Point(813, 376)
        Me.btnFecha.Name = "btnFecha"
        Me.btnFecha.Size = New System.Drawing.Size(128, 40)
        Me.btnFecha.TabIndex = 32
        Me.btnFecha.Text = "Fecha"
        Me.btnFecha.UseVisualStyleBackColor = True
        '
        'cboClientes
        '
        Me.cboClientes.Enabled = False
        Me.cboClientes.FormattingEnabled = True
        Me.cboClientes.Location = New System.Drawing.Point(660, 49)
        Me.cboClientes.Name = "cboClientes"
        Me.cboClientes.Size = New System.Drawing.Size(128, 21)
        Me.cboClientes.TabIndex = 33
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 123)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 13)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "Investigation Id"
        '
        'txtInvestigationId
        '
        Me.txtInvestigationId.Enabled = False
        Me.txtInvestigationId.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvestigationId.Location = New System.Drawing.Point(15, 139)
        Me.txtInvestigationId.Name = "txtInvestigationId"
        Me.txtInvestigationId.Size = New System.Drawing.Size(595, 20)
        Me.txtInvestigationId.TabIndex = 34
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 169)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 13)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Servide Desk Id"
        '
        'txtServiceDeskId
        '
        Me.txtServiceDeskId.Enabled = False
        Me.txtServiceDeskId.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServiceDeskId.Location = New System.Drawing.Point(15, 185)
        Me.txtServiceDeskId.Name = "txtServiceDeskId"
        Me.txtServiceDeskId.Size = New System.Drawing.Size(306, 20)
        Me.txtServiceDeskId.TabIndex = 36
        '
        'btnMutation
        '
        Me.btnMutation.Enabled = False
        Me.btnMutation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMutation.Location = New System.Drawing.Point(358, 182)
        Me.btnMutation.Name = "btnMutation"
        Me.btnMutation.Size = New System.Drawing.Size(128, 23)
        Me.btnMutation.TabIndex = 38
        Me.btnMutation.Text = "Mutation"
        Me.btnMutation.UseVisualStyleBackColor = True
        '
        'btnRunMutation
        '
        Me.btnRunMutation.Enabled = False
        Me.btnRunMutation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRunMutation.Location = New System.Drawing.Point(520, 182)
        Me.btnRunMutation.Name = "btnRunMutation"
        Me.btnRunMutation.Size = New System.Drawing.Size(128, 23)
        Me.btnRunMutation.TabIndex = 39
        Me.btnRunMutation.Text = "Run Mutation"
        Me.btnRunMutation.UseVisualStyleBackColor = True
        '
        'btnInestigationsList
        '
        Me.btnInestigationsList.Enabled = False
        Me.btnInestigationsList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInestigationsList.Location = New System.Drawing.Point(617, 139)
        Me.btnInestigationsList.Name = "btnInestigationsList"
        Me.btnInestigationsList.Size = New System.Drawing.Size(31, 23)
        Me.btnInestigationsList.TabIndex = 40
        Me.btnInestigationsList.Text = "..."
        Me.ToolTip1.SetToolTip(Me.btnInestigationsList, "Investigations List")
        Me.btnInestigationsList.UseVisualStyleBackColor = True
        '
        'btnCSVManual
        '
        Me.btnCSVManual.Enabled = False
        Me.btnCSVManual.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCSVManual.Location = New System.Drawing.Point(813, 182)
        Me.btnCSVManual.Name = "btnCSVManual"
        Me.btnCSVManual.Size = New System.Drawing.Size(128, 23)
        Me.btnCSVManual.TabIndex = 42
        Me.btnCSVManual.Text = "CSV Manual"
        Me.ToolTip1.SetToolTip(Me.btnCSVManual, "Busca o arquivo CSV criado a partir do site Targis XDR \ Investigations")
        Me.btnCSVManual.UseVisualStyleBackColor = True
        '
        'chkProcessoManual
        '
        Me.chkProcessoManual.AutoSize = True
        Me.chkProcessoManual.Enabled = False
        Me.chkProcessoManual.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProcessoManual.Location = New System.Drawing.Point(814, 153)
        Me.chkProcessoManual.Name = "chkProcessoManual"
        Me.chkProcessoManual.Size = New System.Drawing.Size(123, 17)
        Me.chkProcessoManual.TabIndex = 44
        Me.chkProcessoManual.Text = "Processo Manual"
        Me.ToolTip1.SetToolTip(Me.chkProcessoManual, "Busca o arquivo CSV criado a partir do site Targis XDR \ Investigations")
        Me.chkProcessoManual.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 438)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(959, 22)
        Me.StatusStrip1.TabIndex = 41
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
        'btnSQLManual
        '
        Me.btnSQLManual.Enabled = False
        Me.btnSQLManual.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSQLManual.Location = New System.Drawing.Point(813, 226)
        Me.btnSQLManual.Name = "btnSQLManual"
        Me.btnSQLManual.Size = New System.Drawing.Size(128, 23)
        Me.btnSQLManual.TabIndex = 43
        Me.btnSQLManual.Text = "CSV Manual -> SQL"
        Me.btnSQLManual.UseVisualStyleBackColor = True
        '
        'btnSelecioneCliente
        '
        Me.btnSelecioneCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelecioneCliente.Location = New System.Drawing.Point(809, 49)
        Me.btnSelecioneCliente.Name = "btnSelecioneCliente"
        Me.btnSelecioneCliente.Size = New System.Drawing.Size(128, 40)
        Me.btnSelecioneCliente.TabIndex = 45
        Me.btnSelecioneCliente.Text = "<== Selecione um cliente"
        Me.btnSelecioneCliente.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(793, 283)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(166, 13)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Botões desativados em 20/03/25"
        Me.Label5.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(714, 208)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(14, 13)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "↓"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(869, 208)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(14, 13)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "↓"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(959, 460)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnSelecioneCliente)
        Me.Controls.Add(Me.chkProcessoManual)
        Me.Controls.Add(Me.btnSQLManual)
        Me.Controls.Add(Me.btnCSVManual)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnInestigationsList)
        Me.Controls.Add(Me.btnRunMutation)
        Me.Controls.Add(Me.btnMutation)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtServiceDeskId)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtInvestigationId)
        Me.Controls.Add(Me.cboClientes)
        Me.Controls.Add(Me.btnFecha)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnParseJson)
        Me.Controls.Add(Me.btnSQL)
        Me.Controls.Add(Me.btnInvestigations2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPastaProj)
        Me.Controls.Add(Me.txtMensagens)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPastaPython)
        Me.Controls.Add(Me.btnInvestigations1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Form1"
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
    Friend WithEvents ConfiguraçãoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FechaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnParseJson As Button
    Friend WithEvents btnSQL As Button
    Friend WithEvents btnInvestigations2 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPastaProj As TextBox
    Friend WithEvents txtMensagens As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtPastaPython As TextBox
    Friend WithEvents btnInvestigations1 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnFecha As Button
    Friend WithEvents cboClientes As ComboBox
    Friend WithEvents TabelaDeClientesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label3 As Label
    Friend WithEvents txtInvestigationId As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtServiceDeskId As TextBox
    Friend WithEvents btnMutation As Button
    Friend WithEvents btnRunMutation As Button
    Friend WithEvents btnInestigationsList As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ListaDeMutationsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents btnSQLManual As Button
    Friend WithEvents btnCSVManual As Button
    Friend WithEvents chkProcessoManual As CheckBox
    Friend WithEvents btnSelecioneCliente As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
End Class
