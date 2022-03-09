using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FileInstaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //変数宣言
        string FilePathFrom;
        string FilePathTo;
        string FileNameTo;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FileSelectButtonFrom_Click(object sender, RoutedEventArgs e)
        {
            var fileSelectDialog = new CommonOpenFileDialog
            {
                Title = "SelectFile",
                // フォルダ選択ダイアログの場合は true
                IsFolderPicker = true,
                // ダイアログが表示されたときの初期ディレクトリを指定
                InitialDirectory = "適当なパス",
            };
            FilePathFrom = fileSelectDialog.FileName;
        }

        private void FileSelectTo_Click(object sender, RoutedEventArgs e)
        {
            var fileSelectDialog = new CommonOpenFileDialog
            {
                Title = "SelectFile",
                IsFolderPicker = true,
                InitialDirectory = "C:|Program Files"
            };
            FilePathTo = fileSelectDialog.FileName;
        }
        public void Run(object sender, RoutedEventArgs e)
        {
            if(FilePathFrom == null | FilePathTo == null)
            {
                errorWindow();
            }
            else
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"C:\Program Files");
                System.IO.DirectoryInfo[] subFolders = di.GetDirectories("*", System.IO.SearchOption.AllDirectories);
                FileNameTo = System.IO.Path.GetFileName(FilePathTo);
                int index1 = Array.IndexOf(subFolders, FileNameTo);
                if (index1 == -1)
                {
                    Directory.CreateDirectory(FilePathTo);
                }
                System.IO.File.Copy(FilePathFrom, FileNameTo, true);
            }
        }

        public void errorWindow()
        {
            Warning WarningWindow= new Warning();
            WarningWindow.Show();
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            Run(sender, e);
        }
    }
}
