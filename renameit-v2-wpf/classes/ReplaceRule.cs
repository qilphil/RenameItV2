using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace renameit_v2_wpf.classes
{
    [Serializable]
    class ReplaceRule : baseRule
    {
    
        public override string apply(string filename) {
            return filename.Replace(fromStr,toStr);
        }
        public override baseRule clone()
        {
            return this.DeepClone();
        }
        public  ReplaceRule(ReplaceRule pReplaceRule) : base(pReplaceRule)
        {
           

        }
        public ReplaceRule():base()
        {
        }
        public override string ToString()
        {
            return string.Format("Simple Replace - From: {0} To: {1}", fromStr, toStr);
        }
    }
}
