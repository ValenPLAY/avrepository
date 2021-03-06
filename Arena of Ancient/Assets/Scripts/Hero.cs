using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    [Header("Hero Abilities")]
    [SerializeField] List<Ability> startingAbilities = new List<Ability>();
    public List<Ability> abilities = new List<Ability>();

    [Header("Hero Body Parts")]
    [SerializeField] protected GameObject unitModel;
    [SerializeField] protected GameObject upperBody;
    protected Quaternion defaultUpperBodyRotation;
    [SerializeField] protected GameObject lowerBody;
    [SerializeField] protected float rotationSpeed = 500.0f;

    [Header("Hero Upgrades")]
    public int upgradePoints;

    protected CharacterController characterController;

    protected override void Start()
    {
        base.Start();

        if (GameController.Instance != null)
        {
            GameController.Instance.waveFinishedEvent += GainSkillPoint;

            for (int x = 0; x < startingAbilities.Count; x++)
            {
                Ability createdAbility = Instantiate(startingAbilities[x], transform);
                abilities.Add(createdAbility);
            }
        }

        defaultUpperBodyRotation = transform.rotation;
        characterController = GetComponent<CharacterController>();


    }

    protected void GainSkillPoint()
    {
        Debug.Log(name + " gained a Skill Point!");
        upgradePoints++;
    }

    protected override void Update()
    {
        if (unitState == state.normal && GameController.Instance != null)
        {
            //Debug.Log(unitState);
            MoveHero(GameController.Instance.inputVector);
        }

        base.Update();
    }

    private void LateUpdate()
    {

    }

    protected void MoveHero(Vector3 movementVector)
    {

        movementVector.y = Physics.gravity.y;
        //movementSpeedActual = movementSpeed;
        characterController.Move(movementVector * Time.deltaTime * movementSpeedActual);

        if (unitAnimator != null)
        {
            //Debug.Log(characterController.velocity.x + " " + characterController.velocity.z);
            unitAnimator.SetBool("isWalking", characterController.velocity.x != 0 || characterController.velocity.z != 0);
        }

        if (lowerBody != null && movementVector != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementVector, Vector3.up);
            Vector3 rotationXCompensation = toRotation.eulerAngles;
            rotationXCompensation.x = 0.0f;
            toRotation = Quaternion.Euler(rotationXCompensation);
            lowerBody.transform.rotation = Quaternion.RotateTowards(lowerBody.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (upperBody != null)
        {
            upperBody.transform.LookAt(GameController.Instance.playerWorldMousePos);
            //upperBody.transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        if (unitModel != null && lowerBody == null && upperBody == null)
        {
            unitModel.transform.LookAt(GameController.Instance.playerWorldMousePos);
            Vector3 heroRotation = unitModel.transform.rotation.eulerAngles;
            heroRotation.x = 0.0f;
            unitModel.transform.rotation = Quaternion.Euler(heroRotation);
        }
    }
}
