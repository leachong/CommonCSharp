using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCSharp.Extend
{
    public static class ExceptionExtend
    {
        public static string FullMessage(this Exception e)
        {
            var result = e.Message;
            var ex = e.InnerException;
            while (ex != null)
            {
                result += " ---> " + ex.Message;
                ex = ex.InnerException;
            }
            return result;
        }
    }
}
