namespace MondayDriver
{
    partial class FileContentWindow
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
            this.fileDisplayRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // fileDisplayRichTextBox
            // 
            this.fileDisplayRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileDisplayRichTextBox.Location = new System.Drawing.Point(12, 12);
            this.fileDisplayRichTextBox.Name = "fileDisplayRichTextBox";
            this.fileDisplayRichTextBox.ReadOnly = true;
            this.fileDisplayRichTextBox.Size = new System.Drawing.Size(360, 337);
            this.fileDisplayRichTextBox.TabIndex = 0;
            this.fileDisplayRichTextBox.Text = "";
            // 
            // FileContentWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.fileDisplayRichTextBox);
            this.Name = "FileContentWindow";
            this.Text = "FileContent";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox fileDisplayRichTextBox;
    }
}