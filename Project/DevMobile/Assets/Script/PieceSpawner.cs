using UnityEngine;
public class PieceSpawner : MonoBehaviour{
    public GameObject PickUpPrefab;
    public int numberOfPickUp = 25, numberOfPickUpON = 0;
    public float minX = -48f, maxX = 48f, minZ = -48f, maxZ = 48f, minDistance = 10f;
    private void Start(){
        SpawnPickUp();
    }
    public void SpawnPickUp(){
        for (int i = numberOfPickUpON; i < numberOfPickUp; i++){
            Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
            bool isTooClose = false;
            foreach (GameObject piece in GameObject.FindGameObjectsWithTag("PickUpScore")){
                if (Vector3.Distance(spawnPosition, piece.transform.position) < minDistance){
                    isTooClose = true;
                    break;
                }
            }

            if (isTooClose){
                i--;
                continue;
            }

            Instantiate(PickUpPrefab, spawnPosition, Quaternion.identity);
            numberOfPickUpON++;
        }
    }
}