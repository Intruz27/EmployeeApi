using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FullStackApi.Data
{
    public class AuthDbContext: IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "589465c3-7396-46a4-8bf0-40203342faae";
            var writerRoleId = "aa2cf69c-a91a-4967-ba8c-18b8f6d54f60";

            var roles = new List<IdentityRole> {
            new IdentityRole {
            Id = readerRoleId,
             Name = "Reader",
             NormalizedName = "Reader".ToUpper(),
             ConcurrencyStamp = readerRoleId
            },
             new IdentityRole {
             Id = writerRoleId,
             Name = "Writer",
             NormalizedName = "Writer".ToUpper(),
             ConcurrencyStamp = writerRoleId
            }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
