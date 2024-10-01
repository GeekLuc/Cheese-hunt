using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.State_Machine.States{
    public class InitGameState : StateLinkedFsm{
        public override void OnStateEnter(){
           Debug.Log("State initial");
           SceneManager.LoadScene("Launch");
        }

        public override void OnStateUpdate(){
            
        }

        public override void OnStateExit(){
            
        }

        public InitGameState(GameManagerFsm pMyFsm) : base(pMyFsm)
        {
        }
    }
}
