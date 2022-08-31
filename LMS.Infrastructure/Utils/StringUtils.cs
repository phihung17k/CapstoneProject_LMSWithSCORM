using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LMS.Infrastructure.Utils
{
    public class StringUtils
    {
        public const string ClientString = "tms";

        //name include extension
        public static string GetUniqueName(List<string> nameList, string currentName)
        {
            int dotIndex = currentName.LastIndexOf(".");
            //string extension = currentName.Substring(dotIndex);
            string currentNameWithoutExtension = dotIndex > -1 ? currentName.Substring(0, dotIndex) : currentName;

            //check current name for format string "abc(n).extension"
            //if true, get open round bracket "(" in "abc(n).extension"
            //  set currentNameWithoutExtension into "abc"
            //  set currentName into "abc.extension"
            Regex regex = new Regex(@"^.*\(\d\)$");
            if (regex.IsMatch(currentName))
            {
                int openingRoundBracket = currentNameWithoutExtension.LastIndexOf("(");
                currentNameWithoutExtension = currentNameWithoutExtension.Substring(0, openingRoundBracket);
            }
            currentName = currentNameWithoutExtension;

            //check currentName is existed in list of name
            //if true, set currentName = "abc" + "(index)"
            int index = 0;
            bool flag = false;
            do
            {
                bool isExistedName = nameList.Contains(currentName);
                if (isExistedName)
                {
                    index++;
                    currentName = $"{currentNameWithoutExtension}({index})";
                }
                else
                {
                    flag = true;
                }
            }
            while (!flag);
            return currentName;
        }
    }
}
