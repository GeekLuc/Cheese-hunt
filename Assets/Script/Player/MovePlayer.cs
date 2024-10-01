using Script.HUD;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.Player{
    public class MovePlayer : MonoBehaviour{
        [SerializeField] private float joystickRadius = 0.3f;
        [SerializeField] private AnimationCurve joystickCurve;
        private float _maxMagnitude;
        private Vector2 _rawPosition;
        private Vector3 _movement, _targetDirection, _newPosition;
        private Quaternion _newRotation;
        public float speed ;
        private UpgradeSystem _upgradeSystem;

        private void Start(){
            _maxMagnitude = Screen.width * joystickRadius;
            _movement = new Vector3();
            _targetDirection = new Vector3();
            _newPosition = new Vector3();

            if (PlayerPrefs.HasKey("speed")) {
                float speed = PlayerPrefs.GetFloat("speed");
                SetSensitivity(speed);
            }
        }

        public float GetSensitivity(){
            return speed;
        }

        private float SetSensitivity(float p_sensitivity){
            speed = p_sensitivity;
            return speed;
        }

        private void Update(){
            ManageInputs();
        }

        private void ManageInputs(){
            if(Input.touchCount < 1) 
                return;

            Touch touch = Input.GetTouch(0);

            if(touch.phase != TouchPhase.Began){
                Vector2 joystickDirectionAndIntensity = touch.position - _rawPosition;
                float magnitude = joystickDirectionAndIntensity.magnitude;
                float tValue = magnitude / _maxMagnitude;
                float t = Mathf.Lerp(0, 1, tValue);
                float curveValue = joystickCurve.Evaluate(t);
                Vector2 direction = joystickDirectionAndIntensity.normalized ;

                _movement.Set(direction.x, 0, direction.y);
                transform.position += _movement * (curveValue * speed * Time.deltaTime) ;

                _targetDirection.Set(_movement.x, 0.08f, _movement.z);
                float yRotation = Mathf.Atan2(_targetDirection.x, _targetDirection.z) * Mathf.Rad2Deg;
                _newRotation = Quaternion.Euler(0, yRotation, 0);
                transform.rotation = _newRotation;

                _newPosition.Set(transform.position.x,  0.08f, transform.position.z);
                transform.position = _newPosition;
            } else {
                _rawPosition = touch.position;
            }
        }
        public void ReduceSpeed(float percent, float time) {
            StartCoroutine(ReduceSpeedCoroutine(percent, time));
        }

        public IEnumerator ReduceSpeedCoroutine(float percent, float time) {
            float tempSpeed = speed;
            speed = speed * percent;
            yield return new WaitForSeconds(tempSpeed);
            speed = tempSpeed;
        }
    }
}