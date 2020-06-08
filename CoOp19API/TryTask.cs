using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CoOp19API
{
    public class TryTask<T> where T: class
    {
        public static async Task<ActionResult<T>> Run(Func<Task<ActionResult<T>>> Task)
        {
            ActionResult<T> output;

            try
            {
                output = await Task();
            }
            catch(Exception E)
            {
                if(E.Message=="Input" || E.Message=="Output")
                {
                    return new ObjectResult($"ERROR: {E.InnerException.Message}") { StatusCode = 400 };
                } 
                else
                {
                    return new ObjectResult("Unhandled Error in Server") { StatusCode = 500 };
                }
            }
            return output;
        }
    }
}
