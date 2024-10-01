using Script.BehaviorTree;
using Script.Player;
using UnityEngine;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.Ennemy.Trap{
    public class TrapActived : Node{
        public TrapSpeed trapBT;

        public float reduceSpeedPercent;
        public float reduceSpeedTime;
        public Transform target;

        public TrapActived(float reduceSpeedPercent, float reduceSpeedTime, TrapSpeed trapBT, Transform targetTransform) {
            this.reduceSpeedPercent = reduceSpeedPercent;
            this.reduceSpeedTime = reduceSpeedTime;
            this.trapBT = trapBT;
            this.target = targetTransform;
        }

        public override NodeState Evaluate(){
            if(trapBT.hasBeenActivated){
                return NodeState.FAILURE;
            }
            
            if(target == null){
                return NodeState.FAILURE;
            }

            MovePlayer guard = target.GetComponent<MovePlayer>();
            if(guard == null){
                return NodeState.FAILURE;
            }

            guard.ReduceSpeed(reduceSpeedPercent, reduceSpeedTime);
            trapBT.ActivateTrap();

            return NodeState.SUCCESS;
        }
    }
}