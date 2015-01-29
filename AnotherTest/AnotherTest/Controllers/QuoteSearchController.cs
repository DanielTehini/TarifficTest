using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace AnotherTest.Controllers
{
    using System.Data.Linq.SqlClient;
    using System.Web.Http;
    using System.Web.Script.Serialization;
    using AnotherTest.Models;

    public class QuoteSearchController : ApiController
    {
        [HttpPost]
        public string SearchQuote(SearchString searchString)
        {
            try
            {
                using (var context = new QuotesDB())
                {

                    List<UserQuote> getAllQuotesForMood = context.Quotes.Where(c => c.Quote.ToLower().Contains(searchString.searchString.ToLower())).ToList();
                    return new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(getAllQuotesForMood);
                }

            }
            catch (Exception ex)
            {
                return "An error has occured, please contact the site administrator.";
            }
        }

        public class SearchString
        {
            public string searchString { get; set; }
        }
    }

}