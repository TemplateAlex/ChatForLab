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
    public class ChatModel : PageModel
    {
        private readonly ChatDBContext _context;
        public int FirstUserId;
        public int SecondUserId;

        [BindProperty(SupportsGet = true)]
        public string NicknameOfUser { get; set; }

        [BindProperty]
        public string MessageInput { get; set; }

        public string Message { get; set; }

        public ChatModel(ChatDBContext db)
        {
            _context = db;
        }
        public void OnGet()
        {
            int[] id = GetIdForChat();

            

            var chatFromDb = _context.Chats.FirstOrDefault(x => x.FirstUserId == id[0] && x.SecondUserId == id[1]);

            if (chatFromDb == null)
            {
                Chat chat = new Chat();

                chat.FirstUserId = id[0];
                chat.SecondUserId = id[1];
                chat.Message = "";
                _context.Chats.Add(chat);
                _context.SaveChanges();
            }
            else Message = chatFromDb.Message;
        }

        public IActionResult OnGetAjax()
        {
            int[] id = GetIdForChat();
            var chat = _context.Chats.FirstOrDefault(x => x.FirstUserId == id[0] && x.SecondUserId == id[1]);
            string message = chat.Message;
            this.Message = chat.Message;

            return new JsonResult(message);
        }

        public void OnPost()
        {
            int[] id = GetIdForChat();
            var chat = _context.Chats.FirstOrDefault(x => x.FirstUserId == id[0] && x.SecondUserId == id[1]);

            chat.Message += this.MessageInput + "/" + HttpContext.Session.GetString("nickname") + "/";
            
            _context.SaveChanges();

            Message = chat.Message;

        }

        private int[] GetIdForChat()
        {
            int[] id = new int[2];

            id[0] = _context.Users.FirstOrDefault(x => x.Nickname == HttpContext.Session.GetString("nickname")).Id;
            id[1] = _context.Users.FirstOrDefault(x => x.Nickname == NicknameOfUser).Id;

            if (id[0] > id[1])
            {
                int tmp = id[0];
                id[0] = id[1];
                id[1] = tmp;
            }

            return id;
        }

    }
}
