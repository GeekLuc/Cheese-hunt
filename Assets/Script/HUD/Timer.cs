using System;
using System.Collections;
using Cinemachine;
using Script.State_Machine;
using Script.State_Machine.States;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.HUD{
    public class Timer : MonoBehaviour{
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;
        [SerializeField] private float timeMax;
        [SerializeField] private TextMeshProUGUI timerText;
        [FormerlySerializedAs("Alert")] [SerializeField] private GameObject alert;
        [FormerlySerializedAs("TimerImage")] [SerializeField] private Image timerImage;
        [SerializeField] private Spawner pickUpSpawner;
        private float _initialSize, _timeLeft;
        private  Color _timerColor ;
        void Start(){
            if (PlayerPrefs.HasKey("gameTime")) {
                timeMax = PlayerPrefs.GetFloat("gameTime");
            }

            _timeLeft = timeMax;
            timerImage.fillAmount = 1.0f;
            StartCoroutine(TimerCoroutine());
            _virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        IEnumerator TimerCoroutine(){
            while (_timeLeft > 0){
                yield return new WaitForSeconds(1f);
                _timeLeft -= 1f;
                UpdateTimerText();
                UpdateTimerImage();
                UpdateSpawnPickUp();

                if (_timeLeft <= 30f){
                    alert.gameObject.SetActive(true);
                    if (_timeLeft>=27f){
                        StartCoroutine(OffTimer(0.25f));
                    }
                }
                if (_timeLeft <= 15f){
                    StartCoroutine(OffTimer(0.125f));
                    StartCoroutine(CameraShake(15f, 1f));
                }
            }
            GameManagerFsm.Instance.ChangeState(GameStates.LoseGame);
        }

        void UpdateTimerText(){
            int minutes = Mathf.FloorToInt(_timeLeft / 60f);
            int seconds = Mathf.FloorToInt(_timeLeft % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        void UpdateTimerImage(){
            timerImage.fillAmount = _timeLeft / timeMax;
            ColorChanger();
        }
        void ColorChanger(){
            if (_timeLeft >= (timeMax/3)*2){
                _timerColor = Color.green;
            }else if (_timeLeft >= (timeMax/3)){
                _timerColor = Color.yellow;
            }else{
                _timerColor = Color.red;
            }
            
            timerImage.color = _timerColor;
        }

        void UpdateSpawnPickUp(){
            if (Math.Abs(_timeLeft - (timeMax/3)*2) < 0.1f){
                pickUpSpawner.DecrementMaxInstanceCount(0,4); //de 17 ca passe a 13
                pickUpSpawner.IncrementMaxInstanceCount(1,3); // de 8 ca passe a 11
                pickUpSpawner.IncrementMaxInstanceCount(2,1); // de 3 ca passe a 4
            }else  if (Math.Abs(_timeLeft - (timeMax/3)) < 0.1f){
                pickUpSpawner.DecrementMaxInstanceCount(0,8); //de 17 ca passe a 9
                pickUpSpawner.IncrementMaxInstanceCount(1,6); //de 8 ca passe a 14
                pickUpSpawner.IncrementMaxInstanceCount(2,2); // de 3 ca passe a 5
            }
            pickUpSpawner.CheckAndDestroyExcessInstances();
            pickUpSpawner.SpawnPickUps();
        }
        public IEnumerator OffTimer(float intervalle){
            timerImage.enabled = false;
            alert.gameObject.SetActive(false);
            Handheld.Vibrate();
            yield return new WaitForSeconds(intervalle);
            alert.gameObject.SetActive(true);
            timerImage.enabled = true;
            Handheld.Vibrate();
            yield return new WaitForSeconds(intervalle);
        }
        
        public IEnumerator CameraShake(float duration, float magnitude){
            float timer = 0;
            while (timer < duration){
                _virtualCameraNoise.m_AmplitudeGain = magnitude;
                timer += Time.deltaTime;
                yield return null;
            }
            _virtualCameraNoise.m_AmplitudeGain = 0;
        }
    }
}