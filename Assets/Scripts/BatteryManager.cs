using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public RectTransform batteryBar;
    public RoverController roverController;
    public LayerMask objectMask;
    public float maxBattery;
    public float drainSpeed;
    public float currentBattery;
    public float batteryPickupValue;
    public float enemyHitValue;
    public bool alive;
    [Space]
    [Header("Audio Properties")]
    public string batteryPickupSfx;
    public string hitSfx;

    [HideInInspector]
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
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
        pauseMenu.GetComponent<PauseMenu>().EndScreen();
        roverController.enabled = false;
        
        Transform roverTransform = roverController.transform;
        Rigidbody roverBody = roverTransform.GetComponent<Rigidbody>();
        //roverTransform.GetComponent<Rigidbody>().useGravity = true;
        roverBody.constraints = RigidbodyConstraints.None;
        roverBody.AddForce(roverTransform.up);
        roverBody.AddTorque((roverTransform.right*6) + (roverTransform.forward*18));

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
                    audioManager.Play(batteryPickupSfx);
                    break;
                case "enemy":
                    currentBattery -= enemyHitValue;
                    audioManager.Play(hitSfx);
                    break;
                default:
                    Debug.Log(collidedObject.GetComponent<ObjectID>().id.ToString() + " has an unaccounted for case");
                    break;

            }
            currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
        }
    }
}
