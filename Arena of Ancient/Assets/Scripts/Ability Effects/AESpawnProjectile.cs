using UnityEngine;

public class AESpawnProjectile : AbilityEffect
{
    public Projectile spawnedProjectilePrefab;
    public float projectileDamage;

    public override void ApplyEffect(Unit effectOwner)
    {
        if (spawnedProjectilePrefab != null)
        {

        
        base.ApplyEffect(effectOwner);
        SpawnController.Instance.SpawnProjectile(effectOwner.transform.position, GameController.Instance.playerWorldMousePos, effectOwner, spawnedProjectilePrefab, projectileDamage);
        }
    }
}
