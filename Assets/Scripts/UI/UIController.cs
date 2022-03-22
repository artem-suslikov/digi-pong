using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static event Action MenuEnter;
    public static event Action MenuExit;

    private GameController _gController;
    public GameObject menuUI;
    public GameObject gameUI;
    public Text endMessage;

    private void OnEnable()
    {
        endMessage.text = "";
        GameController.End += ShowEndMessage;
        _gController = GetComponent<GameController>();
        menuUI.SetActive(true);
        gameUI.SetActive(false);
    }

    private void OnDisable()
    {
        GameController.End -= ShowEndMessage;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuUI.activeSelf) PlayButtonClick();
            else PauseButtonClick();
        }
    }

    public void PlayButtonClick()
    {
        if (_gController.gameState == GameState.Started) ResumeGame();
        if (_gController.gameState == GameState.Ended) RestartGame();
        if (_gController.gameState == GameState.None) StartGame();
    }

    public void RestartButtonClick()
    {
        if (_gController.gameState == GameState.Started) RestartGame();
        if (_gController.gameState == GameState.Ended) RestartGame();
    }

    public void ExitButtonClick()
    {
        ExitGame();
    }

    public void PauseButtonClick()
    {
        PauseGame();
    }

    private void StartGame()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        _gController.StartGame();
        MenuExit?.Invoke();
    }

    private void PauseGame()
    {
        menuUI.SetActive(true);
        gameUI.SetActive(false);
        _gController.PauseGame();
        MenuEnter?.Invoke();
    }

    private void ResumeGame()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        _gController.ResumeGame();
        MenuExit?.Invoke();
        
    }

    private void RestartGame()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        _gController.RestartGame();
        MenuExit?.Invoke();
    }

    private void ExitGame()
    {
        _gController.ExitGame();
        MenuEnter?.Invoke();
    }

    private void ShowEndMessage(Side s)
    {
        if(s == Side.Player) endMessage.text = "YOU WIN!";
        if(s == Side.Enemy) endMessage.text = "YOU LOSE!";
    }
}
