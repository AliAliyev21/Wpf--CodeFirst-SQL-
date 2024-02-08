using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp13.Entities;

namespace WpfApp13.DataAccess
{
    public class MyContext : DbContext
    {
        public MyContext()
            : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyDbTest;") { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
