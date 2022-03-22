using UnityEngine;
using System.Collections.Generic;

public class UILifePoints : MonoBehaviour
{
    public GameObject gameController;
    public GameObject playerLP;
    public GameObject enemyLP;
    public GameObject playerLifePrefab;
    public GameObject enemyLifePrefab;

    private GameController _gameController;
    private Stack<GameObject> _playerLPIcons = new Stack<GameObject>();
    private Stack<GameObject> _enemyLPIcons = new Stack<GameObject>();

    private void OnEnable()
    {
        Goal.GoalEvent += RemoveLifePoint;
    }

    private void OnDisable()
    {
        Goal.GoalEvent -= RemoveLifePoint;
    }

    private void Start()
    {
        _gameController = gameController.GetComponent<GameController>();

        for (int i = 0; i < _gameController.playerLP; i++)
        {
            _playerLPIcons.Push(Instantiate(playerLifePrefab, new Vector3(playerLP.transform.position.x + i * 32,
                playerLP.transform.position.y, playerLP.transform.position.z), 
                Quaternion.identity, playerLP.transform));
        }
        
        for (int i = 0; i < _gameController.enemyLP; i++)
        {
            _enemyLPIcons.Push(Instantiate(enemyLifePrefab, new Vector3(enemyLP.transform.position.x - i * 32,
                enemyLP.transform.position.y, enemyLP.transform.position.z), 
                Quaternion.identity, enemyLP.transform));
        }
    }

    private void RemoveLifePoint (Side s)
    {
        if (s == Side.Player) Destroy(_playerLPIcons.Pop());
        if (s == Side.Enemy) Destroy(_enemyLPIcons.Pop());
    }
}
