using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using renameit_v2_wpf.classes;
using System.IO;
using System;
using System.Xml.Serialization;
using System.Windows.Threading;
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
        DispatcherTimer dispatcherTimer;


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            FileWrite(defFilePath);
        }
        private void FileLoad(string pLoadFile)
        {
            isLoading = true;
            if (File.Exists(pLoadFile))
            {
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(ruleList));
                System.IO.StreamReader file = new System.IO.StreamReader(pLoadFile);
                ruleList newRules = (ruleList)reader.Deserialize(file);
                foreach (baseRule iRule in newRules)
                {
                    if (iRule.extraControl != null)
                    {
                        iRule.extraControl.myWindow = this;
                    }
                };
                currentRules = newRules;

            }
            isLoading = false;
            sbiLoadSave.Content = "Loaded";

        }
        private void FileWrite(string pWriteFile)
        {
            if (!isLoading && Directory.Exists(Path.GetDirectoryName(pWriteFile)))
            {
                FileStream writeStream = null;
                System.IO.StreamWriter fileWriter = null;
                try
                {
                    dispatcherTimer.Stop(); 
                    writeStream = new FileStream(pWriteFile, FileMode.Create, FileAccess.Write, FileShare.None);
                    System.Xml.Serialization.XmlSerializer writer =
                    new System.Xml.Serialization.XmlSerializer(typeof(ruleList));
                    
                    fileWriter = new System.IO.StreamWriter(writeStream);
                    writer.Serialize(fileWriter, currentRules);
                    sbiLoadSave.Content = "Saved " + DateTime.Now.ToString();
                    
                }
                catch (Exception exc)
                {
                    sbiLoadSave.Content = "Save Error " + exc.Message;
                    lblError.Content = exc.ToString();
                }
                finally
                {
                    //if (writeStream != null) writeStream.Close();
                    if (fileWriter != null) fileWriter.Close();
                }
                

            }
        }
        private bool isLoading = false;
        public MainWindow()
        {
            InitializeComponent();
            string UserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RenameItV2");
            if (!Directory.Exists(UserPath))
            {
                Directory.CreateDirectory(UserPath);
            }
            defFilePath = System.IO.Path.Combine(UserPath, defFileName);
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Stop();
            currentRules = new ruleList();
            FileLoad(defFilePath);

            fList = new fileList();

            lastOpenDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            lbRules.ItemsSource = currentRules;
            lvFiles.ItemsSource = fList;
            gridRules.DataContext = currentRules;
        }

        private void addFileList(string[] pAddFiles)
        {
            int total = pAddFiles.Length;
            int added = 0, ignored = 0;
            foreach (string iFilename in pAddFiles)
                if (!fList.ContainsFileName(iFilename))
                {
                    fList.Add(new listFile { fileName = iFilename });
                    added++;
                }
                else {
                    ignored++;
                }

            
            lastOpenDir = System.IO.Path.GetDirectoryName(pAddFiles[pAddFiles.Length - 1]);
            sbiAddedMsg.Content = String.Format("{0} files added - {1} files ignored",added,ignored);
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
            dispatcherTimer.Stop();
            dispatcherTimer.Start();

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
            if (lbRules.SelectedItem!=null && ((baseRule)lbRules.SelectedItem).extraControl != null)
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

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FileWrite(defFilePath);
        }
    }
}
