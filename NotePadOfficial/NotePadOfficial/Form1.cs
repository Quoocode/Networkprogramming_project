using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePadOfficial
{
    public partial class Form1 : Form
    {
        private bool fileAlreadySaved;
        private bool fileUpdated;
        private string currentfilename;
        public Form1()
        {
            InitializeComponent();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savefile();
        }

        private void savefile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Files (*rtf)|*.rtf";
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(saveFileDialog.FileName) == ".txt")
                {
                    MainRichTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                }
                if (Path.GetExtension(saveFileDialog.FileName) == ".rtf")
                {
                    MainRichTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                }
                this.Text = Path.GetFileName(saveFileDialog.FileName) + " - Notepad";
            }
            fileAlreadySaved = true;
            fileUpdated = false;
            currentfilename = saveFileDialog.FileName;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Files (*rtf)|*.rtf";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog.FileName) == ".txt")
                {
                    MainRichTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                }
                if (Path.GetExtension(openFileDialog.FileName) == ".rtf")
                {
                    MainRichTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
                }
                this.Text = Path.GetFileName(openFileDialog.FileName) + " - Notepad";
            }
            fileAlreadySaved = true;
            fileUpdated = false;
            currentfilename = openFileDialog.FileName;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileAlreadySaved)
            {
                if (Path.GetExtension(currentfilename) == ".txt")
                {
                    MainRichTextBox.SaveFile(currentfilename, RichTextBoxStreamType.PlainText);
                }
                if (Path.GetExtension(currentfilename) == ".rtf")
                {
                    MainRichTextBox.SaveFile(currentfilename, RichTextBoxStreamType.RichText);
                }
                fileUpdated = false;
            }
            else
            {
                if (fileUpdated)
                {
                    savefile();
                }
                else
                {
                    MainRichTextBox.Clear();
                    fileUpdated = false;
                    this.Text = "Notepad";
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fileAlreadySaved = false;
            fileUpdated = false;
            currentfilename = "";
        }

        private void MainRichTextBox_TextChanged(object sender, EventArgs e)
        {
            fileUpdated = true;
        }
    }
}
