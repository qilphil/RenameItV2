using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace renameit_v2.classes
{
    public class ruleList : List<baseRule>
    {
        public String apply(listFile pFile)
        {
            String rReplacedName = pFile.fileName;
            foreach (baseRule iRule in this)
            {
                rReplacedName = iRule.apply(rReplacedName);
            }
            return rReplacedName;
        }
    }
}
