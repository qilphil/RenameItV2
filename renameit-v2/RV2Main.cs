using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using renameit_v2.classes;
namespace renameit_v2
{
    public partial class RV2Main : Form
    {
        public FileList fList { get; set; }
        public RuleList currentRules { get; set; }
        private string lastOpenDir { get; set; }
        public RV2Main()
        {
            InitializeComponent();
            currentRules = new RuleList();
            fList = new FileList();
            lastOpenDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            updateRuleList();
        }

        private void lvFiles_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] addedFileList = (string[])e.Data.GetData(DataFormats.FileDrop);
                addFileList(addedFileList);
            }
        }

        private void updateRuleList()
        {
            lvRules.Items.Clear();
            foreach (BaseRule iRule in currentRules)
            {
                lvRules.Items.Add(iRule.ToString());
                if (!iRule.IsValid())
                {
                    ListViewItem newRuleItem = lvRules.Items[lvRules.Items.Count - 1];
                    newRuleItem.ForeColor = Color.FromName("Red");
                }
            }
        }

        private void updateFileList()
        {
            this.SuspendLayout();
            lvFiles.Items.Clear();
            List<ListViewItem> renderList = new List<ListViewItem>();

            foreach (ListFile iFile in fList)
            {
                string shortFilename = Path.GetFileName(iFile.FileName);
                string[] fileinit = { shortFilename, Path.GetFileName(currentRules.apply(iFile)) };
                renderList.Add(new ListViewItem(fileinit));
            }
            ListViewItem[] renderArray = renderList.ToArray();
            lvFiles.Items.AddRange(renderArray);
            lvFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            this.ResumeLayout();
        }

        private void lvFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void tsClearAll_Click(object sender, System.EventArgs e)
        {
            fList.Clear();
            updateFileList();
        }

        private void lvRules_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int editIndex = lvRules.SelectedIndices[0];
            if (currentRules.Count > editIndex)
                this.editRule(editIndex);
        }

        private void editRule(int pRuleIndex)
        {
            using (ruleForm editRuleForm = new ruleForm())
            {
                editRuleForm.setRule(currentRules[pRuleIndex]);

                DialogResult dResult = editRuleForm.ShowDialog(this);

                if (dResult == DialogResult.OK)
                {
                    currentRules[pRuleIndex] = editRuleForm.editRule.clone();
                }
                updateRuleList();
                updateFileList();
            }
        }

        private void toolStripButton3_Click(object sender, System.EventArgs e)
        {
            currentRules.Add(new ReplaceRule { fromStr = "from", toStr = "to" });
            editRule(currentRules.Count - 1);
        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
            int delIndex = lvRules.SelectedIndices[0];
            if (delIndex >= 0 && delIndex < lvRules.Items.Count)
            {
                currentRules.RemoveAt(delIndex);
                updateRuleList();
                updateFileList();
            }
        }

        private void toolStripButton2_Click(object sender, System.EventArgs e)
        {
            currentRules.Add(new RegExRule { fromStr = "jpe?g", toStr = "JPG" });
            editRule(currentRules.Count - 1);
        }

        private void toolStripButton4_Click(object sender, System.EventArgs e)
        {

        }
        private void addFileList(string[] pAddFiles)
        {
            foreach (string filename in pAddFiles)
                fList.Add(new ListFile { FileName = filename });
            lastOpenDir = Path.GetDirectoryName(pAddFiles[pAddFiles.Length - 1]);
            updateRuleList();
            updateFileList();

        }

        private void toolStripButton6_Click(object sender, System.EventArgs e)
        {
            openFileDialog1.InitialDirectory = lastOpenDir;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                addFileList((string[])openFileDialog1.FileNames);

            }
        }

        private void LvRules_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }
    }
}
