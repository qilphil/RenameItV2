using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;

namespace renameit_v2_wpf.classes
{
    public class ruleList : ObservableCollection<baseRule>
    {
        public String apply(listFile pFile)
        {
            String rReplacedName = pFile.fileName;
            foreach (baseRule iRule in this)
            {
                rReplacedName = iRule.apply(rReplacedName);
            }
            pFile.convertedFileName = rReplacedName;
            return rReplacedName;
        }
    }
}
