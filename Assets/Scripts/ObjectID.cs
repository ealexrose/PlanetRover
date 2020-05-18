using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectID : MonoBehaviour
{
    [System.Serializable]
    public enum ID
    {
        none,
        battery,
        enemy
    }

    public ID id;
}
