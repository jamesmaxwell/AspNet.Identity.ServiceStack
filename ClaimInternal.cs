using ServiceStack.DataAnnotations;

namespace AspNet.Identity.ServiceStack
{
    [Alias("AspNetUserClaims")]
    internal class ClaimInternal
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
