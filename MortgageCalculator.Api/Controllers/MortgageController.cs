using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using MortgageCalculator.Api.Services;
using MortgageCalculator.Dto;
using System;

namespace MortgageCalculator.Api.Controllers
{
    [RoutePrefix("api/Mortgage")]
    [EnableCors(origins: "http://localhost:52187", headers: "*", methods: "GET,POST,PUT")]
    public class MortgageController : ApiController
    {
        private IMortgageService mortgageService = null;

        public MortgageController(IMortgageService service)
        {
            mortgageService = service;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var mortgageList = mortgageService.GetAllMortgages();

            if (mortgageList == null)
            {
                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Error in retrieving the mortgage list. Please try again"),
                    ReasonPhrase = "Error in retrieving the mortgage list. Please try again"
                };

                throw new HttpResponseException(resp);
            }

            return Ok(mortgageList);
        }

        // GET: api/Mortgage/5
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var mortgage = mortgageService.GetMortgage(id);

            if (mortgage == null)
            {
                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Mortgage id {0} not found",  Convert.ToString(id))),
                    ReasonPhrase = "Mortgage not found"
                };

                throw new HttpResponseException(resp);
            }

            return Ok(mortgage);
        }

        // GET: api/Mortgage/5
        [Route("calculate")]
        [HttpPost]
        public IHttpActionResult Post(Mortgage mortgage)
        {
            if (mortgage.LoanAmount == 0 || mortgage.TermsInMonths == 0)
            {
                var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Mortgage loan amount or duration cannot be 0"),
                    ReasonPhrase = "Mortgage loan amount or duration cannot be 0"
                };

                throw new HttpResponseException(resp);
            }

            var mortgageResponseObject = mortgageService.CalculateMortgage(mortgage);

            if (mortgageResponseObject == null)
            {
                return NotFound();
            }

            return Ok(mortgageResponseObject);
        }
    }
}
