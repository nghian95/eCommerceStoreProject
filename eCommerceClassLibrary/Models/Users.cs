using System;
using System.Collections.Generic;

namespace eCommerceClassLibrary.Models
{
    public partial class Users
    {
        public Users()
        {
            Transactions = new HashSet<Transactions>();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public int Access { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
    }
}
