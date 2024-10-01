using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script{
    public class Spawner : MonoBehaviour{
        [FormerlySerializedAs("PickUps")]
        [Header("Array PickUp")]
        [SerializeField] private GameObject[] pickUps;
        [SerializeField] private int[] scores;
        [FormerlySerializedAs("MaxInstanceCounts")] [SerializeField] private int[] maxInstanceCounts;
        [SerializeField] private int[] instanceCounts;
        [Header("Setting PickUp")]
        [SerializeField] private GameObject parentGameObject;
        [SerializeField] private float minX, maxX, minZ, maxZ;
        [SerializeField] private float minimumDistanceBetweenPickUps;
        
        [Header("Setting Obstacle")]
        [SerializeField] private GameObject[] obstacles;
        [SerializeField] private int[] obstacleFrequencies;
        [SerializeField] private GameObject parentGameObjectObstacle;
        [SerializeField] private float minXObstacle, maxXObstacle, minZObstacle, maxZObstacle, minDistanceToOtherObstacles;

        private void Start(){
            SpawnObstacles();
            instanceCounts = new int[pickUps.Length];
            SpawnPickUps();
        }

        public void SpawnPickUps() {
            bool allMaxedOut;
            List<Vector3> spawnedLocations = new List<Vector3>();
            do {
                allMaxedOut = true;
                for (int index = 0; index < pickUps.Length; index++) {
                    if (instanceCounts[index] < maxInstanceCounts[index]) {
                        allMaxedOut = false;
                        Vector3 randomPosition;

                        do {
                            randomPosition = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
                        } while(spawnedLocations.Any(x => Vector3.Distance(x, randomPosition) < minimumDistanceBetweenPickUps) || GameObject.FindGameObjectsWithTag("Player").Concat(GameObject.FindGameObjectsWithTag("Chat")).Any(obstacle => Vector3.Distance(obstacle.transform.position, randomPosition) < 5));
                        GameObject newPickup = Instantiate(pickUps[index], randomPosition, Quaternion.identity);

                        if (newPickup == null){
                            continue;
                        }
                        if (index == 2) {
                            newPickup.transform.Rotate(0, 90, 0);
                        }
                        if (index == 3) {
                            newPickup.transform.Rotate(-90, 0, 0);
                        }

                        instanceCounts[index]++;

                        if (parentGameObject != null){
                            newPickup.transform.parent = parentGameObject.transform;
                        }

                        spawnedLocations.Add(randomPosition);
                    }
                }
            }while (!allMaxedOut);
        }

        public int GetInstanceCount(int index){
            if (index >= 0 && index < instanceCounts.Length){
                return instanceCounts[index];
            }else{
                return -1;
            }
        }
        public int GetScore(int index) {
            return scores[index];
        }
        public void DecrementInstanceCount(int index){
            if (index >= 0 && index < instanceCounts.Length){
                instanceCounts[index]--;
            }
        }
        public void IncrementMaxInstanceCount(int index, int numberMore){
            if(index >= 0 && index < maxInstanceCounts.Length){
                maxInstanceCounts[index]+=numberMore;
            }
        }
        public void DecrementMaxInstanceCount(int index, int numberLess){
            if(index >= 0 && index < maxInstanceCounts.Length){
                if(maxInstanceCounts[index] > 0){
                    maxInstanceCounts[index]-=numberLess;
                }
            }
        }
        public void CheckAndDestroyExcessInstances(){
            for (int i = 0; i < pickUps.Length; i++){
                if (instanceCounts[i] > maxInstanceCounts[i]){
                    List<GameObject> instances = new List<GameObject>();
                    foreach(Transform child in parentGameObject.transform){
                        if (child.gameObject.CompareTag(pickUps[i].tag)){
                            instances.Add(child.gameObject);
                        }
                    }

                    int excess = instances.Count - maxInstanceCounts[i];
                    for (int j = 0; j < excess; j++){
                        int indexToRemove = Random.Range(0, instances.Count);
                        Destroy(instances[indexToRemove]);
                        instances.RemoveAt(indexToRemove);
                        instanceCounts[i]--;
                    }
                }
            }
        }
        void SpawnObstacles(){
            List<Vector3> spawnedObstacleLocations = new List<Vector3>();
            for (int i = 0; i < obstacles.Length; i++){
                for (int j = 0; j < obstacleFrequencies[i]; j++){
                    Vector3 randomPosition;
                    do {
                        randomPosition = new Vector3(Random.Range(minXObstacle, maxXObstacle), 0.5f, Random.Range(minZObstacle, maxZObstacle));
                    }while(spawnedObstacleLocations.Any(x => Vector3.Distance(x, randomPosition) < minDistanceToOtherObstacles) || GameObject.FindGameObjectsWithTag("Player").Concat(GameObject.FindGameObjectsWithTag("Chat")).Any(obstacle => Vector3.Distance(obstacle.transform.position, randomPosition) < 5));

                    GameObject newObstacle = Instantiate(obstacles[i], randomPosition, Quaternion.Euler(0, Random.Range(0, 360), 0));
                    newObstacle.transform.parent = parentGameObjectObstacle.transform;

                    spawnedObstacleLocations.Add(randomPosition);
                }
            }
        }
    }
}