using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public List<Plate> pressingPlatesList = new List<Plate>();
    public bool isPermanentlyOpened = false;
    private Animator animator;
    private AudioSource sndDone;
    //private bool isOpened;

    private int pressedPlates = 0;
    private int currentPlates = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sndDone = GetComponent<AudioSource>();
        pressingPlatesList.ForEach(selectedPlate =>
        {
            selectedPlate.affectedGate = this;
        });
        currentPlates = pressingPlatesList.Count;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void plateCheck()
    {
        pressedPlates = 0;
        for (int i = 0; i < currentPlates; i++)
        {
            if (pressingPlatesList[i].isPressed == true) pressedPlates++;

        }

        Debug.Log(pressedPlates + "/" + currentPlates);
        if (pressedPlates == currentPlates) ChangeState(true);
    }

    public void ChangeState(bool isOpened)
    {
        if (sndDone != null) sndDone.Play();
        animator.SetBool("isOpened", isOpened);
        animator.SetTrigger("ChangeState");

        if (isPermanentlyOpened) Destroy(this);
    }

}
