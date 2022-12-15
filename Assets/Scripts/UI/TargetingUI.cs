using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetingUI : MonoBehaviour
{
    public LayerMask hittableLayers;
    public GameObject targetInfoPanel, targetingReticle;
    public Text hpText, armorText, weaponText, secWeaponText, nameText;

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 50, hittableLayers))
        {
            Enemy enemy = hit.collider.gameObject.GetComponentInParent<Enemy>();
            DamagableObject dob = hit.collider.gameObject.GetComponentInParent<DamagableObject>();

            if(enemy == null && dob == null)
            {
                targetInfoPanel.SetActive(false);
                targetingReticle.SetActive(false);
                return;
            }

            if (enemy != null)
            {
                targetInfoPanel.SetActive(true);
                targetingReticle.SetActive(true);

                hpText.text = ((int)enemy.hp).ToString();
                armorText.text = ((int)enemy.armor).ToString();
                weaponText.text = enemy.weaponName;
                secWeaponText.text = enemy.secondaryWeaponName;
                nameText.text = enemy.enemyName;
            }

            if(dob != null)
            {
                targetInfoPanel.SetActive(true);
                targetingReticle.SetActive(false);

                hpText.text = ((int)dob.HP).ToString();
                armorText.text = ((int)dob.armor).ToString();
                weaponText.text = "";
                secWeaponText.text = "";
                nameText.text = dob.objectName;
            }
        }
    }
}
