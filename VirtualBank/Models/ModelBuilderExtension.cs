using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualBank.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasData(

            new Users
            {
                Id=1,
                FirstName="Gebeyaw",
                LastName="Yegna",
                Account_Number = 10001,
                Address="AdissAbaba",
              
            });
            }
    }
}
