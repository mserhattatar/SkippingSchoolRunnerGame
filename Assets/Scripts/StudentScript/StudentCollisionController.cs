using UnityEngine;
public class StudentCollisionController : MonoBehaviour
{
    public delegate void StudentCollisionDelegate();

    public static StudentCollisionDelegate TeacherCollisionDelegate;
    public static StudentCollisionDelegate ObstacleCollisionDelegate;
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
