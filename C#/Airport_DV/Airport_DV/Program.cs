using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_DV
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(160, 50);
            Console.Title = "Airport";
            Console.WriteLine("It's a search of airlines. How many lines you want to see?");
            int allFlights = int.Parse(Console.ReadLine());
            Console.Clear();
            Flight flight = new Flight();

            flight.GenerateFlightes(allFlights);
            flight.DisplayAllFlights();

            Flight.DisplaySearchOptions();
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
            Flight.IsContinueSearch(flight);
        }
    }
}