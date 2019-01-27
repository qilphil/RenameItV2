using System;
using System.IO;
namespace renameit_v2_wpf.rules
{

    public class ListFile
    {
        public string FileName { get; set; }

        public string ShowName
        {
            get
            {
                return Path.GetFileName(FileName);
            }
        }

        public string ConvertedFileName { get; set; }
        public string ErrorMessage { get; set; }
        public ErrorCode HasError { get; set; }

        public bool TestDuplicate()
        {
            HasError = (ShowName != ConvertedFileName && File.Exists(Path.Combine(Path.GetDirectoryName(FileName), ConvertedFileName))) ? ErrorCode.alreadyExists : ErrorCode.none;
            ErrorMessage = HasError == ErrorCode.alreadyExists ? $"File {ConvertedFileName} exists" : string.Empty;
            return HasError == ErrorCode.alreadyExists;
        }

        internal string DoRename
        {
            get
            {
                if (HasError == ErrorCode.none)
                {
                    try
                    {
                        string targetFileName = Path.Combine(Path.GetDirectoryName(FileName), ConvertedFileName);

                        if (File.Exists(FileName) && !File.Exists(targetFileName))
                            File.Move(FileName, targetFileName);
                    }
                    catch (Exception renExc)
                    {
                        return renExc.ToString();
                    }
                    return String.Empty;
                }
                else
                {
                    return HasError.ToString();
                }
            }
        }
    }
}
