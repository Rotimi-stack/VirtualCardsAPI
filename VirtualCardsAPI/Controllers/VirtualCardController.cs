using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VirtualCardsAPI.Models;

namespace VirtualCardsAPI.Controllers
{
    public class VirtualCardController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
       


        public VirtualCardController(IHttpClientFactory client, IConfiguration config)
        {
            
            _config = config;
            _clientFactory = client;
           
        }


        [HttpPost]
        [Route("api/VirtualCard/create/card")]
        public async Task<object> CreateVirtualCard([FromBody] CreateVirtualCardPayload vc)
        {
            var baseurl = _config.GetSection("BaseAddress").Value.ToString();
            var testkey = _config.GetSection("Test_Key").Value.ToString();


            ErrorResponses error = new ErrorResponses();
            error.status = true;

            if (string.IsNullOrEmpty(vc.debit_currency) && string.IsNullOrEmpty(vc.billing_address) && string.IsNullOrEmpty(vc.billing_city) && string.IsNullOrEmpty(vc.billing_country)
               && string.IsNullOrEmpty(vc.billing_postal_code) && string.IsNullOrEmpty(vc.billing_state) && string.IsNullOrEmpty(vc.billing_name) && (vc.amount != 0))
            {
                return new ErrorResponses { message = "No Field should be empty", status = false };
            }

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", testkey);
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = new System.TimeSpan(0, 0, 1, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var stringContent = new StringContent(JsonConvert.SerializeObject(vc), Encoding.UTF8, "application/json");
                var resp = await client.PostAsync(baseurl, stringContent);
                var vcResp = await resp.Content.ReadAsStringAsync();


                return (vcResp);

            }

        }


        [HttpPost]
        [Route("api/VirtualCard/Fund/card")]
        public async Task<object> FundVirtualCard([FromBody] FundVirtualCard vc, int id)
        {
            var url = _config.GetSection("FundCardURL").Value.ToString();
            var testkey = _config.GetSection("Test_Key").Value.ToString();


            ErrorResponses error = new ErrorResponses();
            error.status = true;

            if (string.IsNullOrEmpty(vc.debit_currency) && (vc.amount == 0))
                
            {
                return new ErrorResponses { message = "No Field should be empty", status = false };
            }

            using (var client = new HttpClient())
            {
                

                client.DefaultRequestHeaders.Add("Authorization", testkey);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = new System.TimeSpan(0, 0, 1, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var stringContent = new StringContent(JsonConvert.SerializeObject(vc), Encoding.UTF8, "application/json");
                var resp = await client.PostAsync(url + "/" + id + "/" + "fund");
                var vcResp = await resp.Content.ReadAsStringAsync();


                return (vcResp);

            }

        }

        [HttpGet]
        [Route("api/VirtualCard/GetAll/card")]
        public async Task<object> GetAllVirtualCard()
        {
            var baseurl = _config.GetSection("BaseAddress").Value.ToString();
            var testkey = _config.GetSection("Test_Key").Value.ToString();


            ErrorResponses error = new ErrorResponses();
            error.status = true;

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", testkey);
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = new System.TimeSpan(0, 0, 1, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var resp = await client.GetAsync(baseurl);
                var vcResp = await resp.Content.ReadAsStringAsync();


                return (vcResp);

            }
            
        }

        [HttpGet]
        [Route("api/VirtualCard/GetA/card/id")]
        public async Task<object> GetAVirtualCard([FromRoute]GetVirtualCard vc)
        {
            var avirtualCard = _config.GetSection("BaseAddress").Value.ToString();
            var testkey = _config.GetSection("Test_Key").Value.ToString();


            ErrorResponses error = new ErrorResponses();
            error.status = true;

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", testkey);
                client.BaseAddress = new Uri(avirtualCard);
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = new System.TimeSpan(0, 0, 1, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var stringContent = new StringContent(JsonConvert.SerializeObject(vc), Encoding.UTF8, "application/json");
                var resp = await client.GetAsync(avirtualCard);
                var vcResp = await resp.Content.ReadAsStringAsync();


                return (vcResp);

            }

        }


    }
}




/*
 
  "amount": 5,
  "currency": "USD",
  "debit_currency": "NGN",
  "debit_amount": "string",
  "billng_name": "Flutterwave Developers",
  "billng_address": "3563 Huntertown Rd, Allison park, PA",
  "billng_city": "San Francisco",
  "billng_state": "CA",
  "billng_country": "US",
  "billng_postal_code": "94105"
 
 
 */
