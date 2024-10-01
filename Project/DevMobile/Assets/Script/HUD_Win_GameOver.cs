using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour{
    public TextMeshProUGUI scoreText;
    private int score;
    void Start(){
        LoadScore();
    }
    public void LoadScore(){
        score = PlayerPrefs.GetInt("Score", 0);
        UpdateScoreText();
    }
    private void UpdateScoreText(){
        scoreText.text = "SCORE : "+score.ToString();
    }
    public void Replay(){
        SceneManager.LoadScene("NV_test");
    }

    public void Quitter(){
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
