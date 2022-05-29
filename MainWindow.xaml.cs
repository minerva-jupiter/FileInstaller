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
        bool isCreateDesktopShortcut = false;
        bool isSingupStartMenu = false;


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

                FilePathFrom = cofd.FileName;
                FilePath.Text = cofd.FileName;
                FilePathTo = @"C:\Program Files\" + System.IO.Path.GetFileName(FilePathFrom);
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
                //デスクトップショートカットを作成するかを確認
                if (isCreateDesktopShortcut == true)
                {
                    CreateShortCut();
                }
                if(isSingupStartMenu == true)
                {
                    SingupStartMenu();
                }
                FileSystem.CopyDirectory(FilePathFrom, FilePathTo, true);
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

        public void CreateShortCut()
        {
            string fileNameFrom = System.IO.Path.GetFileName(FilePathFrom);
            //作成するショートカットのパス
            string shortcutPath = System.IO.Path.Combine(
                Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory),
                fileNameFrom + ".lnk");
            //ショートカットのリンク先
            string targetPath = FilePathTo;

            //WshShellを作成
            Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8"));
            dynamic shell = Activator.CreateInstance(t);

            //WshShortcutを作成
            var shortcut = shell.CreateShortcut(shortcutPath);

            //リンク先
            shortcut.TargetPath = targetPath;
            //アイコンのパス
            shortcut.IconLocation = shortcutPath + ",0";
            //その他のプロパティも同様に設定できるため、省略

            //ショートカットを作成
            shortcut.Save();

            //後始末
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shortcut);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell);
        }

        private void DesktopShortcut_Checked(object sender, RoutedEventArgs e)
        {
            isCreateDesktopShortcut = true;
        }

        public void SingupStartMenu()
        {
            string fileNameFrom = System.IO.Path.GetFileName(FilePathFrom);
            //作成するショートカットのパス
            string shortcutPath = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\" + fileNameFrom + ".lnk";
            //ショートカットのリンク先
            string targetPath = FilePathTo;

            //WshShellを作成
            Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8"));
            dynamic shell = Activator.CreateInstance(t);

            //WshShortcutを作成
            var shortcut = shell.CreateShortcut(shortcutPath);

            //リンク先
            shortcut.TargetPath = targetPath;
            //アイコンのパス
            shortcut.IconLocation = shortcutPath + ",0";
            // ③作業フォルダ
            shortcut.WorkingDirectory = FilePathTo;
            //その他のプロパティも同様に設定できるため、省略

            //ショートカットを作成
            shortcut.Save();

            //後始末
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shortcut);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell);
        }

        private void Sing_up_at_start_menu_Checked(object sender, RoutedEventArgs e)
        {
            isSingupStartMenu = true;
        }
    }
}
