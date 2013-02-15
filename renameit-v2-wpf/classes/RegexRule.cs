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
namespace renameit_v2_wpf.rules
{
    [Serializable]
    [XmlInclude(typeof(baseRule))]
   public class RegExRule : baseRule
    {
        private Regex fromRegEx;

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
        public override string fromStr
        {
            get
            {
                return this.fromStrValue;
            }
            set
            {
                if (this.fromStrValue != value)
                {
                    this.fromStrValue = value;
                    mkRegEx();
                    NotifyPropertyChanged();
                }
                if (value == String.Empty)
                    hasError = true;
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
                    mkRegEx();
                    NotifyPropertyChanged();
                }
            }
        }


        private void mkRegEx()
        {
            Label rexError = ((renameit_v2_wpf.RuleFormRegEx)extraControl).lblRegExError;
            bool regExHasError = false;  
            try
            {
                fromRegEx = new Regex(fromStrValue, RegexOptions.Compiled | ((bool)caseSensitive ? 0 : RegexOptions.IgnoreCase));
                rexError.Content = string.Empty;
            }
            catch (Exception e)
            {
                regExHasError = true;
                fromRegEx = null;
                rexError.Content = e.Message;
            }
            hasError = regExHasError;
        }
   
        public override bool isValid()
        {
            return !(fromRegEx == null);
        }
        public override string apply(string filename)
        {

            return (fromRegEx == null) ? filename : fromRegEx.Replace(filename, toStr);
        }
        public override void saveData(/*ruleForm ruleForm*/)
        {
        }
        public override string ToString()
        {
            string caseStateStr = (bool)caseSensitive ? "On" : "Off";
            return string.Format("Regular Expression - From: {0} To: {1} Case: {2}", fromStr, toStr, caseStateStr);
        }
    }
}
