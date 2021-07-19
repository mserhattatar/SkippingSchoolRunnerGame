using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleObjectsManager : MonoBehaviour
{
    private float _oldPlayerPosition;
    private int _activeObsIndex;

    [SerializeField] private List<ObstacleObjectController> obstacleList = new List<ObstacleObjectController>();
    public GameObject playerGameObject;


    private void OnEnable()
    {
        GameManager.ResetLevelDelegate += ResetObstacleObjects;
    }

    private void Start()
    {
        _activeObsIndex = -1;
        _oldPlayerPosition = playerGameObject.transform.position.z;
    }

    private void Update()
    {
        if (_oldPlayerPosition + 15f < playerGameObject.transform.position.z)
        {
            _oldPlayerPosition = playerGameObject.transform.position.z;
            ObstacleObjectSetActive();
        }
        else
            ObstacleObjectSetPassive();
    }

    private void ObstacleObjectSetPassive(bool all = false)
    {
        var activeObstacleList = obstacleList.Where(o => o.isGameObjectActive);
        foreach (var o in activeObstacleList)
        {
            if (all || _oldPlayerPosition > o.transform.position.z + 10f || o.transform.position.y < -5f)
                o.ObstacleObjectSetActive(false);
        }
    }

    private void ObstacleObjectSetActive()
    {
        var firstPassiveObstacle =
            obstacleList.First(t => !t.isGameObjectActive && obstacleList.IndexOf(t) > _activeObsIndex);
        firstPassiveObstacle.SetObstacleObject(RandomObstaclePosition(), RandomObstacleRotation());
        if (obstacleList.IndexOf(firstPassiveObstacle) == obstacleList.Count - 1)
            _activeObsIndex = -1;
        else
            _activeObsIndex = obstacleList.IndexOf(firstPassiveObstacle);
    }

    private Vector3 RandomObstaclePosition()
    {
        var extraX = Random.Range(-4f, 4f);
        var extraZ = Random.Range(30f, 50f);
        var pPos = playerGameObject.transform.position;
        var obstaclePosition = new Vector3(extraX, 15f, pPos.z + extraZ);
        return obstaclePosition;
    }

    private static Quaternion RandomObstacleRotation()
    {
        float rotationExtraPos = Random.Range(-50, 50);
        var obstacleRotation = Quaternion.Euler(0f, 180f + rotationExtraPos, 0f);
        return obstacleRotation;
    }

    private void ResetObstacleObjects()
    {
        _activeObsIndex = -1;
        _oldPlayerPosition = playerGameObject.transform.position.z;
        ObstacleObjectSetPassive(true);
    }
}