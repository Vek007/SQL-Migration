﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSIS
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class tsEntities : DbContext
    {
        public tsEntities()
            : base("name=tsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<t_x> t_x { get; set; }
        public virtual DbSet<t_s> t_s { get; set; }
        public virtual DbSet<ar> ars { get; set; }
        public virtual DbSet<pr> prs { get; set; }
    }
}
