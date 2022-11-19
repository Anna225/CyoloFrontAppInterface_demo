using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CyoloFrontAppInterface.Data;

namespace CyoloFrontAppInterface.Models
{
    public class User
    {
        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_useremail">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public async Task<bool> IsValid(string _useremail, string _password)
        {
            UserDto userdto = new UserDto
            {
                Email = _useremail,
                Password = _password,
                Remember_token = Convert.ToInt32(true)
            };

            BackendServerAPI ls = new BackendServerAPI();

            bool result = false;
            try
            {
                result = await ls.Login(userdto);
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex);
            }
            return result;
        }

        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_useremail">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public async Task<bool> Register(string _useremail, string _password)
        {
            UserDto userdto = new UserDto
            {
                Email = _useremail,
                Password = _password,
                Remember_token = Convert.ToInt32(false)
            };

            BackendServerAPI ls = new BackendServerAPI();

            bool result = false;
            try
            {
                result = await ls.Register(userdto);
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex);
            }
            return result;
        }
    }
}