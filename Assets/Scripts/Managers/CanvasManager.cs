using System.Collections;
using TMPro;
using UnityEngine;


public class CanvasManager : JoystickManager
{
    public static CanvasManager instance;
    
    private ComponentContainer _myComponent;

    private float _timerTime;
    private bool _isTimerStarted;
    private bool _stopTimer;
    
    [SerializeField] private GameObject gameEndPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject homePanel;

    [SerializeField] private TextMeshProUGUI gameStartTimerText;
    [SerializeField] private TextMeshProUGUI gameEndScoreText;
    [SerializeField] private TextMeshProUGUI startPanelBestScoreText;
    [SerializeField] private TextMeshProUGUI levelScoreText;
    [SerializeField] private Transform student;
    
    public int bestScore;
   
    public void Initialize(ComponentContainer componentContainer)
    {
        _myComponent = componentContainer;
    }
    private void Awake()
    {
        instance = this;
        _timerTime = 3f;
        _isTimerStarted = false;
        _stopTimer = false;
    }

    private void Start()
    {
        HomePanelSetActive();
    }

    private void Update()
    {
        switch (_isTimerStarted)
        {
            case false when CheckJoystickHorizontal():
                StartTimerPanel();
                break;
            case true when !_stopTimer:
                UpdateStartTimer();
                break;
        }

        LevelScoreMetreText();
    }

    private void StartTimerPanel()
    {
        StartPanelSetActive(false);
        GameStartTimerTextSetActive(true);
    }

    private void HomePanelSetPassive()
    {
        homePanel.SetActive(false);
    }
    private void HomePanelSetActive()
    {
        homePanel.SetActive(true);
        StartCoroutine(HomePanelSetPassiveDelay());
    }
    private IEnumerator HomePanelSetPassiveDelay()
    {
        yield return new WaitForSeconds(1.3f);
        HomePanelSetPassive();
        StartPanelSetActive(true);
    }

    private void StartPanelSetActive(bool setActive)
    {
        startPanel.SetActive(setActive);
        if (setActive)
            StartPanelBestScore();
    }

    private void StartPanelBestScore()
    {
        startPanelBestScoreText.text = (bestScore + " M");
    }

    private void GameEndPanelSetActive()
    {
        gameEndPanel.SetActive(true);
    }

    private void GameEndPanelSetPassive()
    {
        gameEndPanel.SetActive(false);
    }

    private void GameEndPanelScoreText()
    {
        gameEndScoreText.text = "Score = " + (int) student.position.z / 2 + " meters";
        UpdateBestScore();
    }

    private void LevelScoreMetreTextSetActive()
    {
        levelScoreText.gameObject.SetActive(true);
    }

    private void LevelScoreMetreTextSetPassive()
    {
        levelScoreText.gameObject.SetActive(false);
    }


    private void LevelScoreMetreText()
    {
        if (levelScoreText.gameObject.activeInHierarchy)
            levelScoreText.text = (int) student.position.z / 2 + "M";
    }

    private void GameStartTimerTextSetActive(bool setActive)
    {
        gameStartTimerText.gameObject.SetActive(setActive);
        if (setActive)
            _isTimerStarted = true;
        else
            _timerTime = 3f;
    }

    private void GameStartTimerText(int time)
    {
        gameStartTimerText.text = time.ToString();
    }

    private void UpdateStartTimer()
    {
        if (!_stopTimer && _timerTime > -1f)
        {
            _timerTime -= Time.deltaTime * 1.9f;
            GameStartTimerText((int) _timerTime);

            if (_timerTime < -0.1f)
            {
                GameStartTimerTextSetActive(false);
                _stopTimer = true;
            }
        }
    }

    private void UpdateBestScore()
    {
        if (bestScore < (int) student.position.z / 2)
            bestScore = (int) student.position.z / 2;
    }

    public void ResetLevelButton()
    {
        _timerTime = 3f;
        _isTimerStarted = false;
        GameManager.ResetLevelDelegate();
        GameDataScript.SaveLevelDataAsJson();
        HomePanelSetActive();
        _stopTimer = false;
    }
}