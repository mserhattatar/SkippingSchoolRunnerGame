using System.Collections.Generic;
using UnityEngine;

public class GroundParallax : MonoBehaviour
{
    private Transform _mainCamera;
    [SerializeField] private List<Transform> groundList;
    [SerializeField] private float groundLenght;
    [SerializeField] private float cameraPassedGroundLenght;

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
        var terrainPos = groundList[0].position;
        
        if (_mainCamera.position.z > terrainPos.z + cameraPassedGroundLenght)
        {
            groundList[0].position = new Vector3(terrainPos.x, terrainPos.y, groundList[2].position.z + groundLenght);
            groundList.Add(groundList[0]);
            groundList.RemoveAt(0);
        }
    }
}