using System.Collections.Generic;
using ServiceStack.OrmLite;

namespace AspNet.Identity.ServiceStack
{
    /// <summary>
    /// Class that represents the Users table in the MySQL Database
    /// </summary>
    public class UserTable
    {
        private MsSQLDatabase _database;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public UserTable(MsSQLDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Returns the user's name given a user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserName(string userId)
        {
            using (var db = _database.Open())
            {
                return db.Scalar<string>(db.From<IdentityUser>().Select(x => x.UserName).Where(x => x.Id == userId));
            }
        }

        /// <summary>
        /// Returns a User ID given a user name
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        public string GetUserId(string userName)
        {
            using (var db = _database.Open())
            {
                return db.Scalar<string>(db.From<IdentityUser>().Select(x => x.Id).Where(x => x.UserName == userName));
            }
        }

        /// <summary>
        /// Returns an IdentityUser given the user's id
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public IdentityUser GetUserById(string userId)
        {
            using (var db = _database.Open())
            {
                return db.SingleById<IdentityUser>(userId);
            }
        }

        /// <summary>
        /// Returns a list of IdentityUser instances given a user name
        /// </summary>
        /// <param name="userName">User's name</param>
        /// <returns></returns>
        public List<IdentityUser> GetUserByName(string userName)
        {
            using (var db = _database.Open())
            {
                return db.Select<IdentityUser>(x => x.UserName == userName);
            }
        }

        /// <summary>
        /// Return the user's password hash
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public string GetPasswordHash(string userId)
        {
            using (var db = _database.Open())
            {
                return db.Scalar<string>(db.From<IdentityUser>().Select(x => x.PasswordHash).Where(x => x.Id == userId));
            }
        }

        /// <summary>
        /// Sets the user's password hash
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public int SetPasswordHash(string userId, string passwordHash)
        {
            using (var db = _database.Open())
            {
                return db.UpdateOnly<IdentityUser>(new IdentityUser() { PasswordHash = passwordHash },
                    onlyFields: q => q.Update(p => p.PasswordHash).Where(p => p.Id == userId));
            }
        }

        /// <summary>
        /// Returns the user's security stamp
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetSecurityStamp(string userId)
        {
            using (var db = _database.Open())
            {
                return db.Scalar<string>(db.From<IdentityUser>().Select(x => x.SecurityStamp).Where(x => x.Id == userId));
            }
        }

        /// <summary>
        /// Inserts a new user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Insert(IdentityUser user)
        {
            using (var db = _database.Open())
            {
                return (int)db.Insert<IdentityUser>(user);
            }
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        private int Delete(string userId)
        {
            using (var db = _database.Open())
            {
                return db.Delete<IdentityUser>(x => x.Id == userId);
            }
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Delete(IdentityUser user)
        {
            return Delete(user.Id);
        }

        /// <summary>
        /// Updates a user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Update(IdentityUser user)
        {
            using (var db = _database.Open())
            {
                return db.Update<IdentityUser>(user);
            }
        }
    }
}
