using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.AddsFeatures.CommandHandlers
{
    public class UpdateAddsCommand : IRequest<Response<string>>
    {
        public string? id { get; set; }
        public string? PublisherID { get; set; }
        public string? BannerID { get; set; }
        public bool? BannerIDOnOff { get; set; } = false;
        public string? InterstitialAdID { get; set; }
        public bool? InterstitialAdIDOnOff { get; set; } = false;
        public string? NativAdID { get; set; }
        public bool? NativAdIDOnOff { get; set; } = false;
        public string? NativAdPosition { get; set; }
        public bool? NativAdPositionOnOff { get; set; } = false;
        public string? InterstitialClicks { get; set; }
        public bool? InterstitialClicksOnOff { get; set; } = false;
        public string? Rewardedinterstitial { get; set; }
        public bool? RewardedinterstitialOnOff { get; set; } = false;
        public string? RewardedAds { get; set; }
        public bool? RewardedAdsOnOff { get; set; } = false;
        public string? OpneAds { get; set; }
        public bool? OpneAdsOnOff { get; set; } = false;
        public bool? Tasting { get; set; } = true;
        public string? UpdatedBy { get; set; }

    }

    public class UpdateAddsHandler : IRequestHandler<UpdateAddsCommand, Response<string>>
    {
        private readonly IAddsServices _services;

        public UpdateAddsHandler(IAddsServices services)
        {
            _services = services;

        }

        public async Task<Response<string>> Handle(UpdateAddsCommand request, CancellationToken cancellationToken)
        {

            var result = await _services.UpdateAsync(request);

            if (result.Success)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    id = result.id,
                    Data = "Success",
                    Message = "Adds Update successfully."
                };
            }
            throw new BadRequestException("Failed to Update the Adds. Please try again.");
        }
    }
}
