using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Robot_Navigation
{
    class ParseString
    {
        private string inputString;

        public ParseString(string str) { inputString = str; }


        public List<int> getIntFromString()
        {
            string[] numbers = Regex.Split(inputString, @"\D+");
            List<int> listInt = new List<int>();

            foreach (string number in numbers)
            {
                if (!string.IsNullOrEmpty(number) && int.TryParse(number, out int num))
                {
                    listInt.Add(num);
                }
            }
            return listInt;
        }
    }
}
