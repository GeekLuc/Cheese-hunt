using UnityEngine;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.Ennemy{
    public class NearbyCatAlert : MonoBehaviour{
        [SerializeField] private GameObject enemy;
        [SerializeField] private GameObject alertObject; 
        [SerializeField] private float detectionRange;

        void Update(){
            float totalDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (totalDistance < detectionRange){
                if(!alertObject.activeSelf){
                    alertObject.SetActive(true);
                }
            }else{
                if(alertObject.activeSelf){
                    alertObject.SetActive(false);
                } 
            }
        }
    }
}