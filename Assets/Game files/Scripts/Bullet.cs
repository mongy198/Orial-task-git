using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    void Update()
    {
        transform.Translate(0, 0, 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (other.tag == "damageable")
        {
            other.GetComponent<Health>().take_damage(20);
        }
    }
}
