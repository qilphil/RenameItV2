using System;
using System.Xml.Serialization;
namespace renameit_v2_wpf.rules
{
    [Serializable]
    [XmlInclude(typeof(BaseRule))]
    public class ReplaceRule : BaseRule
    {

        public override string Apply(string filename)
        {
            if (fromStr != String.Empty)
                return filename.Replace(fromStr, toStr);
            else
                return fromStr;
        }
        public override BaseRule clone()
        {
            return this.DeepClone();
        }
        public ReplaceRule(ReplaceRule pReplaceRule)
            : base(pReplaceRule)
        {


        }
        public ReplaceRule(MainWindow pMainWindow)
            : base()
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
