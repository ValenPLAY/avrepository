using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maximumHp = 10.0f;
    public float currentHp = 10.0f;
    public float movementSpeed = 5.0f;
    public float damage = 1.0f;

    private bool isMovingRight = true;
    private bool isAlive = true;
    private Vector2 movement;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;

    private Animator animator;

    private float timeToChangeDirectionDefault = 5.0f;
    private float timeToChangeDirectionCurrent = 5.0f;

    public Collider2D aggroRange;
    public DamageBound damageBounds;


    void Attack(GameObject target)
    {
        var player = target.GetComponent<Character>();
        if (player != null)
        {
            player.Hit(damage);
        }
    }

    void ChangeDirection()
    {

        isMovingRight = !isMovingRight;

    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        timeToChangeDirectionCurrent = Random.Range(0.0f, timeToChangeDirectionDefault);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rigid.velocity);
        rigid.velocity = new Vector2(0.0f, rigid.velocity.y);
        if (isAlive)
        {
            if (isMovingRight) movement = transform.right;
            if (!isMovingRight) movement = -transform.right;
            sprite.flipX = isMovingRight;
            rigid.velocity += movement * movementSpeed;


            //rigid.GetPointVelocity
            //animator.SetBool("isRunning", horizontal != 0);
        }

        animator.SetBool("isWalking", rigid.velocity.x != 0);

        if (timeToChangeDirectionCurrent <= 0)
        {
            ChangeDirection();
            timeToChangeDirectionCurrent = Random.Range(0.0f, timeToChangeDirectionDefault);
        }
        else
        {
            timeToChangeDirectionCurrent -= Time.deltaTime;
        }

        if (damageBounds.collidedObject != null)
        {
            Attack(damageBounds.collidedObject);
        }

    }
}
