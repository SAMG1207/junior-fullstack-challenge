using System.Net;
using System.Text.Encodings.Web;

namespace LebenChallenge.Utils
{
    public static class StringSanitizer
    {
        public static string StringSanitize(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return WebUtility.HtmlEncode(input.Trim());
        }
    }

}
