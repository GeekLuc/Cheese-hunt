using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD_LANCEMENT : MonoBehaviour{

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
