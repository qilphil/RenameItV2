using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace renameit_v2.classes
{
    [Serializable]
    class RegExRule : baseRule
    {
        private String mFromStr;
        private Regex fromRegEx;

        private string regexError;

        public override baseRule clone()
        {
            return this.DeepClone();
        }
        public RegExRule(RegExRule pRegexRule)
            : base(pRegexRule)
        {
        }
        public RegExRule()
        {
        }
        
        public bool caseSensitive
        {
            set
            {
                ruleProps["mCaseSensitive"] = value ? "true" : "false";
                mkRegEx();
            }
            get
            {
                return ruleProps.ContainsKey("mCaseSensitive") ? ruleProps["mCaseSensitive"] == "true" : false;
            }
        }

        public override String fromStr
        {
            set
            {
                mFromStr = value;
                mkRegEx();
            }
            get
            {
                return mFromStr;
            }
        }

        private void mkRegEx()
        {
            regexError = "";
            try
            {
                fromRegEx = new Regex(mFromStr, RegexOptions.Compiled | (ruleProps.ContainsKey("mCaseSensitive") && ruleProps["mCaseSensitive"] == "true" ? 0 : RegexOptions.IgnoreCase));
            }
            catch (Exception e)
            {
                fromRegEx = null;
                regexError = e.Message;
            }
        }

        public override void setupForm(ruleForm pRuleForm)
        {
            base.setupForm(pRuleForm);
            
            addControl(new Label()
            {
                Location = new System.Drawing.Point(40, 140),
                Text = "Case Sensitive",
                AutoSize = true
            }, "lbCaseSensitive", pRuleForm);

            addControl( new Label()
                    {
                        Location = new System.Drawing.Point(20, 170),
                        Text = "",
                        AutoSize = true
                    }, "lbRegexError", pRuleForm);


            addControl( new CheckBox()
                {
                    Location = new System.Drawing.Point(20, 150)
                }, "cbCaseSensitive", pRuleForm);
            

            // set current Values

            ((CheckBox)pRuleForm.ruleControls["cbCaseSensitive"]).CheckState = caseSensitive ? CheckState.Checked : CheckState.Unchecked;
            mkRegEx();
            ((Label)pRuleForm.ruleControls["lbRegexError"]).Text = regexError;
        }
        public override bool isValid() {
            return !(fromRegEx == null);
        }
        public override string apply(string filename)
        {
            if (fromRegEx == null)
                mkRegEx();
            return (fromRegEx == null) ? filename : fromRegEx.Replace(filename, toStr);
        }
        public override void saveData(ruleForm ruleForm)
        {
            base.saveData(ruleForm);
            caseSensitive = ((CheckBox)ruleForm.ruleControls["cbCaseSensitive"]).CheckState == CheckState.Checked;
        }
        public override string ToString()
        {
            string caseStateStr = caseSensitive ? "On" : "Off";
            return string.Format("Regular Expression - From: {0} To: {1} Case: {2}", fromStr, toStr, caseStateStr);
        }
    }
}
