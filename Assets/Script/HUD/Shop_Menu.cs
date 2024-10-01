using Script.State_Machine;
using Script.State_Machine.States;
using TMPro;
using UnityEditor;
using UnityEngine;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.HUD{
    public class Shop : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI scoreText;
        private int monnaie;

        void Start() {
            UpdatMonnaieText();
        }
        
        private void OnEnable() {
            UpdatMonnaieText();
        }

        public void LoadScore() {
            monnaie = PlayerPrefs.GetInt("monnaie");
        }

        public void Retour() {
            GameManagerFsm.Instance.ChangeState(GameStates.InitGame);
        }

        public void UpdatMonnaieText() {
            LoadScore();
            scoreText.text = monnaie.ToString();
        }
    }
}