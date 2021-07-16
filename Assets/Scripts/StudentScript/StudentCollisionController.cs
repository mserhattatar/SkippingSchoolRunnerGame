using UnityEngine;
public class StudentCollisionController : MonoBehaviour
{
    public delegate void StudentCollisionDelegate();

    public static StudentCollisionDelegate teacherCollisionDelegate;
    public static StudentCollisionDelegate obstacleCollisionDelegate;
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "ActiveObstacleObject":
                other.gameObject.tag = "PassiveObstacleObject";
                obstacleCollisionDelegate();
                break;
            case "Teacher":
                teacherCollisionDelegate();
                break;
        }
    }
}
