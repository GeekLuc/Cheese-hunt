using System.Collections;
using Script.HUD;
using Script.State_Machine;
using Script.State_Machine.States;
using UnityEngine;
using UnityEngine.Serialization;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.Ennemy{
    public class Dead : MonoBehaviour{
        private GameObject _targetObject;
        public AudioSource audioSource;
        [FormerlySerializedAs("HitSound")] public AudioClip hitSound;
        void Start(){
            _targetObject = GameObject.FindWithTag("EnfantsMouse");
            GameObject soundManager = GameObject.Find("SoundManager");
            if (soundManager != null){
                audioSource = soundManager.GetComponent<AudioSource>();
            }else{
                Debug.LogError("SoundManager object not found");
            }
        }
        
        private void OnTriggerEnter(Collider other){
            if (other.CompareTag("Player")){
                Handheld.Vibrate();
                audioSource.PlayOneShot(hitSound); 
                GameObject.Find("GameManager").GetComponent<GameManagement>().DeUpdateScore();
                StartCoroutine(Blink(_targetObject));
                StartCoroutine(DelayedAction());
            }
        }

        private IEnumerator DelayedAction(){
            yield return new WaitForSeconds(0.5f);
            GameManagerFsm.Instance.ChangeState(GameStates.LoseGame);
        }
        
        IEnumerator Blink(GameObject player){
            SkinnedMeshRenderer renderer = player.GetComponent<SkinnedMeshRenderer>();
            if(renderer != null){
                for (int i=0; i<1; i++){
                    renderer.enabled = false;
                    yield return new WaitForSeconds(0.05f);
                    renderer.enabled = true;
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
}