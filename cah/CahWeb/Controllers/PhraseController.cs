using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CahWeb.Controllers
{


    [Route("api/[controller]")]
    public class PhraseController : Controller
    {
        private static string Clean(string text)
        {
            string ret = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
            return ret.Replace("&quot;", "\"").Replace("&amp;", "&"); 
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string basePath = PlatformServices.Default.Application.ApplicationBasePath;
            string[] lines = System.IO.File.ReadAllLines(Path.Combine(basePath, "sayings.txt"));

            List<string> blackCards = new List<string>(10000);
            List<string> whiteCards = new List<string>(10000);

            bool readingBlackCards = true;
            foreach (string line in lines)
            {
                if (line == string.Empty)
                {
                    readingBlackCards = false;
                    continue;
                }
                else
                {
                    if (readingBlackCards)
                    {
                        blackCards.Add(Clean(line));
                    }
                    else
                    {
                        whiteCards.Add(Clean(line));
                    }
                }
            }

            Random rand = new Random();
            string blackCard = blackCards[rand.Next(0, blackCards.Count)];

            string final = "";
            int spaces = blackCard.Count(n => n == '_'); 
            if(spaces > 0)
            {
                final = blackCard; 
                for(int c=0;c<spaces;c++)
                {
                    string whiteCard = whiteCards[rand.Next(0, whiteCards.Count)];
                    Regex regex = new Regex("_", RegexOptions.Multiline);
                    whiteCard = Char.ToLowerInvariant(whiteCard[0]) + whiteCard.Substring(1); 
                    final = regex.Replace(final, whiteCard.Replace(".",""), 1, 0); 
                }
            }
            else
            {
                string whiteCard = whiteCards[rand.Next(0, whiteCards.Count)];
                final = blackCard + " " + whiteCard; 
            }

            return new string[] { final, blackCard };
        }
    }
}
