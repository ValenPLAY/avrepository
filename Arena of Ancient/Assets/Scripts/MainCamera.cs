using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform offsetCenterPoint;
    Vector3 offsetDifference;

    // Start is called before the first frame update
    void Awake()
    {
        if (offsetCenterPoint != null)
        {
            offsetDifference = transform.position - offsetCenterPoint.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.selectedHero != null)
        {
            transform.position = Vector3.Lerp(transform.position, GameController.Instance.selectedHero.transform.position + offsetDifference, 2f * Time.deltaTime);
        }
    }
}
