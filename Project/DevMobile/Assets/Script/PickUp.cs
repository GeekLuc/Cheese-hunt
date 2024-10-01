using System.Collections;
using UnityEngine;


public class PickUp : MonoBehaviour{
    IEnumerator SpawnNewPickUp(){
        yield return new WaitForSeconds(2f);
        GameObject.Find("PickUpSpawner").GetComponent<PieceSpawner>().SpawnPickUp();
        
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("PickUpScore")){
            GameObject.Find("GameManager").GetComponent<GameManagement>().UpdateScore();
            GameObject.Find("PickUpSpawner").GetComponent<PieceSpawner>().numberOfPickUpON--;
            Destroy(other.gameObject);
            StartCoroutine(SpawnNewPickUp());
        }
    }
}
