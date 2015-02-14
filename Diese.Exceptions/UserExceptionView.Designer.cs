namespace Diese.Exceptions
{
    partial class UserExceptionView
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
            this.quitButton = new System.Windows.Forms.Button();
            this.systemIcon = new System.Windows.Forms.PictureBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            this.copyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.systemIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // quitButton
            // 
            this.quitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.quitButton.Location = new System.Drawing.Point(234, 61);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(98, 31);
            this.quitButton.TabIndex = 2;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            // 
            // systemIcon
            // 
            this.systemIcon.BackColor = System.Drawing.SystemColors.Control;
            this.systemIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.systemIcon.Location = new System.Drawing.Point(12, 12);
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
            this.nameLabel.Size = new System.Drawing.Size(272, 13);
            this.nameLabel.TabIndex = 4;
            this.nameLabel.Text = "Exception :";
            // 
            // messageLabel
            // 
            this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageLabel.AutoEllipsis = true;
            this.messageLabel.Location = new System.Drawing.Point(60, 35);
            this.messageLabel.MinimumSize = new System.Drawing.Size(120, 13);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(272, 13);
            this.messageLabel.TabIndex = 5;
            this.messageLabel.Text = "Message...";
            // 
            // copyButton
            // 
            this.copyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.copyButton.Location = new System.Drawing.Point(12, 61);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(109, 31);
            this.copyButton.TabIndex = 7;
            this.copyButton.Text = "Copy to clipboard";
            this.copyButton.UseVisualStyleBackColor = true;
            // 
            // UserExceptionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 102);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.systemIcon);
            this.Controls.Add(this.quitButton);
            this.MinimumSize = new System.Drawing.Size(360, 140);
            this.Name = "UserExceptionView";
            this.Text = "An exception occured!";
            ((System.ComponentModel.ISupportInitialize)(this.systemIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.PictureBox systemIcon;
        private System.Windows.Forms.Button copyButton;
    }
}