using CEZ.AngularDemo.WebAPI.Infrastructure.Utils.Enums;
using Microsoft.EntityFrameworkCore;

namespace CEZ.AngularDemo.WebAPI.Infrastructure.Persistence.Entities
{
    public partial class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<ChangeType> ChangeTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region relationship definition
            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(x => x.Id).HasName("PK_User");
                entity.Property(x => x.Name)
                            .IsRequired()
                            .HasMaxLength(50);
                entity.Property(x => x.Surname)
                            .IsRequired()
                            .HasMaxLength(50);
                entity.Property(x => x.Birthday)
                            .IsRequired();
                entity.Property(x => x.Email)
                            .IsRequired();
                entity.Property(x => x.Telephone);
                entity.HasIndex(x => x.CountryId, "IX_User_Country");
                entity.HasOne(x => x.Country)
                      .WithMany(x => x.Users)
                      .HasForeignKey(x => x.CountryId)
                      .HasConstraintName("FK_User_Country");
                entity.Property(x => x.WishesToBeContacted)
                            .IsRequired();
                entity.Property(x => x.Active)
                            .IsRequired().HasDefaultValue(true);
            });

            // History
            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("History");
                entity.HasKey(x => x.Id).HasName("PK_History");
                entity.Property(x => x.ChangeType)
                            .IsRequired();
                entity.Property(x => x.PrevValue)
                            .HasMaxLength(50);
                entity.Property(x => x.CurrValue)
                            .HasMaxLength(50);
                entity.Property(x => x.ChangeDate)
                            .IsRequired()
                            .HasDefaultValue(new DateTime());
                entity.HasIndex(x => x.UserId, "IX_History_User");
                entity.HasOne(x => x.User)
                      .WithMany(x => x.ChangeHistory)
                      .HasForeignKey(x => x.UserId)
                      .HasConstraintName("FK_History_User");
            });

            // Change Type
            modelBuilder.Entity<ChangeType>(entity => {
                entity.ToTable("ChangeType");
                entity.HasKey(x => x.Id);
            });

            // Country
            modelBuilder.Entity<Country>(entity => {
                entity.ToTable("Country");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Code)
                      .HasMaxLength(3);
            });
			#endregion

			#region Seed
			modelBuilder.Entity<ChangeType>().HasData(
				new ChangeType
				{
					Id = (int)ChangeTypeEnum.New,
					Name = "User created"
				},
				new ChangeType
				{
					Id = (int)ChangeTypeEnum.Update,
					Name = "User Updated"
				},
				new ChangeType
				{
					Id = (int)ChangeTypeEnum.Delete,
					Name = "User Deleted"
				}				
			);
			modelBuilder.Entity<Country>().HasData(
				new Country
				{
					Id = (int)CountryEnum.UK,
					Name = "United Kingdom", Code = "GBR", Value = 44
				},
                new Country
				{
					Id = (int)CountryEnum.USA,
					Name = "United States of America", Code = "USA", Value = 1
				},
                new Country
				{
					Id = (int)CountryEnum.CostaRica,
					Name = "Costa Rica", Code = "CRI", Value = 506
				},
                new Country
				{
					Id = (int)CountryEnum.Argentina,
					Name = "Argentina", Code = "ARG", Value = 54
				}
			);
            #endregion
            OnModelCreatingPartial(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
