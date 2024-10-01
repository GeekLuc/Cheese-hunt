using System;
using Script.BehaviorTree;
using UnityEngine;

namespace Script.Ennemy.Trap{
    public class IsTrapActived : Node{
        private TrapSpeed trapSpeed;
    
        public IsTrapActived(TrapSpeed trapSpeed){
            this.trapSpeed = trapSpeed;
        }

        public override NodeState Evaluate(){
            if (trapSpeed.hasBeenActivated){
                return NodeState.FAILURE;
            }
            return NodeState.SUCCESS;
        }
    }
}
