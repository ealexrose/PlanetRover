using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Makes the alien_sway really small at the beginning of its spawn
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x <= 20f)
        {
            //Slowly turns the alien bigger until it reaches its normal scale of 20f
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}
