using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Xml.Serialization;
namespace renameit_v2_wpf.rules
{

    [Serializable]
    public class BaseRule : INotifyPropertyChanged
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
                return hasErrorValue;
            }
            set
            {
                if (hasErrorValue != value)
                {
                    hasErrorValue = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public virtual string fromStr
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
                    hasError = (value?.Length == 0);
                    NotifyPropertyChanged();
                }
            }
        }

        public virtual string toStr
        {
            get
            {
                return toStrValue;
            }
            set
            {
                if (toStrValue != value)
                {
                    this.toStrValue = value;
                    this.NotifyPropertyChanged();
                }
            }
        }


        public virtual string Apply(string filename) { return filename; }
        public override string ToString() { return "Virtual Rule"; }

        public virtual BaseRule clone()
        {
            return this.DeepClone();
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///         Initialisiert eine neue Instanz der <see cref="object" />-Klasse.
        ///       </summary>
        /// <param name="pBaseRule"></param>
        public BaseRule(BaseRule pBaseRule)
        {

        }

        public BaseRule()
        {

        }



        protected void AddControl(Control pNewControl, string pControlName)
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
        public static Dictionary<Type, string> nameList = new Dictionary<Type, string>() {
            { typeof(ReplaceRule), "Einfaches Ersetzen" },
            { typeof(RegExRule), "Regular Expression" },
        };

        public virtual bool isValid()
        {
            return true;
        }


        public virtual void SaveData(/*ruleForm ruleForm*/)
        {

        }
    }


}
