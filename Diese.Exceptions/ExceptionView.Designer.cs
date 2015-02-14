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
            this.QuitButton = new System.Windows.Forms.Button();
            this.systemIcon = new System.Windows.Forms.PictureBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.StackTraceList = new System.Windows.Forms.ListView();
            this.In = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Method = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Line = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CopyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.systemIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // quitButton
            // 
            this.QuitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.QuitButton.Location = new System.Drawing.Point(299, 379);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.Size = new System.Drawing.Size(98, 31);
            this.QuitButton.TabIndex = 2;
            this.QuitButton.Text = "Quit";
            this.QuitButton.UseVisualStyleBackColor = true;
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
            this.NameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameLabel.AutoEllipsis = true;
            this.NameLabel.Location = new System.Drawing.Point(60, 13);
            this.NameLabel.MinimumSize = new System.Drawing.Size(120, 13);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(337, 13);
            this.NameLabel.TabIndex = 4;
            this.NameLabel.Text = "Exception :";
            // 
            // messageLabel
            // 
            this.MessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageLabel.AutoEllipsis = true;
            this.MessageLabel.Location = new System.Drawing.Point(60, 35);
            this.MessageLabel.MinimumSize = new System.Drawing.Size(120, 13);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(337, 13);
            this.MessageLabel.TabIndex = 5;
            this.MessageLabel.Text = "\"\"";
            // 
            // stackTraceList
            // 
            this.StackTraceList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StackTraceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.In,
            this.Method,
            this.Line});
            this.StackTraceList.FullRowSelect = true;
            this.StackTraceList.GridLines = true;
            this.StackTraceList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.StackTraceList.Location = new System.Drawing.Point(13, 60);
            this.StackTraceList.Name = "StackTraceList";
            this.StackTraceList.Size = new System.Drawing.Size(384, 313);
            this.StackTraceList.TabIndex = 6;
            this.StackTraceList.UseCompatibleStateImageBehavior = false;
            this.StackTraceList.View = System.Windows.Forms.View.Details;
            // 
            // In
            // 
            this.In.Text = "@";
            this.In.Width = 25;
            // 
            // Method
            // 
            this.Method.Text = "Method";
            this.Method.Width = 300;
            // 
            // Line
            // 
            this.Line.Text = "Line";
            this.Line.Width = 50;
            // 
            // copyButton
            // 
            this.CopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyButton.Location = new System.Drawing.Point(13, 379);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(109, 31);
            this.CopyButton.TabIndex = 7;
            this.CopyButton.Text = "Copy to clipboard";
            this.CopyButton.UseVisualStyleBackColor = true;
            // 
            // ExceptionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 422);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.StackTraceList);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.systemIcon);
            this.Controls.Add(this.QuitButton);
            this.Name = "ExceptionView";
            this.Text = "An exception occured!";
            ((System.ComponentModel.ISupportInitialize)(this.systemIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox systemIcon;
        private System.Windows.Forms.ColumnHeader Method;
        private System.Windows.Forms.ColumnHeader Line;
        private System.Windows.Forms.ColumnHeader In;
    }
}