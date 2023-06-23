using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AxonsERP.Contracts.Utils
{
    public static class StringUtil
    {
        public static string ConvertCamelToOracleWord(string propertyName)
        {
            // Change Upper Camel Format to Column in Oracle (OperationCode to operation_code)
            int cnt = 0;
            StringBuilder textForOrderBy = new();
            foreach (var c in propertyName)
            {
                if (Char.IsUpper(c) && cnt != 0)
                    textForOrderBy.Append($"_{c}");
                else
                    textForOrderBy.Append(c);
                cnt += 1;
            }

            return textForOrderBy.ToString();
        }

        public static string SubString(string source, int indexFrom, int indexTo)
        {
            if (!String.IsNullOrEmpty(source) && source.Length >= indexTo && indexFrom < indexTo)
            {
                return source.Substring(indexFrom, indexTo - indexFrom);
            }

            return string.Empty;
        }

        public static int FindIndexOfString(string source, string input)
        {
            int result = -1;
            if (!String.IsNullOrEmpty(input) && !String.IsNullOrEmpty(source))
            {
                result = source.IndexOf(input);
                return result;
            }
            return result;
        }

        public static int FindLengthOfSubstring(string source, string input)
        {
            // When finding length of a non-separated substring
            int result = -1;
            if (!String.IsNullOrEmpty(input) && !String.IsNullOrEmpty(source) && source.IndexOf(input) != -1)
            {
                // Find Length
                // When startPos is found -> length = last - start + 1
                result = source.LastIndexOf(input) - source.IndexOf(input) + 1;
            }

            return result;
        }

        public static int CountSubstringInString(string source, string input)
        {
            int result = -1;
            if (!String.IsNullOrEmpty(input) && !String.IsNullOrEmpty(source) && source.IndexOf(input) != -1)
            {
                result = source.Split(input).Length - 1;
            }
            return result;

        }

        public static string[] ConvertStringToStringArray(string input)
        {
            var stringArray = input.Split(',');
            for (int i = 0; i < stringArray.Length; i++)
            {
                stringArray[i] = stringArray[i].Trim('\'');
            }

            return stringArray;
        }

        public static string ConvertStringArrayToString(string[] input)
        {
            var result = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (string.IsNullOrEmpty(result.ToString()))
                {
                    result.Append($"'{input[i]}'");
                }
                else
                {
                    result.Append($",'{input[i]}'");
                }
            }

            return result.ToString();
        }

        public static string CheckPasswordValidity(string value = "", string locale = "en", string password = "")
        {
            if (string.IsNullOrEmpty(password) && string.IsNullOrEmpty(value))
            {
                return GetLocaleMessage(locale, "Password cannot be empty.", "รหัสผ่าน ต้องไม่เป็นค่าว่าง");
            }

            if (password != value)
            {
                return GetLocaleMessage(locale, "Confirm password do not match.", "ยืนยันรหัสผ่าน ต้องตรงกับรหัสผ่าน");
            }

            return CheckPasswordRules(value, locale);
        }

        private static string CheckPasswordRules(string value, string locale)
        {
            var passwordRules = new Dictionary<Regex, (string, string)>
            {
                {new Regex(@"^\S*$"), ("Password must not contain Whitespaces.", "รหัสผ่าน ต้องไม่เป็นเว้นวรรค")},
                {new Regex(@"^(?=.*[A-Z]).*$"), ("Password must have at least one Uppercase Character.", "รหัสผ่าน ต้องมีอักษรตัวใหญ่อย่างน้อย 1 ตัว")},
                {new Regex(@"^(?=.*[a-z]).*$"), ("Password must have at least one Lowercase Character.", "รหัสผ่าน ต้องมีอักษรตัวเล็กอย่างน้อย 1 ตัว")},
                {new Regex(@"^(?=.*[0-9]).*$"), ("Password must contain at least one Digit.", "รหัสผ่าน ต้องมีอักษรตัวเลขอย่างน้อย 1 ตัว")},
                {new Regex(@"^(?=.*[~`!@#$%^&*()--+={}\[\]|\\:;'<>,.?/_]).*$"), ("Password must contain at least one Special Symbol.", "รหัสผ่าน ต้องมีอักษรตัวอักษรพิเศษอย่างน้อย 1 ตัว")},
                {new Regex(@"^.{8,16}$"), ("Password must be 8-16 Characters Long.", "รหัสผ่าน ต้องมีอักษรระหว่าง 8-16 ตัว")}
            };

            foreach (var rule in passwordRules)
            {
                if (!rule.Key.IsMatch(value))
                {
                    return GetLocaleMessage(locale, rule.Value.Item1, rule.Value.Item2);
                }
            }
            return string.Empty;
        }

        private static string GetLocaleMessage(string locale, string englishMessage, string thaiMessage)
        {
            return locale == "th" ? thaiMessage : englishMessage;
        }
    }
}

