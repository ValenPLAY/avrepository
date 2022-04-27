using UnityEngine;

public class Hero : Unit
{
    [Header("Movement")]
    protected Vector3 movementVector;
    protected float actualMovementSpeed;
    protected float gravity = -9.8f;

    protected CharacterController controller;
    [SerializeField] GameObject upperBody;
    [SerializeField] GameObject lowerBody;
    [SerializeField] float rotationSpeed = 500.0f;

    protected override void Awake()
    {
        base.Awake();
        movementVector.y = gravity;
        controller = GetComponent<CharacterController>();

    }

    void Start()
    {

    }

    protected override void Update()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.z = Input.GetAxis("Vertical");
        actualMovementSpeed = movementSpeed;

        controller.Move(movementVector * Time.deltaTime * actualMovementSpeed);

        if (lowerBody != null && movementVector != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementVector, Vector3.up);
            lowerBody.transform.rotation = Quaternion.RotateTowards(lowerBody.transform.rotation, toRotation, rotationSpeed);
        }

        if (upperBody != null)
        {
            upperBody.transform.LookAt(GameController.Instance.playerWorldMousePos);
        }

        base.Update();
    }

    protected virtual void Attack()
    {

    }
}
