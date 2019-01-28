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
            {
                if (testFile.FileName == pfileName)
                {
                    return true;
                }
            }

            return false;
        }

        internal void Apply(RuleList pCurrentRules)
        {
            foreach (ListFile iFile in this)
            {
                pCurrentRules.apply(iFile);
            }

            var nameCounts = new Dictionary<string, int>();
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
            var alltargetNames = nameCounts.Keys.ToList();
            foreach (ListFile iFile in this)
            {
                if (alltargetNames.Contains(iFile.ConvertedFileName) && nameCounts[iFile.ConvertedFileName] > 1)
                {
                    iFile.HasError = ErrorCode.duplicateName;
                    iFile.ErrorMessage = string.Format("Duplicate Filename {0} created.", iFile.ConvertedFileName);
                }
            }
        }

        internal void Delete(string pDelFileName)
        {
            foreach (ListFile iDelFile in this.Where(x => x.FileName == pDelFileName)) {
                Remove(iDelFile);
            }
        }

        internal void Delete(List<string> pDelFileNames)
        {
            foreach (ListFile iDelFile in this.Where(x => pDelFileNames.Contains(x.FileName)))
                Remove(iDelFile);
        }
    }
}
