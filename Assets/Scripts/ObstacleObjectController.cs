using UnityEngine;

public class ObstacleObjectController : MonoBehaviour
{
   private GameObject _gameObject;
   
   [HideInInspector]
   public bool isGameObjectActive;

   private void Start()
   {
      _gameObject = gameObject;
      ObstacleObjectSetActive(false);
   }

   public void SetObstacleObject(Vector3 _transform, Quaternion _rotation)
   {
      ObstacleObjectSetActive(true);
      var transform1 = _gameObject.transform;
      transform1.position = _transform;
      transform1.rotation = _rotation;
      _gameObject.tag = "ActiveObstacleObject";
   }

   public void ObstacleObjectSetActive(bool setActive)
   {
      gameObject.SetActive(setActive);
      isGameObjectActive = setActive;

   }
}
