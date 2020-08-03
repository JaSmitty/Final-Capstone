using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Game
    {
        public int Game_ID { get; set; }
        public int Organizer_ID { get; set; }
        public string Organizer_Name { get; set; }
        public string Name { get; set; }
        public DateTime End_Date { get; set; }
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
