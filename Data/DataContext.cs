using Microsoft.EntityFrameworkCore;
// using Galleass.Models

namespace Galleass.Data {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions options) : base (options) { }

    }
}