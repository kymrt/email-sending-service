using email_sending_service.Models;
using Microsoft.EntityFrameworkCore;

namespace email_sending_service.Data
{
    public class EmailDataContext : DbContext
    {
        public EmailDataContext(DbContextOptions<EmailDataContext> options) : base(options) { }

        public DbSet<EmailInfo> EmailInfo { get; set; }
    }
}
