using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 * 
 * source : https://docs.unity.com/ads/en-us/manual/ImplementingRewardedAdsUnity
 */
namespace Script.Ads{
    public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener{
        [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
        private string _adUnitId;
        [SerializeField] private int BonusRewards=50;
        private HUD.Shop _shop;
        [SerializeField] private float delayBetweenAds = 30f;
        private float _adTimer;
        [SerializeField] private Button ButtonAds;
        [SerializeField] private Image fillImage;
        [SerializeField] private Image myImage;
        
        void Awake(){   
            _shop = FindObjectOfType<HUD.Shop>();
            _adUnitId = _androidAdUnitId;
        }
        void Start(){
            _adTimer = 0f;
        }
        void Update(){
            if (_adTimer > 0){
                _adTimer -= Time.deltaTime;
                if (_adTimer < 0){
                    _adTimer = 0; 
                }
            }
        }
        
        public void LoadAd(){
            if(_adTimer == 0) {
                Advertisement.Load(_adUnitId, this);
            }
        }

        public void OnUnityAdsAdLoaded(string adUnitId){
            ShowAd();
        }

        public void ShowAd(){
            Advertisement.Show(_adUnitId, this);
        }
        
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState){
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)){
                int monnaieP = PlayerPrefs.GetInt("monnaie");
                PlayerPrefs.SetInt("monnaie", monnaieP+BonusRewards);
                _shop.UpdatMonnaieText();  
                ButtonAds.interactable = false;
                ButtonAds.GetComponent<Image>().color = Color.grey;
                StartCoroutine(EnableButtonOverTime(delayBetweenAds));
                _adTimer = delayBetweenAds;
            }
        }
        
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message){
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }
     
        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message){
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }
     
        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }
        private IEnumerator EnableButtonOverTime(float delay){
            myImage.gameObject.SetActive(false);
            var rate = 1 / delay;
            for (float i = 0; i <= 1; i += rate * Time.deltaTime){
                fillImage.fillAmount = i;
                if(i >= 0.75){
                    myImage.gameObject.SetActive(true);
                }
                yield return null;
            }
    
            fillImage.fillAmount = 1;
            ButtonAds.interactable = true;
            ButtonAds.GetComponent<Image>().color = Color.white; 
        }
    }
}