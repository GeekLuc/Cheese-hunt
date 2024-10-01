using System.Collections.Generic;
using Script.BehaviorTree;
using Script.Ennemy.Trap;
using Script.Player;
using UnityEngine;
using UnityEngine.AI;
using DebugNode = Script.BehaviorTree.DebugNode;
using Tree = Script.BehaviorTree.Tree;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.Ennemy.Cat{
	public class CatBT : Tree{ 
		[SerializeField] private float speedCatHunting = 6.5f;
		[SerializeField] private float fovRange ;
		[SerializeField] private float attackRadius;
		[SerializeField] private NavMeshAgent agent;
		[SerializeField] private float range;
		[SerializeField] private AudioClip hitSound;
		private MovePlayer Player => FindObjectOfType<MovePlayer>();

		protected override Node SetupTree(){
			GameObject targetObject = GameObject.FindWithTag("EnfantsMouse");
			GameObject soundManager = GameObject.Find("SoundManager");
			AudioSource audioSource = soundManager.GetComponent<AudioSource>();
			Node root = new Selector(new List<Node>{
				new Selector(new List<Node>{
					new Sequence(new List<Node>{
						new CheckRange(transform, Player.transform, attackRadius),
						new DeadNode(this, audioSource, hitSound, targetObject),
					}),
					new Sequence(new List<Node>{
						new CheckRange(transform, Player.transform, fovRange),
						new NavMeshAtoB(agent, Player.transform, speedCatHunting),
					}),
					new NavMeshPatrol(agent,speedCatHunting,range,6),
				}),
			});
			return root;
		}
		
		private void Update() {
			base.Update();
			agent.speed = speedCatHunting;
		}

		private void OnDrawGizmos() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, attackRadius);

            Gizmos.color = Color.yellow;
   			Gizmos.DrawWireSphere(transform.position, fovRange);
		}
		
	}
}