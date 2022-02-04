using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ComponentContainer _myComponent;

    public delegate void GameManagerDelegate();

    public static GameManagerDelegate ResetLevelDelegate;

    public void Initialize(ComponentContainer componentContainer)
    {
        _myComponent = componentContainer;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GameDataScript.LoadLevelDataFromJson();
    }
}