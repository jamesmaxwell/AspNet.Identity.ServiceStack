using System.Collections.Generic;
using System.Security.Claims;
using ServiceStack.OrmLite;

namespace AspNet.Identity.ServiceStack
{
    /// <summary>
    /// Class that represents the UserClaims table in the MySQL Database
    /// </summary>
    public class UserClaimsTable
    {
        private MsSQLDatabase _database;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public UserClaimsTable(MsSQLDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Returns a ClaimsIdentity instance given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public ClaimsIdentity FindByUserId(string userId)
        {
            ClaimsIdentity claims = new ClaimsIdentity();

            using (var db = _database.Open())
            {
                var claimInners = db.Select<ClaimInternal>(q => q.Where(x => x.UserId == userId));
                foreach (var claim in claimInners)
                {
                    claims.AddClaim(new Claim(claim.ClaimType, claim.ClaimValue));
                }
            }
            return claims;
        }

        /// <summary>
        /// Deletes all claims from a user given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete(string userId)
        {
            using (var db = _database.Open())
            {
                return db.Delete<ClaimInternal>(x => x.UserId == userId);
            }
        }

        /// <summary>
        /// Inserts a new claim in UserClaims table
        /// </summary>
        /// <param name="userClaim">User's claim to be added</param>
        /// <param name="userId">User's id</param>
        /// <returns></returns>
        public int Insert(Claim userClaim, string userId)
        {
            using (var db = _database.Open())
            {
                return (int)db.Insert<ClaimInternal>(
                    new ClaimInternal { UserId = userId, ClaimType = userClaim.Type, ClaimValue = userClaim.Value });
            }
        }

        /// <summary>
        /// Deletes a claim from a user 
        /// </summary>
        /// <param name="user">The user to have a claim deleted</param>
        /// <param name="claim">A claim to be deleted from user</param>
        /// <returns></returns>
        public int Delete(IdentityUser user, Claim claim)
        {
            using (var db = _database.Open())
            {
                return db.Delete<ClaimInternal>(q => q.Where(x =>
                    x.UserId == user.Id &&
                    x.ClaimType == claim.Type &&
                    x.ClaimValue == claim.Value));
            }
        }
    }
}
