using Script.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.Ennemy.Cat{
    public class NavMeshPatrol : Node{
        private NavMeshAgent agent;
        private float speed;
        private float range;
        private Vector3 destPoint;
        private bool walkpointSet;
        [SerializeField] LayerMask groundLayer; 

        public NavMeshPatrol(NavMeshAgent agent, float speed, float range, LayerMask groundLayer) {
            this.agent = agent;
            this.speed = speed;
            this.range = range;
            this.groundLayer = groundLayer;
        }

        public override NodeState Evaluate(){
            agent.speed = speed;

            Patrol();

            return NodeState.RUNNING;
        }

        void Patrol(){
            if (!walkpointSet) SearchForDest();
            if (walkpointSet) agent.SetDestination(destPoint);
            if (Vector3.Distance(agent.transform.position, destPoint) < 10) walkpointSet = false;
        }

        void SearchForDest(){
            float z = Random.Range(-range, range);
            float x = Random.Range(-range, range);

            destPoint = new Vector3(agent.transform.position.x + x, agent.transform.position.y, agent.transform.position.z + z);

            if (Physics.Raycast(destPoint, Vector3.down, groundLayer)){
                walkpointSet = true;
            }
        }
    }
}