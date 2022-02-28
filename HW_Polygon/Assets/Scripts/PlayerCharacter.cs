using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // Player Stats
    public float movementSpeed = 10f;
    public float health = 100f;
    public float stamina = 100f;
    private float gravity = 9.8f;
    public float jumpStrength = 13;
    private float currentJumpStrength;
    private CharacterController controller;
    public float sprintModifier = 1;

    // Player Camera
    private float lookSensitivity = 2.0f;
    private float cameraLock;

    // Player Body Parts
    public GameObject upperBody;
    public GameObject attachmentPoint;

    // Player Weapons
    public GameObject currentWeaponObject;
    private Weapon weapon;
    public GameObject playerArm;
    private Vector3 defaultGunPosition;
    private Vector3 currentGunPosition;

    // Player Additions
    private Light flashlight;
    private bool flashlightPressed;
    public float affectedRecoil;
    public float currentRecoil;

    private bool isStunned = false;

    // Player Animations
    public Animator charAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        flashlight = GetComponent<Light>();
        charAnimator = GetComponent<Animator>();
        defaultGunPosition = playerArm.transform.localPosition;
        currentGunPosition = defaultGunPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isStunned);
        if (isStunned == false)
        {
            Move();

            if (currentWeaponObject)
            {
                if (Input.GetButtonDown("Fire1") && !weapon.isAutomatic)
                {
                    Attack();
                }
                if (Input.GetButton("Fire1") && weapon.isAutomatic)
                {
                    Attack();
                }
            }
        




        }
        
        if (Input.GetButtonDown("Flashlight"))
        {
            Debug.Log("Flashlight");
            flashlight = upperBody.GetComponent<Light>();
            flashlightPressed = !flashlightPressed;
            Flashlight(flashlightPressed);
            
        }

        Recoil();
        Gravity();
        Falling();

        Debug.Log(controller.velocity);
    }

    private void Recoil()
    {
        float recoilSpeedMultiplier = 10f;
        if (affectedRecoil>0)
        {
            currentRecoil = affectedRecoil;
            affectedRecoil = 0;
        }
        if (currentRecoil>0)
        {
            var recoilInTime = currentRecoil * Time.deltaTime * recoilSpeedMultiplier;
            if (Mathf.Abs(cameraLock + recoilInTime) <= 80)
            {
                cameraLock += currentRecoil * Time.deltaTime * recoilSpeedMultiplier;
                upperBody.transform.Rotate(Vector3.left, recoilInTime);
            }

            currentRecoil -= recoilInTime;
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        //Debug.Log(trigger.tag);
        if (trigger.tag == "WeaponPickup") { 
        var triggerWpnPickup = trigger.GetComponent<Weapon_Pickup>();
            if (triggerWpnPickup.pickedWeapon)
            {
                Destroy(currentWeaponObject);
                currentWeaponObject = Instantiate(triggerWpnPickup.pickedWeapon, attachmentPoint.transform);
                weapon = currentWeaponObject.GetComponent<Weapon>();
                weapon.wpnOwner = gameObject.GetComponent<PlayerCharacter>();
                currentGunPosition = defaultGunPosition + weapon.gunPositionModifier;
                playerArm.transform.localPosition = currentGunPosition;

                charAnimator.SetTrigger("Pickup");
                

            } else
            {
                Debug.Log("Player entered Weapon Pickup trigger, but trigger had no weapons to pick");
            }

        }

    }

    private void Attack()
    {
        if (currentWeaponObject != null)
        {
            //weapon = currentWeaponObject.GetComponent<Weapon>();


            weapon.Fire();
            //Recoil();
        }
    }



    private void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float rotationX = Input.GetAxis("Mouse X");
        float rotationY = Input.GetAxis("Mouse Y");
        bool sprint = Input.GetButton("Sprint");


        if (vertical != 0.0f || horizontal != 0.0f)
        {
            charAnimator.SetBool("isWalking", true);
        } else
        {
            charAnimator.SetBool("isWalking", false);
        }

        var actualMovementSpeed = movementSpeed * sprintModifier;
        if (sprint)
        {
            actualMovementSpeed *= 2;
            charAnimator.SetBool("isRunning", true);
        } else
        {
            charAnimator.SetBool("isRunning", false);
        }
        Vector3 charMovement = new Vector3(horizontal * actualMovementSpeed, 0.0f, vertical * actualMovementSpeed);
        

        //Jump
        bool jump = Input.GetButtonDown("Jump");
        if (jump == true && controller.isGrounded) {
            currentJumpStrength = jumpStrength;
            charAnimator.SetTrigger("Jump");
                }
        if (currentJumpStrength > 0)
        {
            
            charMovement.y += currentJumpStrength;
            currentJumpStrength-= Time.deltaTime * 10;
        }

        controller.Move(transform.TransformDirection(charMovement)*Time.deltaTime);
        var actualCameraRotate = rotationX * lookSensitivity;
        transform.Rotate(Vector3.up, actualCameraRotate);
        if (Mathf.Abs(cameraLock + rotationY) <= 80)
        {
            cameraLock += rotationY;
            upperBody.transform.Rotate(Vector3.left, rotationY);
            //mainCamera.transform.Rotate(Vector3.left, rotationY);
        }
    }

    private void Gravity()
    {
        //if (controller.isGrounded == false) { 
        Vector3 charGravity = new Vector3(0.0f, -gravity, 0.0f);
        controller.Move(transform.TransformDirection(charGravity) * Time.deltaTime);
        //}

    }

    private void Flashlight(bool flswitch)
    {
        if (flashlight) {
            flashlight.enabled = flswitch;
            
        }
    }

    private float timeInAir = 0.0f;
    private void Falling()
    {
        Ray fallScanRay = new Ray(transform.position, -transform.up);
        RaycastHit target;
        
        if (Physics.Raycast(fallScanRay, out target))
        {
            var fallCollider = target.collider.gameObject;
            if (fallCollider)
            {
                //Debug.Log(timeInAir);
            }
            if (controller.isGrounded == false && target.distance<0.5 && timeInAir>1.2f)
            {
                charAnimator.SetTrigger("Fallen");
            }
            if (controller.isGrounded == false)
            {
                timeInAir+= Time.deltaTime;
            } else
            {
                timeInAir = 0;
            }

        }
    }

    private void Stun(int stunned)
    {
        isStunned = stunned == 1 ? true : false;
        //charAnimator.
        charAnimator.SetBool("Stunned",isStunned);

    }
}
