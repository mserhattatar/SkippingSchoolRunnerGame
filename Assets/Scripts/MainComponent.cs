using UnityEngine;

public class MainComponent : MonoBehaviour
{
    private ComponentContainer componentContainer;

    private GameManager gameManager;
    private CanvasManager canvasManager;
    private CineMachineManager cineMachineManager;
    private StudentAnimatorController studentAnimatorController;
    private StudentCollisionController studentCollisionController;
    private StudentMovementController studentMovementController;
    private ObstacleObjectsManager obstacleObjectsManager;


    private void Awake()
    {
        componentContainer = new ComponentContainer();

        CreateGameManager();
        CreateCanvasManager();
        CreateCineMachineManager();
        CreateStudentAnimatorController();
        CreateStudentCollisionController();
        CreateStudentMovementController();
        CreateObstacleObjectsManager();

        InitializeComponents();
    }

    private void CreateGameManager()
    {
        gameManager = FindObjectOfType<GameManager>();
        componentContainer.AddComponent("GameManager", gameManager);
    }

    private void CreateCanvasManager()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        componentContainer.AddComponent("CanvasManager", canvasManager);
    }


    private void CreateCineMachineManager()
    {
        cineMachineManager = FindObjectOfType<CineMachineManager>();
        componentContainer.AddComponent("CineMachineManager", cineMachineManager);
    }

    private void CreateStudentAnimatorController()
    {
        studentAnimatorController = FindObjectOfType<StudentAnimatorController>();
        componentContainer.AddComponent("StudentAnimatorController", studentAnimatorController);
    }

    private void CreateStudentCollisionController()
    {
        studentCollisionController = FindObjectOfType<StudentCollisionController>();
        componentContainer.AddComponent("StudentCollisionController", studentCollisionController);
    }

    private void CreateStudentMovementController()
    {
        studentMovementController = FindObjectOfType<StudentMovementController>();
        componentContainer.AddComponent("StudentMovementController", studentMovementController);
    }

    private void CreateObstacleObjectsManager()
    {
        obstacleObjectsManager = FindObjectOfType<ObstacleObjectsManager>();
        componentContainer.AddComponent("ObstacleObjectsManager", obstacleObjectsManager);
    }


    private void InitializeComponents()
    {
        gameManager.Initialize(componentContainer);
        canvasManager.Initialize(componentContainer);
        obstacleObjectsManager.Initialize(componentContainer);
        //StudentMovementController.Initialize(componentContainer);
        studentCollisionController.Initialize(componentContainer);
        //StudentAnimatorController.Initialize(componentContainer);
        cineMachineManager.Initialize(componentContainer);
    }
}