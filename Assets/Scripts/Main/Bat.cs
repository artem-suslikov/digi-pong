using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BatController))]

public class Bat : MonoBehaviour
{
    public static event SideAction HardHit;
    public static event SideAction LightHit;

    public Side side;
    [Range(0.1f, 3.0f)]
    public float minSpeed;
    [Range(3.1f, 5.0f)]
    public float maxSpeed;
    [Range(0.01f, 0.1f)]
    public float speedStep;
    [Range(1.0f, 5.0f)]
    public float highSpeedThreshold;
    private float _speed;
    private float previousVelocityMagnitude;
    private Rigidbody _rbody;
    private BatController _batContr;
    [HideInInspector]
    public bool highSpeed;
    private InputMode _inputMode;

    private void OnEnable()
    {
        Skill.SkillEvent += ApplySkill;
        highSpeed = false;
        _rbody = GetComponent<Rigidbody>();
        _batContr = GetComponent<BatController>();
        _inputMode = _batContr.inputMode;
        _speed = minSpeed;
    }

    private void OnDisable()
    {
        Skill.SkillEvent -= ApplySkill;
    }

    private void FixedUpdate()
    {
        if (_inputMode == InputMode.Simple) SimpleMove(_batContr.horInput);
        if (_inputMode == InputMode.Accelerometer) AccelerometerMove(_batContr.horInput);
    }

    private void SimpleMove(float input)
    {
        _rbody.AddForce(input * Vector3.forward * _speed, ForceMode.Impulse);

        if (_rbody.velocity.magnitude < previousVelocityMagnitude) _speed = minSpeed;
        else if (_rbody.velocity.magnitude > previousVelocityMagnitude) _speed += speedStep;
        if (_speed >= maxSpeed) _speed = maxSpeed;
        previousVelocityMagnitude = _rbody.velocity.magnitude;

        if (_speed > highSpeedThreshold) highSpeed = true;
        else highSpeed = false;
    }

    private void AccelerometerMove(float input)
    {
        _rbody.AddForce(input * Vector3.forward * maxSpeed, ForceMode.Impulse);

        if (Mathf.Abs(input * maxSpeed) > highSpeedThreshold) highSpeed = true;
        else highSpeed = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "ball")
        {
            if (_speed > highSpeedThreshold) HardHit?.Invoke(side);
            else LightHit?.Invoke(side);
        }
    }

    private void ApplySkill(SkillName skillName, Side s)
    {
        if (skillName == SkillName.Expander && s == side)
            StartCoroutine("ExpandBat");
        if (skillName == SkillName.Squeezer && s != side)
            StartCoroutine("SqueezeBat");
    }

    private IEnumerator ExpandBat()
    {
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y, transform.localScale.z * 2);
        yield return new WaitForSeconds(3);
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y, transform.localScale.z / 2);
    }

    private IEnumerator SqueezeBat()
    {
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y, transform.localScale.z / 2);
        yield return new WaitForSeconds(3);
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y, transform.localScale.z * 2);
    }
}
