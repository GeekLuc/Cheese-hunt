using UnityEngine.SceneManagement;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.State_Machine.States{
    public class PlayGameState : StateLinkedFsm{
        public override void OnStateEnter(){
            SceneManager.LoadScene("NV_test");
        }

        public override void OnStateUpdate(){

        }

        public override void OnStateExit(){
            
        }

        public PlayGameState(GameManagerFsm pMyFsm) : base(pMyFsm)
        {
        }
    }
}
