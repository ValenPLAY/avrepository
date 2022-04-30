using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    [Header("Hero Abilities")]
    public List<Ability> abilities = new List<Ability>();

    [Header("Hero Body Parts")]
    [SerializeField] protected GameObject upperBody;
    protected Quaternion defaultUpperBodyRotation;
    [SerializeField] protected GameObject lowerBody;
    [SerializeField] protected float rotationSpeed = 500.0f;
    protected CharacterController characterController;

    protected override void Awake()
    {
        base.Awake();
        defaultUpperBodyRotation = transform.rotation;
        characterController = GetComponent<CharacterController>();

    }

    protected override void Update()
    {
        if (unitState == state.normal) MoveHero(GameController.Instance.inputVector);
        base.Update();
    }

    protected void MoveHero(Vector3 movementVector)
    {
        movementVector.y = Physics.gravity.y;
        //movementSpeedActual = movementSpeed;
        characterController.Move(movementVector * Time.deltaTime * movementSpeedActual);

        if (lowerBody != null && movementVector != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementVector, Vector3.up);
            lowerBody.transform.rotation = Quaternion.RotateTowards(lowerBody.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (upperBody != null)
        {
            upperBody.transform.LookAt(GameController.Instance.playerWorldMousePos);
            //upperBody.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
