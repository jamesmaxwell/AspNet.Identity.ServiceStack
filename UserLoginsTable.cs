using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using ServiceStack.Logging;
using ServiceStack.OrmLite;

namespace AspNet.Identity.ServiceStack
{
    /// <summary>
    /// Class that represents the UserLogins table in the MySQL Database
    /// </summary>
    public class UserLoginsTable
    {
        private MsSQLDatabase _database;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public UserLoginsTable(MsSQLDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Deletes a login from a user in the UserLogins table
        /// </summary>
        /// <param name="user">User to have login deleted</param>
        /// <param name="login">Login to be deleted from user</param>
        /// <returns></returns>
        public int Delete(IdentityUser user, UserLoginInfo login)
        {
            using (var db = _database.Open())
            {
                return db.Delete<UserLoginInternal>(q => q.Where(x => x.UserId == user.Id && x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey));
            }
        }

        /// <summary>
        /// Deletes all Logins from a user in the UserLogins table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete(string userId)
        {
            using (var db = _database.Open())
            {
                return db.Delete<UserLoginInternal>(q => q.Where(x => x.UserId == userId));
            }
        }

        /// <summary>
        /// Inserts a new login in the UserLogins table
        /// </summary>
        /// <param name="user">User to have new login added</param>
        /// <param name="login">Login to be added</param>
        /// <returns></returns>
        public int Insert(IdentityUser user, UserLoginInfo login)
        {
            using (var db = _database.Open())
            {
                return (int)db.Insert(new UserLoginInternal { UserId = user.Id, LoginProvider = login.LoginProvider, ProviderKey = login.ProviderKey });
            }
        }

        /// <summary>
        /// Return a userId given a user's login
        /// </summary>
        /// <param name="userLogin">The user's login info</param>
        /// <returns></returns>
        public string FindUserIdByLogin(UserLoginInfo userLogin)
        {
            using (var db = _database.Open())
            {
                return db.Scalar<string>(db.From<UserLoginInternal>().Select(x => x.UserId).Where(x => x.LoginProvider == userLogin.LoginProvider && x.ProviderKey == userLogin.ProviderKey));
            }
        }

        /// <summary>
        /// Returns a list of user's logins
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<UserLoginInfo> FindByUserId(string userId)
        {
            List<UserLoginInfo> logins = new List<UserLoginInfo>();
            using (var db = _database.Open())
            {
                var loginInfos = db.Select<UserLoginInternal>(q => q.Where(x => x.UserId == userId));
                foreach (var loginInfo in loginInfos)
                {
                    logins.Add(new UserLoginInfo(loginInfo.LoginProvider, loginInfo.ProviderKey));
                }
            }
            return logins;
        }
    }
}
