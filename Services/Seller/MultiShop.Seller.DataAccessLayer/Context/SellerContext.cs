using Microsoft.EntityFrameworkCore;
using MultiShop.Seller.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Seller.DataAccessLayer.Context
{
    public class SellerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1443; Initial Catalog=MultiShopSellerDb;User=sa;Password=123456aA*");

        }
        public DbSet<Vendor> Sellers { get; set; }  
    }
}
