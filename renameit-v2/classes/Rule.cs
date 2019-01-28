using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace renameit_v2.classes
{

    [Serializable]
    public class BaseRule
    {
        public virtual string fromStr { get; set; }
        public virtual string toStr { get; set; }

        public virtual string apply(string filename) { return filename; }
        public override string ToString() { return "Virtual Rule"; }

        public Dictionary<string, string> ruleProps;
        public virtual BaseRule clone()
        {
            return this.DeepClone();
        }

        /// <summary>Initializes a new instance of the <see cref="object" /> class.</summary>
        public BaseRule(BaseRule pBaseRule)
            : base()
        {

        }
        public BaseRule()
        {
            this.ruleProps = new Dictionary<string, string>();
        }

        public virtual void setupForm(ruleForm pRuleForm)
        {
            pRuleForm.cbRuleType.Items.AddRange(nameList.Values.ToArray<string>());
            pRuleForm.cbRuleType.SelectedIndex = pRuleForm.cbRuleType.Items.IndexOf(nameList[this.GetType()]);
            pRuleForm.gbRule.Text = nameList[this.GetType()];
            pRuleForm.tbFrom.Text = this.fromStr;
            pRuleForm.tbTo.Text = this.toStr;
        }


        protected void addControl(Control pNewControl, String pControlName, ruleForm pRuleForm)
        {
            if (!pRuleForm.ruleControls.ContainsKey(pControlName))
            {
                pRuleForm.addNamedControl(pControlName, pNewControl);

            }
            else
            {
                Control existingControl = (Label)pRuleForm.ruleControls[pControlName];
                existingControl.Show();
            }
        }
        public static Dictionary<Type, string> nameList = new Dictionary<Type, string>() {
            {typeof(ReplaceRule), "Einfaches Ersetzen" },
            {typeof(RegExRule), "Regular Expression" },
            
        };
        public virtual bool IsValid()
        {
            return true;
        }


        public virtual void saveData(ruleForm ruleForm)
        {

        }
    }


}
