using System.Collections.Generic;

namespace weekly.Models
{
    public class Week
    {
        public int WeekNr { get; set; }
        public List<Day> Days { get; set; }
    }

    public class Day
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public List<ToDo> ToDos { get; set; }
    }
}