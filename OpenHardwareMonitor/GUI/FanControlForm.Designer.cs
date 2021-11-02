namespace OpenHardwareMonitor.GUI {
  partial class FanControlForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FanControlForm));
            this.controlEnabledChk = new System.Windows.Forms.CheckBox();
            this.acceptBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.settingsBox = new System.Windows.Forms.TextBox();
            this.rampChk = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.holdTimeNud = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.holdTimeNud)).BeginInit();
            this.SuspendLayout();
            // 
            // controlEnabledChk
            // 
            this.controlEnabledChk.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.controlEnabledChk, 4);
            this.controlEnabledChk.Location = new System.Drawing.Point(3, 3);
            this.controlEnabledChk.Name = "controlEnabledChk";
            this.controlEnabledChk.Size = new System.Drawing.Size(404, 29);
            this.controlEnabledChk.TabIndex = 0;
            this.controlEnabledChk.Text = "Enable automatic software fan control";
            this.controlEnabledChk.UseVisualStyleBackColor = true;
            // 
            // acceptBtn
            // 
            this.acceptBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptBtn.Location = new System.Drawing.Point(403, 396);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(194, 49);
            this.acceptBtn.TabIndex = 1;
            this.acceptBtn.Text = "OK";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(603, 396);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(194, 49);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.controlEnabledChk, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.settingsBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.acceptBtn, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.cancelBtn, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.rampChk, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.holdTimeNud, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // settingsBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.settingsBox, 4);
            this.settingsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsBox.Font = new System.Drawing.Font("Consolas", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsBox.Location = new System.Drawing.Point(3, 38);
            this.settingsBox.Multiline = true;
            this.settingsBox.Name = "settingsBox";
            this.tableLayoutPanel1.SetRowSpan(this.settingsBox, 3);
            this.settingsBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.settingsBox.Size = new System.Drawing.Size(794, 315);
            this.settingsBox.TabIndex = 3;
            this.settingsBox.WordWrap = false;
            // 
            // rampChk
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.rampChk, 2);
            this.rampChk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rampChk.Location = new System.Drawing.Point(403, 359);
            this.rampChk.Name = "rampChk";
            this.rampChk.Size = new System.Drawing.Size(394, 31);
            this.rampChk.TabIndex = 4;
            this.rampChk.Text = "Gradual ramp up / down";
            this.rampChk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 356);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 37);
            this.label1.TabIndex = 5;
            this.label1.Text = "Hold Time (sec):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // holdTimeNud
            // 
            this.holdTimeNud.DecimalPlaces = 2;
            this.holdTimeNud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.holdTimeNud.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.holdTimeNud.Location = new System.Drawing.Point(203, 359);
            this.holdTimeNud.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.holdTimeNud.MaximumSize = new System.Drawing.Size(120, 0);
            this.holdTimeNud.Name = "holdTimeNud";
            this.holdTimeNud.Size = new System.Drawing.Size(120, 31);
            this.holdTimeNud.TabIndex = 6;
            // 
            // FanControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FanControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fan Control";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.holdTimeNud)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckBox controlEnabledChk;
    private System.Windows.Forms.Button acceptBtn;
    private System.Windows.Forms.Button cancelBtn;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TextBox settingsBox;
    private System.Windows.Forms.CheckBox rampChk;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.NumericUpDown holdTimeNud;
  }
}