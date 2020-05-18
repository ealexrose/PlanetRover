using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    public RectTransform batteryBar;
    public RoverController roverController;
    public LayerMask objectMask;
    public float maxBattery;
    public float drainSpeed;
    public float currentBattery;
    public float batteryPickupValue;
    public float enemyHitValue;
    public bool alive;
    // Start is called before the first frame update
    void Start()
    {
        currentBattery = maxBattery;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        batteryBar.localScale = new Vector3(currentBattery / maxBattery, 1, 1);
        if (currentBattery > 0)
        {
            currentBattery -= drainSpeed * Time.deltaTime;
        }
        else if(alive)
        {
            BatteryDepleted();
            alive = false;
        }
       currentBattery =  Mathf.Clamp(currentBattery,0, maxBattery);
    }

    void BatteryDepleted()
    {
        roverController.enabled = false;
        
        Transform roverTransform = roverController.transform;
        Rigidbody roverBody = roverTransform.GetComponent<Rigidbody>();
        //roverTransform.GetComponent<Rigidbody>().useGravity = true;
        roverBody.constraints = RigidbodyConstraints.None;
        roverBody.AddForce(roverTransform.up);
        roverBody.AddTorque(roverTransform.right + (roverTransform.forward*3));

    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.collider.gameObject;
        if (collidedObject.layer == 8)
        {
            Debug.Log("Touched a " + collidedObject.GetComponent<ObjectID>().id.ToString());
            switch (collidedObject.GetComponent<ObjectID>().id.ToString())
            {
                case "battery":
                    Destroy(collidedObject);
                    currentBattery += batteryPickupValue;
                    break;
                case "enemy":
                    currentBattery -= enemyHitValue;
                    break;
                default:
                    Debug.Log(collidedObject.GetComponent<ObjectID>().id.ToString() + " has an unaccounted for case");
                    break;

            }
            currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
        }
    }
}
