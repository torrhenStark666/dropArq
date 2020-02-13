using Microsoft.EntityFrameworkCore;
using dropArquivo.Models;

namespace dropArquivo.Context
{
    public class DropArquivoDbContext : DbContext
    {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<File> Files { get; set; }

        public DropArquivoDbContext ( DbContextOptions<DropArquivoDbContext> options) : base(options){ }

        public DropArquivoDbContext() { }

    }
}