using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;

namespace ChatForLab.HtmlHelpers
{
    public static class HelperWithMessage
    {
        public static HtmlString CreateDialog (this IHtmlHelper html, string message, string nickname, string anotherUser)
        {
            string result = "";
            bool checker = true;
            int index = 0;

            if (message != null)
            {
                result += "<div class='screen-wrapper container-fluid'>";

                while (checker)
                {
                    int indNick = message.IndexOf("/" + nickname + "/");
                    int indAnothUser = message.IndexOf("/" + anotherUser + "/");
                    checker = false;

                    if ((indNick < indAnothUser && indNick != -1) || (indAnothUser == -1 && indNick != -1))
                    {
                        result += "<div class='user__wrapper row justify-content-end'><p>";

                        for (int i = 0; i < indNick; i++)
                        {
                            result += message[i];
                        }

                        result += "</p></div>";
                        index = indNick + nickname.Length + 2;
                        checker = true;
                    }
                    else if ((indAnothUser < indNick && indAnothUser != -1) || (indNick == -1 && indAnothUser != -1))
                    {
                        result += "<div class='another-user__wrapper row justify-content-start'><p>";

                        for (int i = 0; i < indAnothUser; i++)
                        {
                            result += message[i];
                        }

                        result += "</p></div>";
                        index = indAnothUser + anotherUser.Length + 2;
                        checker = true;
                    }

                    if (index > message.Length)
                    {
                        checker = false;
                    }
                    else
                    {
                        message = message.Substring(index);
                    }
                }

                result += "</div>";
            }

            HtmlString htmlString = new HtmlString(result);
            return htmlString;
        }
    }
}
