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
    private CharacterController controller;

    // Player Camera
    private float lookSensitivity = 2.0f;
    private float cameraLock;

    // Player Body Parts
    public GameObject upperBody;
    public GameObject attachmentPoint;

    public int currentLevel;

    // Player Additions
    private Light flashlight;
    private bool flashlightPressed;

    private bool isStunned = false;

    // Player Animations
    //public Animator charAnimator;

    public GameObject soundStepPrefab;
    private float timerBetweenStepsDefault = 0.5f;
    private float timerBetweenStepsCurrent;

    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //flashlight = GetComponent<Light>();
        flashlight = upperBody.GetComponent<Light>();
        //charAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //flashlight.intensity = Random.Range(1.8f, 2.2f);
        //Debug.Log(isStunned);
        if (isStunned == false)
        {
            Move();
        }
        
        if (Input.GetButtonDown("Flashlight"))
        {
            Debug.Log("Flashlight");
            
            flashlightPressed = !flashlightPressed;
            Flashlight(flashlightPressed);
            
        }
        
        Gravity();
        
        //Debug.Log(controller.velocity);
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.tag == "Level")
        {
            Floor triggeredFloor = trigger.gameObject.GetComponent<Floor>();
            currentLevel = triggeredFloor.levelNumber;
            Debug.Log("Current Level: "+currentLevel);
        }
    }



    private void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float rotationX = Input.GetAxis("Mouse X");
        float rotationY = Input.GetAxis("Mouse Y");



        if (vertical != 0.0f || horizontal != 0.0f)
        {
            //charAnimator.SetBool("isWalking", true);
        } else
        {
            //charAnimator.SetBool("isWalking", false);
        }

        var actualMovementSpeed = movementSpeed;
        Vector3 charMovement = new Vector3(horizontal * actualMovementSpeed, 0.0f, vertical * actualMovementSpeed);

        controller.Move(transform.TransformDirection(charMovement)*Time.deltaTime);
        var actualCameraRotate = rotationX * lookSensitivity;
        transform.Rotate(Vector3.up, actualCameraRotate);
        if (Mathf.Abs(cameraLock + rotationY) <= 80)
        {
            cameraLock += rotationY;
            upperBody.transform.Rotate(Vector3.left, rotationY);
            //mainCamera.transform.Rotate(Vector3.left, rotationY);
        }

        if (controller.velocity.magnitude > 0.2f && timerBetweenStepsCurrent <= 0)
        {
            GameObject stepSound = Instantiate(soundStepPrefab, transform.position, transform.rotation);
            AudioSource stepSoundSource = stepSound.GetComponent<AudioSource>();
            stepSoundSource.pitch = Random.Range(0.7f, 1.4f);
            Destroy(stepSound, 3);
            timerBetweenStepsCurrent = timerBetweenStepsDefault;
        }

        if (timerBetweenStepsCurrent > 0)
        {
            timerBetweenStepsCurrent -= Time.deltaTime * controller.velocity.magnitude;
        }
        //Debug.Log(controller.velocity.magnitude);
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
            flashlight.enabled = !flashlight.enabled;
            
        }
    }
}
