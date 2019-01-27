using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace renameit_v2_wpf.rules
{
    public class FileList : ObservableCollection<ListFile>
    {
        public bool ContainsFileName(string pfileName)
        {
            foreach (ListFile testFile in this)
                if (testFile.FileName == pfileName)
                {
                    return true;
                }

            return false;
        }

        internal void apply(RuleList pCurrentRules)
        {

            foreach (ListFile iFile in this)
            {
                pCurrentRules.apply(iFile);
            }
            
            Dictionary<string,int> nameCounts = new Dictionary<string,int>();
            foreach (ListFile iFile in this) {
                string fName = iFile.ConvertedFileName;
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
            foreach (ListFile iFile in this)
            {
                if (alltargetNames.Contains(iFile.ConvertedFileName) && nameCounts[iFile.ConvertedFileName] > 1)
                {
                    iFile.HasError = ErrorCode.duplicateName;
                    iFile.ErrorMessage = string.Format("Duplicate Filename {0} created.", iFile.ConvertedFileName);
                }
            }
            

        }

        internal void delete(string pDelFileName)
        {
            List<ListFile> delFiles =new List<ListFile>();
            foreach(ListFile iTestFile in this) 
            {
                if (iTestFile.FileName == pDelFileName) {
                    delFiles.Add(iTestFile);
                }
            }
            foreach (ListFile iDelFile in delFiles) {
                Remove(iDelFile);
            }
           
        }

        internal void delete(List<string> pDelFileNames)
        {
            List<ListFile> delFiles = new List<ListFile>();
            foreach (ListFile iTestFile in this)
                if (pDelFileNames.Contains(iTestFile.FileName))
                    delFiles.Add(iTestFile);

            foreach (ListFile iDelFile in delFiles)
                Remove(iDelFile);
        }
    }
}
