using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace renameit_v2.classes
{
    [Serializable]
    class ReplaceRule : BaseRule
    {

        public override string apply(string filename)
        {
            return filename.Replace(fromStr, toStr);
        }
        public override BaseRule clone()
        {
            return this.DeepClone();
        }
        public ReplaceRule(ReplaceRule pReplaceRule)
            : base(pReplaceRule)
        {


        }
        public ReplaceRule()
            : base()
        {
        }
        public override string ToString()
        {
            return string.Format("Simple Replace - From: {0} To: {1}", fromStr, toStr);
        }
    }
}
