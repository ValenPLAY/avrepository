using UnityEngine;

public class Character : MonoBehaviour
{
    public float playerHealthCurrent;
    public float playerHealthDefault = 10.0f;
    private float defaultInvulTime = 0.4f;
    private float currentInvulTime;

    private float visionDistanceCurrent;
    public float visionDistanceDefault = 0.3f;
    public float visionDistanceZoom = 0.7f;

    public float movementSpeed = 5.0f;
    public float jumpStrength = 10.0f;
    private Rigidbody2D rigid;

    private Animator animator;
    private SpriteRenderer charRenderer;
    private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask groundedLayers;

    public Camera mainCamera;
    private Vector3 defaultCameraPos;

    public GameObject hitSound;

    private bool isAlive = true;
    //private float gravity = 9.8f;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        charRenderer = GetComponent<SpriteRenderer>();
        defaultCameraPos = mainCamera.transform.localPosition;



        playerHealthDefault = StatsContainer.playerHp;
        playerHealthCurrent = playerHealthDefault;
        movementSpeed = StatsContainer.playerMovementSpeed;
        jumpStrength = StatsContainer.playerJumpStrength;

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Move(horizontal);

        if (Input.GetButton("Fire2"))
        {
            visionDistanceCurrent = visionDistanceZoom;
        }
        else
        {
            visionDistanceCurrent = visionDistanceDefault;
        }

        CameraMove();

    }

    private void Move(float horizontal)
    {
        Vector2 charMovement = new Vector2(horizontal, 0.0f);
        rigid.velocity = new Vector2(0.0f, rigid.velocity.y);
        if (isAlive)
        {
            rigid.velocity += charMovement * movementSpeed;

            animator.SetBool("isRunning", horizontal != 0);

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }

    private void CameraMove()
    {
        var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newCameraPos = Vector3.Lerp(transform.position, mousePos, visionDistanceCurrent);
        mainCamera.transform.position = new Vector3(newCameraPos.x, newCameraPos.y + defaultCameraPos.y, -10f);
        charRenderer.flipX = !(mousePos.x - transform.position.x > 0);
        if (currentInvulTime > 0) currentInvulTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {

    }

    private void Jump()
    {
        //Ray raycastRay = new Ray(transform.position, -transform.up);
        //RaycastHit hit;
        //if (Physics.Raycast(raycastRay, out hit, 10.0f)) {
        //    Debug.Log("Hit!");
        //}
        if (isGrounded())
        {
            rigid.AddForce(transform.up * jumpStrength);
            animator.SetTrigger("Jump");
        }
    }

    private bool isGrounded()
    {
        //RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + 0.5f,groundedLayers);
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0.0f, Vector2.down, 0.5f, groundedLayers);
        return raycastHit.collider != null;
    }

    public void Hit(float incomingDamage)
    {
        if (currentInvulTime <= 0 && isAlive)
        {
            animator.SetTrigger("Hit");
            currentInvulTime = defaultInvulTime;
            playerHealthCurrent -= incomingDamage;
            //Debug.Log("Hit");

            if (playerHealthCurrent <= 0)
            {
                //Debug.Log("Dead");
                animator.SetTrigger("Death");
                isAlive = false;
            }

            GameObject damageSound = Instantiate(hitSound, transform.position, transform.rotation);
            Destroy(damageSound, 2);
        }

    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Hazard collidedObject = collision.gameObject.GetComponent<Hazard>();
    //    if (collidedObject != null)
    //    {
    //        Hit(collidedObject.hazardDamage);
    //    }
    //}
}
