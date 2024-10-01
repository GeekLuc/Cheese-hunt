using System.Collections.Generic;
using Script.BehaviorTree;
using Script.Player;
using UnityEngine;
using Tree = Script.BehaviorTree.Tree;

namespace Script.Ennemy.Trap{
    public class TrapSpeed : Tree{
        public float checkRange, pourcentage, delay;
        public bool hasBeenActivated;
        public Transform target => FindObjectOfType<MovePlayer>().GetComponent<Transform>();
        protected override Node SetupTree(){
            Node root = new Selector(new List<Node> {
                    new Sequence(new List<Node> {
                        new CheckRange(transform, target, checkRange),
                        new TrapActived(pourcentage, delay, this, target),
                    }),

                    new Selector(new List<Node> {
                        new IsTrapActived(this),
                        new TrapDisabled(this, target, checkRange),
                    }),
                });
            return root; 
        }
        public void DeactivateTrap(){
            hasBeenActivated = false;
        }
        public void ActivateTrap(){
            hasBeenActivated = true;
        }

        private void OnDrawGizmos(){
            Gizmos.DrawWireSphere(transform.position, checkRange);
        }
    }
}