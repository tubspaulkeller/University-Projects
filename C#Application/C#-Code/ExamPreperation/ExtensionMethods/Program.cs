// Susing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // ExtensionsMethod with String
            string str = "This sentence look strage without vocals!";
            str = str.RemoveAllVocals();
            Console.WriteLine(str);
          

            "This sentence look strage without vocals!".RemoveAllVocals();
            //############################################################
            string umlauts = "Hallö, wä, gühts där?";

            Console.WriteLine(umlauts);
            umlauts = umlauts.replaceGermanUmlauts();

            Console.WriteLine(umlauts);

            //############################################################

            // ExtensionsMethod with Integer
            int ranNumber = 4;
            ranNumber = ranNumber.AddToAllNumbersATwo();
            Console.WriteLine(ranNumber);

            int[] numbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            numbers = numbers.removeEvenNumbers();

            foreach (var item in numbers)
            {
                Console.WriteLine(item);
            }

            Numbers nums = new Numbers();
            nums.NumbersArr = nums.NumbersArr.removeEvenNumbers();
            foreach (var item in nums)
            {
                Console.WriteLine(item);
            }

            char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F' };
            IEnumerator enu = letters.GetEnumerator();

            while (enu.MoveNext())
            {
                Console.WriteLine(enu.Current);
            }

      



            string ssss = "Heute ist es heiß!";
            ssss = ssss.ReplaceSSS(); 
            Console.WriteLine(ssss);
            

            string[] nameArr = "Paul,Keller".SplitName();
            Console.WriteLine("My surname is: {0} \nMy lastname is: {1}", nameArr[0], nameArr[1]);


            string[] nameSplit =  "Paul,Keller".SplitName2();
            Console.WriteLine("My surname is: {0} \nMy lastname is: {1}", nameSplit[0], nameSplit[1]);
            
            
            
            Console.ReadKey();
        }

       
    }
}