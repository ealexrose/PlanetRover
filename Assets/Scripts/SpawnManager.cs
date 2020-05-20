using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public bool debugSpheres;
    public GameObject homePlanet;
    public GameObject enemy;
    public GameObject battery;
    public LayerMask objectMask;
    public LayerMask roverMask;
    public float minimumObjectSpace;
    public float minimumRoverSpace;


    public float enemyTimer;
    float enemyTimerIndex;
    public float batteryTimer;
    float batteryTimerIndex;
    private bool drawSpawnAttempt;
    private Vector3 spawnAttemptPosition;
    private float spawnAttemptSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        batteryTimerIndex += Time.deltaTime;
        enemyTimerIndex += Time.deltaTime;
        if (enemyTimerIndex >= enemyTimer)
        {
            SpawnItem(enemy, 15f);
            enemyTimerIndex = 0;
        }
        if (batteryTimerIndex >= batteryTimer)
        {
            SpawnItem(battery, 15f);
            batteryTimerIndex = 0;   
        }
    }

    public void SpawnItem(GameObject _objectToSpawn, float _planetRadius)
    {

        bool objectSpawned = false;
        
        int repeatLimit = 30;
        int repeatIndex = 0;
        do
        {
            Vector3 spawnPosition = RandomPointOnSphere(_planetRadius);

            spawnAttemptPosition = spawnPosition;
            spawnAttemptSize = minimumObjectSpace;
            drawSpawnAttempt = true;
            if (!Physics.CheckSphere(spawnPosition, minimumObjectSpace, objectMask) && !Physics.CheckSphere(spawnPosition, minimumRoverSpace, roverMask))
            {
               GameObject newObject = Instantiate(_objectToSpawn, spawnPosition, Quaternion.identity, null);
                Vector3 downVector = (newObject.transform.position - homePlanet.transform.position).normalized;
                Quaternion toRotation = Quaternion.FromToRotation(transform.up, downVector) * transform.rotation;
                newObject.transform.rotation = toRotation;
                objectSpawned = true;
            }
            repeatIndex++;
        } while (!objectSpawned && !(repeatIndex > repeatLimit));

    }

    public Vector3 RandomPointOnSphere(float _radius)
    {
        //Determine the radial Theta (rotational position around object)
        float theta = Random.Range(-180f, 180f);
        //Determine the angular Phi (Distance abovee or below equator)
        float phi = Random.Range(-180f, 180f);


        float xPos = _radius * Mathf.Sin(phi * Mathf.Deg2Rad) * Mathf.Cos(theta * Mathf.Deg2Rad);
        float yPos = _radius * Mathf.Sin(phi * Mathf.Deg2Rad) * Mathf.Sin(theta * Mathf.Deg2Rad);
        float zPos = _radius * Mathf.Cos(phi * Mathf.Deg2Rad);

        Vector3 spawnPosition = new Vector3(xPos, yPos, zPos);
        return spawnPosition;
    }

    private void OnDrawGizmos()
    {
        if (debugSpheres)
        {
            if (drawSpawnAttempt)
            {
                Gizmos.color = new Color(0, 0, 1, .3f);
                Gizmos.DrawSphere(spawnAttemptPosition, spawnAttemptSize);
            }
        }
    }
}
