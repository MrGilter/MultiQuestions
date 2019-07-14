namespace MultiQuestions
{
    partial class EditForm
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
            this.editForm_treeView = new System.Windows.Forms.TreeView();
            this.nameTest_textBox = new System.Windows.Forms.TextBox();
            this.question_richTextBox = new System.Windows.Forms.RichTextBox();
            this.response_richTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trueness_checkBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.addAnswer_button = new System.Windows.Forms.Button();
            this.deleteSelect_button = new System.Windows.Forms.Button();
            this.addQuestion_button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.questionType_comboBox = new System.Windows.Forms.ComboBox();
            this.listAnsrwers_comboBox = new System.Windows.Forms.ComboBox();
            this.comment_richTextBox = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.help_button = new System.Windows.Forms.Button();
            this.editAnswer_button = new System.Windows.Forms.Button();
            this.editQuestion_button = new System.Windows.Forms.Button();
            this.deleteQuestion_button = new System.Windows.Forms.Button();
            this.FormClosing += editF_fMain_FormClosing;
            this.SuspendLayout();
            // 
            // editForm_treeView
            // 
            this.editForm_treeView.Location = new System.Drawing.Point(12, 12);
            this.editForm_treeView.Name = "editForm_treeView";
            this.editForm_treeView.Size = new System.Drawing.Size(191, 488);
            this.editForm_treeView.TabIndex = 0;
            this.editForm_treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.editForm_treeView_MouseDoubleClick);
            // 
            // nameTest_textBox
            // 
            this.nameTest_textBox.Location = new System.Drawing.Point(209, 28);
            this.nameTest_textBox.Name = "nameTest_textBox";
            this.nameTest_textBox.Size = new System.Drawing.Size(354, 20);
            this.nameTest_textBox.TabIndex = 1;
            // 
            // question_richTextBox
            // 
            this.question_richTextBox.Location = new System.Drawing.Point(209, 67);
            this.question_richTextBox.Name = "question_richTextBox";
            this.question_richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.question_richTextBox.Size = new System.Drawing.Size(756, 113);
            this.question_richTextBox.TabIndex = 2;
            this.question_richTextBox.Text = "";
            // 
            // response_richTextBox
            // 
            this.response_richTextBox.Location = new System.Drawing.Point(209, 199);
            this.response_richTextBox.Name = "response_richTextBox";
            this.response_richTextBox.Size = new System.Drawing.Size(471, 119);
            this.response_richTextBox.TabIndex = 3;
            this.response_richTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name test:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Question:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(213, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Response text:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(213, 321);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "List of answers:";
            // 
            // trueness_checkBox
            // 
            this.trueness_checkBox.AutoSize = true;
            this.trueness_checkBox.Location = new System.Drawing.Point(689, 220);
            this.trueness_checkBox.Name = "trueness_checkBox";
            this.trueness_checkBox.Size = new System.Drawing.Size(48, 17);
            this.trueness_checkBox.TabIndex = 8;
            this.trueness_checkBox.Text = "True";
            this.trueness_checkBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(686, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Trueness:";
            // 
            // addAnswer_button
            // 
            this.addAnswer_button.Location = new System.Drawing.Point(686, 282);
            this.addAnswer_button.Name = "addAnswer_button";
            this.addAnswer_button.Size = new System.Drawing.Size(279, 36);
            this.addAnswer_button.TabIndex = 10;
            this.addAnswer_button.Text = "Add answer";
            this.addAnswer_button.UseVisualStyleBackColor = true;
            this.addAnswer_button.Click += new System.EventHandler(this.addAnswer_button_Click);
            // 
            // deleteSelect_button
            // 
            this.deleteSelect_button.Location = new System.Drawing.Point(686, 337);
            this.deleteSelect_button.Name = "deleteSelect_button";
            this.deleteSelect_button.Size = new System.Drawing.Size(279, 23);
            this.deleteSelect_button.TabIndex = 11;
            this.deleteSelect_button.Text = "Delete selected answer from the list";
            this.deleteSelect_button.UseVisualStyleBackColor = true;
            this.deleteSelect_button.Click += new System.EventHandler(this.deleteSelect_button_Click);
            // 
            // addQuestion_button
            // 
            this.addQuestion_button.Location = new System.Drawing.Point(725, 477);
            this.addQuestion_button.Name = "addQuestion_button";
            this.addQuestion_button.Size = new System.Drawing.Size(240, 25);
            this.addQuestion_button.TabIndex = 12;
            this.addQuestion_button.Text = "Add question";
            this.addQuestion_button.UseVisualStyleBackColor = true;
            this.addQuestion_button.Click += new System.EventHandler(this.addQuestion_button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(593, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Qustion type:";
            // 
            // questionType_comboBox
            // 
            this.questionType_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.questionType_comboBox.FormattingEnabled = true;
            this.questionType_comboBox.Location = new System.Drawing.Point(596, 28);
            this.questionType_comboBox.Name = "questionType_comboBox";
            this.questionType_comboBox.Size = new System.Drawing.Size(160, 21);
            this.questionType_comboBox.TabIndex = 14;
            // 
            // listAnsrwers_comboBox
            // 
            this.listAnsrwers_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listAnsrwers_comboBox.FormattingEnabled = true;
            this.listAnsrwers_comboBox.Location = new System.Drawing.Point(209, 337);
            this.listAnsrwers_comboBox.Name = "listAnsrwers_comboBox";
            this.listAnsrwers_comboBox.Size = new System.Drawing.Size(471, 21);
            this.listAnsrwers_comboBox.TabIndex = 15;
            this.listAnsrwers_comboBox.SelectedValueChanged += new System.EventHandler(this.comboBox_listAnsvers_SelectedValueChanged);
            // 
            // comment_richTextBox
            // 
            this.comment_richTextBox.Location = new System.Drawing.Point(209, 375);
            this.comment_richTextBox.Name = "comment_richTextBox";
            this.comment_richTextBox.Size = new System.Drawing.Size(756, 96);
            this.comment_richTextBox.TabIndex = 16;
            this.comment_richTextBox.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(213, 361);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Comment:";
            // 
            // help_button
            // 
            this.help_button.Location = new System.Drawing.Point(619, 477);
            this.help_button.Name = "help_button";
            this.help_button.Size = new System.Drawing.Size(100, 25);
            this.help_button.TabIndex = 18;
            this.help_button.Text = "Help";
            this.help_button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.help_button.UseVisualStyleBackColor = true;
            this.help_button.Click += new System.EventHandler(this.help_button_Click);
            // 
            // editAnswer_button
            // 
            this.editAnswer_button.Location = new System.Drawing.Point(686, 245);
            this.editAnswer_button.Name = "editAnswer_button";
            this.editAnswer_button.Size = new System.Drawing.Size(279, 31);
            this.editAnswer_button.TabIndex = 19;
            this.editAnswer_button.Text = "Edit answer";
            this.editAnswer_button.UseVisualStyleBackColor = true;
            this.editAnswer_button.Click += new System.EventHandler(this.editAnswer_button_Click);
            // 
            // editQuestion_button
            // 
            this.editQuestion_button.Location = new System.Drawing.Point(209, 477);
            this.editQuestion_button.Name = "editQuestion_button";
            this.editQuestion_button.Size = new System.Drawing.Size(240, 25);
            this.editQuestion_button.TabIndex = 20;
            this.editQuestion_button.Text = "Edit question";
            this.editQuestion_button.UseVisualStyleBackColor = true;
            this.editQuestion_button.Click += new System.EventHandler(this.editQuestion_button_Click);
            // 
            // deleteQuestion_button
            // 
            this.deleteQuestion_button.Location = new System.Drawing.Point(455, 477);
            this.deleteQuestion_button.Name = "deleteQuestion_button";
            this.deleteQuestion_button.Size = new System.Drawing.Size(108, 25);
            this.deleteQuestion_button.TabIndex = 21;
            this.deleteQuestion_button.Text = "Delete question";
            this.deleteQuestion_button.UseVisualStyleBackColor = true;
            this.deleteQuestion_button.Click += new System.EventHandler(this.deleteQuestion_button_Click);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 512);
            this.Controls.Add(this.deleteQuestion_button);
            this.Controls.Add(this.editQuestion_button);
            this.Controls.Add(this.editAnswer_button);
            this.Controls.Add(this.help_button);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comment_richTextBox);
            this.Controls.Add(this.listAnsrwers_comboBox);
            this.Controls.Add(this.questionType_comboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.addQuestion_button);
            this.Controls.Add(this.deleteSelect_button);
            this.Controls.Add(this.addAnswer_button);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.trueness_checkBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.response_richTextBox);
            this.Controls.Add(this.question_richTextBox);
            this.Controls.Add(this.nameTest_textBox);
            this.Controls.Add(this.editForm_treeView);
            this.Name = "EditForm";
            this.Text = "EditForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void EditForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            throw new System.NotImplementedException();
        }



        #endregion

        private System.Windows.Forms.TreeView editForm_treeView;
        private System.Windows.Forms.TextBox nameTest_textBox;
        private System.Windows.Forms.RichTextBox question_richTextBox;
        private System.Windows.Forms.RichTextBox response_richTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox trueness_checkBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button addAnswer_button;
        private System.Windows.Forms.Button deleteSelect_button;
        private System.Windows.Forms.Button addQuestion_button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox questionType_comboBox;
        private System.Windows.Forms.ComboBox listAnsrwers_comboBox;
        private System.Windows.Forms.RichTextBox comment_richTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button help_button;
        private System.Windows.Forms.Button editAnswer_button;
        private System.Windows.Forms.Button editQuestion_button;
        private System.Windows.Forms.Button deleteQuestion_button;
    }
}