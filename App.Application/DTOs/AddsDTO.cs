using App.Application.Common;


namespace App.Application.DTOs
{
    public class AddsDTO : BaseDTOs
    {
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
    }
}
