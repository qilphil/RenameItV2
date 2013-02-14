using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Xml.Serialization;
namespace renameit_v2_wpf.rules
{

    [Serializable]
    public class baseRule : INotifyPropertyChanged
    {
        protected string fromStrValue;
        protected string toStrValue;

        public event PropertyChangedEventHandler PropertyChanged;

        [XmlIgnore]
        public RuleForm extraControl;

        [XmlIgnore]
        public bool hasErrorValue;

        public virtual bool hasError
        {
            get
            {
                return this.hasErrorValue;
            }
            set
            {
                if (this.hasErrorValue != value)
                {
                    this.hasErrorValue = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public virtual string fromStr
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
                    NotifyPropertyChanged();
                }
            }
        }

        public virtual string toStr
        {
            get
            {
                return this.toStrValue;
            }
            set
            {
                if (this.toStrValue != value)
                {
                    this.toStrValue = value;
                    this.NotifyPropertyChanged();
                }
            }
        }


        public virtual string apply(string filename) { return filename; }
        public override string ToString() { return "Virtual Rule"; }

        public virtual baseRule clone()
        {
            return this.DeepClone();
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public baseRule(baseRule pBaseRule)
            : base()
        {

        }

        public baseRule()
        {

        }



        protected void addControl(Control pNewControl, string pControlName)
        {
            /*   if (!pRuleForm.ruleControls.ContainsKey(pControlName))
               {
                   pRuleForm.addNamedControl(pControlName, pNewControl);

               }
               else
               {
                   Control existingControl = (Label)pRuleForm.ruleControls[pControlName];
                   existingControl.Show();
               }*/
        }
        public static Dictionary<System.Type, string> nameList = new Dictionary<System.Type, string>() {
            { typeof(ReplaceRule), "Einfaches Ersetzen" },
            { typeof(RegExRule), "Regular Expression" },
        };
        public virtual bool isValid()
        {
            return true;
        }


        public virtual void saveData(/*ruleForm ruleForm*/)
        {

        }
    }


}
