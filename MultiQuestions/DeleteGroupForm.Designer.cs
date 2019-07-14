namespace MultiQuestions
{
    partial class DeleteGroupForm
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
            this.deleteGroup_button = new System.Windows.Forms.Button();
            this.DeleteGroup_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // deleteGroup_button
            // 
            this.deleteGroup_button.Location = new System.Drawing.Point(218, 113);
            this.deleteGroup_button.Name = "deleteGroup_button";
            this.deleteGroup_button.Size = new System.Drawing.Size(80, 25);
            this.deleteGroup_button.TabIndex = 5;
            this.deleteGroup_button.Text = "Удалить";
            this.deleteGroup_button.UseVisualStyleBackColor = true;
            this.deleteGroup_button.Click += new System.EventHandler(this.deleteGroup_button_Click);
            // 
            // DeleteGroup_textBox
            // 
            this.DeleteGroup_textBox.Location = new System.Drawing.Point(158, 73);
            this.DeleteGroup_textBox.Name = "DeleteGroup_textBox";
            this.DeleteGroup_textBox.Size = new System.Drawing.Size(200, 20);
            this.DeleteGroup_textBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Введите название категории тестов";
            // 
            // DeleteGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 181);
            this.Controls.Add(this.deleteGroup_button);
            this.Controls.Add(this.DeleteGroup_textBox);
            this.Controls.Add(this.label1);
            this.Name = "DeleteGroupForm";
            this.Text = "DeleteGroup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button deleteGroup_button;
        private System.Windows.Forms.TextBox DeleteGroup_textBox;
        private System.Windows.Forms.Label label1;
    }
}