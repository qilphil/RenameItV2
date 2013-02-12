using System.Windows;
using System.Windows.Controls;

namespace renameit_v2_wpf
{
    /// <summary>
    /// Interaction logic for extRuleForms.xaml
    /// </summary>
    ///  
    public  class RuleForm :UserControl
    {
        public MainWindow myWindow;

    }
    public partial class RuleFormRegEx :RuleForm
    {
        public RuleFormRegEx()
        {
            InitializeComponent();
        }

        private void chkCaseSensitive_Checked(object sender, RoutedEventArgs e)
        {
            myWindow.updateFileList();
        }


    }
}
