using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Garden.Shared.Models;

[CollectionName("Roles")]
public class ApplicationRole: MongoIdentityRole<Guid>
{
    
}