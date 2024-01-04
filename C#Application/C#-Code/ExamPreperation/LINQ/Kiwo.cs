namespace Funfair;

public class Kiwo 
{
    public string Name { get; set; }
    public double Costs { get; set; }
    public double Win { get; set; }
    public int Visitors { get; set; }
    
    public Kiwo(string n, double c, double w, int v)
    {
        Name = n;
        Costs = c;
        Win = w;
        Visitors = v;
    }
    
}