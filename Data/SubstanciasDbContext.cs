using Microsoft.EntityFrameworkCore;

public class SubstanciasDbContext : DbContext
{
    public SubstanciasDbContext(DbContextOptions<SubstanciasDbContext> options) : base(options) { }
    
    public DbSet<Substancia> Substancias { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Propriedade> Propriedades { get; set; }
    public DbSet<SubstanciaPropriedade> SubstanciaPropriedades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SubstanciaPropriedade>()
        .HasKey(sp => new { sp.SubstanciaId, sp.PropriedadeId });


        modelBuilder.Entity<SubstanciaPropriedade>()
        .HasOne(sp => sp.Substancia)
        .WithMany(s => s.Propriedades)
        .HasForeignKey(sp => sp.SubstanciaId);
        modelBuilder.Entity<SubstanciaPropriedade>()
        .HasOne(sp => sp.Propriedade)
        .WithMany(p => p.Substancias)
        .HasForeignKey(sp => sp.PropriedadeId);

        modelBuilder.Entity<Substancia>()
        .HasIndex(s => s.Codigo)
        .IsUnique();
    }


}