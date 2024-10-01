using Script.State_Machine;
using Script.State_Machine.States;
using UnityEditor;
using UnityEngine;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.HUD{
        public class GameOver : MonoBehaviour{

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
