using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_PingPong : MonoBehaviour
{
    //public GameObject particleEffect;
    private GameObject targetVisible;
    public GameObject targetVisiblePrefab;

    public Vector3 currentOrder;
    public float movementSpeed;
    public float reachedRoundUp;

    public float orderRandomRange;

    private Vector3 initialPosition;
    private Vector3 movementVector;
    private Vector3 endPosition;
    private float distanceBetween;

    private Rigidbody objectPhysics;
    
    //
    // Start is called before the first frame update
    void Start()
    {
        objectPhysics = this.GetComponent<Rigidbody>();
        //Debug.Log(objectPhysics.mass);
        if (reachedRoundUp == 0.0f) {
            reachedRoundUp = 0.3f;
        }
        
        initialPosition = transform.position;
        endPosition = new Vector3(Random.Range(-orderRandomRange,orderRandomRange), Random.Range(0.0f, orderRandomRange), Random.Range(-orderRandomRange, orderRandomRange));
        if (currentOrder==new Vector3(0.0f,0.0f,0.0f)) {
        currentOrder = endPosition;
        }
        TargetVisibleSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        //particleEffect.transform.position = transform.position;
        movementVector = currentOrder - transform.position;
        distanceBetween = movementVector.magnitude;
        movementVector.Normalize();
        //transform.Translate(movementVector*movementSpeed*Time.deltaTime);
        objectPhysics.AddForce(movementVector*movementSpeed*Time.deltaTime);
        if (objectPhysics.mass >= 0.1) {
            objectPhysics.mass = objectPhysics.mass - 0.0001f;
        } else
        {
            objectPhysics.mass = 0.1f;
        }
        
        if (distanceBetween <= reachedRoundUp)
        {
            objectPhysics.mass = 1;
            if (currentOrder == endPosition) {
                currentOrder = initialPosition;
            }
            else if (currentOrder == initialPosition) {
                currentOrder = endPosition;
            }
            TargetVisibleSpawn();
        }
    }

    //private Vector3 targetVisiblePosition;
    //private Vector3 targetVisibleRotation;
    private GameObject targetInstance;
    void TargetVisibleSpawn() 
    {
        if (targetVisiblePrefab!=null) {
            Destroy(targetInstance);
            var targetVisiblePosition = currentOrder;
            var targetVisibleRotation = Quaternion.identity;
            targetInstance = Instantiate(targetVisiblePrefab, targetVisiblePosition, targetVisibleRotation);
        }
    }

    private void OnDestroy()
    {
        if (targetInstance!=null) {
            Destroy(targetInstance);
        }
    }
}
