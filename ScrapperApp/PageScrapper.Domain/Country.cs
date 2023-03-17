namespace PageScrapper.Domain
{
    public class Country
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public int Population { get; set; }
        public double Area { get; set; }
        public int Id { get; set; }

        public Country(string name, string capital, int population, double area)
        {
            Name = name;
            Population = population;
            Area = area;
            Capital= capital;
        }


    }
}