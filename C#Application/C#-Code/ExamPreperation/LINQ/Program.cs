namespace Funfair
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            List<MyRides> myRides = new List<MyRides>();
            myRides.Add(new MyRides("Rollercoaster", 40000, 500, 800, 7));
            myRides.Add(new MyRides("Ghosttrain", 10000, 300, 1200, 5));
            myRides.Add(new MyRides("Water-ride", 30000, 800, 300, 6.40));
            myRides.Add(new MyRides("Freefall-Tower", 20000, 100, 200, 10));
            myRides.Add(new MyRides("Children Carousel", 5000, 100, 300, 2.50));

            // a.)
            double fixedYearlyCosts = myRides.Select(s => s.Costs_per_year).Sum();

            // b.)
            double[] dailyRevenue = myRides.Select(x => x.Costs_per_day * x.Visitors_per_day).ToArray();

            // c.)
            double[] benefit200 = myRides.Select(r => (r.Price_per_ticket * r.Visitors_per_day - r.Costs_per_day) * 200 - r.Costs_per_year).ToArray();

            // d.)
            List<string> NamesOfBenificalRide = myRides.OrderBy(x => (x.Price_per_ticket * x.Visitors_per_day - x.Costs_per_day) * 200 - x.Costs_per_year).Select(r => r.Designation).ToList();

            List<string> NamesOfBenificalRideII = myRides.OrderBy(x => (x.Price_per_ticket * x.Visitors_per_day - x.Costs_per_day) * 200 - x.Costs_per_year).Where((x,i) => i < 2).Select(r => r.Designation).ToList();

            MyOwnCollection myowncollection= new MyOwnCollection();
            double MaxKiwoCosts = myowncollection.Select(x => x.Costs).Max();

            string [] nameOfMaxCosts = myowncollection.Where(x => x.Costs == MaxKiwoCosts).Select(x => x.Name).ToArray();

            string [] nameOfMaxCosts2  = myowncollection.Where(x => x.Costs > 9999).OrderByDescending(x => x.Costs).Select(x => x.Name).ToArray();
            
            

            string[] nameOfMaxCosts3 = myowncollection.Where((x, i) => x.Win > MaxKiwoCosts && i < 2).Select(x => x.Name).ToArray();

            double[] ys = new double[1000]; 
            ys = ys.Select((x, i) => Math.Sin(i * Math.PI * 2 / 1000)).ToArray(); 
            /*
            // unsinn
            String[] unsinn = myRides.Select(h => "huhu").ToArray(); // gibt ein string arrray zurück mit 5 mal HUHU, da 5 Objekte 

            // sinn
            string[] sinn = myRides.Select(h => h.Designation).ToArray();


            // Where

            // if (newData != null) { newData(voltages.Where((i, x) => (i < 960)).ToArray());  };

            String[] getGhostTrain = myRides.Where(h => h.Designation == "Ghosttrain").Select(r => r.Designation).ToArray();

            int[] getNumberHigher2 = new int[3] { 2, 3, 5 };

            int[] result = getNumberHigher2.Where(x => x > 2).ToArray();

            double avgPricePerDay = myRides.Select(x => x.Price_per_ticket).Average();

            double[] highestPricePerTicket = myRides.Where(x => x.Price_per_ticket > avgPricePerDay).Select(y => y.Price_per_ticket).ToArray();
            
     

            
            MyOwnCollection myowncollection = new MyOwnCollection(); 
            double fixedYearlyCostsII = myowncollection.Select(s => s.Costs_per_year).Sum();

            double[] ys = new double[1000];

            ys = ys.Select((x, i) => Math.Sin(i * Math.PI * 2 / 1000)).ToArray();

            double effective = Math.Sqrt(ys.Select(d => d * d).Average());
            
            
            // LINQ Exercise 
            // Unique Values
            List<String> list = new List<string>() { "abc", "xyz", "klm", "xyz", "abc", "abc", "rst" };
            list = list.GroupBy(x => x).Where(x => x.Count() == 1).Select(x => x.Key).ToList();
            foreach (var s in list)
            {
                Console.WriteLine((s));
            }
            
            // UpperCase 
            string word = "DDD example CQRS Event Sourcing";
            var uppercaseOnly = word.Split(' ').Where(x => string.Equals(x, x.ToUpper()));
            
            // Frequency letter 
            string gam = "gamma"; 
            var letters = gam.GroupBy(x => x);
            foreach (var l in letters)
            {
                Console.Write($"Letter {l.Key} occurs {l.Count()} time(s), ");
            }
            
            // shuffle Array
            int[] shuffle = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var rnd = new Random();
            shuffle = shuffle.OrderBy(x => rnd.Next()).ToArray();
            
            // Numbers from Range 
            int[] range = new int[] { 67, 92, 153, 15 };
            range = range.Where((x) => x < 100 && x > 25).ToArray();
            
            // Min length 
            string[] minLength = new string[] { "Computer", "usb" };
            minLength = minLength.Where(x => x.Length > 5).Select(x => x.ToUpper()).ToArray(); 

            // top 5 numbers 
            int[] top5 = new int[] { 78, -9, 0, 23, 54, 21, 7, 86 };
            top5 = top5.OrderByDescending(x => x).Where((x,i) => i < 5).ToArray();
            
            // Squares 
            int[] squares = new int[] { 7, 2, 30 };
            squares = squares.Where(x => x*x > 20).Select(x => x * x).ToArray();
        
            // Replace ea with * 
            string[] replaceSubstring = new String[] { "learn", "current", "deal" };
            replaceSubstring = replaceSubstring.Select(x => x.Replace("ea", "*")).ToArray();

            // Word contains e return one word 
            var words2 = new List<string> { "cow", "dog", "elephant", "cat", "rat", "squirrel", "snake", "stork" };
            var w = words2.OrderBy(x => x).LastOrDefault(w => w.Contains("e"));
            
            // Transponse Array 
            var array = new int[][] {
                new int[]{ 1, 2, 3, 4, 5 },
                new int[]{ 6, 7, 8, 9, 10 },
               };

            var transposedArray = Enumerable.Range(0, array.Length).Select(x => array.Select(y => y[x]));
            
            foreach (var row in transposedArray)
            {
                foreach (var number in row)
                {
                    Console.Write(number + " ");
                }
                Console.WriteLine();
            }
            */
            
            // more examples 
            int [] samecosts = myRides.GroupBy(x => x.Visitors_per_day).Where(x => x.Count() > 1).Select(x => x.Key).ToArray();

            double avgCosts = myRides.Select(x => x.Costs_per_year).Average();
            string[] namesOfSameVisitors = myRides
                .OrderByDescending(x => (x.Visitors_per_day * x.Price_per_ticket) - x.Costs_per_day)
                .Where(x => x.Costs_per_year < avgCosts)
                .Select(x => x.Designation).ToArray();
            
            // LINQ Exercise 
            // Unique Values
            List<String> list = new List<string>() { "abc", "xyz", "klm", "xyz", "abc", "abc", "rst" };
            
            // UpperCase 
            string word = "DDD example CQRS Event Sourcing";

            // Frequency letter 
            string gam = "gamma";
            
          
            // Numbers from Range 
            int[] range = new int[] { 67, 92, 153, 15 };
            
            // Min length 
            string[] minLength = new string[] { "Computer", "usb" };

            int[] top5 = new int[] { 78, -9, 0, 23, 54, 21, 7, 86 };
            // Squares 
            int[] squares = new int[] { 7, 2, 30 };
            
            
            // Word contains e return one word 
            var words2 = new List<string> { "cow", "dog", "elephant", "cat", "rat", "squirrel", "snake", "stork" };
            // Replace ea with * 
            string[] replaceSubstring = new String[] { "learn", "current", "deal" };
            
        }


    }
}
