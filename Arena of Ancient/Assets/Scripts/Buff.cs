using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] bool isAuraBuff;

    [SerializeField] float buffDuration = 1.0f;
    float buffDurationCurrent;

    [SerializeField] float procTime = 1.0f;
    float procTimeCurrent;
    Unit buffOwner;



    // Start is called before the first frame update
    void Awake()
    {
        buffOwner = transform.parent.GetComponent<Unit>();
        buffDurationCurrent = buffDuration;
        procTimeCurrent = procTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAuraBuff)
        {
            if (buffDurationCurrent > 0)
            {
                buffDurationCurrent -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
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

    public void BuffApply()
    {

    }
}
