using UnityEngine;

public class Script_001 : MonoBehaviour{
    [SerializeField] float sensitivity = 0.1f, joystickRadius = 0.3f;
    [SerializeField] AnimationCurve joystickCurve;
    float maxMagnitude;
    Vector2 rawPosition;
    void Start(){
        maxMagnitude = Screen.width * joystickRadius;
    }

    void Update(){
        ManageInputs();
    }

    void ManageInputs(){
        if(Input.touchCount < 1) 
            return;

        Touch touch = Input.GetTouch(0);
        
        if(touch.phase == TouchPhase.Began){
            rawPosition = touch.position;
        }else{
            Vector2 joystickDirectionAndIntensity = touch.position - rawPosition;

            float magnitude = joystickDirectionAndIntensity.magnitude;
            float tValue = magnitude / maxMagnitude;
            float t = Mathf.Lerp(0, 1, tValue);

            float curveValue = joystickCurve.Evaluate(t);

            Vector2 direction = joystickDirectionAndIntensity.normalized ;
            Vector3 movement = new Vector3(direction.x, 0, direction.y) ;
            transform.position += movement * curveValue * sensitivity * Time.deltaTime ;
        }
    }
}
