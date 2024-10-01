using UnityEngine;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.State_Machine{
    public abstract class Fsm : MonoBehaviour{
        protected State currentState, initState; //Necessaire de l'initialisser dans les scripts qui hérite de FSM

        protected virtual void OnEnable(){
            if (initState != null)
                SetState(initState);
            else 
                Debug.LogError("Init State is null in FSM");
        }

        void Update(){
            currentState.OnStateUpdate();
        }

        public virtual void SetState(State pState){
            if(currentState != null)
                currentState.OnStateExit();
            currentState = pState;
            if(currentState != null)
                currentState.OnStateEnter();
            else
                Debug.LogError("Current State is null in FSM");
        }
    }
}
