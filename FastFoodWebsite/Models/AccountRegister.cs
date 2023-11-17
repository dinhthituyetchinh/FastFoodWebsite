using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastFoodWebsite.Models
{
    public class AccountRegister
    {
        DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();
        [Required(ErrorMessage = "Họ tên không để trống")]
        public string fullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage ="Sai định dạng số điện thoại")]
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

        //public int roleId { get; set; }
        public AccountRegister()
        {

        }
        public AccountRegister(string fullName, string phone, string email, DateTime dayOfBirth, string address, string password, string confirmPassword)
        {
            handleRegister(fullName, phone, email, dayOfBirth, address, password);
            this.fullName = fullName;           
            this.phone = phone;
            this.email = email;
            this.dayOfBirth = dayOfBirth;
            this.address = address;
            this.password = password;
            this.confirmPassword = confirmPassword;

        }
        public USER handleRegister(string fullName, string phone, string email, DateTime dayOfBirth, string address, string password)
        {
            USER user = new USER() { FULLNAME = fullName, PHONE = phone, EMAIL = email, DAYOFBIRTH = dayOfBirth, USERADDRESS = address, USERPASSWORD = password, ROLEID = 2, CREATEDAT = DateTime.Now};

            return user;
        }
    }
}