using UnityEngine;

public class ObstacleObjectController : MonoBehaviour
{
    [HideInInspector] public bool isGameObjectActive;


    public void SetObstacleObject(Vector3 position, Quaternion rotation)
    {
        ObstacleObjectSetActive(true);
        var o = gameObject;
        var oTransform = o.transform;
        oTransform.position = position;
        oTransform.rotation = rotation;
        o.tag = "ActiveObstacleObject";
    }

    public void ObstacleObjectSetActive(bool setActive)
    {
        isGameObjectActive = setActive;
    }
}