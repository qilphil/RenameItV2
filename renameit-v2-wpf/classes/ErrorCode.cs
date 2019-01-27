using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
namespace renameit_v2_wpf.rules
{
    public enum ErrorCode
    {
        none = 0,
        alreadyExists,
        duplicateName
    }
}
