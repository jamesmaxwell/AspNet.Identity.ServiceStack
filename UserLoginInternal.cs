using ServiceStack.DataAnnotations;

namespace AspNet.Identity.ServiceStack
{
    [Alias("AspNetUserLogins")]
    internal class UserLoginInternal
    {
        public string UserId { get; set; }

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
