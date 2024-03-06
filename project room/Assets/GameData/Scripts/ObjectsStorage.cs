using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FurnitureObjects", order = 1)]
public class ObjectsStorage : ScriptableObject
{
    public List<GameObject> sofas;
    public List<GameObject> Cupboard;
    public List<GameObject> Chair;
    public List<GameObject> TV;
    public List<GameObject> FloorLamp;
    public List<GameObject> CelingLights;

    public List<Material> walls;
}
