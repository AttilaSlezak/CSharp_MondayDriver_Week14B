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
        public MondayDriver()
        {
            InitializeComponent();
        }

        private void MondayDriver_Load(object sender, EventArgs e)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo oneDrive in allDrives)
            {
                driveComboBox.Items.Add(oneDrive.Name.ToString() + " (" + oneDrive.DriveType.ToString() + ")");
            }
            driveComboBox.SelectedIndex = 0;
            pathTextBox.Text = allDrives[driveComboBox.SelectedIndex].RootDirectory.ToString();

            DirectoryInfo[] allDirectories = allDrives[driveComboBox.SelectedIndex].RootDirectory.GetDirectories();
            
            foreach (DirectoryInfo oneDirectory in allDirectories)
            {
                directoryListBox.Items.Add(oneDirectory.FullName.ToString() + "\\");
            }

            FileInfo[] allFiles = allDrives[driveComboBox.SelectedIndex].RootDirectory.GetFiles();

            foreach (FileInfo oneFile in allFiles)
            {
                directoryListBox.Items.Add(oneFile.FullName.ToString());
            }
        }
    }
}
