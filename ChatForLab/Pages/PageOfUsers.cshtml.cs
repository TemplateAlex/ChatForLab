using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChatForLab.Models;
using Microsoft.AspNetCore.Http;

namespace ChatForLab.Pages
{
    public class PageOfUsersModel : PageModel
    {
        private readonly UserDBContext _context;

        public List<User> users { get; set; }
        public PageOfUsersModel(UserDBContext db)
        {
            _context = db;
        }
        public void OnGet()
        {
            users = _context.Users.Where(x => x.Nickname != HttpContext.Session.GetString("nickname")).ToList();
        }
    }
}
