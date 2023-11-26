using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastFoodWebsite.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Họ tên không để trống")]
        public string fullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Sai định dạng số điện thoại")]
        public string phone { get; set; }
        [Required(ErrorMessage = "Email không để trống")]
        [EmailAddress()]
        public string email { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        public DateTime dayOfBirth { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string confirmPassword { get; set; }
        [Required(ErrorMessage = "Địa chỉ không để trống")]
        public string address { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "The updated date product can't be blank!")]
        public DateTime UpdatedDate { get; set; }

        [Required(ErrorMessage = "The product type can't be blank!")]
        public int roleID { get; set; }
        public User()
        {

        }
        public User(int userId, string fullName, string phone, string email, DateTime dayOfBirth, string password, string address, DateTime createdDate, DateTime updatedDate, int roleID)
        {
            UserId = userId;
            this.fullName = fullName;
            this.phone = phone;
            this.email = email;
            this.dayOfBirth = dayOfBirth;
            this.password = password;
            this.address = address;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            this.roleID = roleID;
        }
    }
}