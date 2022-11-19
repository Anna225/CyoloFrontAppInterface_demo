using CyoloFrontAppInterface.Data;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CyoloFrontAppInterface.Models
{
    public class Register
    {
        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        public string? ConfirmPassword { get; set; }

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
                result = await ls.IsExist(userdto);
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
        public async Task<bool> Store(string _useremail, string _password)
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
