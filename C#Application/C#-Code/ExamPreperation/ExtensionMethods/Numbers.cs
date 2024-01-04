using System;
using System.Collections;
namespace ExtensionMethods
{
    public class Numbers : IEnumerable
    {
        private int[] numbers = new int[13] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

        public int[] NumbersArr
        {
            get => numbers;
            set
            {
                numbers = value;
            }
        }


        public IEnumerator GetEnumerator()
        {
            foreach (int number in numbers)
            {
                yield return number;
            }

        }
    }
}

