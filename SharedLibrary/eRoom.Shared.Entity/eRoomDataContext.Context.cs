﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eRoom.Shared.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class eRoomConnectionString : DbContext
    {
        public eRoomConnectionString()
            : base("name=eRoomConnectionString")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ElectricUsing> ElectricUsings { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Motorbike> Motorbikes { get; set; }
        public virtual DbSet<MotorType> MotorTypes { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<ReferenceLibrary> ReferenceLibraries { get; set; }
        public virtual DbSet<RefLib> RefLibs { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomDetail> RoomDetails { get; set; }
        public virtual DbSet<RoomPeople> RoomPeoples { get; set; }
        public virtual DbSet<RoomStatus> RoomStatus { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<WaterUsing> WaterUsings { get; set; }
    }
}
