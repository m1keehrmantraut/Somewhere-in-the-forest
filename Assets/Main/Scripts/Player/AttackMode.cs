using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackMode : MonoBehaviour
{
    public Button attackButton;

    private MeleeCombat attackPoint;
    private Shooting shootPoint;

    private bool isMelee = false;
    
    // Start is called before the first frame update
    void Start()
    {
        attackPoint = GameObject.FindWithTag("AttackPoint").GetComponent<MeleeCombat>();
        shootPoint = GameObject.FindWithTag("ShootPoint").GetComponent<Shooting>();
    }

    public void SetMelee()
    {
        isMelee = true;
    }

    public void SetShooting()
    {
        isMelee = false;
    }

    public void Update()
    {
        if (isMelee)
        {
            attackButton.onClick.RemoveListener(shootPoint.Shoot);
            attackButton.onClick.AddListener(attackPoint.MeleeAttack);
        } 
        else
        {
            shootPoint.FindClosestObjectInRange();
            attackButton.onClick.RemoveListener(attackPoint.MeleeAttack);
            attackButton.onClick.AddListener(shootPoint.Shoot);
        }
    }
}
