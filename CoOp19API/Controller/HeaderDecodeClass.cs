using Microsoft.AspNetCore.Http;
using System;
using System.Text;

namespace CoOp19.App.Controllers
{
    public static class HeaderDecode
    {
        /// <summary>
        /// Parses request for login details
        /// accepts basic auth or google sign in token
        /// </summary>
        /// <param name="req">Header containing login info</param>
        /// <returns>decoded username and/or password in string array</returns>
        public static string[] DecodeHeader(HttpRequest req)
        {
            if(req.Headers.ContainsKey("Authorization"))
            {
                string[] auth = req.Headers["Authorization"].ToString().Split(" ");
                if (auth[0] == "Basic")
                {
                    byte[] data = Convert.FromBase64String(auth[1]);
                    string decodedString = Encoding.UTF8.GetString(data);
                    return decodedString.Split(":");
                }
            }
            return null;   
        }
    }
}
