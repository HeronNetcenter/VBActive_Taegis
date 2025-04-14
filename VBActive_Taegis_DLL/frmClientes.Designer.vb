<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClientes
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
        Me.btnFecha = New System.Windows.Forms.Button()
        Me.btnExclui = New System.Windows.Forms.Button()
        Me.btnGrava = New System.Windows.Forms.Button()
        Me.btnCancela = New System.Windows.Forms.Button()
        Me.btnAltera = New System.Windows.Forms.Button()
        Me.btnInclui = New System.Windows.Forms.Button()
        Me.txtClientSecret = New System.Windows.Forms.TextBox()
        Me.txtClientId = New System.Windows.Forms.TextBox()
        Me.txtTenantId = New System.Windows.Forms.TextBox()
        Me.txtNomeCliente = New System.Windows.Forms.TextBox()
        Me.lvwClientes = New System.Windows.Forms.ListView()
        Me.Cliente = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Tenant_ID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Client_ID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Client_Secret = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Abrev = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtAbrevCliente = New System.Windows.Forms.TextBox()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnFecha
        '
        Me.btnFecha.Location = New System.Drawing.Point(1040, 413)
        Me.btnFecha.Name = "btnFecha"
        Me.btnFecha.Size = New System.Drawing.Size(75, 23)
        Me.btnFecha.TabIndex = 29
        Me.btnFecha.Text = "Fecha"
        Me.btnFecha.UseVisualStyleBackColor = True
        '
        'btnExclui
        '
        Me.btnExclui.Enabled = False
        Me.btnExclui.Location = New System.Drawing.Point(336, 413)
        Me.btnExclui.Name = "btnExclui"
        Me.btnExclui.Size = New System.Drawing.Size(75, 23)
        Me.btnExclui.TabIndex = 28
        Me.btnExclui.Text = "Exclui"
        Me.btnExclui.UseVisualStyleBackColor = True
        '
        'btnGrava
        '
        Me.btnGrava.Enabled = False
        Me.btnGrava.Location = New System.Drawing.Point(255, 413)
        Me.btnGrava.Name = "btnGrava"
        Me.btnGrava.Size = New System.Drawing.Size(75, 23)
        Me.btnGrava.TabIndex = 27
        Me.btnGrava.Text = "Grava"
        Me.btnGrava.UseVisualStyleBackColor = True
        '
        'btnCancela
        '
        Me.btnCancela.Enabled = False
        Me.btnCancela.Location = New System.Drawing.Point(174, 413)
        Me.btnCancela.Name = "btnCancela"
        Me.btnCancela.Size = New System.Drawing.Size(75, 23)
        Me.btnCancela.TabIndex = 26
        Me.btnCancela.Text = "Cancela"
        Me.btnCancela.UseVisualStyleBackColor = True
        '
        'btnAltera
        '
        Me.btnAltera.Enabled = False
        Me.btnAltera.Location = New System.Drawing.Point(93, 413)
        Me.btnAltera.Name = "btnAltera"
        Me.btnAltera.Size = New System.Drawing.Size(75, 23)
        Me.btnAltera.TabIndex = 25
        Me.btnAltera.Text = "Altera"
        Me.btnAltera.UseVisualStyleBackColor = True
        '
        'btnInclui
        '
        Me.btnInclui.Location = New System.Drawing.Point(12, 413)
        Me.btnInclui.Name = "btnInclui"
        Me.btnInclui.Size = New System.Drawing.Size(75, 23)
        Me.btnInclui.TabIndex = 24
        Me.btnInclui.Text = "Inclui"
        Me.btnInclui.UseVisualStyleBackColor = True
        '
        'txtClientSecret
        '
        Me.txtClientSecret.Enabled = False
        Me.txtClientSecret.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClientSecret.Location = New System.Drawing.Point(614, 387)
        Me.txtClientSecret.Name = "txtClientSecret"
        Me.txtClientSecret.Size = New System.Drawing.Size(501, 20)
        Me.txtClientSecret.TabIndex = 24
        '
        'txtClientId
        '
        Me.txtClientId.Enabled = False
        Me.txtClientId.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClientId.Location = New System.Drawing.Point(336, 387)
        Me.txtClientId.Name = "txtClientId"
        Me.txtClientId.Size = New System.Drawing.Size(281, 20)
        Me.txtClientId.TabIndex = 23
        '
        'txtTenantId
        '
        Me.txtTenantId.Enabled = False
        Me.txtTenantId.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTenantId.Location = New System.Drawing.Point(255, 387)
        Me.txtTenantId.Name = "txtTenantId"
        Me.txtTenantId.Size = New System.Drawing.Size(82, 20)
        Me.txtTenantId.TabIndex = 22
        '
        'txtNomeCliente
        '
        Me.txtNomeCliente.Enabled = False
        Me.txtNomeCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNomeCliente.Location = New System.Drawing.Point(12, 387)
        Me.txtNomeCliente.Name = "txtNomeCliente"
        Me.txtNomeCliente.Size = New System.Drawing.Size(190, 20)
        Me.txtNomeCliente.TabIndex = 20
        '
        'lvwClientes
        '
        Me.lvwClientes.BackColor = System.Drawing.SystemColors.Info
        Me.lvwClientes.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Cliente, Me.Abrev, Me.Tenant_ID, Me.Client_ID, Me.Client_Secret})
        Me.lvwClientes.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwClientes.FullRowSelect = True
        Me.lvwClientes.GridLines = True
        Me.lvwClientes.HideSelection = False
        Me.lvwClientes.Location = New System.Drawing.Point(12, 12)
        Me.lvwClientes.MultiSelect = False
        Me.lvwClientes.Name = "lvwClientes"
        Me.lvwClientes.Size = New System.Drawing.Size(1103, 369)
        Me.lvwClientes.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvwClientes.TabIndex = 19
        Me.lvwClientes.UseCompatibleStateImageBehavior = False
        Me.lvwClientes.View = System.Windows.Forms.View.Details
        '
        'Cliente
        '
        Me.Cliente.Text = "Cliente"
        Me.Cliente.Width = 190
        '
        'Tenant_ID
        '
        Me.Tenant_ID.Text = "Tenant_ID"
        Me.Tenant_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Tenant_ID.Width = 78
        '
        'Client_ID
        '
        Me.Client_ID.Text = "Client_ID"
        Me.Client_ID.Width = 278
        '
        'Client_Secret
        '
        Me.Client_Secret.Text = "Client_Secret"
        Me.Client_Secret.Width = 500
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 447)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1127, 22)
        Me.StatusStrip1.TabIndex = 30
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
        'Abrev
        '
        Me.Abrev.Text = "Abrev"
        Me.Abrev.Width = 55
        '
        'txtAbrevCliente
        '
        Me.txtAbrevCliente.Enabled = False
        Me.txtAbrevCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAbrevCliente.Location = New System.Drawing.Point(202, 387)
        Me.txtAbrevCliente.Name = "txtAbrevCliente"
        Me.txtAbrevCliente.Size = New System.Drawing.Size(58, 20)
        Me.txtAbrevCliente.TabIndex = 21
        '
        'frmClientes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1127, 469)
        Me.Controls.Add(Me.txtAbrevCliente)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnFecha)
        Me.Controls.Add(Me.btnExclui)
        Me.Controls.Add(Me.btnGrava)
        Me.Controls.Add(Me.btnCancela)
        Me.Controls.Add(Me.btnAltera)
        Me.Controls.Add(Me.btnInclui)
        Me.Controls.Add(Me.txtClientSecret)
        Me.Controls.Add(Me.txtClientId)
        Me.Controls.Add(Me.txtTenantId)
        Me.Controls.Add(Me.txtNomeCliente)
        Me.Controls.Add(Me.lvwClientes)
        Me.Name = "frmClientes"
        Me.Text = "frmClientes"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnFecha As Windows.Forms.Button
    Friend WithEvents btnExclui As Windows.Forms.Button
    Friend WithEvents btnGrava As Windows.Forms.Button
    Friend WithEvents btnCancela As Windows.Forms.Button
    Friend WithEvents btnAltera As Windows.Forms.Button
    Friend WithEvents btnInclui As Windows.Forms.Button
    Friend WithEvents txtClientSecret As Windows.Forms.TextBox
    Friend WithEvents txtClientId As Windows.Forms.TextBox
    Friend WithEvents txtTenantId As Windows.Forms.TextBox
    Friend WithEvents txtNomeCliente As Windows.Forms.TextBox
    Friend WithEvents lvwClientes As Windows.Forms.ListView
    Friend WithEvents Cliente As Windows.Forms.ColumnHeader
    Friend WithEvents Tenant_ID As Windows.Forms.ColumnHeader
    Friend WithEvents Client_ID As Windows.Forms.ColumnHeader
    Friend WithEvents Client_Secret As Windows.Forms.ColumnHeader
    Friend WithEvents StatusStrip1 As Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Abrev As Windows.Forms.ColumnHeader
    Friend WithEvents txtAbrevCliente As Windows.Forms.TextBox
End Class
