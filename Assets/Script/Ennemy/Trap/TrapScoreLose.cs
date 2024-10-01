using System.Collections;
using Script.HUD;
using UnityEngine;
using UnityEngine.Serialization;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.Ennemy.Trap{
    public class TrapScoreLose : MonoBehaviour{
        private GameObject _targetObject;
        public AudioSource audioSource;
        [FormerlySerializedAs("HitSound")] public AudioClip hitSound;
        void Start() {
            _targetObject = GameObject.FindWithTag("EnfantsMouse");
            GameObject soundManager = GameObject.Find("SoundManager");
            if (soundManager != null) {
                audioSource = soundManager.GetComponent<AudioSource>();
            } else {
                Debug.LogError("SoundManager object not found");
            }
        }
        private void OnCollisionEnter(Collision collision){
            if (collision.gameObject.CompareTag("Player")){
                Handheld.Vibrate();
                audioSource.PlayOneShot(hitSound); 
                GameObject.Find("GameManager").GetComponent<GameManagement>().DeUpdateScore();
                if (_targetObject != null){
                    StartCoroutine(Blink(_targetObject));
                } else {
                    Debug.Log("Cannot find GameObject with tag 'EnfantsMouse'");
                }
            }
        }
        IEnumerator Blink(GameObject player){
            SkinnedMeshRenderer renderer = player.GetComponent<SkinnedMeshRenderer>();
            if(renderer != null){
                for (int i=0; i<5; i++){
                    renderer.enabled = false;
                    yield return new WaitForSeconds(0.05f);
                    renderer.enabled = true;
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
}