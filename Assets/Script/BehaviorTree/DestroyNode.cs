using UnityEngine;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.BehaviorTree{
	public class DestroyNode : Node{
		GameObject objectToDestroy;

		public DestroyNode(GameObject objectToDestroy){
			this.objectToDestroy = objectToDestroy;
		}

		public override NodeState Evaluate(){
			try{
				Object.Destroy(objectToDestroy);
				return NodeState.SUCCESS;
			}
			catch{
				return NodeState.FAILURE;
			}
		}
	}
}
