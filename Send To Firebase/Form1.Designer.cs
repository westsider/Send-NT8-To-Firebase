namespace Send_To_Firebase
{
    partial class Form1
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
            this.ReadFileBttn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ReadFileBttn
            // 
            this.ReadFileBttn.Location = new System.Drawing.Point(38, 57);
            this.ReadFileBttn.Name = "ReadFileBttn";
            this.ReadFileBttn.Size = new System.Drawing.Size(190, 35);
            this.ReadFileBttn.TabIndex = 0;
            this.ReadFileBttn.Text = "Read";
            this.ReadFileBttn.UseVisualStyleBackColor = true;
            this.ReadFileBttn.Click += new System.EventHandler(this.ReadFileBttn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(38, 109);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1103, 1359);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.RichTextBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 1496);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.ReadFileBttn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ReadFileBttn;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

