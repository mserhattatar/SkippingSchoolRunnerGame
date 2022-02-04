using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PointLightSpawnController : MonoBehaviour
{
    private Transform _mainCamera;
    [SerializeField] private List<Transform> pointLightList;
    [SerializeField] private float eachLightLenght;
    [SerializeField] private float cameraPosLenght;

    private void Start()
    {
        if (Camera.main != null)
            _mainCamera = Camera.main.transform;
        else
            Debug.LogWarning("Main Camera not found!!!!");
    }

    private void LateUpdate()
    {
        TerrainMovement();
    }

    private void TerrainMovement()
    {
        var cameraPosZ = _mainCamera.position.z + cameraPosLenght;
        var terrainPosZ = pointLightList[1].position.z;

        if (cameraPosZ > terrainPosZ)
        {
            pointLightList[0].position = new Vector3(pointLightList[0].position.x,
                pointLightList[0].position.y, pointLightList[2].position.z + eachLightLenght);
            pointLightList.Add(pointLightList[0]);
            pointLightList.RemoveAt(0);
        }
    }
}