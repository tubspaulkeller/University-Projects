using System;
namespace Abstract_Classes_Interfaces_Properties_events
{
    public delegate void matNoChanged(uint matno);
    public interface IMatNo
    {
        public event matNoChanged OnChange;
        //public event Action  onChange2;
        uint getMatNo();
    }
}

