using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatForLab.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
        public string Message { get; set; }
        public User FirstUser { get; set; }
        public User SecondUser { get; set; }
    }
}
