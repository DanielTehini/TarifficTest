using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnotherTest.Controllers
{
    using System.Threading;

    using AnotherTest.Models;

    using Twitterizer;

    public class HomeController : Controller
    {
        List<string> transformedStrings = new List<string>();

        public ActionResult Index()
        {
            return View();
        }

       

        public RedirectToRouteResult AddQuote(string quote, string author, string emotion, string source)
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
                    TempData["Result"] = "Your quote has been added to the database.";
                    return this.RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Result"] = "An error has occured, please contact the system administrator.";
                return this.RedirectToAction("Index");
            }
            
        }

        public ActionResult MultiThreading()
        {
            try
            {
                using (var context = new QuotesDB())
                {
                    
                    List<UserQuote> getAllQuotes = context.Quotes.ToList();
                    foreach (UserQuote userQuote in getAllQuotes)
                    {
                        Thread workerThread = new Thread(() => SomeDelegate(userQuote));
                        workerThread.Start();
                    }

                    TempData["TransformedQuotes"] = transformedStrings;
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["TransformedQuotes"] = null;
                return View();
            }                   
        }

        public void SomeDelegate(UserQuote userQuote)
        {
            transformedStrings.Add(userQuote.ToString());
        }

    }
}
