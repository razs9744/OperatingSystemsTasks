using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator
{
    public static class Validations
    {
        public static bool IsRightNumberOfArguments(int numberOfArguments)
        {
            if (numberOfArguments == 1)
                return true;
            return false;
        }

        public static bool IsFileExists(out StreamReader InputFile, string argument)
        {
            try
            {
                 InputFile = new StreamReader(argument);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                InputFile = null;
                return false;
            }
            return true;
        }
    }
}
