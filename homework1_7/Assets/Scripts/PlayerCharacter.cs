using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [Header("Player Stats")]
    public float movementSpeed = 10f;
    public float health = 100f;
    public float stamina = 100f;
    public float pickupRange = 1.0f;
    [SerializeField] float gravity = 9.8f;

    private CharacterController controller;
    public int currentLevel;

    [Header("Camera Settings")]
    [SerializeField] float lookSensitivity = 2.0f;
    [SerializeField] float cameraLock = 80.0f;
    private Vector3 cameraRotation;

    [Header("Body Part Settings")]
    [SerializeField] GameObject upperBody;

    [Header("Flashlight Settings")]
    public Light flashlight;
    [SerializeField] AudioSource flashlightSoundPrefab;

    private bool isStunned = false;

    [Header("Player Sounds Settings")]
    public AudioSource soundStepPrefab;
    private float timerBetweenStepsDefault = 0.5f;
    private float timerBetweenStepsCurrent;

    [Header("Raycast Look")]
    [SerializeField] LayerMask lookIgnores;
    [SerializeField] bool isInPickupRange;
    [SerializeField] float raycastDistance = 20.0f;
    private GameObject lookingTarget;

    [Header("Player Animations")]
    private Animator animator;

    [Header("Watch")]
    public AudioSource watchOpenSound;
    public Transform watchPosition;
    public GameObject watchPrefab;
    private GameObject currentWatch;

    // Start is called before the first frame update
    void Start()
    {
        cameraRotation = upperBody.transform.rotation.eulerAngles;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (flashlight == null) GetComponentInChildren<Light>();

        GameController.Instance.FloorCheck(currentLevel);
        //isStunned = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (!isStunned)
        {
            Move();
            VisionCheck();
            CheckWatch();
            Interact();
            Flashlight();
        }
        Gravity();
    }

    private void VisionCheck()
    {
        Ray lookRay = new Ray(upperBody.transform.position, upperBody.transform.forward);
        RaycastHit target;
        if (Physics.Raycast(lookRay, out target, raycastDistance, ~lookIgnores))
        {

            isInPickupRange = target.distance <= pickupRange;

            if (target.collider.gameObject != lookingTarget)
            {

                lookingTarget = target.collider.gameObject;

                //Debug
                Debug.Log(lookingTarget.name);
                Debug.DrawRay(upperBody.transform.position, upperBody.transform.forward * 10.0f, Color.white, 1f);


                //Ghost Check
                Ghost spottedGhost = target.collider.GetComponent<Ghost>();
                if (spottedGhost != null)
                {
                    spottedGhost.Disappear();
                }

                //Interactable Check
                Interactable currentInteractable = lookingTarget.GetComponent<Interactable>();
                if (currentInteractable != null && isInPickupRange)
                {
                    UIController.Instance.DisplayTip(true, currentInteractable.pickupMessageID);
                }
                else
                {
                    UIController.Instance.DisplayTip(false, 0);

                }

            }
        }
    }

    private void Flashlight()
    {
        if (Input.GetButtonDown("Flashlight") && flashlight != null)
        {
            Debug.Log("Flashlight");

            flashlight.enabled = !flashlight.enabled;

            AudioSource watchSound = GameMechanics.playerSound(flashlightSoundPrefab, transform.position, 1.0f, 0.2f);
            Destroy(watchSound.gameObject, 3);

        }
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && lookingTarget != null)
        {
            Message foundMessage = lookingTarget.GetComponent<Message>();
            if (foundMessage != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    foundMessage.MessagePickup();
                }
            }
        }
    }

    private void CheckWatch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {


            animator.SetBool("isCheckingClock", true);
            animator.SetTrigger("checkClock");
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            animator.SetBool("isCheckingClock", false);
        }
    }

    public void LoadWatch()
    {
        if (currentWatch == null)
        {
            currentWatch = Instantiate(watchPrefab, watchPosition.position, watchPosition.rotation);
            currentWatch.transform.parent = watchPosition;
            currentWatch.transform.localScale = Vector3.one;
        }
        else
        {
            currentWatch.SetActive(true);
        }

        AudioSource watchSound = GameMechanics.playerSound(watchOpenSound, transform.position, 1.0f, 0.2f);
        watchSound.transform.parent = transform;

        Destroy(watchSound.gameObject, 3);
    }

    public void UnloadWatch()
    {
        if (currentWatch != null) currentWatch.SetActive(false);

        AudioSource watchSound = GameMechanics.playerSound(watchOpenSound, transform.position, -1.3f, 0.2f);
        watchSound.transform.parent = transform;
        Destroy(watchSound.gameObject, 3);
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.tag == "Level")
        {
            Floor triggeredFloor = trigger.gameObject.GetComponent<Floor>();
            currentLevel = triggeredFloor.levelNumber;
            Debug.Log("Current Level: " + currentLevel);

            GameController.Instance.FloorCheck(currentLevel);
        }
    }



    private void Move()
    {
        //User Inputs
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        //Animator
        animator.SetBool("isWalking", vertical != 0.0f || horizontal != 0.0f);

        //Movement & Movement Modifiers
        var actualMovementSpeed = movementSpeed;
        Vector3 charMovement = new Vector3(horizontal * actualMovementSpeed, 0.0f, vertical * actualMovementSpeed);
        controller.Move(transform.TransformDirection(charMovement) * Time.deltaTime);

        //Body Rotation
        transform.Rotate(Vector3.up, mouseX);

        //Camera Rotation
        cameraRotation.y += mouseX;
        cameraRotation.x -= mouseY;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -cameraLock, cameraLock);
        upperBody.transform.rotation = Quaternion.Euler(cameraRotation);

        //Step Sounds
        if (controller.velocity.magnitude > 0.2f && timerBetweenStepsCurrent <= 0)
        {
            AudioSource stepSound = GameMechanics.playerSound(soundStepPrefab, transform.position, 1.0f, 0.3f);
            Destroy(stepSound.gameObject, 3);
            timerBetweenStepsCurrent = timerBetweenStepsDefault;
        }
        else if (timerBetweenStepsCurrent > 0)
        {
            timerBetweenStepsCurrent -= Time.deltaTime * controller.velocity.magnitude;
        }

    }

    private void Gravity()
    {
        Vector3 charGravity = new Vector3(0.0f, -gravity, 0.0f);
        controller.Move(transform.TransformDirection(charGravity) * Time.deltaTime);
    }
}
