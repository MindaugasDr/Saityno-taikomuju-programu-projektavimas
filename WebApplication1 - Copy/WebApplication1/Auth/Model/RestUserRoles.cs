using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidToBuy.Auth.Model
{
    public class RestUserRoles
    {
        public const string Admin = nameof(Admin);
        public const string RegularUser = nameof(RegularUser);
        public static readonly IReadOnlyCollection<string> All = new[] { Admin, RegularUser };
    }
}
