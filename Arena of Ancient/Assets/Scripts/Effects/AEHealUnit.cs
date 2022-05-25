using UnityEngine;

public class AEHealUnit : AbilityEffect
{
    [SerializeField] float healFlatAmount;
    [SerializeField] float healPercentageAmount;

    public override void ApplyEffect(Unit effectOwner)
    {
        base.ApplyEffect(effectOwner);
        if (healFlatAmount > 0) effectOwner.HealFlat(healFlatAmount);
        if (healPercentageAmount > 1) effectOwner.HealPercentage(healPercentageAmount);
    }
}
