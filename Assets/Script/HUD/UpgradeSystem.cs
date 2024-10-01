using UnityEngine;
using TMPro;
using System.Collections.Generic;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.HUD {
    public class UpgradeSystem : MonoBehaviour{
        
        [System.Serializable]
            public class Upgrade {
                public int level = 1;
                public int maxLevel = 4;
                public int[] cost;
                public List<GameObject> upgradeLevels;
            }
        [Header("Text Mesh Pro")]
        [SerializeField] private TextMeshProUGUI MonnaieText; 
        [SerializeField] private TextMeshProUGUI[] priceUpgradeTexts = new TextMeshProUGUI[4];
        [SerializeField] private TextMeshProUGUI[] upgradeDescriptionTexts = new TextMeshProUGUI[6];
        
        [Header("Materials")]
        [SerializeField] private Material onMaterial;
        [SerializeField] private Material offMaterial;
        
        [Header("Setting Shop")]
        public Upgrade[] upgrades = new Upgrade[6];
        
        [Header("Setting Speed")]
        public float[] speedLevels = new float[4];
        public float GetSpeed(int upgradeIndex) {
            return speedLevels[upgrades[upgradeIndex].level - 1];
        }
        public void SetSpeed(int upgradeIndex) {
            float newSpeed = speedLevels[upgrades[upgradeIndex].level - 1];
            Debug.Log("Speed sera de "+newSpeed);
            PlayerPrefs.SetFloat("speed", newSpeed);
        }
        
        [Header("Setting Pickup Points")]
        public int[] pickup1Points = new int[4];
        public int[] pickup2Points = new int[4];
        public int[] pickup3Points = new int[4];
        
        public int GetPickupPoints(int upgradeIndex, int pickupType) {
            switch (pickupType) {
                case 1: return pickup1Points[upgrades[upgradeIndex].level - 1];
                case 2: return pickup2Points[upgrades[upgradeIndex].level - 1];
                case 3: return pickup3Points[upgrades[upgradeIndex].level - 1];
                default: return 0;
            }
        }
        public void SetPickupScore(int upgradeIndex, int pickupType) {
            int newScore = GetPickupPoints(upgradeIndex, pickupType);
            PlayerPrefs.SetInt("Pickup" + pickupType + "Score", newScore);
        }
        
        [Header("Setting Pepper Duration Increase")]
        public float[] pepperDurationLevels = new float[4];
        public float GetPepperDuration(int upgradeIndex) {
            return pepperDurationLevels[upgrades[upgradeIndex].level - 1];
        }
        public void SetPepperDuration(int upgradeIndex) {
            float newDuration = GetPepperDuration(upgradeIndex);
            Debug.Log("The new pepper duration will be " + newDuration);
            PlayerPrefs.SetFloat("pepperDuration", newDuration);
        }
        
        [Header("Setting Game Time Increase")]
        public float[] gameTimeLevels = new float[4];
        public float GetGameTime(int upgradeIndex) {
            return gameTimeLevels[upgrades[upgradeIndex].level - 1];
        }
        public void SetGameTime(int upgradeIndex) {
            float newGameTime = GetGameTime(upgradeIndex);
            Debug.Log("The new game time will be " + newGameTime);
            PlayerPrefs.SetFloat("gameTime", newGameTime);
        }
        
        public void BuyUpgrade(int upgradeIndex){
            Upgrade upgrade = upgrades[upgradeIndex];
            int playerMoney = PlayerPrefs.GetInt("monnaie");

            if (upgrade.level < upgrade.maxLevel && playerMoney >= upgrade.cost[upgrade.level - 1]){
                int spentMoney = PlayerPrefs.GetInt("monnaieDepensee", 0);
                spentMoney += upgrade.cost[upgrade.level - 1];
                PlayerPrefs.SetInt("monnaieDepensee", spentMoney);
                PlayerPrefs.SetInt("monnaie", playerMoney - upgrade.cost[upgrade.level - 1]);
                upgrade.level++;
                PlayerPrefs.SetInt("upgrade_" + upgradeIndex, upgrade.level);

                foreach (var renderer in upgrade.upgradeLevels[upgrade.level - 1].GetComponentsInChildren<Renderer>()){
                    renderer.material = onMaterial;
                }
                UpdatePriceTexts();
                UpdateMonnaieText();
                UpdateDescription();
                if (upgradeIndex == 0){ //Amelioration speed joueur
                    SetSpeed(0);
                }
                if (upgradeIndex == 1){ // Amelioration Scoring
                    for (int i = 1; i <= 3; i++){
                        SetPickupScore(1, i);
                    }
                }
                if (upgradeIndex == 2){ // Pepper duration boost
                    SetPepperDuration(2);
                }
                if (upgradeIndex == 3){  //Timer in Game
                    SetGameTime(3);
                }
            }
        }
        void Start(){
            for (int i = 0; i < upgrades.Length; i++){
                if (PlayerPrefs.HasKey("upgrade_" + i)){
                    upgrades[i].level = PlayerPrefs.GetInt("upgrade_" + i);
                }

                for (int j = 0; j < upgrades[i].upgradeLevels.Count; j++){
                    var renderer = upgrades[i].upgradeLevels[j].GetComponent<Renderer>();
                    if (renderer != null){
                        renderer.material = j < upgrades[i].level ? onMaterial : offMaterial;
                    }
                }
            }
    
            if(PlayerPrefs.HasKey("pepperDuration")){
                pepperDurationLevels[2] = PlayerPrefs.GetFloat("pepperDuration");
            }
    
            UpdatePriceTexts();
            UpdateMonnaieText();
            UpdateDescription();
        }
      
        void OnEnable(){
            UpdatePriceTexts();
            UpdateMonnaieText();
            UpdateDescription();
        }
        private float GetUpgradeValue(int upgradeIndex){
            float value = 0.0f;
            switch (upgradeIndex){
                case 0: value = GetSpeed(upgradeIndex); break;
                case 2: value = GetPepperDuration(upgradeIndex); break;
                case 3: value = GetGameTime(upgradeIndex); break;
            }
            return value;
        }
        private float GetUpgradeValue(int upgradeIndex, int indexPickUP){
            return GetPickupPoints(upgradeIndex, indexPickUP);
        }
        private void UpdateMonnaieText(){
            int monnaie = PlayerPrefs.GetInt("monnaie");
            MonnaieText.text = monnaie.ToString();
        }

        private void UpdateDescription(){
            for (int k = 0; k < upgrades.Length; k++){
                switch (k){
                    case 0 : 
                        upgradeDescriptionTexts[k].text = "Actuel: \n" + GetUpgradeValue(k) ;
                        break;
                    case 1 : 
                        upgradeDescriptionTexts[k].text = "Actuel: \n" + GetUpgradeValue(k,1)+ " | "  + GetUpgradeValue(k,2)+ " | "  + GetUpgradeValue(k,3);
                        break;
                    case 2 : 
                        upgradeDescriptionTexts[k].text = "Actuel: \n" + GetUpgradeValue(k) + " secondes";
                        break;
                    case 3 : 
                        upgradeDescriptionTexts[k].text = "Actuel: \n" + GetUpgradeValue(k) + " secondes" ;
                        break;
                }
            }
        }
        private void UpdatePriceTexts() {
            for (int i = 0; i < upgrades.Length; i++){
                if(upgrades[i].level < upgrades[i].maxLevel) {
                    priceUpgradeTexts[i].text = upgrades[i].cost[upgrades[i].level - 1].ToString();
                } else {
                    priceUpgradeTexts[i].text = "Max";
                }
            }
        }
        public void ResetUpgrades(){
            int moneySpent = PlayerPrefs.GetInt("monnaieDepensee", 0);
            int playerMoney = PlayerPrefs.GetInt("monnaie");

            for (int i = 0; i < upgrades.Length; i++){
                upgrades[i].level = 1;
                PlayerPrefs.SetInt("upgrade_" + i, 1);
                foreach (var renderer in upgrades[i].upgradeLevels){
                    renderer.GetComponent<Renderer>().material = offMaterial;
                }
                upgrades[i].upgradeLevels[0].GetComponent<Renderer>().material = onMaterial;
                priceUpgradeTexts[i].text = upgrades[i].cost[0].ToString();
            }

            // Reset all the upgrade values to level 1
            SetSpeed(0);
            SetPickupScore(0, 1);
            SetPickupScore(0, 2);
            SetPickupScore(0, 3);
            SetPepperDuration(0);
            SetGameTime(0);

            PlayerPrefs.SetInt("monnaie", playerMoney + moneySpent);
            PlayerPrefs.SetInt("monnaieDepensee", 0);

            UpdateMonnaieText();
            UpdateDescription();
            Debug.Log("RESET fait");
        }
    }
}