using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MultiQuestions
{
    public partial class DeleteGroupForm : Form
    {
        public DeleteGroupForm()
        {
            InitializeComponent();
        }

        private void deleteGroup_button_Click(object sender, EventArgs e)
        {

            Loading.DeleteDB(DeleteGroup_textBox.Text);
            this.Close();
        }
    }
}
