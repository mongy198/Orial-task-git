using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    int health = 100;
    [SerializeField]
    Image health_bar;
    public void take_damage(int dmg)
    {
        health -= dmg;
        health_bar.fillAmount -= .2f;
        if (health <= 0)
        {
            if(gameObject.name == "enemy")
            {
                Destroy(gameObject);
            }
        }
    }
}
