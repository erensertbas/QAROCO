﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Qaroco.DL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QarocoDBEntities : DbContext
    {
        public QarocoDBEntities()
            : base("name=QarocoDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<BlogComment> BlogComments { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<CargoPayment> CargoPayments { get; set; }
        public virtual DbSet<CargoTransaction> CargoTransactions { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Courier> Couriers { get; set; }
        public virtual DbSet<CourierStatistic> CourierStatistics { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<MessageSystem> MessageSystems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<ProductInfo> ProductInfoes { get; set; }
        public virtual DbSet<RegisterFolder> RegisterFolders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ShippingType> ShippingTypes { get; set; }
        public virtual DbSet<UserBank> UserBanks { get; set; }
        public virtual DbSet<UserCardInfo> UserCardInfoes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<MessageReply> MessageReplies { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
    }
}
