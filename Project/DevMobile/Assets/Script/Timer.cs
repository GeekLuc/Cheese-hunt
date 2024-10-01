using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Timer : MonoBehaviour{
    public float timeLeft = 300f;
    public TextMeshProUGUI timerText, textAlert;

    void Start(){
        StartCoroutine(TimerCoroutine());
        textAlert.gameObject.SetActive(false);
    }


    IEnumerator TimerCoroutine(){
        while (timeLeft > 0){
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
            UpdateTimerText();

            if (timeLeft <= 30f){
                textAlert.gameObject.SetActive(true);
            }
        }
        SceneManager.LoadScene("Game_Over");
    }

    void UpdateTimerText(){
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
