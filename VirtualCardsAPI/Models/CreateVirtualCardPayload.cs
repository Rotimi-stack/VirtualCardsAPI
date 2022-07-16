using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualCardsAPI.Models
{
    public class CreateVirtualCardPayload
    {
        public int amount { get; set; }
        public string currency { get; set; }
        public string debit_currency { get; set; }
        public string debit_amount { get; set; }
        public string billing_name { get; set; }
        public string billing_address { get; set; }
        public string billing_city { get; set; }
        public string billing_state { get; set; }
        public string billing_country { get; set; }
        public string billing_postal_code { get; set; }
        public string callback_url { get; set; }

    }

    public class ErrorResponses
    {
        public string message { get; set; }
        public string statuscode { get; set; }
        public bool status { get; set; }
    }

    public class GetVirtualCard
    {
        public string id { get; set; }
    }

    public class FundVirtualCard
    {
        
        public string debit_currency { get; set; }
        public int amount { get; set; }
    }
}
