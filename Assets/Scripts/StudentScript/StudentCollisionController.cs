using UnityEngine;

public class StudentCollisionController : MonoBehaviour
{
    private ComponentContainer _myComponent;

    public delegate void StudentCollisionDelegate();

    public static StudentCollisionDelegate TeacherCollisionDelegate;
    public static StudentCollisionDelegate ObstacleCollisionDelegate;

    public void Initialize(ComponentContainer componentContainer)
    {
        _myComponent = componentContainer;
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "ActiveObstacleObject":
                other.gameObject.tag = "PassiveObstacleObject";
                ObstacleCollisionDelegate();
                break;
            case "Teacher":
                TeacherCollisionDelegate();
                break;
        }
    }
}