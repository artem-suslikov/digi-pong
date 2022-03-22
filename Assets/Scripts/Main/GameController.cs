using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static event SideAction End; 

    public GameObject playerBall;
    public GameObject enemyBall;
    private GameObject _ball;
    public int lifePoints;
    [HideInInspector]
    public int playerLP, enemyLP;
    [HideInInspector]
    public GameState gameState;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        gameState = GameState.None;
        Goal.GoalEvent += Score;
        playerLP = lifePoints;
        enemyLP = lifePoints;
    }

    private void OnDisable()
    {
        Goal.GoalEvent -= Score;
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        gameState = GameState.Started;
        StartCoroutine("BallToGame", Side.Player);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator BallToGame(Side s)
    {
        yield return new WaitForSeconds(1.5f);
        if (s == Side.Enemy) _ball = Instantiate(enemyBall);
        if (s == Side.Player) _ball = Instantiate(playerBall);
    }

    private void Score(Side s)
    {
        if (s == Side.Player)
        {
            playerLP--;
            if (playerLP > 0) StartCoroutine("BallToGame", Side.Player);
            else StartCoroutine("ChooseWinner", Side.Enemy);
        }
        if (s == Side.Enemy)
        {
            enemyLP--;
            if (enemyLP > 0) StartCoroutine("BallToGame", Side.Enemy);
            else StartCoroutine("ChooseWinner", Side.Player);
        }
    }

    private IEnumerator ChooseWinner(Side s)
    {
        End?.Invoke(s);
        gameState = GameState.Ended;
        yield return new WaitForSeconds(3.0f);
        Time.timeScale = 0f;
    }
}
