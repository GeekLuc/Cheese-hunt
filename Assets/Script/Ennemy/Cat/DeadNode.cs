using Script.BehaviorTree;
using System.Collections;
using Script.HUD;
using Script.State_Machine;
using Script.State_Machine.States;
using UnityEngine;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.Ennemy{
    public class DeadNode : Node{
        private AudioSource audioSource;
        private AudioClip hitSound;
        private GameObject _targetObject;
        private MonoBehaviour monoBehaviour;

        public DeadNode(MonoBehaviour monoBehaviour, AudioSource audioSource, AudioClip hitSound, GameObject targetObject){
            this.audioSource = audioSource;
            this.hitSound = hitSound;
            this.monoBehaviour = monoBehaviour;
            this._targetObject = targetObject;
        }

        public override NodeState Evaluate(){
            GameObject.Find("GameManager").GetComponent<GameManagement>().DeUpdateScore();
            monoBehaviour.StartCoroutine(Blink(_targetObject));
            monoBehaviour.StartCoroutine(DelayedAction());
            return NodeState.SUCCESS;
        }

        private IEnumerator DelayedAction(){
            yield return new WaitForSeconds(0.4f);
            Handheld.Vibrate();
            audioSource.PlayOneShot(hitSound);
            yield return new WaitForSeconds(0.1f);
            GameManagerFsm.Instance.ChangeState(GameStates.LoseGame);
        }
        IEnumerator Blink(GameObject player){
            SkinnedMeshRenderer renderer = player.GetComponent<SkinnedMeshRenderer>();
            if (renderer != null){
                for (int i = 0; i < 1; i++){
                    renderer.enabled = false;
                    yield return new WaitForSeconds(0.05f);
                    renderer.enabled = true;
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
}