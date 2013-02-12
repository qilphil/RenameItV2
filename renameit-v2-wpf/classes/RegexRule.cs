using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Controls;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
namespace renameit_v2_wpf.classes
{
    [Serializable]
    [XmlInclude(typeof(baseRule))]
   public class RegExRule : baseRule
    {
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
        public RegExRule(MainWindow pMainWindow)
            : base()
        {
            extraControl = new renameit_v2_wpf.RuleFormRegEx() { myWindow = pMainWindow };
        }
        public RegExRule()
            : base()
        {
            extraControl = new renameit_v2_wpf.RuleFormRegEx();
        }
        public override String fromStr
        {
            get
            {
                return this.fromStrValue;
            }
            set
            {
                if (this.fromStrValue != value)
                {
                    Label rexError = ((renameit_v2_wpf.RuleFormRegEx)extraControl).lblRegExError;
                    this.fromStrValue = value;
                    rexError.Content = "";
                    try
                    {
                        Regex testRegex = new Regex(this.fromStrValue);
                        fromRegEx = testRegex;
                    }
                    catch (ArgumentException errorEx)
                    {
                        rexError.Content = errorEx.Message;
                    }
                    NotifyPropertyChanged();
                }
            }
        }
        private bool? caseSensitiveValue;
        public bool? caseSensitive
        {
            get
            {
                return this.caseSensitiveValue;
            }
            set
            {
                if (this.caseSensitiveValue != value)
                {
                    this.caseSensitiveValue = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private void mkRegEx()
        {
            regexError = "";
            try
            {
                fromRegEx = new Regex(fromStrValue, RegexOptions.Compiled | ((bool)caseSensitive ? 0 : RegexOptions.IgnoreCase));
            }
            catch (Exception e)
            {
                fromRegEx = null;
                regexError = e.Message;
            }
        }
        /* 

    public override void setupForm()
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
             },"cbCaseSensitive", pRuleForm);
            

         // set current Values

         ((CheckBox)pRuleForm.ruleControls["cbCaseSensitive"]).CheckState = caseSensitive ? CheckState.Checked : CheckState.Unchecked;
         mkRegEx();
         ((Label)pRuleForm.ruleControls["lbRegexError"]).Text = regexError;
    }
*/
        public override bool isValid()
        {
            return !(fromRegEx == null);
        }
        public override string apply(string filename)
        {
            if (fromRegEx == null)
                mkRegEx();
            return (fromRegEx == null) ? filename : fromRegEx.Replace(filename, toStr);
        }
        public override void saveData(/*ruleForm ruleForm*/)
        {
            //  base.saveData(ruleForm);
            //     caseSensitive = ((CheckBox)ruleForm.ruleControls["cbCaseSensitive"]).IsChecked ;
        }
        public override string ToString()
        {
            string caseStateStr = (bool)caseSensitive ? "On" : "Off";
            return string.Format("Regular Expression - From: {0} To: {1} Case: {2}", fromStr, toStr, caseStateStr);
        }
    }
}
