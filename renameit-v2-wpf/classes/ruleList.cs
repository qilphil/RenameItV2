using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;
namespace renameit_v2_wpf.rules
{
    [Serializable]
    [XmlInclude(typeof(BaseRule))]
    [XmlInclude(typeof(ReplaceRule))]
    [XmlInclude(typeof(RegExRule))]
    public class RuleList : ObservableCollection<BaseRule>
    {
        public string apply(ListFile pFile)
        {
            string rReplacedName = pFile.FileName;
            foreach (BaseRule iRule in this)
            {
                rReplacedName = iRule.Apply(System.IO.Path.GetFileName(rReplacedName));
            }
                       
            pFile.ConvertedFileName = rReplacedName;
            pFile.TestDuplicate();
            return rReplacedName;
        }

        internal void MoveUp(BaseRule pMoveUpRule)
        {
            int indexpos = IndexOf(pMoveUpRule);
            if (indexpos > 0)
            {
                Remove(pMoveUpRule);
                Insert(indexpos - 1, pMoveUpRule);
            }
        }

        internal void MoveDown(BaseRule pMoveDownRule)
        {
            int indexpos = IndexOf(pMoveDownRule);
            if (indexpos < Count-1)
            {
                Remove(pMoveDownRule);
                Insert(indexpos + 1, pMoveDownRule);
            }
        }
    }
}
