using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Weapon Stats
    public float damage = 1;
    public float attackspeed = 1.0f;
    public bool isAutomatic = false;
    private float currentCooldown;
    private float damagePushModifier = 50;
    public float recoil = 0.2f;

    //Weapon Info
    public string weaponName = "None";
    public string weaponDesc = "None";

    //BulletStats
    public GameObject bulletPrefab;
    public float bulletTime = 10;
    public float bulletSpeed = 1000;
    public GameObject bulletFirePoint;
    public GameObject hitEffect;
    //private bool hasFirePoint = false;

    //OwnerInfo
    public PlayerCharacter wpnOwner;
    public Vector3 gunPositionModifier;

    private AudioSource sound;

    // Start is called before the first frame update
    public void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public void Fire()
    {
        if (currentCooldown <= 0) { 
        if (bulletFirePoint == null)
        {
            //hasFirePoint = true;
            bulletFirePoint = gameObject;
        }
        if (bulletPrefab)
        {
                FireProjectile();
        } else
            {
                FireHitscan();
            }


        sound.pitch = Random.Range(0.8f,1.2f);
        sound.Play();

            currentCooldown = attackspeed;
            Recoil();
        }
    }

    private void Recoil()
    {
        var wpnRecoil = recoil * damage;
        wpnOwner.affectedRecoil += wpnRecoil;
        wpnOwner.charAnimator.SetInteger("FireID", Random.Range(1,4));
        wpnOwner.charAnimator.SetTrigger("Fire");

    }

    public void FireHitscan()
    {
        Ray hitScanRay = new Ray(transform.position,transform.forward);
        RaycastHit target;
        if (Physics.Raycast(hitScanRay, out target))
        {
            var hitTarget = target.collider.gameObject;
            Debug.Log(target.collider.gameObject);
            if (hitEffect)
            {
                var createdEffect = Instantiate(hitEffect, target.point, new Quaternion());
                Destroy(createdEffect, 1.0f);
            }
            
            
            var targetRigid = hitTarget.GetComponent<Rigidbody>();
            if (targetRigid)
            {
                targetRigid.AddForce(transform.forward * damage * damagePushModifier);
            }

            var destructible = hitTarget.GetComponent<Destructible>();
            if (destructible)
            {
                destructible.Hit(damage);
            }

        }
    }

    public void FireProjectile()
    {
        var bullet = Instantiate(bulletPrefab, bulletFirePoint.transform.position, bulletFirePoint.transform.rotation);
        var bulletRigid = bullet.GetComponent<Rigidbody>();
        bulletRigid.AddForce(transform.forward * bulletSpeed);
        var bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage += damage;
        bulletScript.bulletTime += bulletTime;
    }
}
