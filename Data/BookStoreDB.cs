using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class BookStoreDB : DbContext
    {
        public BookStoreDB(DbContextOptions<BookStoreDB> options) : base(options)
        { }
        public DbSet<Books> Books { get; set; }
        public DbSet<Language> Language { get; set; }
    }
}
