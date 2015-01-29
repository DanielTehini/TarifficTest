using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnotherTest.Controllers
{
    using AnotherTest.Models;

    using Twitterizer;

    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddQuote(string quote, string author, string emotion, string source)
        {
            try
            {
                using (var context = new QuotesDB())
                {
                    UserQuote addQuote = new UserQuote();
                    addQuote.Quote = quote;
                    addQuote.Author = author;
                    addQuote.Emotion = emotion;
                    addQuote.Source = source;
                    addQuote.TransformedQuote = addQuote.ToString();
                    addQuote.DateCreated = DateTime.Now;
                    context.Quotes.Add(addQuote);
                    context.SaveChanges();
                    OAuthTokens accesstoken = new OAuthTokens()
                    {
                        AccessToken = "1168437102-OPhanbNtRtHHlCmRHiEhduDGtJuhnd7iiMVnImW",
                        AccessTokenSecret = "Z0oya1CYE2hion4GS1VTEYq2xSuxqkqC8vbyLAPQpfjbg",
                        ConsumerKey = "EqmTjW9ILHdBtNJyob8WpqFKr",
                        ConsumerSecret = "J6zUucZk33nwvrWHsD7v0AcFaN7NqRlSsZHmfazXk3NkoKfzc2"
                    };

                    TwitterResponse<TwitterStatus> response = TwitterStatus.Update(
                        accesstoken,
                        quote);
                    ViewBag.Result = "Your quote has been added to the database.";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Result = "An error has occured, please contact the system administrator.";
                return View("Index");
            }
            
        }       
    }
}
