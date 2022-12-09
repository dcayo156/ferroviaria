using LaJuana.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Infrastructure.Persistence
{
    public class LaJuanaDbContextSeed
    {
        public static async Task SeedAsync(LaJuanaDbContext context, ILogger<LaJuanaDbContextSeed> logger)
        {
            //if (!context.Tags!.Any())
            //{
            //    context.Tags!.AddRange(GetPreconfiguredTag());
            //    await context.SaveChangesAsync();
            //    logger.LogInformation("Estamos insertando nuevos records al db {context}", typeof(LaJuanaDbContext).Name);
            //}
        
        }

        //private static IEnumerable<Tag> GetPreconfiguredTag()
        //{
        //    return new List<Tag>
        //    {
        //        new Tag {CreatedBy = "pepeargento", Name = "Comedor", Description = "Asiste regularmente al comedor" },
        //        new Tag {CreatedBy = "pepeargento", Name = "Vianda", Description = "Retira regularmente la vianda" },
        //    };
        
        //}


    }
}
