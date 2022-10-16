using Microsoft.EntityFrameworkCore;

namespace email_sending_service.Data
{
    public class HangfireContext : DbContext
    {
        public HangfireContext(DbContextOptions<HangfireContext> options) : base(options) {  }
    }
}
