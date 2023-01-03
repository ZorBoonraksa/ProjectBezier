using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Route : MonoBehaviour
{
    public Transform[] controlPoints;
    [SerializeField]
    private string routeFileName;
    private Vector3 gizmosPosition;
    public int frequency = 100;
    public float duration = 2f;
    private Vector3 objectPosition;

    public void SetRoute()
    {
        if(duration <= 0)
        {
            Debug.LogWarning("duration must be more than 0");
            return;
        }
        if(string.IsNullOrEmpty(routeFileName))
        {
            Debug.LogWarning("Please Enter the name");
            return;
        }
        float period = duration / (float)frequency;
        RouteData thisRoute = ScriptableObject.CreateInstance<RouteData>();
        for(int i = 0; i < frequency; i++)
        {
            float t = i / (float)frequency;

            objectPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;

            Path newPoint = new Path(){
                time = period * i,
                position = objectPosition
            };
            thisRoute.routePath.Add(newPoint);
        }
        if(!AssetDatabase.IsValidFolder("Assets/Resources/Route"))
        {
            if(!AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                AssetDatabase.CreateFolder("Assets","Resources");
            }
            AssetDatabase.CreateFolder("Assets/Resources","Route");
        }
        string path = "Assets/Resources/Route/" + routeFileName + ".asset";
        AssetDatabase.CreateAsset(thisRoute, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = thisRoute;
    }

    private void OnDrawGizmos()
    {
        for(float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmosPosition, 0.15f);
        }

        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y), new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));
        Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y), new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));

    }
}
