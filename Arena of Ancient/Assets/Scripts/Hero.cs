using UnityEngine;

public class Hero : Unit
{
    [Header("Movement")]
    protected Vector3 movementVector;
    protected float actualMovementSpeed;
    protected float gravity = -9.8f;
    
    [SerializeField] protected GameObject upperBody;
    [SerializeField] protected GameObject lowerBody;
    [SerializeField] protected float rotationSpeed = 500.0f;

    protected CharacterController characterController;

    protected override void Awake()
    {
        base.Awake();
        movementVector.y = gravity;
        characterController = GetComponent<CharacterController>();

    }

    protected override void Update()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.z = Input.GetAxis("Vertical");
        actualMovementSpeed = movementSpeed;

        characterController.Move(movementVector * Time.deltaTime * actualMovementSpeed);

        if (lowerBody != null && movementVector != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementVector, Vector3.up);
            lowerBody.transform.rotation = Quaternion.RotateTowards(lowerBody.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (upperBody != null)
        {
            upperBody.transform.LookAt(GameController.Instance.playerWorldMousePos);
            
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        base.Update();
    }

    protected override void Attack()
    {

    }
}
