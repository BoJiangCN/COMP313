using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WeLinkUp.Models;

namespace WeLinkUp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public DbSet<CreateEvent> Events { get; set; }
        
        public DbSet<FriendLists> FriendLists {get;set;}

        public DbSet<AttendeeList> AttendeeList { get; set; }
    }
}
