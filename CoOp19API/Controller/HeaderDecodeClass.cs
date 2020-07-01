using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        public static async Task<string[]> DecodeHeader(HttpRequest req)
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
                else if(auth[0] == "Google")
                {
                    try
                    {
                        var validPayload = await GoogleJsonWebSignature.ValidateAsync(auth[1]);
                        return new string[] { validPayload.Email };
                       
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
            return null;   
        }
        private const string GoogleApiTokenInfoUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={0}";

       
    }


}
