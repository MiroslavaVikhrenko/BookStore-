using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Helpers
{
    public class InputHelper
    {
        private static string _subMessage;

        static InputHelper()
        {
            _subMessage = "Enter";
        }
        public static int GetInt(string value)
        {
            int result = 0;
            Console.WriteLine("{0} {1}: ", _subMessage, value);
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("{0} {1}: ", _subMessage, value);
            }
            return result;
        }
        public static decimal GetDecimal(string value)
        {
            decimal result = 0;
            Console.WriteLine("{0} {1}: ", _subMessage, value);
            while (!decimal.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("{0} {1}: ", _subMessage, value);
            }
            return result;
        }
        public static bool GetBoolean(string value)
        {
            bool result = false;
            Console.WriteLine("{0} {1}: ", _subMessage, value);
            while (!bool.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("{0} {1}: ", _subMessage, value);
            }
            return result;
        }

        public static string GetString(string value)
        {
            string result = String.Empty;
            Console.WriteLine("{0} {1}: ", _subMessage, value);
            while (String.IsNullOrWhiteSpace(result = Console.ReadLine()))
            {
                Console.Write("{0} {1}: ", _subMessage, value);
            }
            return result;
        }

        public static DateOnly GetDate(string value)
        {
            DateOnly dateOnly = new();
            Console.WriteLine("{0} {1}: ", _subMessage, value);
            while (!DateOnly.TryParse(Console.ReadLine(), out dateOnly))
            {
                Console.Write("{0} {1}: ", _subMessage, value);
            }
            return dateOnly;
        }
    }
}
