using UnityEngine;

public class MainComponent : MonoBehaviour
{
    private ComponentContainer componentContainer;

    private GameManager gameManager;
    private CanvasManager canvasManager;


    private void Awake()
    {
        componentContainer = new ComponentContainer();

        CreateGameManager();
        CreateCanvasManager();

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


    private void InitializeComponents()
    {
        gameManager.Initialize(componentContainer);
        canvasManager.Initialize(componentContainer);
    }
}