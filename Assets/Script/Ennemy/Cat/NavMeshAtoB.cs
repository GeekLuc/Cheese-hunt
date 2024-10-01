using Script.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace Script.Ennemy.Cat{
    public class NavMeshAtoB : Node{
        private NavMeshAgent agent;
        private Transform waypoint;
        private float speed, speed_t;
        private float stopDistance;

        public NavMeshAtoB(NavMeshAgent agent, Transform waypoint, float speed, float stopDistance = 0.1f) {
            this.agent = agent;
            this.waypoint = waypoint;
            this.speed = speed;
            this.stopDistance = stopDistance;
        }

        public NavMeshAtoB(NavMeshAgent agent, CatBT tree, float speed, float stopDistance = 0.1f) {
            this.agent = agent;
            this.speed = speed;
            this.stopDistance = stopDistance;
        }

        public NavMeshAtoB(NavMeshAgent agent, string dataName, float speed, float stopDistance = 0.1f) {
            this.agent = agent;
            waypoint = (Transform)GetData(dataName);
            this.speed = speed;
            this.stopDistance = stopDistance;
        }
    
        public override NodeState Evaluate(){
            if(agent == null) return NodeState.FAILURE;
            if(waypoint == null){
                return NodeState.FAILURE;
            }

            if (Vector3.Distance(agent.transform.position, waypoint.position) < stopDistance){
                agent.speed = speed_t;
                return NodeState.SUCCESS;
                
            }else{
                speed_t = agent.speed;
                agent.speed = speed;
                agent.SetDestination(waypoint.position);
            }
            return NodeState.RUNNING;
        }
    }
}
