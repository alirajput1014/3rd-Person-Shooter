using UnityEngine;
using UnityEngine.Audio;
public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;
    public Transform cam;
    public float speed = 6f;
    float turnsmoothvelocity;
    float turnsmoothtime = 0.1f;
    public bool isaiming = false;
    public bool isshooting = false;
    public float gravity = -10f;
    float yVelocity;

    public AudioSource footstepAudio;
    float footstepTimer = 0f;

    public AudioSource AimSound;

    void Update()
    {
        //aiming
        if (Input.GetMouseButton(1))
        {
            isaiming = true;
            footstepAudio.Stop();
          
        }
        else
        {
            isaiming = false;
        }
        anim.SetBool("isaiming", isaiming);

        //play aim sound once
        if (Input.GetMouseButtonDown(1))
        {
            AimSound.PlayOneShot(AimSound.clip);
        }
        //shoot
        if (Input.GetButton("Fire1"))
        {
            isshooting = true;
        }
        else
        {
            isshooting = false;
        }
        anim.SetBool("isshooting", isshooting);
        //move player
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v).normalized;
        Vector2 moveinput = new Vector2(h, v);
        float mag = moveinput.magnitude;
        anim.SetFloat("Speed", mag);

        if (dir.magnitude > 0.1f)
        {
            // movement rotation only work when not shooting
            if (!isshooting)
            {
                float rawangle = Mathf.Atan2(dir.x, dir.z);
                float angleindegree = rawangle * Mathf.Rad2Deg;
                float targetangle = angleindegree + cam.eulerAngles.y;
                float smoothangle = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y,
                    targetangle,
                    ref turnsmoothvelocity,
                    turnsmoothtime);
                transform.rotation = Quaternion.Euler(0f, smoothangle, 0f);
            }
            Vector3 movedir = Quaternion.Euler(0f, cam.eulerAngles.y, 0f) * dir;
            float currentspeed = speed;
            if (isaiming)
            {
                currentspeed = speed * 0.5f;
            }
            if (isshooting)
            {
                currentspeed = speed * 0.1f;
            }
            controller.Move(movedir.normalized * currentspeed * Time.deltaTime);

            if (controller.isGrounded)
            {
                footstepTimer -= Time.deltaTime;
                if (footstepTimer <= 0f)
                {
                    footstepAudio.Play();
                    footstepTimer = 2f;
                }
            }
        }
        else
        {
            footstepTimer = 0f;
            footstepAudio.Stop();
        }

        // during shooting player rotate in camera direction
        if (isshooting)
        {
            Vector3 camForward = cam.forward;
            camForward.y = 0f;
            Quaternion targetRot = Quaternion.LookRotation(camForward);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                15f * Time.deltaTime
            );
        }
        // GRAVITY SYSTEM to stick player on ground
        if (controller.isGrounded)
        {
            yVelocity = -0.5f;
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }
        controller.Move(new Vector3(0, yVelocity, 0) * Time.deltaTime);
    }
}