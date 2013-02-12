using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using renameit_v2_wpf.classes;
using System.IO;
using System;
using System.Xml.Serialization;

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
        private string defFileName = "default.xml";
        private string defFilePath;
        private void FileLoad(string pLoadFile)
        {
            isLoading = true;
            if (File.Exists(pLoadFile))
            {
                System.Xml.Serialization.XmlSerializer reader =  new System.Xml.Serialization.XmlSerializer(typeof(ruleList));
                System.IO.StreamReader file = new System.IO.StreamReader(pLoadFile);
                currentRules = (ruleList) reader.Deserialize(file);
                foreach(baseRule iRule in currentRules) {
                    if (iRule.extraControl != null) {
                        iRule.extraControl.myWindow = this;
                    }
                }
            }
            isLoading = false;
        }
        private void FileWrite(string pWriteFile)
        {
            if (!isLoading && Directory.Exists(Path.GetDirectoryName(pWriteFile)))
            {
                isLoading = true;
                System.Xml.Serialization.XmlSerializer writer =
                    new System.Xml.Serialization.XmlSerializer(typeof(ruleList));

                System.IO.StreamWriter file = new System.IO.StreamWriter( pWriteFile);
                writer.Serialize(file, currentRules);
                file.Close();
                isLoading = false;
            }
        }
        private bool isLoading = false;
        public MainWindow()
        {
            InitializeComponent();
            string UserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"RenameItV2");
            if (!Directory.Exists(UserPath))
            {
                Directory.CreateDirectory(UserPath);
            }
            defFilePath = System.IO.Path.Combine(UserPath, defFileName);
            FileLoad(defFilePath);
            
            currentRules = new ruleList();
            fList = new fileList();
            lastOpenDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            lbRules.ItemsSource = currentRules;

            lvFiles.ItemsSource = fList;
            gridRules.DataContext = currentRules;
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
        public void updateFileList()
        {
            var d = Dispatcher.DisableProcessing();

            foreach (listFile iFile in fList)
            {
                currentRules.apply(iFile);
            }

            d.Dispose();
            lvFiles.Items.Refresh();
            lbRules.Items.Refresh();
            FileWrite(defFilePath);
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
            currentRules.Add(new ReplaceRule(this) { fromStr = "jpg", toStr = "gif" });
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

        private void tbToStr_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateFileList();
        }

        private void but_addRegExRule_Click(object sender, RoutedEventArgs e)
        {
            currentRules.Add(new RegExRule(this) { fromStr = "jpg", toStr = "gif", caseSensitive = true });

        }



        private void lbRules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((baseRule)lbRules.SelectedItem).extraControl != null)
            {
                gridExtraControl.Children.Clear();
                gridExtraControl.Children.Add(((baseRule)lbRules.SelectedItem).extraControl);
            }
            else
            {
                if (gridExtraControl.Children.Count > 0)
                {
                    gridExtraControl.Children.Clear();
                }
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            updateFileList();
        }
    }
}
