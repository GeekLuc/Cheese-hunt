using UnityEngine;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.Ennemy{
    public class ArrowDirection : MonoBehaviour{
        [SerializeField] private Transform target;

        private void Update(){
            Vector3 targetPosition = target.transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
    }
}
