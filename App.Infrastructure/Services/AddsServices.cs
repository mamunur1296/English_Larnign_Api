using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Features.AddsFeatures.CommandHandlers;
using App.Application.Interfaces;
using App.Domain.Abstractions;
using App.Domain.Entities;
using AutoMapper;

namespace App.Infrastructure.Services
{
    public class AddsServices : IAddsServices
    {
        private readonly IUowRepo _uowRepo;
        private readonly IMapper _mapper;
        public AddsServices(IUowRepo uowRepo, IMapper mapper)
        {
            _uowRepo = uowRepo;
            _mapper = mapper;
        }
        public async Task<(bool Success, string id)> CreateAsync(CreateAddsCommand entity)
        {
            var newAdds = new Adds
            {
                Id = Guid.NewGuid().ToString(),
                PublisherID = entity.PublisherID,
                BannerID = entity.BannerID,
                BannerIDOnOff = entity.BannerIDOnOff ?? false,
                InterstitialAdID = entity.InterstitialAdID,
                NativAdID = entity.NativAdID,
                NativAdIDOnOff = entity.NativAdIDOnOff ?? false,
                NativAdPosition = entity.NativAdPosition,
                NativAdPositionOnOff = entity.NativAdPositionOnOff ?? false,
                InterstitialClicks = entity.InterstitialClicks,
                InterstitialClicksOnOff = entity.InterstitialClicksOnOff ?? false,
                Rewardedinterstitial = entity.Rewardedinterstitial,
                RewardedAds = entity.RewardedAds,
                RewardedAdsOnOff = entity.RewardedAdsOnOff ?? false,
                OpneAds = entity.OpneAds,
                OpneAdsOnOff = entity.OpneAdsOnOff ?? false,
                Tasting = entity.Tasting ?? true,
            };
            newAdds.SetCreatedDate(DateTime.Now, entity?.CreatedBy);
            await _uowRepo.addsCommandRepository.AddAsync(newAdds);
            await _uowRepo.SaveAsync();
            return (Success: true, id: newAdds.Id);
        }

        public async Task<(bool Success, string id)> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AddsDTO>> GetAllAsync()
        {
            var itemList = await _uowRepo.addsQueryRepository.GetAllAsync();
            var Category = itemList.Select(emp => _mapper.Map<AddsDTO>(emp));
            return Category;
        }

        public async Task<AddsDTO> GetByIdAsync(string id)
        {
            var Category = await _uowRepo.addsQueryRepository.GetByIdSqlAsync(id);
            if (Category == null)
            {
                throw new NotFoundException($"Adds with id = {id} not found");
            }
            return _mapper.Map<AddsDTO>(Category);
        }

        public async Task<(bool Success, string id)> UpdateAsync(UpdateAddsCommand entity)
        {
            var adds = await _uowRepo.addsQueryRepository.GetByIdAsync(entity?.id);
            if (adds == null)
            {
                throw new NotFoundException($"adds with id = {entity?.id} not found");
            }
            // Update  properties
            adds.SetUpdateDate(DateTime.Now, entity.UpdatedBy);
            adds.PublisherID = entity.PublisherID;
            adds.BannerID = entity.BannerID;
            adds.BannerIDOnOff = entity.BannerIDOnOff ;
            adds.InterstitialAdID = entity.InterstitialAdID;
            adds.NativAdID = entity.NativAdID;
            adds.NativAdIDOnOff = entity.NativAdIDOnOff ;
            adds.NativAdPosition = entity.NativAdPosition;
            adds.NativAdPositionOnOff = entity.NativAdPositionOnOff ;
            adds.InterstitialClicks = entity.InterstitialClicks;
            adds.InterstitialClicksOnOff = entity.InterstitialClicksOnOff ;
            adds.Rewardedinterstitial = entity.Rewardedinterstitial;
            adds.RewardedAds = entity.RewardedAds;
            adds.RewardedAdsOnOff = entity.RewardedAdsOnOff ;
            adds.OpneAds = entity.OpneAds;
            adds.OpneAdsOnOff = entity.OpneAdsOnOff;
            adds.Tasting = entity.Tasting ;


            await _uowRepo.addsCommandRepository.UpdateAsync(adds);
            await _uowRepo.SaveAsync();
            return (Success: true, id: adds.Id);
        }
    }
}
