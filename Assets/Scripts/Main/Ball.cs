using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class Ball : MonoBehaviour
{
    public Side side;
    public float startSpeed;
    public float speedStep;
    public float maxSpeed;
    public float burstSpeed;
    private bool _burstActive;
    [HideInInspector]
    public float currentSpeed;
    private Vector3 _direction;
    private Rigidbody _rbody;

    private void OnEnable()
    {
        Skill.SkillEvent += ApplySkill;
        _burstActive = false;
        _rbody = GetComponent<Rigidbody>();
        StartCoroutine(BallThrow());
    }

    private void OnDisable()
    {
        Skill.SkillEvent -= ApplySkill;
    }

    private void FixedUpdate()
    {
        _direction = _rbody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "bat")
        {
            BurstStop();
            SpeedUp();
            if (collision.gameObject.GetComponent<Bat>().highSpeed)
                BurstStart();
        }
        BallReflect(collision.GetContact(0).normal);
    }

    private void BallReflect(Vector3 collisionNormal)
    {
        _direction = Vector3.Reflect(_direction, collisionNormal);
        BallMove();
    }

    private IEnumerator BallThrow()
    {
        yield return new WaitForSeconds(2);
        currentSpeed = startSpeed;
        if (side == Side.Player)
            _direction = Quaternion.AngleAxis(Random.Range(225, 315), 
                Vector3.up) * Vector3.forward;
        else
            _direction = Quaternion.AngleAxis(Random.Range(45, 135),
                Vector3.up) * Vector3.forward;
        BallMove();
    }

    private void BallMove()
    {
        _direction.Normalize();
        _rbody.velocity = _direction * currentSpeed;
    }

    private void SpeedUp()
    {
        currentSpeed += speedStep;
        if (currentSpeed >= maxSpeed) currentSpeed = maxSpeed;
    }

    private void BurstStart()
    {
        _burstActive = true;
        currentSpeed += burstSpeed;
    }

    private void BurstStop()
    {
        if (_burstActive)
            currentSpeed -= burstSpeed;
        _burstActive = false;
    }

    private void ApplySkill(SkillName skillName, Side s)
    {
        switch (skillName)
        {
            case SkillName.Slower: StartCoroutine("SlowBall"); break;
            case SkillName.Faster: StartCoroutine("FastBall"); break;
            case SkillName.Reverse: ReverseBall(); break;
            default: break;
        }
    }

    private IEnumerator SlowBall()
    {
        currentSpeed -= 5f;
        BallMove();
        yield return new WaitForSeconds(3);
        currentSpeed += 5f;
        BallMove();
    }

    private IEnumerator FastBall()
    {
        currentSpeed += 10f;
        maxSpeed += 10f;
        BallMove();
        yield return new WaitForSeconds(3);
        currentSpeed -= 10f;
        maxSpeed -= 10f;
        BallMove();
    }

    private void ReverseBall()
    {
        _direction = -_direction;
        BallMove();
    }
}
