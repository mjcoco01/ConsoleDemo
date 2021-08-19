using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleDemo.ApplicationCore
{
    public static class Helper
    {
        private static bool IsNumeric(string text)
        {
            return text.All(char.IsNumber);
        }


    }
}
