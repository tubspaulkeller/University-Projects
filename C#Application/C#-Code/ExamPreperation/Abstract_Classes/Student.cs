using System;
namespace Abstract_Classes_Interfaces_Properties_events
{
    public class Student: Person, IMatNo
    {
        private uint matno;

        public uint MatNo
        {
            get => matno;
            set
            {
                matno = value;
                if (OnChange != null) OnChange(matno);
            }
        }
        public event matNoChanged? OnChange;
        
        public Student()
        {
        }

        public Student(string n, int a, double h, uint matno) : base(n, a, h)
        {
            this.matno = matno;
        }

        

        public uint getMatNo()
        {
            return matno;
        }

        public override string idCard()
        {
            return name + " " + age.ToString() + " " + height.ToString() + " " + matno.ToString(); // has to be the properties of the bass class e.g. name not just n etc.
        }

        /*  Alternative   
        public override string idCard()
            {

            return base.idCard()+ " " + matriculationNumber + Environment.NewLine;
         }
          */

    }
}

