using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace renameit_v2_wpf.rules
{
    public enum errorCode
    {
        none = 0,
        alreadyExists,
        duplicateName
    }

    public class listFile
    {
        public string fileName { get; set; }
        public string showName
        {
            get
            {
                return System.IO.Path.GetFileName(fileName);
            }
        }
        public string convertedFileName { get; set; }
        public string errorMessage { get; set; }
        public errorCode hasError { get; set; }
        public bool testDuplicate()
        {
            hasError = (showName != convertedFileName && File.Exists(Path.Combine(Path.GetDirectoryName(fileName), convertedFileName))) ? errorCode.alreadyExists : errorCode.none;
            errorMessage = hasError == errorCode.alreadyExists ? String.Format("File {0} exists", convertedFileName) : String.Empty;
            return hasError == errorCode.alreadyExists;
        }
    }
    public class fileList : ObservableCollection<listFile>
    {
        public bool ContainsFileName(string pfileName)
        {
            foreach (listFile testFile in this)
                if (testFile.fileName == pfileName)
                    return true;
            return false;
        }

        internal void apply(ruleList pCurrentRules)
        {

            foreach (listFile iFile in this)
            {
                pCurrentRules.apply(iFile);
            }
            
            Dictionary<string,int> nameCounts = new Dictionary<string,int>();
            foreach (listFile iFile in this) {
                string fName = iFile.convertedFileName;
                if (nameCounts.ContainsKey(fName))
                {
                    nameCounts[fName]++;
                }
                else 
                {
                    nameCounts[fName] = 1;
                }
            }
            List<string> alltargetNames = nameCounts.Keys.ToList<string>();
            foreach (listFile iFile in this)
            {
                if (alltargetNames.Contains(iFile.convertedFileName) && nameCounts[iFile.convertedFileName] > 1)
                {
                    iFile.hasError = errorCode.duplicateName;
                    iFile.errorMessage = string.Format("Duplicate Filename {0} created.", iFile.convertedFileName);
                }
            }
            

        }
    }
}
