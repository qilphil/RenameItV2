using renameit_v2_wpf.rules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace renameit_v2_wpf
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FileList fList { get; set; }
        public RuleList CurrentRules { get; set; }
        private string LastOpenDir { get; set; }
        private string defFileName = "default.xml";
        private string defFilePath;
        readonly DispatcherTimer dispatcherTimer;


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            FileWrite(defFilePath);
        }

        private void FileLoad(string pLoadFile)
        {
            isLoading = true;
            if (File.Exists(pLoadFile))
            {
                var reader = new XmlSerializer(typeof(RuleList));
                var file = new StreamReader(pLoadFile);
                var newRules = (RuleList)reader.Deserialize(file);
                file.Close();

                foreach (BaseRule iRule in newRules)
                {
                    if (iRule.extraControl != null)
                    {
                        iRule.extraControl.myWindow = this;
                    }
                };
                CurrentRules = newRules;

            }
            isLoading = false;
            sbiLoadSave.Content = "Loaded";

        }
        private void FileWrite(string pWriteFile)
        {
            if (!isLoading && Directory.Exists(Path.GetDirectoryName(pWriteFile)))
            {
                StreamWriter fileWriter = null;
                try
                {
                    dispatcherTimer.Stop();
                    FileStream writeStream = new FileStream(pWriteFile, FileMode.Create, FileAccess.Write, FileShare.None);
                    fileWriter = new StreamWriter(writeStream);
                    var writer = new XmlSerializer(typeof(RuleList));
                    writer.Serialize(fileWriter, CurrentRules);
                    sbiLoadSave.Content = "Saved " + DateTime.Now.ToString();

                }
                catch (Exception exc)
                {
                    sbiLoadSave.Content = "Save Error " + exc.Message;

                }
                finally
                {
                    //if (writeStream != null) writeStream.Close();
                    fileWriter?.Close();
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
            defFilePath = Path.Combine(UserPath, defFileName);
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Stop();
            CurrentRules = new RuleList();
            FileLoad(defFilePath);

            fList = new FileList();

            LastOpenDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            lbRules.ItemsSource = CurrentRules;
            lvFiles.ItemsSource = fList;
            gridRules.DataContext = CurrentRules;
        }

        private void AddFileList(string[] pAddFiles)
        {
            int total = pAddFiles.Length;
            int added = 0, ignored = 0;
            foreach (string iFilename in pAddFiles)
                if (!fList.ContainsFileName(iFilename))
                {
                    fList.Add(new ListFile { FileName = iFilename });
                    added++;
                }
                else
                {
                    ignored++;
                }


            LastOpenDir = Path.GetDirectoryName(pAddFiles[pAddFiles.Length - 1]);
            sbiAddedMsg.Content = $"{added} files added - {ignored} files ignored";
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

            if (toolBar.Template.FindName("MainPanelBorder", toolBar) is FrameworkElement mainPanelBorder)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }

        public void updateFileList()
        {
            var d = Dispatcher.DisableProcessing();

            fList.apply(CurrentRules);

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
                var addedFileList = e.Data.GetData(DataFormats.FileDrop) as string[];
                AddFileList(addedFileList);
            }
        }

        private void but_addRule_Click(object sender, RoutedEventArgs e)
        {
            CurrentRules.Add(new ReplaceRule(this) { fromStr = "", toStr = "" });
            updateFileList();
        }


        private void but_clear_Click(object sender, RoutedEventArgs e)
        {
            CurrentRules.Clear();
            updateFileList();
        }

        private void tbToStr_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateFileList();
        }

        private void but_addRegExRule_Click(object sender, RoutedEventArgs e)
        {
            CurrentRules.Add(new RegExRule(this) { fromStr = "", toStr = "", CaseSensitive = true });
        }



        private void lbRules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbRules.SelectedItem != null && (lbRules.SelectedItem as BaseRule).extraControl != null)
            {
                gridExtraControl.Children.Clear();
                gridExtraControl.Children.Add(((BaseRule)lbRules.SelectedItem).extraControl);
            }
            else if (gridExtraControl.Children.Count > 0)
            {
                gridExtraControl.Children.Clear();
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

        private void but_delRule_Click(object sender, RoutedEventArgs e)
        {
            if (lbRules.SelectedItem != null)
            {
                CurrentRules.RemoveAt(lbRules.SelectedIndex);
                updateFileList();
            }
        }

        private void but_removeFile_Click(object sender, RoutedEventArgs e)
        {
            var d = Dispatcher.DisableProcessing();
            var delFileNames = new List<string>();
            if (lvFiles.SelectedItems.Count > 0)
            {
                delFileNames.AddRange(((IList<ListFile>)lvFiles.SelectedItems).Select(delFile => delFile.FileName));
                fList.delete(delFileNames);
            }
            d.Dispose();
            sbiAddedMsg.Content = string.Format("{0} Files Removed", delFileNames.Count());

        }

        private void but_clear_Files_Click(object sender, RoutedEventArgs e)
        {
            var d = Dispatcher.DisableProcessing();
            int delCount = fList.Count();
            fList.Clear();
            d.Dispose();
            updateFileList();
            sbiAddedMsg.Content = string.Format("{0} Files Removed", delCount);
        }

        private void but_rule_up_Click(object sender, RoutedEventArgs e)
        {
            if (lbRules.SelectedItem != null)
            {
                var movingRule = (BaseRule)lbRules.SelectedItem;
                CurrentRules.MoveUp(movingRule);
                lbRules.SelectedItem = movingRule;
            }
        }

        private void but_rule_down_Click(object sender, RoutedEventArgs e)
        {
            if (lbRules.SelectedItem != null)
            {
                var movingRule = (BaseRule)lbRules.SelectedItem;
                CurrentRules.MoveDown(movingRule);
                lbRules.SelectedItem = movingRule;
            }

        }

        private void but_renameFiles_Click(object sender, RoutedEventArgs e)
        {
            int renamed = 0, failed = 0;
            foreach (ListFile renameFile in fList)
            {
                if (renameFile.DoRename?.Length == 0)
                {
                    renamed++;
                }
                else
                {
                    failed++;
                };

            };
            fList.Clear();
            sbiAddedMsg.Content = $"{renamed} Files renamed, {failed} Files failed";
        }
    }
}
