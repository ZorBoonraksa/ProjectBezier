using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{
    public float time;
    public Vector3 position;
}

public class RouteData : ScriptableObject
{
    public List<Path> routePath = new List<Path>();
    public bool _IsRotate;
    public Vector3 rotationAxis;
}
