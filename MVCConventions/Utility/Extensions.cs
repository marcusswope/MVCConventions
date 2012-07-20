using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using HtmlTags;

namespace MVCConventions.Utility
{
    public static class Extensions
    {
        public static HtmlTag Format(this HtmlTag tag, string formatString)
        {
            var currentText = tag.Text();
            var newText = string.Format(formatString, currentText);
            tag.Text(newText);
            return tag;
        }

        public static void Each<T>(this IEnumerable<T> list, Action<T> func)
        {
            foreach (var item in list)
            {
                func(item);
            }
        }

        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            items.Each(list.Add);
        }

        public static string Join(this IEnumerable<string> stringList, string separator)
        {
            return string.Join(separator, stringList);
        }

        public static string ToFormat(this string format, params object[] parameters)
        {
            return string.Format(format, parameters);
        }

        public static bool PropertyMatches(this PropertyInfo prop1, PropertyInfo prop2)
        {
            return prop1.DeclaringType == prop2.DeclaringType && prop1.Name == prop2.Name;
        }

        public static string ToPrettyString(this object originalString)
        {
            if (originalString == null || string.IsNullOrEmpty(originalString.ToString()))
            {
                return string.Empty;
            }

            if (originalString.GetType().IsEnum)
            {
                var member = originalString.GetType().GetMember(originalString.ToString()).FirstOrDefault();
                var descriptionAttribute = member.GetCustomAttributes(typeof (DescriptionAttribute), true).FirstOrDefault() as DescriptionAttribute;
                if (descriptionAttribute != null)
                {
                    return descriptionAttribute.Description;
                }
            }

            if (originalString.ToString().ToUpper() == originalString.ToString())
            {
                return originalString.ToString();
            }

            var newString = string.Empty;
            for (var index = 0; index < originalString.ToString().Length; index++)
            {
                var previousChar = index > 0 ? originalString.ToString()[index - 1] : default(char);
                var character = originalString.ToString()[index];
                var nextCharacter = index + 1 <= originalString.ToString().Length - 1 ? originalString.ToString()[index + 1] : default(char);

                if (previousChar == default(char))
                {
                    newString = newString + character;
                }
                else if (character.IsUpperOrNumber() && previousChar.IsLower())
                {
                    newString = newString + " " + character;
                }
                else if (character.IsUpper() && previousChar.IsUpperOrNumber() && nextCharacter.IsLower())
                {
                    newString = newString + " " + character;
                }
                else if (!character.IsNumber() && previousChar.IsNumber())
                {
                    newString = newString + " " + character;
                }
                else
                {
                    newString = newString + character;
                }
            }
            return newString;
        }

        public static bool IsUpperOrNumber(this char value)
        {
            return value.ToString().IsUpperOrNumber();
        }

        public static bool IsUpperOrNumber(this string value)
        {
            int parser;
            var result = int.TryParse(value, out parser);
            return result || value.ToUpper() == value;
        }

        public static bool IsNumber(this char value)
        {
            return value.ToString().IsNumber();
        }

        public static bool IsNumber(this string value)
        {
            int parser;
            return int.TryParse(value, out parser);
        }

        public static bool IsUpper(this string value)
        {
            return value.ToUpper() == value && !value.IsNumber();
        }

        public static bool IsLower(this string value)
        {
            return value.ToLower() == value && !value.IsNumber();
        }

        public static bool IsUpper(this char value)
        {
            return value.ToString().IsUpper();
        }

        public static bool IsLower(this char value)
        {
            return value.ToString().IsLower();
        }
    }
}