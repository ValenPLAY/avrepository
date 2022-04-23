//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Unit base values")]
    [SerializeField] float health = 10.0f;
    protected float _currentHealth;
    [SerializeField] private ParticleSystem _bloodSplash = null;
    public System.Action<GameObject> DieAction;

    [Header("Unit movement parameters")]
    [SerializeField] protected bool isMovingRandomly;
    [SerializeField] protected float moveDelay = 5.0f;
    [SerializeField] protected float randomMoveRange = 1.0f;
    protected float _moveDelayCurrent = 5.0f;

    [SerializeField] protected float timeToCancelOrder = 2.0f;
    protected float timeToCancelOrderCurrent;

    [SerializeField] protected float movementSpeed = 5.0f;
    [SerializeField] float reachedTargetRoundingValue = 0.5f;
    public Vector3 moveDestination;
    protected bool _isReachedDestination = false;
    protected Vector3 _distanceTillTarget;

    [Header("Attack parameters")]
    [SerializeField] protected float damage = 2.0f;
    [SerializeField] protected float attackDelay = 2.0f;
    protected float attackDelayCurrent;

    [Header("Target Detection")]
    public GameObject currentTarget;

    Rigidbody _rigidBody;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        // Rigidbody Initialization
        _rigidBody = GetComponent<Rigidbody>();
        if (_rigidBody == null)
        {
            Debug.Log(gameObject.name + " is Missing Rigidbody!");
            gameObject.AddComponent<Rigidbody>();
            //transform.LookAt
        }

        _currentHealth = health;

        if (currentTarget == null)
        {
            currentTarget = GameObject.FindGameObjectWithTag("Player");
        }

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Debug.Log(GameManager.Instance.GameStatus);
        if (GameManager.Instance.GameStatus == EGameStatus.InGame) { 
            Move();
        if (attackDelayCurrent > 0) attackDelayCurrent -= Time.deltaTime;
        }

    }

    protected virtual void MoveRandom()
    {
        if (isMovingRandomly && _isReachedDestination)
        {
            _moveDelayCurrent -= Time.deltaTime;
            if (_moveDelayCurrent <= 0)
            {
                Vector3 newDirection = new Vector3(transform.position.x + Random.Range(-randomMoveRange, randomMoveRange), 0.0f, transform.position.z + Random.Range(-randomMoveRange, randomMoveRange));

                StartMoving(newDirection);
            }
        }
    }

    protected virtual void Death()
    {
        DieAction?.Invoke(this.gameObject);
        var bloodSplash = Instantiate(_bloodSplash, transform.position, Quaternion.identity);

        GameManager.Instance.DestroyAny(bloodSplash.gameObject, 2f);

        Destroy(this.gameObject);
    }

    protected virtual void Attack()
    {
        
    }

    protected virtual void Move()
    {
        if (!_isReachedDestination)
        {
            Debug.DrawLine(transform.position, moveDestination, Color.white);

            Vector3 actualMovementDirection =  moveDestination - transform.position;
            moveDestination.y = transform.position.y;
            actualMovementDirection.Normalize();
            transform.position += actualMovementDirection * movementSpeed * Time.deltaTime;

            _distanceTillTarget = moveDestination - transform.position;
            _distanceTillTarget.x = Mathf.Abs(_distanceTillTarget.x);
            _distanceTillTarget.z = Mathf.Abs(_distanceTillTarget.z);

            timeToCancelOrderCurrent -= Time.deltaTime;

            if ((_distanceTillTarget.x <= reachedTargetRoundingValue && _distanceTillTarget.z <= reachedTargetRoundingValue) || timeToCancelOrderCurrent <= 0)
            {
                _isReachedDestination = true;
            }

            
            if (_isReachedDestination) _moveDelayCurrent = moveDelay;
        }
    }

    protected virtual void StartMoving(Vector3 destination)
    {
        _isReachedDestination = false;
        moveDestination = destination;
        transform.LookAt(destination);
        timeToCancelOrderCurrent = timeToCancelOrder;
    }

    public virtual void TakeDamage(float incomingDamage)
    {
        _currentHealth -= incomingDamage;
        if (_currentHealth <= 0) Death();
    }
}
