namespace _Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OnlineShop : DbContext
    {
        public OnlineShop()
            : base("name=OnlineShop")
        {
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<About>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<About>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<About>()
                .Property(e => e.ModifiedDate)
                .IsUnicode(false);

            modelBuilder.Entity<About>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.ModifiedDate)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.Tags)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerAccount>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerAccount>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerAccount>()
                .Property(e => e.GioiTinh)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerAccount>()
                .Property(e => e.ConfirmPassWord)
                .IsUnicode(false);

            modelBuilder.Entity<MenuType>()
                .HasMany(e => e.Menus)
                .WithRequired(e => e.MenuType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<News>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.ModifiedDate)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .HasMany(e => e.Contents)
                .WithRequired(e => e.News)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permission>()
                .Property(e => e.PermissionName)
                .IsFixedLength();

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.UserAccounts)
                .WithRequired(e => e.Permission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ModifiedDate)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.ModifiedDate)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Slide>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Slide>()
                .Property(e => e.ModifiedDate)
                .IsUnicode(false);

            modelBuilder.Entity<Slide>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<UserAccount>()
                .Property(e => e.UserAccount1)
                .IsUnicode(false);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.Abouts)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.CreateBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.Contents)
                .WithOptional(e => e.UserAccount)
                .HasForeignKey(e => e.ModifiedBy);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.News)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.CreateBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.CreateBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.ProductCategories)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.CreateBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.Slides)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.CreateBy)
                .WillCascadeOnDelete(false);
        }
    }
}
