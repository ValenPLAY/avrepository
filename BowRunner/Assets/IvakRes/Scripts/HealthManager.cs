using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private ParticleSystem _bloodSplash = null;
    private bool _die = false;
    
    public System.Action<GameObject> DieAction;
    
    public void Damage(int damage)
    {
        _health -= damage;

        if (_health <= 0 && !_die)
        {
            _die = true;
            
            Die();
        }
    }

    private void Die()
    {
        DieAction?.Invoke(this.gameObject);
        var bloodSplash = Instantiate(_bloodSplash, transform.position, Quaternion.identity);
        
        GameManager.Instance.DestroyAny(bloodSplash.gameObject, 2f);
        
        Destroy(this.gameObject);
    }
}
