using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ComponentContainer myComponent;

    public delegate void GameManagerDelegate();

    public static GameManagerDelegate ResetLevelDelegate;

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GameDataScript.LoadLevelDataFromJson();
    }
}