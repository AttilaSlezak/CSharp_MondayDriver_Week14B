using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MondayDriver
{
    public partial class MondayDriver : Form
    {
        DriveInfo[] allDrives;
        string currentPath;

        public MondayDriver()
        {
            InitializeComponent();
        }

        private void MondayDriver_Load(object sender, EventArgs e)
        {
            driveComboBox.Items.Clear();
            GetAllDrives();
            driveComboBox.SelectedIndex = 0;
            GetDirectoriesAndFilesInRoot();
        }

        private void GetAllDrives()
        {
            allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo oneDrive in allDrives)
            {
                driveComboBox.Items.Add(oneDrive.Name.ToString() + " (" + oneDrive.DriveType.ToString() + ")");
            }
        }

        private void GetDirectoriesAndFilesInRoot()
        {
            directoryListBox.Items.Clear();
            DirectoryInfo[] allDirectories = allDrives[driveComboBox.SelectedIndex].RootDirectory.GetDirectories();

            foreach (DirectoryInfo oneDirectory in allDirectories)
            {
                directoryListBox.Items.Add(oneDirectory.FullName.ToString().Replace(currentPath, "") + "\\");
            }
            FileInfo[] allFiles = allDrives[driveComboBox.SelectedIndex].RootDirectory.GetFiles();

            foreach (FileInfo oneFile in allFiles)
            {
                directoryListBox.Items.Add(oneFile.FullName.ToString().Replace(currentPath, ""));
            }
        }

        private void GetDirectoriesAndFiles()
        {
            directoryListBox.Items.Clear();
            if (currentPath.Length > 3)
            {
                directoryListBox.Items.Add("..");
            }

            DirectoryInfo[] allDirectories = new DirectoryInfo(currentPath).GetDirectories();

            foreach (DirectoryInfo oneDirectory in allDirectories)
            {
                directoryListBox.Items.Add(oneDirectory.FullName.ToString().Replace(currentPath, "") + "\\");
            }

            FileInfo[] allFiles = new DirectoryInfo(currentPath).GetFiles();

            foreach (FileInfo oneFile in allFiles)
            {
                directoryListBox.Items.Add(oneFile.FullName.ToString().Replace(currentPath, ""));
            }
        }

        private void driveComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pathTextBox.Text = allDrives[driveComboBox.SelectedIndex].RootDirectory.ToString();
                currentPath = allDrives[driveComboBox.SelectedIndex].RootDirectory.ToString();
                GetDirectoriesAndFilesInRoot();
            }
            catch
            {
                MondayDriver_Load(sender, e);
            }
        }

        private void pathTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (Directory.Exists(pathTextBox.Text))
                {
                    if (pathTextBox.Text[pathTextBox.Text.Length - 1] != '\\')
                    {
                        pathTextBox.Text += "\\";
                    }
                    currentPath = pathTextBox.Text;
                    GetDirectoriesAndFiles();
                }
                else
                {
                    pathTextBox.Text = currentPath;
                    MessageBox.Show("The given path does not exist!");
                }
            }
        }

        private void pathTextBox_Leave(object sender, EventArgs e)
        {
            pathTextBox.Text = currentPath;
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedItem = directoryListBox.GetItemText(directoryListBox.SelectedItem);

                if (selectedItem == "..")
                {
                    char[] charArray = currentPath.ToCharArray();
                    charArray[charArray.Length - 1] = '1';
                    Array.Reverse(charArray);
                    int lastBackslash = Array.IndexOf(charArray, '\\');
                    currentPath = currentPath.Substring(0, currentPath.Length - lastBackslash);
                    pathTextBox.Text = currentPath;
                    GetDirectoriesAndFiles();
                }
                else if (selectedItem[selectedItem.Length - 1] == '\\')
                {
                    pathTextBox.Text = currentPath + selectedItem;
                    currentPath += selectedItem;
                    GetDirectoriesAndFiles();
                }
                else
                {
                    FileContentWindow fileContentWin = new FileContentWindow();
                    fileContentWin.Text = selectedItem;
                    for (int i = 0; i < fileContentWin.Controls.Count; i++)
                    {
                        if (fileContentWin.Controls[i].Name == "fileDisplayRichTextBox")
                        {
                            string text = File.ReadAllText(currentPath + selectedItem);
                            fileContentWin.Controls[i].Text = text;
                        }
                    }
                    fileContentWin.ShowDialog();
                }
            }
            catch
            {
                MondayDriver_Load(sender, e);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string selectedItem = directoryListBox.GetItemText(directoryListBox.SelectedItem);
            string itemType = selectedItem[selectedItem.Length - 1] == '\\' ? " directory " : " file ";
            string question = "Do you really want to delete the selected" + itemType + selectedItem + "?";

            DialogResult dialogResult = MessageBox.Show(question, "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (itemType == " directory ")
                {
                    DirectoryInfo dirInf = new DirectoryInfo(currentPath + selectedItem);
                    bool isEmpty = !Directory.EnumerateFiles(currentPath + selectedItem).Any();
                    if (isEmpty)
                    {
                        dirInf.Delete();
                    }
                    else
                    {
                        question = "The folder is not empty!\nAre you really sure you want to delete it?";
                        dialogResult = MessageBox.Show(question, "Delete", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            dirInf.Delete(true);
                        }
                    }
                }
                else if (itemType == " file ")
                {
                    FileInfo fileInf = new FileInfo(currentPath + selectedItem);
                    fileInf.Delete();
                }
                GetDirectoriesAndFiles();
            }
        }
    }
}
