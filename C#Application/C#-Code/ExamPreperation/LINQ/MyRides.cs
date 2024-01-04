using System;
using System.Collections;

namespace Funfair
{
	public class MyRides
	{
		private string designtion;
		private double costs_per_year;
		private double costs_per_day;
		private int visitors_per_day;
		private double price_per_ticket;

		public MyRides(string des, double cpy, double cpd, int vpd, double ppt)
		{

			designtion = des;
			costs_per_year = cpy;
			costs_per_day = cpd;
			visitors_per_day = vpd;
			price_per_ticket = ppt;
		}
		
		
		public string Designation { get => designtion; set => designtion = value; }
		public double Costs_per_year { get => costs_per_year; set => costs_per_year = value; }
		public double Costs_per_day { get => costs_per_day; set => costs_per_day = value; }
		public int Visitors_per_day { get => visitors_per_day; set => visitors_per_day = value; }
		public double Price_per_ticket { get => price_per_ticket; set => price_per_ticket = value; }

		
	}
}
