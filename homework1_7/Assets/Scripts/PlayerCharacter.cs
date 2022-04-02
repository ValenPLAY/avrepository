using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [Header("Player Stats")]
    public float movementSpeed = 10f;
    public float health = 100f;
    public float stamina = 100f;
    private float gravity = 9.8f;
    private CharacterController controller;
    public int currentLevel;

    [Header("Camera Settings")]
    private float lookSensitivity = 2.0f;
    private float cameraLock;

    [Header("Body Part Settings")]
    public GameObject upperBody;
    public GameObject attachmentPoint;

    [Header("Flashlight Settings")]
    public Light flashlight;
    private bool flashlightPressed;

    private bool isStunned = false;

    [Header("Player Sounds Settings")]
    public GameObject soundStepPrefab;
    private float timerBetweenStepsDefault = 0.5f;
    private float timerBetweenStepsCurrent;

    [Header("Raycast Look")]
    public LayerMask lookIgnores;
    [SerializeField] float raycastDistance = 20.0f;

    [Header("Player Animations")]
    private Animator animator;

    [Header("UI")]
    public GameObject watchOpenSound;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (flashlight == null) GetComponentInChildren<Light>();
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

        Ray lookRay = new Ray(upperBody.transform.position, upperBody.transform.forward);
        RaycastHit target;
        if (Physics.Raycast(lookRay, out target, raycastDistance, ~lookIgnores))
        {
            Debug.DrawRay(upperBody.transform.position, upperBody.transform.forward * 10.0f, Color.white);
            Debug.Log(target.collider.gameObject.name);
            Ghost spottedGhost = target.collider.GetComponent<Ghost>();
            if (spottedGhost != null)
            {
                spottedGhost.Disappear();
            }
        }

        Gravity();
        CheckWatch();

        //Debug.Log(controller.velocity);
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
        GameObject watchSound = GameMechanics.playerSound(watchOpenSound, transform.position, 1.0f, 0.2f);
        watchSound.transform.parent = transform;
        Destroy(watchSound, 3);
    }

    public void UnloadWatch()
    {
        GameObject watchSound = GameMechanics.playerSound(watchOpenSound, transform.position, -1.3f, 0.2f);
        watchSound.transform.parent = transform;
        Destroy(watchSound, 3);
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.tag == "Level")
        {
            Floor triggeredFloor = trigger.gameObject.GetComponent<Floor>();
            currentLevel = triggeredFloor.levelNumber;
            Debug.Log("Current Level: " + currentLevel);
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
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        var actualMovementSpeed = movementSpeed;
        Vector3 charMovement = new Vector3(horizontal * actualMovementSpeed, 0.0f, vertical * actualMovementSpeed);

        controller.Move(transform.TransformDirection(charMovement) * Time.deltaTime);
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
            //GameObject stepSound = Instantiate(soundStepPrefab, transform.position, transform.rotation);
            GameObject stepSound = GameMechanics.playerSound(soundStepPrefab, transform.position, 1.0f, 0.3f);
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
        if (flashlight)
        {
            flashlight.enabled = !flashlight.enabled;

        }
    }
}
