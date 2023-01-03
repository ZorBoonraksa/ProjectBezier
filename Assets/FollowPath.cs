using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public List<RouteData> route;
    private float elapsedTime;
    public bool isRotate = false;
    public float rotateSpeed = 1f;
    private int routeIndex;

    private void Start() {
        elapsedTime = 0;
        routeIndex = 0;
    }

    void Update()
    {
        if(elapsedTime > route[routeIndex].routePath[route[routeIndex].routePath.Count-1].time)
        {
            elapsedTime = 0f;
            routeIndex++;
        }
        if(routeIndex >= route.Count){
            routeIndex = 0;
        }
        elapsedTime += Time.deltaTime;
        int index = (int)(elapsedTime / route[routeIndex].routePath[1].time);
        if(index < route[routeIndex].routePath.Count-1)
        {
            float percentComplete = (elapsedTime - route[routeIndex].routePath[index].time) / (route[routeIndex].routePath[index + 1].time - route[routeIndex].routePath[index].time);
            transform.position = Vector3.Lerp(route[routeIndex].routePath[index].position,route[routeIndex].routePath[index + 1].position,Mathf.SmoothStep(0,1,percentComplete));
        }
        if(isRotate)
        {
            transform.Rotate(new Vector3(0,0,rotateSpeed) * Time.deltaTime);
        }
    }
}
