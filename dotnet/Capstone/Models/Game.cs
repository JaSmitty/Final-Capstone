using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public int OrganizerId { get; set; }
        public string OrganizerName { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime StartDate { get; set; }
        public long StartDateAsTicks
        {
            get
            {
                return this.StartDate.Ticks;
            }
        }
        public string StartDateAsString
        {
            get
            {
                return this.StartDate.ToString("MM/dd/yyyy");
            }
        }
        public DateTime EndDate { get; set; }
        public long EndDateAsTicks
        {
            get
            {
                return this.EndDate.Ticks;
            }
        }
        public string EndDateAsString
        {
            get
            {
                return this.EndDate.ToString("MM/dd/yyyy");
            }
        }
        public decimal Balance { get; set; }

        //public Game(int organizer_id, string name, DateTime end_date, decimal balance)
        //{
        //    Organizer_ID = organizer_id;
        //    Name = name;
        //    End_Date = end_date;
        //    this.Balance = balance;
        //}


    }
}
