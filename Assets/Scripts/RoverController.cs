using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject homePlanet;
    //Copy This part and start function to have audio manager access


    public float moveSpeed;
    public float turnSpeed;
    public float radius;
    public float offset;
    public float cameraRelativeDistance;
    public bool adjust;
    [Space]
    [Header("Audio Properties")]
    public string driveSound;
    public string mainTheme;
    [HideInInspector]
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play(mainTheme);
    }

    // Update is called once per frame
    void Update()
    {
        //Get the amount fo distance to turn
        audioManager.Play(driveSound);

        float radiusSquared = Mathf.Pow((radius+offset), 2);
        if (adjust)
        {
            Vector3 downVector = (transform.position - homePlanet.transform.position).normalized;
            Quaternion toRotation = Quaternion.FromToRotation(transform.up, downVector) * transform.rotation;
            transform.rotation = toRotation;
        }

        float horizontal = Input.GetAxis("Horizontal") * turnSpeed;
        transform.Rotate(Vector3.up * (horizontal * Time.deltaTime));


        transform.position += transform.right * moveSpeed * Time.deltaTime;

        float objectRadius = Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y, 2) + Mathf.Pow(transform.position.z, 2);
        if (objectRadius > radiusSquared+(radiusSquared*.01f) || objectRadius < radiusSquared - (radiusSquared * .01f))
        {
            transform.position = (transform.position - homePlanet.transform.position).normalized * (radius+offset);
        }

    }
    void LateUpdate()
    {
        mainCamera.transform.position = (transform.position - homePlanet.transform.position).normalized * (radius + cameraRelativeDistance);
        mainCamera.transform.LookAt(this.transform.position);
    }
}
