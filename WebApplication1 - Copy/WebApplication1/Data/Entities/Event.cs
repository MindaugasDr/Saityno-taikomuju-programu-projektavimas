using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Entities
{
    public class Event
    {

        [Key]
        public int Event_id { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
        public int CurrentSum { get; set; }
        public string User_id { get; set; }
        public int Item_id { get; set; }


        //public Event(string action, DateTime creationDate, int currentSum, string user_id, int item_id)
        //{
        //    this.Action = action;
        //    this.Date = creationDate;
        //    this.CurrentSum = currentSum;
        //    this.User_id = user_id;
        //    this.Item_id = item_id;
        //}
    }
}
