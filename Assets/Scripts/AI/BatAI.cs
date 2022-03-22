using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BallRaycast))]

public class BatAI : MonoBehaviour
{
    private GameObject _ball;
    private Rigidbody _rbody;
    private BallRaycast _bRaycast;
    [HideInInspector]
    public float moveDirection;
    public float moveAccuracy;

    private void Start()
    {
        _bRaycast = GetComponent<BallRaycast>();
        StartCoroutine("BallSearch");
    }

    private void OnEnable()
    {
        Goal.GoalEvent += NextBallSearch;
    }

    private void OnDisable()
    {
        Goal.GoalEvent -= NextBallSearch;
    }

    private void Update()
    {
        if (_bRaycast.hit.point.z < transform.position.z + moveAccuracy
            && _bRaycast.hit.point.z > transform.position.z - moveAccuracy)
            moveDirection = 0;
        else
        {
            if (_bRaycast.hit.point.z > transform.position.z)
                moveDirection = 1.0f;
            if (_bRaycast.hit.point.z < transform.position.z)
                moveDirection = -1.0f;
        }
    }

    private void NextBallSearch(Side s)
    {
        StartCoroutine("BallSearch");
    }

    private IEnumerator BallSearch()
    {
        do
        {
            _ball = GameObject.FindGameObjectWithTag("ball");
            yield return new WaitForSecondsRealtime(1);
        }
        while (_ball == null);
        _rbody = _ball.GetComponent<Rigidbody>();
        _bRaycast.rbody = _rbody;
    }
}
