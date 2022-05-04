using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChatForLab.Models
{
    public class ChatDBContext:DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }

        public ChatDBContext(DbContextOptions<ChatDBContext> options): base(options)
        {

        }
    }
}
