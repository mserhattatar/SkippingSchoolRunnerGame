using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleObjectsManager : MonoBehaviour
{
    private float _oldPlayerPosition;

    public static ObstacleObjectsManager instance;
    public List<ObstacleObjectController> obstacleObjectsList = new List<ObstacleObjectController>();
    public GameObject playerGameObject;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GetPlayerPos();
    }

    private void Update()
    {
        
        if ((_oldPlayerPosition + 15f < playerGameObject.transform.position.z))
        {
            GetPlayerPos();
            ObstacleObjectSetActive();
        }
        else
        {
            ObstacleObjectSetPassive();
        }
    }

    private void GetPlayerPos()
    {
        _oldPlayerPosition = playerGameObject.transform.position.z;
    }

    private void ObstacleObjectSetPassive(bool all = false)
    {
        foreach (var o in obstacleObjectsList.Where(o => o.isGameObjectActive))
        {
            if (all)
            {
                o.ObstacleObjectSetActive(false);
                return;
            }
            if (_oldPlayerPosition - 10f > o.transform.position.z || o.transform.position.y < -5f)
            {
                o.ObstacleObjectSetActive(false);
            }
        }
    }
    private void  ObstacleObjectSetActive()
    {
        foreach (var t in obstacleObjectsList.Where(t => !t.isGameObjectActive))
        {
            t.SetObstacleObject(RandomObstaclePosition(), RandomObstacleRotation());
            return;
        }
    }

    private Vector3 RandomObstaclePosition()
    {
        var extraX = Random.Range(-3f, 4f);
        var extraZ =Random.Range(30f, 50f);
        var pPos = playerGameObject.transform.position;
        var obstaclePosition =new Vector3(pPos.x +extraX, 15f, pPos.z +extraZ);
        return obstaclePosition;
    }

    private static Quaternion RandomObstacleRotation()
    {
        float rotationExtraPos = Random.Range(-50, 50);
        var obstacleRotation = Quaternion.Euler(0f, 180f + rotationExtraPos, 0f);
        return obstacleRotation;
    }
    public void ResetObstacleObjects()
    {
        GetPlayerPos();
        ObstacleObjectSetPassive(true);
    }
}
