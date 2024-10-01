using UnityEngine;
using UnityEngine.Advertisements;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 * 
 * Source : https://docs.unity.com/ads/en-us/manual/InitializingTheUnitySDK
 */
namespace Script.Ads{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener{
        [SerializeField] string _androidGameId;
        [SerializeField] bool _testMode = true;
        private string _gameId;

        private void Awake(){
            InitializeAds();
        }

        private void InitializeAds(){
        _gameId = _androidGameId;
            if (!Advertisement.isInitialized && Advertisement.isSupported){
                Advertisement.Initialize(_gameId, _testMode, this);
            }
        }

     
        public void OnInitializationComplete(){
            Debug.Log("Unity Ads initialization complete.");
        }
     
        public void OnInitializationFailed(UnityAdsInitializationError error, string message){
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}