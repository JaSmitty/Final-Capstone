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
        public DateTime EndDate { get; set; }
        public string DateAsString
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
