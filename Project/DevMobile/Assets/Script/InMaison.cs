using UnityEngine;
using UnityEngine.SceneManagement;

public class InMaison : MonoBehaviour{
    public GameObject gameManagement;
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Maison")){
            Debug.Log("Dans la maison");
            GameManagement gm = gameManagement.GetComponent<GameManagement>();
            gm.SaveScore();
            SceneManager.LoadScene("Win");
        }
    }
}
