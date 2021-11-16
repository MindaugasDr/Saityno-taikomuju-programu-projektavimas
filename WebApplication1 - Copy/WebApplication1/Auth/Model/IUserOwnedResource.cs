using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidToBuy.Auth.Model
{
    public interface IUserOwnedResource
    {
        string User_id { get; }
    }
}
