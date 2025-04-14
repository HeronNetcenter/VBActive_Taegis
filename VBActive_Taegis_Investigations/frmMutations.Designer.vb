<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMutations
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
        Me.cboClientes = New System.Windows.Forms.ComboBox()
        Me.lvwMutationsList = New System.Windows.Forms.ListView()
        Me.Id_Log = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ClientName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.DateLog = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Investigation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Chamado = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnFecha = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboDias = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cboClientes
        '
        Me.cboClientes.FormattingEnabled = True
        Me.cboClientes.Location = New System.Drawing.Point(706, 12)
        Me.cboClientes.Name = "cboClientes"
        Me.cboClientes.Size = New System.Drawing.Size(128, 21)
        Me.cboClientes.TabIndex = 34
        '
        'lvwMutationsList
        '
        Me.lvwMutationsList.BackColor = System.Drawing.SystemColors.Info
        Me.lvwMutationsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Id_Log, Me.ClientName, Me.DateLog, Me.Investigation, Me.Chamado})
        Me.lvwMutationsList.FullRowSelect = True
        Me.lvwMutationsList.GridLines = True
        Me.lvwMutationsList.Location = New System.Drawing.Point(12, 44)
        Me.lvwMutationsList.MultiSelect = False
        Me.lvwMutationsList.Name = "lvwMutationsList"
        Me.lvwMutationsList.Size = New System.Drawing.Size(822, 471)
        Me.lvwMutationsList.TabIndex = 35
        Me.lvwMutationsList.UseCompatibleStateImageBehavior = False
        Me.lvwMutationsList.View = System.Windows.Forms.View.Details
        '
        'Id_Log
        '
        Me.Id_Log.Text = "Id Log"
        Me.Id_Log.Width = 80
        '
        'ClientName
        '
        Me.ClientName.Text = "Cliente"
        Me.ClientName.Width = 250
        '
        'DateLog
        '
        Me.DateLog.Text = "Data do Log"
        Me.DateLog.Width = 170
        '
        'Investigation
        '
        Me.Investigation.Text = "Investigação"
        Me.Investigation.Width = 150
        '
        'Chamado
        '
        Me.Chamado.Text = "Chamado"
        Me.Chamado.Width = 150
        '
        'btnFecha
        '
        Me.btnFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFecha.Location = New System.Drawing.Point(706, 521)
        Me.btnFecha.Name = "btnFecha"
        Me.btnFecha.Size = New System.Drawing.Size(128, 23)
        Me.btnFecha.TabIndex = 41
        Me.btnFecha.Text = "Fecha"
        Me.btnFecha.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 547)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(844, 22)
        Me.StatusStrip1.TabIndex = 42
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(128, 17)
        Me.ToolStripStatusLabel1.Text = "Nostromo C 2022-2023"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(654, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "Cliente:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(12, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(129, 13)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Mutations nos últimos"
        '
        'cboDias
        '
        Me.cboDias.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDias.ForeColor = System.Drawing.Color.Red
        Me.cboDias.FormattingEnabled = True
        Me.cboDias.Items.AddRange(New Object() {"0", "1", "2", "3", "7", "15", "30", "60", "90"})
        Me.cboDias.Location = New System.Drawing.Point(144, 12)
        Me.cboDias.Name = "cboDias"
        Me.cboDias.Size = New System.Drawing.Size(45, 21)
        Me.cboDias.TabIndex = 45
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(192, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "dias"
        '
        'frmMutations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(844, 569)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboDias)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnFecha)
        Me.Controls.Add(Me.lvwMutationsList)
        Me.Controls.Add(Me.cboClientes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "frmMutations"
        Me.Text = "frmMutations"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboClientes As ComboBox
    Friend WithEvents lvwMutationsList As ListView
    Friend WithEvents Id_Log As ColumnHeader
    Friend WithEvents ClientName As ColumnHeader
    Friend WithEvents DateLog As ColumnHeader
    Friend WithEvents btnFecha As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents Investigation As ColumnHeader
    Friend WithEvents Chamado As ColumnHeader
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cboDias As ComboBox
    Friend WithEvents Label3 As Label
End Class
