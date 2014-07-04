using Microsoft.AspNet.Identity;
using System;
using ServiceStack.DataAnnotations;

namespace AspNet.Identity.ServiceStack
{
    /// <summary>
    /// Class that implements the ASP.NET Identity
    /// IUser interface 
    /// </summary>
    [Alias("AspNetUsers")]
    public class IdentityUser : IUser
    {
        /// <summary>
        /// Default constructor 
        /// </summary>
        public IdentityUser()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Constructor that takes user name as argument
        /// </summary>
        /// <param name="userName"></param>
        public IdentityUser(string userName)
            : this()
        {
            UserName = userName;
        }

        /// <summary>
        /// User ID
        /// </summary>
        [PrimaryKey]
        public string Id { get; set; }

        /// <summary>
        /// User's name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User's password hash
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// User's security stamp
        /// </summary>
        public string SecurityStamp { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }
    }
}
