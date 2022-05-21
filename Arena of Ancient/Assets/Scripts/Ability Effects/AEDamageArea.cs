using UnityEngine;

public class AEDamageArea : AbilityEffect
{
    [SerializeField] HitZone hitZonePrefab;
    [SerializeField] float abilityDamage;
    //[SerializeField] float abilityRange;

    public override void ApplyEffect(Unit effectOwner)
    {
        SpawnController.Instance.CreateHitZone(hitZonePrefab, effectOwner, abilityDamage);
        base.ApplyEffect(effectOwner);
    }
}
