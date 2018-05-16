using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeQuoteSDK
{
    public static class FakeQuote
    {
        public static QuoteResult GetPrice(string name)
        {
            Random rand = new Random();

            return new QuoteResult
            {
                Name = name.ToUpper(),
                Value = rand.NextDouble() * 100
            };
        }
    }
}
