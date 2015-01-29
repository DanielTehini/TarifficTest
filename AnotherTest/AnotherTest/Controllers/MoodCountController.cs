using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnotherTest.Controllers
{
    using System.Web.Http;
    using System.Web.Script.Serialization;
    using AnotherTest.Models;

    public class MoodCountController : ApiController
    {
        [HttpGet]
        public string QuotesPerMood()
        {
            try
            {
                using (var context = new QuotesDB())
                {
                    Dictionary<string, int> moodNumber = new Dictionary<string, int>();
                    List<UserQuote> getAllQuotes = context.Quotes.ToList();
                    List<string> getMoods = context.Quotes.Select(c => c.Emotion).Distinct().ToList();
                    foreach (string mood in getMoods)
                    {
                        int count = context.Quotes.Where(c => c.Emotion == mood).Count();
                        moodNumber.Add(mood, count);
                    }
                    return new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(moodNumber);
                }
               
            }
            catch (Exception ex)
            {
                return "An error has occured, please contact the site administrator.";
            }
        }
    }
}
