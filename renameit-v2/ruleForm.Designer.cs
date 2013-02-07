namespace renameit_v2
{
    partial class ruleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbRule = new System.Windows.Forms.GroupBox();
            this.cbRuleType = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.tbTo = new System.Windows.Forms.TextBox();
            this.tbFrom = new System.Windows.Forms.TextBox();
            this.gbRule.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRule
            // 
            this.gbRule.Controls.Add(this.cbRuleType);
            this.gbRule.Controls.Add(this.btnCancel);
            this.gbRule.Controls.Add(this.btnSave);
            this.gbRule.Controls.Add(this.lblTo);
            this.gbRule.Controls.Add(this.lblFrom);
            this.gbRule.Controls.Add(this.tbTo);
            this.gbRule.Controls.Add(this.tbFrom);
            this.gbRule.Location = new System.Drawing.Point(12, 12);
            this.gbRule.Name = "gbRule";
            this.gbRule.Size = new System.Drawing.Size(506, 348);
            this.gbRule.TabIndex = 4;
            this.gbRule.TabStop = false;
            this.gbRule.Text = "Rule:";
            // 
            // cbRuleType
            // 
            this.cbRuleType.FormattingEnabled = true;
            this.cbRuleType.Location = new System.Drawing.Point(74, 43);
            this.cbRuleType.Name = "cbRuleType";
            this.cbRuleType.Size = new System.Drawing.Size(270, 21);
            this.cbRuleType.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(368, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(368, 43);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(24, 111);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(23, 13);
            this.lblTo.TabIndex = 6;
            this.lblTo.Text = "To:";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(24, 77);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(33, 13);
            this.lblFrom.TabIndex = 7;
            this.lblFrom.Text = "From:";
            // 
            // tbTo
            // 
            this.tbTo.Location = new System.Drawing.Point(74, 108);
            this.tbTo.Name = "tbTo";
            this.tbTo.Size = new System.Drawing.Size(270, 20);
            this.tbTo.TabIndex = 2;
            // 
            // tbFrom
            // 
            this.tbFrom.Location = new System.Drawing.Point(74, 74);
            this.tbFrom.Name = "tbFrom";
            this.tbFrom.Size = new System.Drawing.Size(270, 20);
            this.tbFrom.TabIndex = 1;
            // 
            // ruleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 372);
            this.Controls.Add(this.gbRule);
            this.Name = "ruleForm";
            this.Text = "ruleForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ruleForm_FormClosing);
            this.gbRule.ResumeLayout(false);
            this.gbRule.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox gbRule;
        public System.Windows.Forms.ComboBox cbRuleType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        public System.Windows.Forms.TextBox tbTo;
        public System.Windows.Forms.TextBox tbFrom;

    }
}