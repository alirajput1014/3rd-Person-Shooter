using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float height = 1.5f;
    public float distance = 3f;
    public float mouseSensitivity = 200f;
    Vector2 pitchlimit = new Vector2(-30f, 60f);

    float yaw; // left right
    float pitch; //up down

    //camera zoom
    public Camera cam;
    public float normalFOV = 60f;
    public float aimFOV = 30f;

    // side offset
    public float normalSideOffset = 0.5f;
    public float aimSideOffset = 0.9f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        //input
        float mousex = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mousex;
        pitch -= mousey;
        pitch = Mathf.Clamp(pitch, pitchlimit.x, pitchlimit.y);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);

        transform.position = player.position - transform.forward * distance + Vector3.up * height;

        // set side offset
        ThirdPersonMovement pm = player.GetComponent<ThirdPersonMovement>();
        float sideOffset = normalSideOffset;

        if (pm != null && pm.isaiming)
        {
            sideOffset = aimSideOffset;
        }

        transform.position = transform.position + (transform.right * sideOffset);


        // zoom during aiming
        if (pm != null)
        {
            float targetFOV = normalFOV;

            if (pm.isaiming)
            {
                targetFOV = aimFOV;
            }
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * 10);
        }
    }
}
