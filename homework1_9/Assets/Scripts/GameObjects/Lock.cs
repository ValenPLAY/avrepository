using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private Animator animator;
    private int trueActivators;
    private int totalActivators;
    private bool completed = false;

    public List<Activator> activatorsList = new List<Activator>();
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        totalActivators = activatorsList.Count;

    }

    // Update is called once per frame
    void Update()
    {
        if (totalActivators > 0 && completed == false)
        {
            //Debug.Log("Not Complete");
            trueActivators = 0;
            activatorsList.ForEach(selectedActivator =>
            {
                if (selectedActivator.isTrue == true || selectedActivator == null) trueActivators++;
            }
            );
            if (totalActivators == trueActivators)
            {
                ChangeState();
                completed = true;
            }
        }
    }

    void ChangeState()
    {
        if (completed == false)
        {
            Debug.Log("Complete");

            animator.SetBool("isOpened", !animator.GetBool("isOpened"));
            animator.SetTrigger("changeState");
        }
    }

}
