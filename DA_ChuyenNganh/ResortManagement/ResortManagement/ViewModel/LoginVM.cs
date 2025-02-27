﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResortManagement.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        public string Password { get; set; }
    }
}
