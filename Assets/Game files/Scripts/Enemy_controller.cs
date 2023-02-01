using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_controller : MonoBehaviour
{
    [SerializeField]
    Transform player;
    Animator animator;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform shoot_point;
    float counter_to_shoot = 2;
    [SerializeField]
    ParticleSystem flash_effect;
    [SerializeField]
    AudioClip glock_sound;
    AudioSource source;

    void Start()
    {
        source= GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 12)
        {
            if (can_see_player())
            {
                shoot();
            }
        }
        else
        {
            animator.SetBool("aim", false);
        }
    }
    bool can_see_player()
    {
        Vector3 dir = player.position - transform.position;

        if (Vector3.Angle(transform.forward, dir) < 90)
        {
            //get direction to the player and rotate towards it
            Quaternion player_dir = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, player_dir.eulerAngles.y, 0), .5f);
            return true;
        }
        else 
        {
            return false; 
        }
    }
    void shoot()
    {
        animator.SetBool("aim", true);
        //set random counter between every shot
        counter_to_shoot -= Time.deltaTime;
        if (counter_to_shoot <= 0)
        {
            flash_effect.Play();
            source.PlayOneShot(glock_sound);
            Instantiate(bullet, shoot_point.position, Quaternion.Euler(0, transform.eulerAngles.y, 0));
            counter_to_shoot = Random.Range(1, 4);
        }

    }
}
