using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace orgchart
{
    public static class Utility
    {
        public static string Encode(string original)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(original);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string SUCCESS_MESSAGE = "SUCCESS";
        public static string FAILED_MESSAGE = "FAILED";

        public static string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }
    }

    public static class errorstate
    {
        public static string errors(ModelStateDictionary modelstate, int rows = 0)
        {
            StringBuilder sb = new StringBuilder();
            var errors = modelstate.Select(x => x.Value.Errors)
                    .Where(y => y.Count > 0)
                    .ToList();
            int i = 1;
            if (errors.Count > 0)
            {
                sb.Append("<ul style='font-color:red'>");
                foreach (ModelErrorCollection mdl in errors)
                {
                    sb.Append("<li>");
                    sb.Append(mdl[0].ErrorMessage);
                    sb.Append("</li>");
                    if (i == rows)
                    {
                        break;
                    }
                    i++;
                }
                sb.Append("</ul>");
            }

            return sb.ToString();
        }



    }
}