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
    public class Win : MonoBehaviour{
        [SerializeField] private GameManagement GameManagement;
        [SerializeField] private TextMeshProUGUI scoreText;

        void Start(){
            LoadScore();
            GameManagement.CombineScores();
        }

        public void LoadScore(){
            int score = PlayerPrefs.GetInt("monnaieActuel", 0);
            UpdateScoreText(score);
        }

        void UpdateScoreText(int score){
            scoreText.text = "Score: " + score;
        }

        public void Play(){
            GameManagerFsm.Instance.ChangeState(GameStates.PlayGame);
        }
        public void Menu(){
            GameManagerFsm.Instance.ChangeState(GameStates.InitGame);
        }
        public void Quitter(){
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}