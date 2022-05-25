using UnityEngine;

public class AEAddBuff : AbilityEffect
{
    //[SerializeField] bool isAddBuffToTarget;
    [SerializeField] Buff appliedBuff;

    public override void ApplyEffect(Unit effectOwner)
    {
        base.ApplyEffect(effectOwner);
        if (!isRequireTarget)
        {
            SpawnController.Instance.CreateBuff(effectOwner, appliedBuff);
        } else
        {
            if (GameController.Instance.targetUnit != null)
            {
                SpawnController.Instance.CreateBuff(GameController.Instance.targetUnit, appliedBuff);
            }
        }
    }
}
