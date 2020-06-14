using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public ScoreManager score;
    public Image batteryBar;
    public RoverController roverController;
    public LayerMask objectMask;
    public float maxBattery;
    public float drainSpeed;
    [HideInInspector]
    public float currentBattery;
    public float batteryPickupValue;
    public float enemyHitValue;
    public float hitSlowdownDuration;
    public float hitSlowdownMultiplier;
    [HideInInspector]
    public bool alive;
    [Space]
    [Header("Audio Properties")]
    public string batteryPickupSfx;
    public string hitSfx;
    public string collectableSfx;

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
        batteryBar.fillAmount = currentBattery / maxBattery;
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

        score.active = false;

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
                    StartCoroutine(HitStun());
                    break;
                case "collectable":
                    Destroy(collidedObject);
                    score.score += 1;
                    audioManager.Play(collectableSfx);
                    break;
                default:
                    Debug.Log(collidedObject.GetComponent<ObjectID>().id.ToString() + " has an unaccounted for case");
                    break;

            }
            currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
        }
    }

    IEnumerator HitStun()
    {
        //Play Animation
        roverController.moveSpeed = roverController.moveSpeed * hitSlowdownMultiplier;
        //Wait
        yield return new WaitForSeconds(hitSlowdownDuration);
        //Load Scene
        roverController.moveSpeed = roverController.moveSpeed / hitSlowdownMultiplier;
    }
}
