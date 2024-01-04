using System;
namespace Collections
{
	public class Car
	{
		private string name;
		private string color;

		public Car(string name, string color)
		{
			this.name = name;
			this.color = color;
		}

		public Car()
		{
				
		}
		public string Name { get => name; set => name = value; }
		public string Color { get => color; set => color = value; }


		public override string ToString()
		{
			return String.Format("{0} {1}", Name, Color);
		}


	}
}

