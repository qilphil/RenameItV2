using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using renameit_v2.classes;
namespace renameit_v2
{
    public partial class ruleForm : Form
    {
        public Dictionary<string, Control> ruleControls = new Dictionary<string, Control>();
        private baseRule mEditRule;
        public baseRule editRule { get; set; }

        public void addNamedControl(string pControlName, Control pControl)
        {
            ruleControls[pControlName] = pControl;
            pControl.Parent = gbRule;
            Application.DoEvents();
            gbRule.Refresh();
        }

        public void hideControls()
        {
            foreach (string iControlName in ruleControls.Keys)
            {
                ruleControls[iControlName].Hide();
            }
        }

        public ruleForm()
        {
            InitializeComponent();
        }

        public void setRule(baseRule pRule)
        {
            editRule = pRule.clone();
            pRule.setupForm(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            editRule.fromStr = tbFrom.Text;
            editRule.toStr = tbTo.Text;
        }

        private void ruleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            editRule.saveData(this);
        }


    }
}
