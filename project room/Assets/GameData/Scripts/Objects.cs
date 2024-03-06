using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{

    public enum ObjectType { sofa , Cupboards , chair , TV , FloorLamp , CellingLights , wall};
    public ObjectType objectType;

    public GameObject Options;

    public bool movable = true;
}
