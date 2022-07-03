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
        string FilePathTo = @"C:\Program Files\";
        string shortcutkey;

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
                System.Windows.MessageBox.Show($"You selected '{cofd.FileName}'");

                FilePath.Text = cofd.FileName;
                FilePathTo = @"C:\Program Files\" + System.IO.Path.GetFileName(FilePath.Text);
                FilePath_To.Text = FilePathTo;
            }
        }

        public void Run(object sender, RoutedEventArgs e)
        {
            //インストール元と先ファイルがあるか確認。
            if(FilePath.Text == null | FilePath.Text == "Where program File")
            {
                errorWindow();
            }
            else
            {
                //デスクトップショートカットを作成するかを確認
                if (DesktopShortcut1.IsChecked == true)
                {
                    shortcutkey = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) +@"\" + System.IO.Path.GetFileNameWithoutExtension(FilePath_To.Text)+ ".lnk";
                    CreateShortCut(shortcutkey);
                }
                if(Sing_up_at_start_menu1.IsChecked == true)
                {
                    SingupStartMenu();
                }
                FileSystem.CopyDirectory(FilePath.Text, FilePath_To.Text, true);
                System.Windows.MessageBox.Show("Done!");
            }
        }

        public void errorWindow()
        {
            //ファイルが選択されていません。の画面を表示
            System.Windows.MessageBox.Show("You forget to select a file");
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            Run(sender, e);
        }

        public void CreateShortCut(string shortcutPath)
        {
            //https://cammy.co.jp/technical/c_shortcut/
            // ショートカットそのもののパスは呼び出し側で指定

            // ショートカットのリンク先(起動するプログラムのパス)
            //string targetPath = Application.ExecutablePath;
            string targetPath = FilePath_To.Text;

            // WshShellを作成
            Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8"));
            dynamic shell = Activator.CreateInstance(t);

            //WshShortcutを作成
            var shortcut = shell.CreateShortcut(shortcutPath);

            //リンク先
            shortcut.TargetPath = targetPath;
            //アイコンのパス
            shortcut.IconLocation = FilePath.Text + ",0";

            // 引数
            //shortcut.Arguments = "/a /b /c";
            // 作業フォルダ
            //shortcut.WorkingDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
            // 実行時の大きさ 1が通常、3が最大化、7が最小化
            shortcut.WindowStyle = 1;
            // コメント
            //shortcut.Description = "テストのアプリケーション";

            //ショートカットを作成
            shortcut.Save();

            //後始末
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shortcut);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell);
        }


        public void SingupStartMenu()
        {
            shortcutkey = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\" + System.IO.Path.GetFileNameWithoutExtension(FilePath_To.Text) + ".lnk";
            CreateShortCut(shortcutkey);
        }


        private void FileSelectButtonTo_Click(object sender, RoutedEventArgs e)
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
                System.Windows.MessageBox.Show($"You selected '{cofd.FileName}'");

                FilePath_To.Text = cofd.FileName + @"\" + System.IO.Path.GetFileName(FilePath.Text);
                FilePathTo = cofd.FileName + System.IO.Path.GetFileName(FilePath.Text);
            }
        }
    }
}
