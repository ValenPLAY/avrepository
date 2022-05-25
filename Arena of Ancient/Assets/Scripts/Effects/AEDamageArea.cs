using UnityEngine;

public class AEDamageArea : AbilityEffect
{
    [SerializeField] HitZone hitZonePrefab;
    [SerializeField] float abilityDamage;
    [SerializeField] bool isSpawnedOnPlayer;
    //[SerializeField] float abilityRange;

    public override void ApplyEffect(Unit effectOwner)
    {
        //Vector3 hitzoneSpawnPosition;
        if (isSpawnedOnPlayer)
        {
            SpawnController.Instance.CreateHitZone(hitZonePrefab, effectOwner, abilityDamage);
        } else
        {
            SpawnController.Instance.CreateHitZone(hitZonePrefab, effectOwner, GameController.Instance.playerWorldMousePos, abilityDamage);

        }
        
        base.ApplyEffect(effectOwner);
    }
}
