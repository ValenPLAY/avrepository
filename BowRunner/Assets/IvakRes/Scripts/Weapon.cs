using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform _pewPoint = null;

    [SerializeField] protected float _rareShoot = 1f;
    [SerializeField] protected int _burstCount = 5;
    [SerializeField] protected EStrikeType _strikeType = EStrikeType.Single;

    private float _timer = float.MinValue;
    private bool _shootAcces = false;

    private Quaternion _defaultRotation;

    private void Awake()
    {
        _timer = _rareShoot;

        _defaultRotation = transform.localRotation;
    }

    protected void Start()
    {
        Player.Instance.PewPew += Shoot;
    }

    public void SetDefaultRotation()
    {
        transform.localRotation = _defaultRotation;
    }

    protected void Update()
    {
        _timer -= Time.deltaTime;
    }

    protected virtual void Shoot()
    {
        if (_timer <= 0)
        {
            _timer = _rareShoot;

            switch (_strikeType)
            {
                case EStrikeType.Burst:

                    for (int i = 0; i < _burstCount; i++)
                    {
                        Pool.Instance.GetFreeElement(_pewPoint);
                    }

                    break;
                case EStrikeType.Multi:
                    break;
                case EStrikeType.Single:
                    Pool.Instance.GetFreeElement(_pewPoint);
                    break;
            }
        }
    }
}

public enum EStrikeType
{
    Single,
    Multi,
    Burst
}