using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FileSelectButton_Click(object sender, RoutedEventArgs e)
        {
            var fileSelectDialog = new CommonOpenFileDialog
            {
                Title = "SelectFile",
                // フォルダ選択ダイアログの場合は true
                IsFolderPicker = true,
                // ダイアログが表示されたときの初期ディレクトリを指定
                InitialDirectory = "適当なパス",
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileSelectDialog = new CommonOpenFileDialog
            {
                Title = "SelectFile",
                IsFolderPicker = true,
                InitialDirectory = "C:/Program Files"
            };
        }
    }
}
