using JobSearch.DTOs;
using JobSearch.DTOs.OfferDTOs;

namespace JobSearch.Services.OfferService
{
    public interface IOfferServices
    {
        public Task<ServiceResponse<int>> AddOffer(AddOfferDTO addOffer);
        public Task<ServiceResponse<object>> DeleteOffer(int OfferId);
        public Task<ServiceResponse<GetOfferDTO>> GetOffer(int OfferId);
        public Task<ServiceResponse<List<GetOfferDTO>>> GetMyOffers();
        public Task<ServiceResponse<List<GetOfferDTO>>> GetOfferByField(int fieldId);
        public Task<ServiceResponse<List<GetOfferDTO>>> GetOfferByCompany(string companyName);
        public Task<ServiceResponse<List<GetOfferDTO>>> GetOfferByDescription(string description);
    }
}
