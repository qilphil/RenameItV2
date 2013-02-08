using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using renameit_v2_wpf.classes;

namespace renameit_v2_wpf
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public fileList fList { get; set; }
        public ruleList currentRules { get; set; }
        private string lastOpenDir { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            
            currentRules = new ruleList();
            fList = new fileList();
            lastOpenDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            lbRules.ItemsSource = currentRules;
            lvFiles.ItemsSource = fList;

        }

        private void addFileList(string[] pAddFiles)
        {
            foreach (string filename in pAddFiles)
                fList.Add(new listFile { fileName = filename });
            lastOpenDir = System.IO.Path.GetDirectoryName(pAddFiles[pAddFiles.Length - 1]);
            updateFileList();
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }

            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }
        private void updateFileList()
        {
            var d = Dispatcher.DisableProcessing();
            
            foreach (listFile iFile in fList)
            {
                currentRules.apply(iFile);
            }
            
            d.Dispose();
            
        }

        private void lvFiles_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] addedFileList = (string[])e.Data.GetData(DataFormats.FileDrop);
                addFileList(addedFileList);
            }
        }

        private void but_addRule_Click(object sender, RoutedEventArgs e)
        {
            currentRules.Add(new ReplaceRule() { fromStr = "jpg", toStr = "gif" });
            updateFileList();
        }

      
        private void lbRules_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void but_clear_Click(object sender, RoutedEventArgs e)
        {
            currentRules.Clear();
            updateFileList();
        }


    }
   
}
