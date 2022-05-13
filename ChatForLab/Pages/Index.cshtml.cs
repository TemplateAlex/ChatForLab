using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatForLab.Models;
using Microsoft.AspNetCore.Http;

namespace ChatForLab.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ChatDBContext _context;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Surname { get; set; }

        [BindProperty]
        public string Nickname { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ChatDBContext db)
        {
            _logger = logger;
            _context = db;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _context.Users.FirstOrDefault(x => x.Nickname.Equals(this.Nickname.ToLower()));
            HttpContext.Session.SetString("nickname", this.Nickname);

            if (user == null)
            {
                User person = new User();
                person.Name = this.Name;
                person.Surname = this.Surname;
                person.Nickname = this.Nickname;
                _context.Users.Add(person);
                await _context.SaveChangesAsync();

            }

            return RedirectToPage("PageOfUsers");
        }
    }
}
