using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models.Classes
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Name is required !")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Surname is required !")]
        public string UserSurname { get; set; }

        [Required(ErrorMessage = "Mail is required !")]
        public string UserMail { get; set; }

        [Required(ErrorMessage ="Password is required !")]
        [Compare("ComparePassword",ErrorMessage ="Passwords does not match !")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Re-entered password is required !")]
        public string ComparePassword { get; set; }

        public bool Logged { get; set; }
    }
}