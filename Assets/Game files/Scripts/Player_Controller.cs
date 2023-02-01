using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Animations.Rigging;

public class Player_Controller : MonoBehaviour
{
    float speed;
    Rigidbody rb;
    [SerializeField]
    Transform cam;
    Vector3 move_dirction;
    Vector3 x_input, y_input;
    //Vector3 gravity;
    Animator animator;

    [SerializeField]
    Transform bottom;
    //Rig rig;
    [SerializeField]
    GameObject bullet;
    bool aiming = false;
    float aim_thread;
    [SerializeField]
    Transform shoot_point;
    [SerializeField]
    ParticleSystem flash_effect;
    [SerializeField]
    AudioClip glock_sound;
    AudioSource source;
    void Start()
    {
        speed = .1f;
        source= GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //get direction from cam and input
        x_input = cam.right * Input.GetAxis("Horizontal");
        y_input = cam.forward * Input.GetAxis("Vertical");
        move_dirction = (x_input + y_input).normalized;
        move_dirction.y = 0;

        move_and_rotate();
        //stop rotating while aiming
        if (aiming)
        {
            aim_thread -= Time.deltaTime;
            if(aim_thread < 0)
            {
                aiming = false;
            }
        }
    }
    private void LateUpdate()
    {
        if (Physics.Raycast(bottom.position,-transform.up,.22f))
        {
            //jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //gravity.y = 6;
                rb.AddForce(0, 25000, 0);
                animator.SetTrigger("Jump");
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
        else
        {
            animator.SetBool("aim", false);
        }
    }
    void move_and_rotate()
    {
        if (move_dirction != Vector3.zero)
        {
            animator.SetBool("Run", true);
            //player postion
            rb.transform.position += move_dirction * speed;
            //player rotation
            if (!aiming)
            {
                Quaternion look_to_dir = Quaternion.LookRotation(move_dirction);
                Quaternion look_to = Quaternion.Euler(0, look_to_dir.eulerAngles.y, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, look_to, .1f);
            }
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
    void shoot()
    {
        aiming = true;
        aim_thread = .4f;
        flash_effect.Play();
        source.PlayOneShot(glock_sound);
        transform.rotation = Quaternion.Euler(0, cam.eulerAngles.y, 0);
        animator.SetBool("aim", true);
        Instantiate(bullet, shoot_point.position, Quaternion.Euler(0,transform.eulerAngles.y,0));
    }
}
