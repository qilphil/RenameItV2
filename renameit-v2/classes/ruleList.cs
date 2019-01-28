using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace renameit_v2.classes
{
    public class RuleList : List<BaseRule>
    {
        public String apply(ListFile pFile)
        {
            String rReplacedName = pFile.FileName;
            foreach (BaseRule iRule in this)
            {
                rReplacedName = iRule.apply(rReplacedName);
            }
            return rReplacedName;
        }
    }
}
