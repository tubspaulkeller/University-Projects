using System;
using System.Collections.ObjectModel;
namespace Collections
{
    public delegate void carInserted(Car c);
    public class CarCollection : Collection<Car>
    {
        public event carInserted CarInserted;
        public event Action Inserted;
        public event Action Removed;
        public event Action AllCleared;
        
        protected override void InsertItem(int index, Car item)
        {
            if (CarInserted != null) CarInserted(item);
            if (Inserted != null) Inserted();
                
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, Car item)
        {
            if (Inserted != null) Inserted();
            base.SetItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            if (Removed != null) Removed();
            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            if (AllCleared != null) AllCleared();
            base.ClearItems();
        }
        
        
    }
}

