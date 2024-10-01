using System.Collections.Generic;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.BehaviorTree{

    public class Selector : Node{
        public Selector() : base() {}
        public Selector(List<Node> children) : base(children) {}

        public override NodeState Evaluate(){
            foreach (Node child in children){
                switch(child.Evaluate()){
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        return NodeState.SUCCESS;
                    case NodeState.RUNNING:
                        return NodeState.RUNNING;
                    default:
                        continue;
                }
            }
            
			Reset();
            return NodeState.FAILURE;
        }

		public override void Reset(){
			foreach(Node child in children)
				child.Reset();
		}
    }

}