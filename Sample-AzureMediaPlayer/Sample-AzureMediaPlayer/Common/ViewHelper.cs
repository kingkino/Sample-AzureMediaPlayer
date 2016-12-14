using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample_AzureMediaPlayer.Common
{
    public class ViewHelper
    {
        public static IHtmlString GetFormatSizeString(float size)
        {
            return MvcHtmlString.Create(GetFormatSizeString(size, 1024));
        }

        private static string GetFormatSizeString(float size, int p)
        {
            return GetFormatSizeString(size, p, "#,##0.##");
        }

        private static string GetFormatSizeString(float size, int p, string specifier)
        {
            string[] suffix = { "", "K", "M", "G", "T", "P", "E", "Z", "Y" };
            int index = 0;

            while (size >= p)
            {
                size /= p;
                index++;
            }

            return string.Format(
                "{0}{1}B",
                size.ToString(specifier),
                index < suffix.Length ? suffix[index] : "-");
        }
    }
}