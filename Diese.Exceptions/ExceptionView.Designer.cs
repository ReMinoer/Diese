namespace Diese.Exceptions
{
    partial class ExceptionView
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.systemIcon = new System.Windows.Forms.PictureBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.systemIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(12, 67);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(282, 180);
            this.textBox.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(12, 253);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(282, 31);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // systemIcon
            // 
            this.systemIcon.BackColor = System.Drawing.SystemColors.Control;
            this.systemIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.systemIcon.Location = new System.Drawing.Point(13, 13);
            this.systemIcon.Name = "systemIcon";
            this.systemIcon.Size = new System.Drawing.Size(40, 40);
            this.systemIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.systemIcon.TabIndex = 3;
            this.systemIcon.TabStop = false;
            // 
            // nameLabel
            // 
            this.nameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameLabel.AutoEllipsis = true;
            this.nameLabel.Location = new System.Drawing.Point(60, 13);
            this.nameLabel.MinimumSize = new System.Drawing.Size(120, 13);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(234, 13);
            this.nameLabel.TabIndex = 4;
            this.nameLabel.Text = "An exception occurred :";
            // 
            // messageLabel
            // 
            this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageLabel.AutoEllipsis = true;
            this.messageLabel.Location = new System.Drawing.Point(60, 35);
            this.messageLabel.MinimumSize = new System.Drawing.Size(120, 13);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(234, 13);
            this.messageLabel.TabIndex = 5;
            this.messageLabel.Text = "\"\"";
            // 
            // ExceptionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 296);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.systemIcon);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.textBox);
            this.Name = "ExceptionView";
            this.Text = "ExceptionView";
            ((System.ComponentModel.ISupportInitialize)(this.systemIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox systemIcon;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label messageLabel;
    }
}