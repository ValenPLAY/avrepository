using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Bullet Statistics
    public float bulletTime;
    private float bulletTimeCurrent;
    public float damage;   

    //Bullet Options
    public bool isDestroyOnHit = false;
    public float destroyOnHitTime;

    public bool isExploding = false;
    public bool isDealDamageOnlyOnExplosion = false;
    public float explosionRadius;
    public float explosionDamage;
    public GameObject explosionEffect;

    private bool isHitObject = false;
    

    void Start()
    {
        //Debug.Log("Bullet Damage: "+damage);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Bullet Damage: " + damage);
        bool isTimedOut = false;
        bulletTimeCurrent += Time.deltaTime;
        if (bulletTimeCurrent>=bulletTime)
        {
            isTimedOut = true;
              
        }
        if (bulletTimeCurrent >= destroyOnHitTime && isHitObject)
        {
            isTimedOut = true;
        }
        if (isTimedOut)
        {
            onActivation();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hitCollider = collision.gameObject.GetComponent<Rigidbody>();
        if (hitCollider) {
            //Debug.Log(collision.gameObject + " collided with " + gameObject);
            var decorRigid = collision.gameObject.GetComponent<Rigidbody>();
            decorRigid.AddForce(gameObject.transform.forward * 1000);
        }    

        if (isDestroyOnHit && !isHitObject) {
            //onActivation();
            isHitObject = true;
            bulletTimeCurrent = 0;
        //Destroy(gameObject,destroyOnHitTime);
        }

        if (!isDealDamageOnlyOnExplosion)
        {      
        var destructible = collision.gameObject.GetComponent<Destructible>();
        if (destructible)
        {
                destructible.Hit(damage);
        }
        }

    }

    private void OnDestroy()
    {

    }

    private void onActivation()
    {
        if (isExploding && explosionEffect)
        {
            var explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            explosion.transform.localScale = explosion.transform.localScale * explosionRadius;
            var explosionScript = explosion.GetComponent<Explosion>();
            if (explosionScript)
            {
                if (isDealDamageOnlyOnExplosion)
                {
                    explosionScript.explosionDamage = damage;
                }
                else
                {
                    explosionScript.explosionDamage = explosionDamage;
                }
            }
            else
            {
                Debug.Log("Weapon caused an explosion, but no explosion script was found.");
            }

            Destroy(explosion, 1);
            Destroy(gameObject);
        }
    }

}
