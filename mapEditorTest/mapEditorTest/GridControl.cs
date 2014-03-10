using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mapEditorTest
{
    public partial class GridControl : Form
    {
        public GridControl(){
            InitializeComponent();
        }

        //read a map text file into mapeditor
        private void loadBtn_Click(object sender, System.EventArgs e) {
            rowBox.Text = ""+(int)' ';
        }

        //write map to a text file
        private void saveBtn_Click(object sender, System.EventArgs e) {
            
        }

        //clears mapeditor
        private void newBtn_Click(object sender, System.EventArgs e) {
            
        }
    }
}
