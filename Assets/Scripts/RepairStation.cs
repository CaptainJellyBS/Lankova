using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairStation : MonoBehaviour
{
    public GameObject repairPanel;
    public int amountOfSpecificSystemRepair = 3, amountOfRandomSystemRepair = 5;
    [Range(0.0f,1.0f)]
    public float hpRepairPercentage = 1.0f;
    PlayerMovement player;
    DamageHandler damageHandler;

    private void Start()
    {
        player = PlayerMovement.Instance;
        damageHandler = player.GetComponent<DamageHandler>();
    }

    public void OnEnter()
    {
        repairPanel.SetActive(true);
        player.repairPanelActive = true;
        player.GetComponent<MechTurning>().enabled = false;
        player.GetComponent<GrenadeLauncher>().repairPanelActive = true;
        //player.GetComponentInChildren<MouseLook>().enabled = false;
        //There's multiple of these. Ideally, they all reset to like, their standard position as some kind of cool animation.

        //Repair all UI, since the repair screen is on there
        damageHandler.RepairUILeft();
        damageHandler.RepairUIMiddle();
        damageHandler.RepairUIRight();

        damageHandler.hpRepairButton.onClick.RemoveAllListeners(); //WANT WAAROM ZOU JE DE LISTENER LIST KUNNEN CHECKEN VOOR DUPLICATES????
        damageHandler.randomSystemRepairButton.onClick.RemoveAllListeners(); //UNITY IK HAAAAAAAT JE
        damageHandler.hpRepairButton.onClick.AddListener(RepairHP);
        damageHandler.randomSystemRepairButton.onClick.AddListener(RepairRandomSystems);

        damageHandler.hpRepairButton.GetComponentInChildren<Text>().text = "Repair " + (int)(hpRepairPercentage * 100) + "% HP";
        damageHandler.randomSystemRepairButton.GetComponentInChildren<Text>().text = "Repair " + amountOfRandomSystemRepair + " random systems";
        damageHandler.specificSystemRepairButton.GetComponentInChildren<Text>().text = "Repair " + amountOfSpecificSystemRepair + " systems";
    }

    public void RepairHP()
    {
        damageHandler.SetTorsoHealth(damageHandler.currentTorsoHealth + damageHandler.maxTorsoHealth * hpRepairPercentage);
        damageHandler.SetArmHealth(damageHandler.currentArmHealth + damageHandler.maxArmHealth * hpRepairPercentage);
        damageHandler.SetLeftLegHealth(damageHandler.currentLeftLegHealth + damageHandler.maxLegHealth * hpRepairPercentage);
        damageHandler.SetRightLegHealth(damageHandler.currentRightLegHealth + damageHandler.maxLegHealth * hpRepairPercentage);
        damageHandler.SetLauncherHealth(damageHandler.currentLauncherHealth + damageHandler.maxLauncherHealth * hpRepairPercentage);
    }

    public void RepairRandomSystems()
    {
        List<int> damagedSystems = damageHandler.GetDamagedSystems();
        Utility.FisherYates(ref damagedSystems); //I love you past Mana
        for (int i = 0; i < damagedSystems.Count && i < amountOfRandomSystemRepair; i++)
        {
            damageHandler.RepairSystem(damagedSystems[i]);
        }
    }    

    public void OnExit()
    
    {
        repairPanel.SetActive(false);
        player.repairPanelActive = false;
        player.GetComponent<MechTurning>().enabled = true;
        player.GetComponent<GrenadeLauncher>().repairPanelActive = false;
        //player.GetComponentInChildren<MouseLook>().enabled = true;

        damageHandler.hpRepairButton.onClick.RemoveListener(RepairHP);
        damageHandler.randomSystemRepairButton.onClick.RemoveListener(RepairRandomSystems);
    }

    private void OnDisable()
    {
        damageHandler.hpRepairButton.onClick.RemoveListener(RepairHP);
        damageHandler.randomSystemRepairButton.onClick.RemoveListener(RepairRandomSystems);
    }


}
