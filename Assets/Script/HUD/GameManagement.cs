using TMPro;
using UnityEngine;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.HUD{
    public class GameManagement : MonoBehaviour{
        [SerializeField] private TextMeshProUGUI MonnaieText;
        private int _score;

        public void AddScore(int pointsToAdd){
            _score += pointsToAdd;
            UpdateMonnaieText();
        }
        
        public void DeUpdateScore(){
            _score -= 2;
            if (_score < 0)
                _score = 0;
            UpdateMonnaieText();
        }

        public void UpdateScoreFromExternalCall(int newScore){
            _score = newScore;
            UpdateMonnaieText();
        }

        private void UpdateMonnaieText(){
            MonnaieText.text = _score.ToString();
        }

        public void SaveScore(){
            PlayerPrefs.SetInt("monnaieActuel", _score);
        }
        
        public void CombineScores(){
            int currentCurrency = PlayerPrefs.GetInt("monnaieActuel");
            int additionalCurrency = PlayerPrefs.GetInt("monnaie");
            int combinedCurrency = currentCurrency + additionalCurrency;
            PlayerPrefs.SetInt("monnaie", combinedCurrency);
        }
    }
}