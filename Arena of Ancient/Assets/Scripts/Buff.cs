using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [Header ("Buff Options")]
    [SerializeField] bool isAuraBuff;
    [SerializeField] float buffDuration = 1.0f;
    float buffDurationCurrent;
    [SerializeField] float procTime = 1.0f;
    float procTimeCurrent;
    Unit buffOwner;
    [Header("Buff Effects")]
    [SerializeField] List<AbilityEffect> buffEffects = new List<AbilityEffect>();


    // Start is called before the first frame update
    protected void Awake()
    {
        buffOwner = transform.parent.GetComponent<Unit>();
        buffDurationCurrent = buffDuration;
        procTimeCurrent = procTime;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!isAuraBuff)
        {
            if (buffDurationCurrent > 0)
            {
                buffDurationCurrent -= Time.deltaTime;
            }
            else
            {
                BuffExpire();
            }
        }

        if (procTimeCurrent <= 0)
        {
            BuffApply();
            procTimeCurrent = procTime;
        }
        else
        {
            procTimeCurrent -= Time.deltaTime;
        }
    }

    public void BuffExpire()
    {
        Destroy(gameObject);
    }

    protected virtual void BuffApply()
    {
        for (int x = 0; x < buffEffects.Count; x++)
        {
            buffEffects[x].ApplyEffect(buffOwner);
        }
    }
}
