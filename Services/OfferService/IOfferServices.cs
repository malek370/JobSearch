using JobSearch.DTOs;
using JobSearch.DTOs.OfferDTOs;

namespace JobSearch.Services.OfferService
{
    public interface IOfferServices
    {
        public Task<ServiceResponse<int>> AddOffer(AddOfferDTO addOffer);
        public Task<ServiceResponse<object>> DeleteOffer(int OfferId);
        public Task<ServiceResponse<GetOfferDTO>> AddOffer(AddOfferDTO addOffer);


    }
}
