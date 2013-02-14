using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace renameit_v2_wpf.classes
{
    public class listFile
    {
        public string fileName { get; set; }
        public  string showName
        {
            get
            {
                return System.IO.Path.GetFileName(fileName);
            }
        }
        public string convertedFileName { get; set; }

    }
    public class fileList : ObservableCollection<listFile> { 
        public bool ContainsFileName(string pfileName) {
            foreach (listFile testFile in this)
                if (testFile.fileName == pfileName)
                    return true;
            return false;
        }
    }
}
