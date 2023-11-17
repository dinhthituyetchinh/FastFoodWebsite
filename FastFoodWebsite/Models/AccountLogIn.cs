using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FastFoodWebsite.Models
{
    public class AccountLogIn
    {
        DB_FASTFOODDataContext db = new DB_FASTFOODDataContext();
        //[EmailAddress(ErrorMessage ="Email sai định dạng")]
        //[Phone(ErrorMessage ="Số điện thoại không đúng")]
        [Required(ErrorMessage = "Bạn phải nhập số điện thoại hoặc email")]
        public string emailOrPhone { get; set; } // username ở đây là email or phone
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string password { get; set; }

        public AccountLogIn()
        {

        }
        public AccountLogIn(string username, string password)
        {
            handleLogin(username, password);
            this.emailOrPhone = username;
            this.password = password;
        }

        public USER handleLogin(string emailOrPhone, string password)
        {
            USER nguoiDung = db.USERs.SingleOrDefault(user => user.EMAIL == emailOrPhone || user.PHONE == emailOrPhone && user.USERPASSWORD == password ) ;
            if (nguoiDung == null)
            {

                return null;
            }

            return nguoiDung;
        }

    }
}