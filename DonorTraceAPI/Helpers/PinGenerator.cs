using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorTraceAPI.Helpers
{
    public static class PinGenerator
    {

        private static char[] sybmols = {
                             '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                             'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                         'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
    };

        public static string WebHash()
        {
            int length = sybmols.Length;
            ulong num = (ulong)DateTime.Now.Ticks;

            string output = string.Empty;
            ulong tmp = num;
            ulong mod = 0;
            while (tmp != 0)
            {
                mod = tmp % (ulong)length;
                tmp = tmp / (ulong)length;
                output = sybmols[mod] + output;
            }
            output += RandomString(6);
            return output;
        }

        public static string RandomString(int length)
        {
            Stack<byte> bytes = new Stack<byte>();
            string output = string.Empty;

            for (int i = 0; i < length; i++)
            {
                if (bytes.Count == 0)
                {
                    bytes = new Stack<byte>(Guid.NewGuid().ToByteArray());
                }
                byte pop = bytes.Pop();
                output += sybmols[(int)pop % sybmols.Length];
            }
            return output;
        }
    }
}
