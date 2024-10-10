using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public WindowHandler windowhandler;
    private CharacterController cc;
    public CameraLook cam;
    
    [Space]
    [SerializeField] private float CrouchSpeed = 2f;
    [SerializeField] private float WalkSpeed = 4f;
    [SerializeField] private float RunSpeed = 7f;
    [SerializeField] private float JumpPower = 5.5f;
    [Space]
    [SerializeField] private float CrouchTransitionSpeed = 5f;
    [SerializeField] private float Gravity = -15f;
    // private float gravityacceleration;
    private float y_velocity;
    public bool isCrouching = false;
    [HideInInspector] public bool crouching;
    [HideInInspector] public bool walking;
    [HideInInspector] public bool running;

    [Header("Footsteps")]
    private AudioSource audioS;

    private float currentCrouchLength;
    private float currentWalkLength;
    private float currentRunLength;

    public float crouchStepLength;
    public float walkStepLength;
    public float runStepLength;
    void Start()
    {
        windowhandler = GetComponent<WindowHandler>();
        cc = GetComponent<CharacterController>();
        cam = GetComponentInChildren<CameraLook>();
        audioS = GetComponent<AudioSource>();
        // if (cam == null)
        // {
        //     Debug.LogError("CameraLook component not found on child objects!");
        // }

        // gravityacceleration = Gravity * Gravity;
        // gravityacceleration = Time.deltaTime;
    }

    private void Update()
    {
        if (crouching)
        {
            if (currentCrouchLength < crouchStepLength)
            {
                currentCrouchLength += Time.deltaTime;
            }
            else
            {
                currentCrouchLength = 0;

                audioS.PlayOneShot(GetFootStep());
            }
        }
        else if (walking)
        {
            if (currentWalkLength < walkStepLength)
            {
                currentWalkLength += Time.deltaTime;
            }
            else
            {
                currentWalkLength = 0;

                audioS.PlayOneShot(GetFootStep());
            }
        }
        else if (running)
        {
            if (currentRunLength < runStepLength)
            {
                currentRunLength += Time.deltaTime;
            }
            else
            {
                currentRunLength = 0;

                audioS.PlayOneShot(GetFootStep());
            }
        }
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 moveDir = Vector3.zero; //Mulai direction pertama atau originalnya
        if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            moveDir.z += 1;
        }
        if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            moveDir.x += 1;
        }
        if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            moveDir.z -= 1;
        }
        if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            moveDir.x -= 1;
        }

        //-------------------------------------------------------------------------------//

        // if(Input.GetKey(KeyCode.LeftShift))
        // {
        //     moveDir *= RunSpeed;

        //     cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 2, 0), CrouchTransitionSpeed * Time.deltaTime);
        //     cc.height = Mathf.Lerp(cc.height, 2, CrouchTransitionSpeed * Time.deltaTime);
        //     cc.center = Vector3.Lerp(cc.center, new Vector3(0, 1, 0), CrouchTransitionSpeed * Time.deltaTime);
        // }
        // else if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        // {
        //     moveDir *= CrouchSpeed;

        //     cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 1, 0), CrouchTransitionSpeed * Time.deltaTime);
        //     cc.height = Mathf.Lerp(cc.height, 1.2f, CrouchTransitionSpeed * Time.deltaTime);
        //     cc.center = Vector3.Lerp(cc.center, new Vector3(0, 0.59f, 0), CrouchTransitionSpeed * Time.deltaTime);
        // }
        // else
        // {
        //     moveDir *= WalkSpeed;

        //     cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 2, 0), CrouchTransitionSpeed * Time.deltaTime);
        //     cc.height = Mathf.Lerp(cc.height, 2, CrouchTransitionSpeed * Time.deltaTime);
        //     cc.center = Vector3.Lerp(cc.center, new Vector3(0, 1, 0), CrouchTransitionSpeed * Time.deltaTime);
        // }

        // Sprinting
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching && Input.GetKey(KeyCode.W))
        {
            moveDir *= RunSpeed;

            crouching = false;
            walking = false;
            running = true;
        }
        // Crouching
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            moveDir *= CrouchSpeed;
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 1, 0), CrouchTransitionSpeed * Time.deltaTime);
            cc.height = Mathf.Lerp(cc.height, 1.2f, CrouchTransitionSpeed * Time.deltaTime);
            cc.center = Vector3.Lerp(cc.center, new Vector3(0, 0.59f, 0), CrouchTransitionSpeed * Time.deltaTime);
            crouching = true;
            walking = false;
            running = false;
        }
        else
        {
            isCrouching = false;
            moveDir *= WalkSpeed;
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 2, 0), CrouchTransitionSpeed * Time.deltaTime);
            cc.height = Mathf.Lerp(cc.height, 2, CrouchTransitionSpeed * Time.deltaTime);
            cc.center = Vector3.Lerp(cc.center, new Vector3(0, 1, 0), CrouchTransitionSpeed * Time.deltaTime);
            crouching = false;
            walking = true;
            running = false;
        }

        if (moveDir == Vector3.zero)
        {
            crouching = false;
            walking = false;
            running = false;
        }

        if(cc.isGrounded)
        {
            y_velocity = 0;

            if(Input.GetKey(KeyCode.Space))
            {
                y_velocity = JumpPower;
            }
        }
        else
        {
            y_velocity += Gravity * Time.deltaTime;
        }
        moveDir.y = y_velocity;
        moveDir = transform.TransformDirection(moveDir);
        moveDir *= Time.deltaTime;

        cc.Move(moveDir);
    }

    public AudioClip GetFootStep()
    {
        RaycastHit hit;

        if (Physics.SphereCast(cc.center, 0.2f, Vector3.down, out hit, cc.bounds.extents.y + 0.3f))
        {
            Surface surface = hit.transform.GetComponent<Surface>();

            if (surface != null)
            {
                int i = Random.Range(0, surface.surface.footsteps.Length);
                return surface.surface.footsteps[i];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
