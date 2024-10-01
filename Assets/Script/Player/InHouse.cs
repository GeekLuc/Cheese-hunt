using Script.HUD;
using Script.State_Machine;
using Script.State_Machine.States;
using UnityEngine;

/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.Player{
    public class InHouse : MonoBehaviour{
        [SerializeField] private  GameObject gameManagement;
        private void OnTriggerEnter(Collider other){
            if (other.CompareTag("Maison")){
                GameManagement gm = gameManagement.GetComponent<GameManagement>();
                gm.SaveScore();
                GameManagerFsm.Instance.ChangeState(GameStates.WinGame);
            }
        }
    }
}