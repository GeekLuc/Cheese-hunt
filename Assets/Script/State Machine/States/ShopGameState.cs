using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.State_Machine.States{
    public class ShopGameState : StateLinkedFsm{
        public override void OnStateEnter(){
           Debug.Log("State Shop");
           SceneManager.LoadScene("Shop");
        }

        public override void OnStateUpdate(){
            
        }

        public override void OnStateExit(){
            
        }

        public ShopGameState(GameManagerFsm pMyFsm) : base(pMyFsm)
        {
        }
    }
}
