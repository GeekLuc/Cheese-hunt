using Script.BehaviorTree;
using UnityEngine;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.Ennemy.Trap{
    public class CheckRange : Node{
        private Transform thisTransform;
        private float checkRadius;
        public Transform target;

        public CheckRange(Transform thisTransform, Transform targetTransform, float checkRadius) {
            this.thisTransform = thisTransform;
            this.checkRadius = checkRadius;
            this.target = targetTransform;
        }

        public CheckRange(Transform thisTransform, string targetDataName, float checkRadius) {
            this.thisTransform = thisTransform;
            this.checkRadius = checkRadius;
            this.target = (Transform)GetData(targetDataName);
        }

        public override NodeState Evaluate() {
            if(target == null) return NodeState.FAILURE;
            if (Vector3.Distance(thisTransform.position, target.position) < checkRadius) {
                return NodeState.SUCCESS;
            }

            return NodeState.FAILURE;
        }
    }
}
