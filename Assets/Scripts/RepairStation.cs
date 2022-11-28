using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour
{
    public GameObject repairPanel;
    public int amountOfHPRepair = 200, amountOfSpecificSystemRepair = 3, amountOfRandomSystemRepair = 5;
    PlayerMovement player;

    private void Start()
    {
        player = PlayerMovement.Instance;
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
        player.GetComponent<DamageHandler>().RepairUILeft();
        player.GetComponent<DamageHandler>().RepairUIMiddle();
        player.GetComponent<DamageHandler>().RepairUIRight();
    }

    public void Onxit()
    
    {
        repairPanel.SetActive(false);
        player.repairPanelActive = false;
        player.GetComponent<MechTurning>().enabled = true;
        player.GetComponent<GrenadeLauncher>().repairPanelActive = false;
        //player.GetComponentInChildren<MouseLook>().enabled = true;
    }


}
