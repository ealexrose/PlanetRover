using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoverController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject homePlanet;
    public GameObject signal;
    public GameObject transmissionStart;
    public GameObject transmissionEnd;
    public MobileControls mobileControls;
    //Copy This part and start function to have audio manager access


    public float moveSpeed;
    public float turnSpeed;
    public float radius;
    public float offset;
    public float cameraRelativeDistance;
    public bool adjust;
    public float inputDelay;
    public bool inputStream;
    public List<Timer> actionQueue = new List<Timer>();
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
        float horizontal = 0;
        if (inputDelay == 0)
        {
            horizontal = Input.GetAxis("Horizontal") * turnSpeed;
            if (mobileControls.RightSideTouch())
            {
                horizontal = 1 * turnSpeed; 
            }
            if (mobileControls.LeftSideTouch())
            {
                horizontal = -1 * turnSpeed;
            }
        }
        else
        {
            AccumulatAction();
            horizontal = ProgressQueue() * turnSpeed;
        }

        transform.Rotate(Vector3.up * (horizontal * Time.deltaTime));

        string output;
        output = "( ";
        foreach (Timer t in actionQueue)
        {
            output += t.timerDelay.ToString("0.00");
            output += ", ";
        }
        output.Remove(output.Length - 1);
        output += ")";
        //Debug.Log(output);

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


    public void AccumulatAction()
    {
        float input = Input.GetAxis("Horizontal");

        if (mobileControls.RightSideTouch())
        {
            input = 1 * turnSpeed;
        }
        if (mobileControls.LeftSideTouch())
        {
            input = -1 * turnSpeed;
        }
        //Debug.Log(input);
        if (input != 0)
        {
            if (!inputStream)
            {
                Timer timer = new Timer(inputDelay, .1f, Mathf.Sign(input));
                timer.partner = Instantiate(signal, transmissionStart.transform);
                if (Mathf.Sign(input) > 0)
                {
                    timer.partner.GetComponent<Image>().color = new Color(0, 188f / 256f, 202f / 256f, 256f / 256f);
                }
                else
                {
                    timer.partner.GetComponent<Image>().color = new Color(219f/256f, 165f/256f, 0f, 256f / 256f);
                }               
                actionQueue.Add(timer);
                inputStream = true;
            }
            else if (inputStream)
            {
                actionQueue[actionQueue.Count - 1].activeTimer += Time.deltaTime;              
            }
        }
        else
        {
            inputStream = false;
        }

    }
    public float ProgressQueue()
    {
        float delayedInput = 0;
        List<Timer> removeQueue = new List<Timer>();
        foreach (Timer t in actionQueue)
        {
            t.ResizePartner(0.1f + t.activeTimer);
            Vector3 offset = transmissionEnd.GetComponent<RectTransform>().localPosition - transmissionStart.GetComponent<RectTransform>().localPosition;
            Vector3 rectMove = (Mathf.Clamp(inputDelay - t.timerDelay, 0, inputDelay) / inputDelay) * offset;
            t.partner.GetComponent<RectTransform>().localPosition = rectMove;
            //Debug.Log(rectMove);
            if (t.timerDelay > 0)
            {
                t.timerDelay -= Time.deltaTime;
            }
            if(t.timerDelay <= 0)
            {
                t.activeTimer -= Time.deltaTime;
                delayedInput = /*Time.deltaTime **/ t.index; 
                if (t.activeTimer <= 0)
                {
                    removeQueue.Add(t);
                }
                
            }
        }
        foreach (Timer t in removeQueue)
        {
            Destroy(t.partner);
            actionQueue.Remove(t);
        }
        return delayedInput;
    }
}
