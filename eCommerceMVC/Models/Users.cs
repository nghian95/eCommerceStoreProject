using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCommerceMVC.Models
{
    public partial class Users
    {
        public Users()
        {
            Transactions = new HashSet<Transactions>();
        }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        [RegularExpression("[a-zA-Z]+\\d*[a-zA-Z]*|[a-zA-Z]*\\d*[a-zA-Z]w+", ErrorMessage ="There needs to be letters")]
        public string UserName { get; set; }
        [Required]
        //[StringLength(30, MinimumLength = 8)]
        //[RegularExpression("(\\S*\\d+\\S*)", ErrorMessage = "There needs to be a number.")]-~=# squiggly bad
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&-])[A-Za-z\\d@$!%*?&-]{8,30}$", ErrorMessage = "There needs to be a lowercase and uppercase letter, a number, a symbol and at least 8 characters.")]
        public string Password { get; set; }
        [Required]
        public int Access { get; set; }
        [StringLength(25)]
        [RegularExpression("[a-zA-Z]+\\s{0,1}[a-zA-Z]*\\.?", ErrorMessage = "It must only contain letters and/or Jr. or Sr.")]
        public string FirstName { get; set; }
        [StringLength(25)]
        [RegularExpression("[a-zA-Z]+\\s{0,1}[a-zA-Z]*\\.?", ErrorMessage = "It must only contain letters and/or Jr. or Sr.")]
        public string LastName { get; set; }
        [StringLength(100)]
        [RegularExpression("\\d{1,5}\\s[a-zA-Z]+[\\w\\s\\.\\,]*", ErrorMessage = "Your address is not formatted correctly.")]
        public string Address { get; set; }
        [RegularExpression("\\d{10}", ErrorMessage = "It has to be 10 digits.")]
        //[RegularExpression("\\d{0,4}-*\\d{0,3}\\}?(\\d{10}|\\d{3}-\\d{3}-\\d{4})", ErrorMessage = "It can either be formatted like 111-111-1111 or 1111111111. Country codes are optional")]
        public int? Phone { get; set; }
        [StringLength(50)]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
    }
}
