using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Garden.Shared.Models;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{
    
}