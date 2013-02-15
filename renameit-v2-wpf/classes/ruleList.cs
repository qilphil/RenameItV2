using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;
namespace renameit_v2_wpf.rules
{
    [Serializable]
    [XmlInclude(typeof(baseRule))]
    [XmlInclude(typeof(ReplaceRule))]
    [XmlInclude(typeof(RegExRule))]
    public class ruleList : ObservableCollection<baseRule>
    {
        public string apply(listFile pFile)
        {
            string rReplacedName = pFile.fileName;
            foreach (baseRule iRule in this)
            {
                rReplacedName = iRule.apply(System.IO.Path.GetFileName(rReplacedName));
            }
                       
            pFile.convertedFileName = rReplacedName;
            pFile.testDuplicate();
            return rReplacedName;
        }

        internal void MoveUp(baseRule pMoveUpRule)
        {
            int indexpos = IndexOf(pMoveUpRule);
            if (indexpos > 0)
            {
                Remove(pMoveUpRule);
                Insert(indexpos - 1, pMoveUpRule);
            }
        }

        internal void MoveDown(baseRule pMoveDownRule)
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
