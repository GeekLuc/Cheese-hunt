using System.Collections;
using Script.HUD;
using UnityEngine;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.Player{
    public class PickUp : MonoBehaviour{
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip pickupSound,SoundPepper;
        private Spawner _pickUpSpawner;
        [SerializeField] private MovePlayer movePlayerInstance;
        void Start(){
            _pickUpSpawner = FindObjectOfType<Spawner>();
        }

        IEnumerator SpawnNewPickUp(){
            yield return new WaitForSeconds(2f);
            _pickUpSpawner.SpawnPickUps();                
        }

        private void OnTriggerEnter(Collider other){
            if (other.CompareTag("PickUpScore1") || other.CompareTag("PickUpScore3") || other.CompareTag("PickUpScore10") || other.CompareTag("PickUpPepper")){
                audioSource.PlayOneShot(pickupSound); 
                Destroy(other.gameObject);
                StartCoroutine(SpawnNewPickUp());
            }

            if (other.CompareTag("PickUpScore1")){
                int score = PlayerPrefs.GetInt("Pickup1Score", 1);
                GameObject.Find("GameManager").GetComponent<GameManagement>().AddScore(score); 
                _pickUpSpawner.DecrementInstanceCount(0);
            }
            if (other.CompareTag("PickUpScore3")){
                int score = PlayerPrefs.GetInt("Pickup2Score", 3);
                GameObject.Find("GameManager").GetComponent<GameManagement>().AddScore(score); 
                _pickUpSpawner.DecrementInstanceCount(1);
            }
            if (other.CompareTag("PickUpScore10")){
                int score = PlayerPrefs.GetInt("Pickup3Score", 10); 
                GameObject.Find("GameManager").GetComponent<GameManagement>().AddScore(score); 
                _pickUpSpawner.DecrementInstanceCount(2);
            }
            if (other.CompareTag("PickUpPepper")) {
                audioSource.PlayOneShot(SoundPepper); 
                Destroy(other.gameObject);
                float pepperDuration = PlayerPrefs.GetFloat("pepperDuration", 5f);
                StartCoroutine(TemporaryIncreaseInSspeed(pepperDuration, 1.5f));
            }
        }
        IEnumerator TemporaryIncreaseInSspeed(float delay, float increasedFactor) {
            float oldSpeed = movePlayerInstance.speed;
            movePlayerInstance.speed = (oldSpeed * increasedFactor);
            yield return new WaitForSeconds(delay);
            movePlayerInstance.speed = oldSpeed;
        }
    }
}