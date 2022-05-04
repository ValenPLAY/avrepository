using UnityEngine;

public class HitZone : MonoBehaviour
{
    [SerializeField] float hitZoneDuration = 0.5f;
    [SerializeField] float hitZoneDurationCurrent;
    private CapsuleCollider hitZoneCollider;
    private Unit hitZoneOwner;

    // Start is called before the first frame update
    void Awake()
    {
        hitZoneCollider = GetComponent<CapsuleCollider>();
        hitZoneOwner = transform.parent.GetComponent<Unit>();
        transform.position = hitZoneOwner.transform.position;
        transform.localScale *= hitZoneOwner.attackRange;
        CharacterController hitZoneOwnerCollider = hitZoneOwner.GetComponent<CharacterController>();
        Physics.IgnoreCollision(hitZoneCollider, hitZoneOwnerCollider);

        hitZoneDurationCurrent = hitZoneDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitZoneDurationCurrent <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            hitZoneDurationCurrent -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Unit collidedUnit = other.gameObject.GetComponent<Unit>();

        if (collidedUnit != null)
        {
            hitZoneOwner.DealDamage(collidedUnit);
            //Debug.Log();
        }
    }
}
