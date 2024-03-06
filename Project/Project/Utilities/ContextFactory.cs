using Microsoft.EntityFrameworkCore;
using Project.Data;

namespace Project.Utilities
{
    public class ContextFactory
    {
        public KcalContext CreateContext()
        {
            var oB = new DbContextOptionsBuilder<KcalContext>();
            var conf = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = conf.GetConnectionString("KcalContext");
            oB.UseSqlServer(connectionString);
            return new KcalContext(oB.Options);
        }
    }
}
