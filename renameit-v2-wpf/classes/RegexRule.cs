using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace renameit_v2_wpf.rules
{
    [Serializable]
    [XmlInclude(type: typeof(BaseRule))]
   public class RegExRule : BaseRule
    {
        private Regex fromRegEx;

        public override BaseRule clone() => this.DeepClone();

        public RegExRule(RegExRule pRegexRule)
            : base(pRegexRule)
        {


        }

        public RegExRule(MainWindow pMainWindow)
        {
            extraControl = new RuleFormRegEx() { myWindow = pMainWindow };
        }

        public RegExRule()
        {
            extraControl = new RuleFormRegEx();
        }

        public override string fromStr
        {
            get
            {
                return fromStrValue;
            }
            set
            {
                if (fromStrValue != value)
                {
                    fromStrValue = value;
                    MkRegEx();
                    NotifyPropertyChanged();
                }
                if (value?.Length == 0)
                    hasError = true;
            }
        }
        private bool? caseSensitiveValue;

        public bool? CaseSensitive
        {
            get
            {
                return caseSensitiveValue;
            }
            set
            {
                if (caseSensitiveValue != value)
                {
                    caseSensitiveValue = value;
                    MkRegEx();
                    NotifyPropertyChanged();
                }
            }
        }


        private void MkRegEx()
        {
            Label rexError = ((RuleFormRegEx)extraControl).lblRegExError;
            bool regExHasError = false;
            try
            {
                fromRegEx = new Regex(fromStrValue, RegexOptions.Compiled | ((bool)CaseSensitive ? 0 : RegexOptions.IgnoreCase));
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

        public override bool isValid() => fromRegEx != null;
        public override string Apply(string filename)
        {

            return (fromRegEx == null) ? filename : fromRegEx.Replace(filename, toStr);
        }

        public override void SaveData(/*ruleForm ruleForm*/)
        {
        }

        public override string ToString()
        {
            string caseStateStr = (bool)CaseSensitive ? "On" : "Off";
            return $"Regular Expression - From: {fromStr} To: {toStr} Case: {caseStateStr}";
        }
    }
}
