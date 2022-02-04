using System.Collections.Generic;
using UnityEngine;

public class ParallaxCorridor : MonoBehaviour
{
    private Transform _mainCamera;
    [SerializeField] private List<Transform> terrainList;
    [SerializeField] private float terrainLenght;
    [SerializeField] private float cameraFocusTerrainLenght;

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
        var cameraPosZ = _mainCamera.position.z + cameraFocusTerrainLenght;
        var terrainPosZ = terrainList[1].position.z;

        if (cameraPosZ > terrainPosZ)
        {
            terrainList[0].position = new Vector3(terrainList[0].position.x,
                terrainList[0].position.y, terrainList[2].position.z + terrainLenght);
            terrainList.Add(terrainList[0]);
            terrainList.RemoveAt(0);
        }
    }
}