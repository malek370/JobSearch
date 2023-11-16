using AutoMapper;
using JobSearch.Data;
using JobSearch.DTOs;
using JobSearch.DTOs.OfferDTOs;
using JobSearch.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
namespace JobSearch.Services.OfferService
{
    public class OfferServices : IOfferServices
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly JobDbContext _dbContext;
        private readonly IMapper _mapper; 
        public OfferServices(IHttpContextAccessor httpContextAccessor, JobDbContext jobDbContext,IMapper mapper) 
        {
            _contextAccessor = httpContextAccessor;
            _dbContext = jobDbContext;
            _mapper = mapper;
        }
        private int getUserId => int.Parse(_contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        public async Task<ServiceResponse<int>> AddOffer(AddOfferDTO addOffer)
        {
            ServiceResponse<int> result = new ServiceResponse<int>();
            try
            {
                if (!check_AddOfferDTO(addOffer)) throw new Exception("wrong inputs");
                Offer offer = new Offer()
                {Company=addOffer.Company,
                 recruiter=await _dbContext.Users.FirstOrDefaultAsync(u=>u.Id==getUserId),
                 Field=await _dbContext.Fields.FirstOrDefaultAsync(f=>f.Id==addOffer.FieldId),
                 JobDescription=addOffer.JobDescription,
                };
                await _dbContext.Offers.AddAsync(offer);
                await _dbContext.SaveChangesAsync();
                result.Data = offer.Id;
            }
            catch (Exception ex) { result.Data = -1;result.Success = false;result.Message = ex.Message; }
            return result;
        }

        public async Task<ServiceResponse<object>> DeleteOffer(int OfferId)
        {
            ServiceResponse<object> result = new ServiceResponse<object>() { Data=null};
            try
            {
                var offer = await _dbContext.Offers.FirstOrDefaultAsync(o => o.Id == OfferId);
                if (offer == null) throw new Exception("offer not found");
                _dbContext.Offers.Remove(offer);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) {result.Success = false; result.Message = ex.Message; }
            return result;
        }

        public async Task<ServiceResponse<List<GetOfferDTO>>> GetMyOffers()
        {
            ServiceResponse<List<GetOfferDTO>> result = new ServiceResponse<List<GetOfferDTO>>();
            try
            {
                List <GetOfferDTO> offers = await _dbContext.Offers
                    .Include(o => o.recruiter)
                    .Where(r=>r.recruiter.Id==getUserId)
                    .Select(o=>_mapper.Map<GetOfferDTO>(o))
                    .ToListAsync();
                result.Data= offers;
            }
            catch (Exception ex) { result.Success = false; result.Message = ex.Message; }
            return result;
        }

        public async Task<ServiceResponse<GetOfferDTO>> GetOffer(int OfferId)
        {
            ServiceResponse<GetOfferDTO> result = new ServiceResponse<GetOfferDTO>();
            try
            {
                var offer = await _dbContext.Offers.
                    Include(o=>o.Field).
                    FirstOrDefaultAsync(o => o.Id == OfferId);
                if (offer == null) throw new Exception("offer not found");
                result.Data=_mapper.Map<GetOfferDTO>(offer);
            }
            catch (Exception ex) { result.Success = false; result.Message = ex.Message; }
            return result;
        }

        public async Task<ServiceResponse<List<GetOfferDTO>>> GetOfferByCompany(string companyName)
        {
            ServiceResponse<List<GetOfferDTO>> result = new ServiceResponse<List<GetOfferDTO>>();
            try
            {
                var offers = await _dbContext.Offers.
                    Include(o=>o.recruiter).
                    Include(o=>o.Field).
                    Where(o=>o.Company.ToLower()==companyName.ToLower()).
                    Select(o=>_mapper.Map<GetOfferDTO>(o)).ToListAsync();
                result.Data = offers;
            }
            catch (Exception ex) { result.Success = false; result.Message = ex.Message; }
            return result;
        }

        public async Task<ServiceResponse<List<GetOfferDTO>>> GetOfferByField(int fieldId)
        {
            ServiceResponse<List<GetOfferDTO>> result = new ServiceResponse<List<GetOfferDTO>>();
            try
            {
                var offers = await _dbContext.Offers.
                    Include(o => o.recruiter).
                    Include(o => o.Field).
                    Where(o => o.Field.Id==fieldId).
                    Select(o => _mapper.Map<GetOfferDTO>(o)).ToListAsync();
                result.Data = offers;
            }
            catch (Exception ex) { result.Success = false; result.Message = ex.Message; }
            return result;
        }

        //add check for offer form
        private bool check_AddOfferDTO(AddOfferDTO addOffer)
        {
            return true;
        }
    }
}
