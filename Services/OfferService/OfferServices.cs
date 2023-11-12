using JobSearch.Data;
using JobSearch.DTOs;
using JobSearch.DTOs.OfferDTOs;

namespace JobSearch.Services.OfferService
{
    public class OfferServices : IOfferServices
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly JobDbContext _dbContext;
        public OfferServices(IHttpContextAccessor httpContextAccessor, JobDbContext jobDbContext) 
        {
            _contextAccessor = httpContextAccessor;
            _dbContext = jobDbContext;
        }
        public async Task<ServiceResponse<int>> AddOffer(AddOfferDTO addOffer)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<object>> DeleteOffer(int OfferId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetOfferDTO>>> GetMyOffers()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<GetOfferDTO>> GetOffer(int OfferId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetOfferDTO>>> GetOfferByCompany(string companyName)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetOfferDTO>>> GetOfferByField(int fieldId)
        {
            throw new NotImplementedException();
        }
    }
}
