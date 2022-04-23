using UnityEngine;

[RequireComponent(typeof(PoolObject), typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask _ignoredLayer;
    [SerializeField] private float _speed = 15f, _gravity = 10f;
    [SerializeField] private int _damage = 10;

    [SerializeField] private ParticleSystem _damageEffect = null;

    private PoolObject _poolObj = null;
    private Rigidbody _rigidbody = null;

    private Rigidbody _rb
    {
        get => _rigidbody = _rigidbody ?? GetComponent<Rigidbody>();
    }
    private PoolObject _poolObject
    {
        get => _poolObj = _poolObj ?? GetComponent<PoolObject>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = (transform.forward * _speed);
    }

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.gameObject.layer & 1 << _ignoredLayer);


    }

    private void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.gameObject.name);
        var hp = col.gameObject.GetComponent<Enemy>();

        if (hp)
        {
            //Debug.Log(col.gameObject.name);
            hp.TakeDamage(_damage);

            var damageEffect = Instantiate(_damageEffect, transform.position, Quaternion.identity);
            damageEffect.transform.LookAt(Player.Instance.transform);
            GameManager.Instance.DestroyAny(damageEffect.gameObject, 1f);
        }
        //if ((col.gameObject.layer & 1 << _ignoredLayer) == 0)
        // {
        // }

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        _poolObject.ReturnToPool();
    }
}
