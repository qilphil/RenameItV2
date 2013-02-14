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
            return rReplacedName;
        }
    }
}
