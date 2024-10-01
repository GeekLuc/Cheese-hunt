using TMPro;
using UnityEngine;

public class GameManagement : MonoBehaviour{
    public TextMeshProUGUI scoreText;
    private int score;

    void Start(){
        score = 0;
        UpdateScoreText();
    }
    public void UpdateScore(){
        score++;
        UpdateScoreText();
    }
    private void UpdateScoreText(){
        scoreText.text = score.ToString();
    }
    public void SaveScore(){
        PlayerPrefs.SetInt("Score", score);
    }

}
