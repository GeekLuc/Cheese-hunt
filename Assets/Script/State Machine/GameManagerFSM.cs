using Script.State_Machine.States;
using UnityEngine;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.State_Machine{
    public class GameManagerFsm : Fsm{
        public static GameManagerFsm Instance {get; private set;}   
        public State PreviousState { get; private set; }

        private void Awake(){
            if(Instance != null && Instance != this){
                Destroy(gameObject);
            } else {
                Instance = this;
            }
            initState = new InitGameState(this);
        }
        
        public void ChangeState(GameStates pState){
            switch(pState){
                case GameStates.InitGame: SetState(new InitGameState(this)); break;
                case GameStates.PlayGame: SetState(new PlayGameState(this)); break;
                case GameStates.ShopGame: SetState(new ShopGameState(this)); break;
                case GameStates.WinGame: SetState(new WinGameState(this)); break;
                case GameStates.LoseGame: SetState(new LoseGameState(this)); break;
                default: Debug.LogError("GameState inconnu");break;
            }
        }
        public void StorePreviousState() => PreviousState = currentState;
        public void ResumePreviousState() => SetState(PreviousState);
    }
}