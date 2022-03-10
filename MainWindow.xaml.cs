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
using Microsoft.VisualBasic.FileIO;

namespace FileInstaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //変数宣言
        string FilePathFrom;
        string FilePathTo = @"C:\Program Files\";


        public MainWindow()
        {
            InitializeComponent();
        }

        private void FileSelectButtonFrom_Click(object sender, RoutedEventArgs e)
        {
            using (var cofd = new CommonOpenFileDialog()
            {
                Title = "フォルダを選択してください",
                InitialDirectory = @"C:\Program Files\",
                // フォルダ選択モードにする
                IsFolderPicker = true,
            })
            {
                if (cofd.ShowDialog() != CommonFileDialogResult.Ok)
                {
                    return;
                }

                // FileNameで選択されたフォルダを取得する
                System.Windows.MessageBox.Show($"{cofd.FileName}を選択しました");

                FilePathFrom = cofd.FileName;
                FilePath.Text = cofd.FileName;
                FilePathTo = @"C:\Program Files\" + System.IO.Path.GetFileName(FilePathFrom);
                System.Windows.MessageBox.Show("To" + FilePathTo + "From" + FilePathFrom);
            }
        }

        public void Run(object sender, RoutedEventArgs e)
        {
            //インストール元と先ファイルがあるか確認。
            if(FilePathFrom == null)
            {
                errorWindow();
            }
            else
            {
                FileSystem.CopyDirectory(FilePathFrom, FilePathTo, true);
            }
        }

        public void errorWindow()
        {
            //ファイルが選択されていません。の画面を表示
            Warning WarningWindow= new Warning();
            WarningWindow.Show();
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            Run(sender, e);
        }
    }
}
