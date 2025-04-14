<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvestigationsList
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
        Me.lvwInvestigationsList = New System.Windows.Forms.ListView()
        Me.Id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ShortId = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.DateCreated = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Description = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Priority = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Service_desk_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Alt_data = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnTransfereId = New System.Windows.Forms.Button()
        Me.btnFecha = New System.Windows.Forms.Button()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvwInvestigationsList
        '
        Me.lvwInvestigationsList.BackColor = System.Drawing.SystemColors.Info
        Me.lvwInvestigationsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Id, Me.ShortId, Me.DateCreated, Me.Description, Me.Priority, Me.Status, Me.Service_desk_id, Me.Alt_data})
        Me.lvwInvestigationsList.FullRowSelect = True
        Me.lvwInvestigationsList.GridLines = True
        Me.lvwInvestigationsList.Location = New System.Drawing.Point(12, 12)
        Me.lvwInvestigationsList.MultiSelect = False
        Me.lvwInvestigationsList.Name = "lvwInvestigationsList"
        Me.lvwInvestigationsList.Size = New System.Drawing.Size(1202, 420)
        Me.lvwInvestigationsList.TabIndex = 0
        Me.lvwInvestigationsList.UseCompatibleStateImageBehavior = False
        Me.lvwInvestigationsList.View = System.Windows.Forms.View.Details
        '
        'Id
        '
        Me.Id.Text = "Id"
        Me.Id.Width = 250
        '
        'ShortId
        '
        Me.ShortId.Text = "Short Id"
        Me.ShortId.Width = 80
        '
        'DateCreated
        '
        Me.DateCreated.Text = "Data de Criação"
        Me.DateCreated.Width = 170
        '
        'Description
        '
        Me.Description.Text = "Descrição"
        Me.Description.Width = 280
        '
        'Priority
        '
        Me.Priority.Text = "Prior"
        Me.Priority.Width = 40
        '
        'Status
        '
        Me.Status.Text = "Status"
        Me.Status.Width = 150
        '
        'Service_desk_id
        '
        Me.Service_desk_id.Text = "Service Desk Id"
        Me.Service_desk_id.Width = 90
        '
        'Alt_data
        '
        Me.Alt_data.Text = "Data Alt"
        Me.Alt_data.Width = 120
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 463)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1223, 22)
        Me.StatusStrip1.TabIndex = 14
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(128, 17)
        Me.ToolStripStatusLabel1.Text = "Nostromo C 2022-2023"
        '
        'btnTransfereId
        '
        Me.btnTransfereId.Enabled = False
        Me.btnTransfereId.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransfereId.Location = New System.Drawing.Point(12, 437)
        Me.btnTransfereId.Name = "btnTransfereId"
        Me.btnTransfereId.Size = New System.Drawing.Size(128, 23)
        Me.btnTransfereId.TabIndex = 39
        Me.btnTransfereId.Text = "Transfere Id"
        Me.btnTransfereId.UseVisualStyleBackColor = True
        '
        'btnFecha
        '
        Me.btnFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFecha.Location = New System.Drawing.Point(1086, 437)
        Me.btnFecha.Name = "btnFecha"
        Me.btnFecha.Size = New System.Drawing.Size(128, 23)
        Me.btnFecha.TabIndex = 40
        Me.btnFecha.Text = "Fecha"
        Me.btnFecha.UseVisualStyleBackColor = True
        '
        'frmInvestigationsList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1223, 485)
        Me.Controls.Add(Me.btnFecha)
        Me.Controls.Add(Me.btnTransfereId)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.lvwInvestigationsList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "frmInvestigationsList"
        Me.Text = "frmInvestigationsList"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvwInvestigationsList As ListView
    Friend WithEvents Id As ColumnHeader
    Friend WithEvents DateCreated As ColumnHeader
    Friend WithEvents Description As ColumnHeader
    Friend WithEvents Priority As ColumnHeader
    Friend WithEvents Status As ColumnHeader
    Friend WithEvents Service_desk_id As ColumnHeader
    Friend WithEvents Alt_data As ColumnHeader
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents btnTransfereId As Button
    Friend WithEvents btnFecha As Button
    Friend WithEvents ShortId As ColumnHeader
End Class
