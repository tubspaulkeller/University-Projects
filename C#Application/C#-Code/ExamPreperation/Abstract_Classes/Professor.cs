using System;
namespace Abstract_Classes_Interfaces_Properties_events
{
    public class Professor : Person
    {
        private uint employeeNum;

        public Professor(string n, int a, double h, uint eNum): base(n,a,h)
        {
            employeeNum = eNum;

        }
        
        
        


        public override string idCard()
        {
            return name + " " + age.ToString() + " " + height.ToString() + " " + employeeNum.ToString();
        }
    }
}

