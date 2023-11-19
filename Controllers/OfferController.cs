using JobSearch.DTOs.OfferDTOs;
using JobSearch.DTOs;
using JobSearch.Services.OfferService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferServices _offerServices;
        public OfferController(IOfferServices offerServices) 
        {
            _offerServices = offerServices;
        }

        [HttpPost("add")]
    public async Task<IActionResult> AddOffer(AddOfferDTO addOffer)
    {
        return Ok(await _offerServices.AddOffer(addOffer));
    }
        [HttpDelete("delete")]
    public async Task<IActionResult> DeleteOffer(int OfferId)
        {
            return Ok(await _offerServices.DeleteOffer(OfferId));
        }
        [HttpGet("getbyId")]
    public async Task<IActionResult> GetOffer(int OfferId)
        {
            return Ok(await _offerServices.GetOffer(OfferId));
        }
        [HttpGet("myoffers")]
    public async Task<IActionResult> GetMyOffers()
        {
            return Ok(await _offerServices.GetMyOffers());
        }
        [HttpGet("offerbyfield")]
    public async Task<IActionResult> GetOfferByField(int fieldId)
        {//not working
            return Ok(await _offerServices.GetOfferByField(fieldId));
        }
        [HttpGet("offerbydescription")]
        public async Task<IActionResult> GetOfferByDescription(string description)
        {//not working
            return Ok(await _offerServices.GetOfferByDescription(description));
        }
        [HttpGet("offerbycompany")]
    public async Task<IActionResult> GetOfferByCompany(string companyName)
        {//not working
            return Ok(await _offerServices.GetOfferByCompany(companyName));
        }
}
}
