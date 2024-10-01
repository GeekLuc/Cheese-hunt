/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.State_Machine{
    public abstract class State{
        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        //public abstract void OnStateFixedUpdate(); //Seulement si on a besoin de physique dans nos states
        public abstract void OnStateExit();
        
    }
}
