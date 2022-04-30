using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Ability Description")]
    [SerializeField] string abilityName;
    [SerializeField] string abilityDescription;

    [Header("Ability Stats")]
    public int abilityLevel = 1;
    [SerializeField] bool isActive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
