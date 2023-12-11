using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastFoodWebsite.Models
{
    public class Contact
    {

       [Required(ErrorMessage = "Họ tên không để trống")]
        public string fullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage ="Sai định dạng số điện thoại")]
        public string phone { get; set; }
        [Required(ErrorMessage = "Email không để trống")]
        [EmailAddress()]
        public string email { get; set; }

        [Required(ErrorMessage = "Comments không để trống")]
        public string comments { get; set; }
        public Contact()
        {

        }

        public Contact(string fullName, string phone, string email, string comments)
        {
            this.fullName = fullName;
            this.phone = phone;
            this.email = email;
            this.comments = comments;
        }
    }
}