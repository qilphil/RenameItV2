using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace renameit_v2_wpf.classes
{

    [Serializable]
    public class baseRule : INotifyPropertyChanged
    {
        private String fromStrValue;
        private String toStrValue;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual String fromStr
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
        public virtual String toStr
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
                    NotifyPropertyChanged();
                }
            }
        }
        

        public virtual string apply(string filename) { return filename; }
        public override string ToString() { return "Virtual Rule"; }

        public Dictionary<string, string> ruleProps;
        public virtual baseRule clone()
        {
            return this.DeepClone();
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public baseRule(baseRule pBaseRule)
            : base()
        {

        }
        public baseRule()
        {
            ruleProps = new Dictionary<string, string>();
        }

        public virtual void setupForm(ruleForm pRuleForm)
        {
            /*  pRuleForm.cbRuleType.Items.AddRange(nameList.Values.ToArray<string>());
              pRuleForm.cbRuleType.SelectedIndex = pRuleForm.cbRuleType.Items.IndexOf(nameList[this.GetType()]);
              pRuleForm.gbRule.Text = nameList[this.GetType()];
              pRuleForm.tbFrom.Text = fromStr;
              pRuleForm.tbTo.Text = toStr;
             */
        }


        protected void addControl(Control pNewControl, String pControlName, ruleForm pRuleForm)
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
            {typeof(ReplaceRule),"Einfaches Ersetzen" },
            {typeof(RegExRule),"Regular Expression" },
            
        };
        public virtual bool isValid()
        {
            return true;
        }


        public virtual void saveData(ruleForm ruleForm)
        {

        }
    }


}
