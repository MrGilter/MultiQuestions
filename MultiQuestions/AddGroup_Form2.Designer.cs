namespace MultiQuestions
{
    partial class AddGroup_Form2
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
            this.label1 = new System.Windows.Forms.Label();
            this.addGroup_textBox = new System.Windows.Forms.TextBox();
            this.addGroup_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите название категории тестов";
            // 
            // addGroup_textBox
            // 
            this.addGroup_textBox.Location = new System.Drawing.Point(150, 60);
            this.addGroup_textBox.Name = "addGroup_textBox";
            this.addGroup_textBox.Size = new System.Drawing.Size(200, 20);
            this.addGroup_textBox.TabIndex = 1;
            // 
            // addGroup_button
            // 
            this.addGroup_button.Location = new System.Drawing.Point(210, 100);
            this.addGroup_button.Name = "addGroup_button";
            this.addGroup_button.Size = new System.Drawing.Size(80, 25);
            this.addGroup_button.TabIndex = 2;
            this.addGroup_button.Text = "Добавить";
            this.addGroup_button.UseVisualStyleBackColor = true;
            this.addGroup_button.Click += new System.EventHandler(this.addGroup_button_Click);
            // 
            // AddGroup_Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 142);
            this.Controls.Add(this.addGroup_button);
            this.Controls.Add(this.addGroup_textBox);
            this.Controls.Add(this.label1);
            this.Name = "AddGroup_Form2";
            this.Text = "AddGroup_Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox addGroup_textBox;
        private System.Windows.Forms.Button addGroup_button;
    }
}