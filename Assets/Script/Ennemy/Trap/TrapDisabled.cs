using System;
using Script.BehaviorTree;
using UnityEngine;

namespace Script.Ennemy.Trap{
    public class TrapDisabled : Node{
        public TrapSpeed TrapSpeed;
        private float checkRange;
        private Transform target;
    
        public TrapDisabled(TrapSpeed trapBT, Transform targetTransform, float checkRadius){
            this.TrapSpeed = trapBT;
            this.target = targetTransform;
            this.checkRange = checkRadius;
        }
    
        public override NodeState Evaluate(){
            float distance = Vector3.Distance(TrapSpeed.transform.position, target.position);

            if (distance > checkRange){
                TrapSpeed.DeactivateTrap();
                return NodeState.SUCCESS;
            }
            return NodeState.FAILURE;            
        }
    }
}
