
using System.Collections.Generic;
namespace ExtensionMethods
{
    internal static class ExtensionMethos
    {
        public static string RemoveAllVocals(this string str)
        {
            str = str.Replace("a", "");
            str = str.Replace("e", "");
            str = str.Replace("i", "");
            str = str.Replace("o", "");
            str = str.Replace("u", "");

            str = str.Replace("A", "");
            str = str.Replace("E", "");
            str = str.Replace("I", "");
            str = str.Replace("0", "");
            str = str.Replace("U", "");
            return str;
        }

        public static string replaceGermanUmlauts(this string str)
        {

            str = str.Replace("ä", "ae");
            str = str.Replace("ö", "oe");
            str = str.Replace("ü", "ue");

            return str;


        }

        public static int AddToAllNumbersATwo(this int num)
        {
            if (num <= 2)
            {
                return num += 4;
            }
            return num += 2;
        }

        public static int[] removeEvenNumbers(this int[] numbers)
        {
            List<int> tmp = new List<int>(numbers);
            List<int> uneven = new List<int>();

            foreach (int number in tmp)
            {
                if (!(number % 2 == 0))
                {
                    uneven.Add(number);
                }
            }

            return uneven.ToArray();

        }

        public static int[] removeEvenNumbers(this Numbers n)
        {
            List<int> tmp = new List<int>(n.NumbersArr);
            List<int> uneven = new List<int>();

            foreach (int number in tmp)
            {
                if (!(number % 2 == 0))
                {
                    uneven.Add(number);
                }
            }

            return uneven.ToArray();

        }
        
        public static string ReplaceSSS(this string s)
        {
            s = s.Replace("ß", "ss");
            return s;
        }

        public static string[] SplitName2(this string fullname)
        {
            return fullname.Split(","); 
        }


        public static string [] SplitName(this string fullname)
        {
            string[] splitarr;
            splitarr = fullname.Split(",");
            return splitarr;
        }

    }
}

