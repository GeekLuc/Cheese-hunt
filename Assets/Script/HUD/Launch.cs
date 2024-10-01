using Script.State_Machine;
using Script.State_Machine.States;
using UnityEngine;
using TMPro;
using UnityEditor;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */

namespace Script.HUD {
    public class Launch : MonoBehaviour{
        public void Play(){
            GameManagerFsm.Instance.ChangeState(GameStates.PlayGame);
        }
        public void Shop(){
            GameManagerFsm.Instance.ChangeState(GameStates.ShopGame);
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