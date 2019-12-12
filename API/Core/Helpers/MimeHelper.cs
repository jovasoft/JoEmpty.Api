using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Helpers
{
    public static class MimeHelper
    {
        public static string GetMimeFromType(string type)
        {
            Dictionary<string, string> mimeTypes = new Dictionary<string, string>();
            mimeTypes.Add(".jpg", "image/jpeg");
            mimeTypes.Add(".jpeg", "image/jpeg");
            mimeTypes.Add(".png", "image/png");
            mimeTypes.Add(".pdf", "application/pdf");
            mimeTypes.Add(".doc", "application/msword");
            mimeTypes.Add(".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            mimeTypes.Add(".txt", "text/plain");

            string value = string.Empty;
            try
            {
                value = mimeTypes[type];
            }
            catch (Exception) { }

            return value;
        }
    }
}
