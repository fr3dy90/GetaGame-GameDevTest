using System.Collections;
using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
public class CheckPointCreator : MonoBehaviour
{
    public GameObject checkpointPref;
    private WayPointSystem wpSystem;

    public void CreateCheckPoints()
    {
        if (wpSystem == null)
        {
            wpSystem = FindObjectOfType<WayPointSystem>();
        }

        for (int i = 0; i < wpSystem.points.Length; i++)
        {
            int nextWp = i + 1;
            if (nextWp >= wpSystem.points.Length)
            {
                nextWp = 0;
            }

            GameObject go = Instantiate(checkpointPref);
            go.transform.position = wpSystem.points[i];
            go.transform.LookAt(wpSystem.points[nextWp]);
            go.name = i.ToString();
            go.transform.parent = transform;
        }
    }
}