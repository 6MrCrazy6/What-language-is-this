using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class BannerAd : MonoBehaviour
    {
        private string appId = "ca-app-pub-7878393602023041~5163870101";

#if UNITY_ANDROID
        private string bannerId = "ca-app-pub-7878393602023041/4014439966";
#endif

        BannerView bannerView;

        private void Start()
        {
            MobileAds.RaiseAdEventsOnUnityMainThread = true;
            MobileAds.Initialize(initStatus => {

                print("Ads Initialised !!");
                LoadBannerAd();

            });
        }

        #region Banner

        public void LoadBannerAd()
        {
            CreateBannerView();
            ListenToBannerEvents();

            if (bannerView == null)
            {
                CreateBannerView();
            }

            var adRequest = new AdRequest();
            adRequest.Keywords.Add("unity-admob-sample");

            print("Loading banner Ad !!");
            bannerView.LoadAd(adRequest);
        }
        void CreateBannerView()
        {

            if (bannerView != null)
            {
                DestroyBannerAd();
            }
            bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
        }
        void ListenToBannerEvents()
        {
            bannerView.OnBannerAdLoaded += () =>
            {
                Debug.Log("Banner view loaded an ad with response : "
                    + bannerView.GetResponseInfo());
            };

            bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                Debug.LogError("Banner view failed to load an ad with error : "
                    + error);
            };

            bannerView.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log("Banner view paid {0} {1}." +
                    adValue.Value +
                    adValue.CurrencyCode);
            };

            bannerView.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Banner view recorded an impression.");
            };

            bannerView.OnAdClicked += () =>
            {
                Debug.Log("Banner view was clicked.");
            };

            bannerView.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Banner view full screen content opened.");
            };

            bannerView.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Banner view full screen content closed.");
            };
        }
        public void DestroyBannerAd()
        {

            if (bannerView != null)
            {
                print("Destroying banner Ad");
                bannerView.Destroy();
                bannerView = null;
            }
        }
        #endregion
    }
}

