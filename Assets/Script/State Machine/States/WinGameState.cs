using UnityEngine.SceneManagement;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.State_Machine.States{
    public class WinGameState : StateLinkedFsm{
        public override void OnStateEnter(){
            SceneManager.LoadScene("Win");
        }

        public override void OnStateUpdate(){

        }

        public override void OnStateExit(){
            
        }

        public WinGameState(GameManagerFsm pMyFsm) : base(pMyFsm)
        {
        }
    }
}