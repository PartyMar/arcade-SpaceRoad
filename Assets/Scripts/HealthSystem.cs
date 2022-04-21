using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

   [SerializeField] 
    private GameObject explosion;


    private int hpCur = 0;

    [SerializeField]
    private int hpMax = 0;
    [SerializeField]
    private int armor = 0;

    void Start()
    {
        this.hpCur = this.hpMax;
    }



    public void damageHealth(int num)
    {
        int damage = num - armor;
        Debug.Log("initial damage:" + num + "..armor: " + armor + "..after armor damage: " + damage);
        if (damage <= 0)
        {
            return;
        }

        this.hpCur -= damage;

        if (this.hpCur <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


    public void damageBoth(HealthSystem col1HP, HealthSystem col2HP)
    {
        col1HP.damageHealth(col2HP.hpMax);
        col2HP.damageHealth(col1HP.hpMax);
    }
    public void healHealth(int num)
    {

    }


    public int maxHP()
    {
        return hpMax;
    }

    public void setArmor(int num)
    {
        this.armor = num;
    }
}
