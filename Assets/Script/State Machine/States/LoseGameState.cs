using UnityEngine.SceneManagement;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.State_Machine.States{
    public class LoseGameState : StateLinkedFsm{
        public override void OnStateEnter(){
            SceneManager.LoadScene("Game_Over");
        }

        public override void OnStateUpdate(){
           
        }

        public override void OnStateExit(){
            
        }

        public LoseGameState(GameManagerFsm pMyFsm) : base(pMyFsm)
        {
        }
    }
}

