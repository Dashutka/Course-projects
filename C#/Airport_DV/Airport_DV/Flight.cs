using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_DV
{
    public class Flight
    {
        public enum FlightStatus
        {
            CheckIn,
            GateClosed,
            Arrived,
            DepartedAt,
            Unknown,
            Cancelled,
            ExpectedAt,
            Delayed,
            InFlight
        }
        public struct FlightObj
        {
            public DateTime At;
            public int Number;
            public string City;
            public string AirLine;
            public int Terminal;
            public FlightStatus FlightStatus;

            public override string ToString()
            {
                return $"{At,20} {Number,20} {City,20} {AirLine,20} {Terminal,10} {FlightStatus,20}";
            }
        }
        private FlightObj[] fly;

        public void GenerateFlightes(int countOfFlights)
        {
            fly = new FlightObj[countOfFlights];
            Random r = new Random(DateTime.Now.Minute);
            DateTime now = DateTime.Now;
            string[] cities = new string[] { "Kharkiv", "Kiev", "Lviv", "Paris", "Oslo", "Munich" };
            string[] airLines = new string[] { "MAU", "WizzAir", "Pegasus", "DubaiAirLines" };

            for (int i = 0; i < fly.Length; i++)
            {
                fly[i].At = now.AddDays(r.Next(30)).AddMinutes(r.Next(1440));
                fly[i].Number = r.Next(0, 45);
                fly[i].City = cities[r.Next(0, cities.Length - 1)];
                fly[i].AirLine = airLines[r.Next(0, airLines.Length - 1)];
                fly[i].Terminal = r.Next(1, 23);
                fly[i].FlightStatus = (FlightStatus)r.Next(8);
            }
        }

        public void DisplayAllFlights()
        {
            DisplayTitle();
            for (int i = 0; i < fly.Length; i++)
            {
                SortingByDateTime();
                if (fly[i].FlightStatus == FlightStatus.Cancelled)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(fly[i]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                if (fly[i].FlightStatus == FlightStatus.Delayed)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{fly[i]} {DateTime.Now.Minute} minutes");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                    Console.WriteLine(fly[i]);
            }
        }
        public void SearchByNumber(int searchNumber)
        {
            DisplayTitle();
            for (int i = 0; i < fly.Length; i++)
            {
                SortingByDateTime();
                if (fly[i].Number == searchNumber)
                    Console.WriteLine(fly[i]);
            }
        }

        public void SearchByArrivalTime(DateTime date)
        {
            DisplayTitle();
            for (int i = 0; i < fly.Length; i++)
            {
                SortingByDateTime();
                var diff = date - fly[i].At;
                var span = new TimeSpan(0, 30, 0);
                if (diff < span)
                    Console.WriteLine(fly[i]);
            }
        }

        public void SearchByPort(string city)
        {
            bool TEKCT_BbIBEgEH = false;
            for (int i = 0; i < fly.Length; i++)
            {
                SortingByDateTime();
                if (fly[i].City.ToUpper() == city.ToUpper())
                {
                    if (TEKCT_BbIBEgEH == false)
                    {
                        DisplayTitle();
                        TEKCT_BbIBEgEH = true;
                    }
                    Console.WriteLine(fly[i]);
                }
                if (TEKCT_BbIBEgEH == false)
                {
                    Console.WriteLine($"The {city} city was not found");
                    TEKCT_BbIBEgEH = true;
                }
            }
        }

        public void SearchByOneHour()
        {
            DisplayTitle();
            for (int i = 0; i < fly.Length; i++)
            {
                SortingByDateTime();
                if (DateTime.Now >= fly[i].At && fly[i].At <= DateTime.Now.AddMinutes(60))
                    Console.WriteLine(fly[i]);
            }
        }
        public void DisplayTitle()
        {
            Console.WriteLine($"{"Time of arrival",20} {"Flight number",24} {"City",16} {"AirLine",20} {"Terminal",14} {"FlightStatus",16}");
            Console.WriteLine();
        }

        private void SortingByDateTime()
        {
            for (int j = 0; j < fly.Length - 1; j++)
            {
                if (fly[j].At > fly[j + 1].At)
                {
                    DateTime temp = fly[j + 1].At;
                    fly[j + 1].At = fly[j].At;
                    fly[j].At = temp;
                }
            }
        }
        public static void DisplaySearchOptions()
        {
            Console.WriteLine();
            Console.WriteLine("If you want to search some flight, please select one of the options:");
            Console.WriteLine("1. by the flight number");
            Console.WriteLine("2. by time of arrival");
            Console.WriteLine("3. by arrival (departure) port");
            Console.WriteLine("4. the flight which is the nearest (1 hour)");
        }

        public static void IsContinueSearch(Flight flight)
        {
            Console.WriteLine("Would you like to continue search? Please enter Y / N.");
            char povtornbIyPoisk = char.Parse(Console.ReadLine());
            while (povtornbIyPoisk == 'Y' || povtornbIyPoisk == 'y')
            {
                Console.Clear();
                DisplaySearchOptions();
                int selectUserSearch = int.Parse(Console.ReadLine());

                switch (selectUserSearch)
                {
                    case 1:
                        Console.WriteLine("Please enter a number for searching ");
                        int searchNumber = int.Parse(Console.ReadLine());
                        flight.SearchByNumber(searchNumber);
                        break;
                    case 2:
                        Console.WriteLine("Please enter a day and time for searching in format 'dd/mm/yyyy hh:mm AM':");
                        DateTime date;
                        string dateString = Console.ReadLine();
                        bool isChecked = DateTime.TryParse(dateString, CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.None, out date);
                        flight.SearchByArrivalTime(date);
                        break;
                    case 3:
                        Console.WriteLine("Please enter a city from/where");
                        string searchCity = Console.ReadLine();
                        flight.SearchByPort(searchCity);
                        break;
                    case 4:
                        flight.SearchByOneHour();
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Would you like to continue search? Please enter Y / N.");
                povtornbIyPoisk = char.Parse(Console.ReadLine());
            }
        }
    }
}