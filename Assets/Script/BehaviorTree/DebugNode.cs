using UnityEngine;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.BehaviorTree{
    public class DebugNode : Node{
        private string message;
        private NodeState state;

        public DebugNode(string message, NodeState state) {
            this.message = message;
            this.state = state;
        }
        
        public override BehaviorTree.NodeState Evaluate() {
            Debug.Log($"<color=#35E88C>Debug Node : {message}</color>");
            return state;
        }
    }
}
