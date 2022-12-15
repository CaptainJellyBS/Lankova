using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetingUI : MonoBehaviour
{
    public LayerMask hittableLayers, UILayer;
    public GameObject targetInfoPanel, targetingReticle;
    public Text hpText, armorText, weaponText, secWeaponText, nameText;

    public Text reticleNameText;
    public GameObject[] chevrons;

    public float baseDistanceReticle = 12.0f;
    public float scaleSpeed;

    Enemy lastTargetedEnemy;

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
                GameManager.Instance.SetCursorOverEnemy(false);
                return;
            }

            if (enemy != null)
            {
                targetInfoPanel.SetActive(true);
                targetingReticle.SetActive(true);
                GameManager.Instance.SetCursorOverEnemy(true);

                hpText.text = ((int)enemy.hp).ToString();
                armorText.text = ((int)enemy.armor).ToString();
                weaponText.text = enemy.weaponName;
                secWeaponText.text = enemy.secondaryWeaponName;
                nameText.text = enemy.enemyName;

                reticleNameText.text = enemy.enemyName;

                float hpRatio = enemy.hp / enemy.maxHP;
                for (int i = 0; i < chevrons.Length; i++)
                {
                    chevrons[i].SetActive(hpRatio > (1.0f / (float)chevrons.Length) * i);                   
                }

                RaycastHit hit1;
                if(Physics.Raycast(enemy.transform.position, (Camera.main.transform.position - enemy.transform.position).normalized, out hit1, 50, UILayer))
                {
                    targetingReticle.transform.position = hit1.point;
                    float scaleRatio = Mathf.Clamp(2.0f - Mathf.Pow(hit1.distance / baseDistanceReticle, scaleSpeed), 0.25f, 2.0f);
                    targetingReticle.transform.localScale = new Vector3(scaleRatio, scaleRatio, scaleRatio);
                }
                else
                {
                    targetingReticle.SetActive(false);
                }


            }

            if(dob != null)
            {
                targetInfoPanel.SetActive(true);
                targetingReticle.SetActive(false);
                GameManager.Instance.SetCursorOverEnemy(true);

                hpText.text = (dob.HP).ToString();
                armorText.text = (dob.armor).ToString();
                weaponText.text = "";
                secWeaponText.text = "";
                nameText.text = dob.objectName;                
            }
        }
    }
}
