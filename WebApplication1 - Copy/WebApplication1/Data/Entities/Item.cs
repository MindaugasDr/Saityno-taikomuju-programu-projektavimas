using BidToBuy.Auth.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.Entities
{
    public class Item: IUserOwnedResource
    {
        [Key]
        public int Item_id { get; set; }
        public string Item_name { get; set; }
        public string Item_description { get; set; }
        public DateTime CreationDate { get; set; }
        public string User_id { get; set; }
        //public DateTime CreationDate { get; set; }

    }
}
