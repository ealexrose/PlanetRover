using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthAtSpawn : MonoBehaviour
{
    //How small you want the object to be at spawn
    public float desiredStart;
    //How big you want the object to be in the end
    public float desiredSize;
    //How fast you want the object to grow
    public float desiredGrowth;

    // Start is called before the first frame update
    void Start()
    {
        //Makes the object really small at the beginning of its spawn
        transform.localScale = new Vector3(desiredStart, desiredStart, desiredStart);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x <= desiredSize)
        {
            //Slowly turns the object bigger until it reaches its desired size
            transform.localScale += new Vector3(desiredGrowth, desiredGrowth, desiredGrowth) * Time.deltaTime;
        }
    }
}
