using UnityEngine;

[RequireComponent(typeof(Ball))]

public class BallParticleSystem : MonoBehaviour
{
    public ParticleSystem tracePS;
    public ParticleSystem collisionSplashPS;
    public GameObject goalSplash;
    public ParticleSystem instCircleSplashPS;
    public ParticleSystem instSphereSplashPS;
    public ParticleSystem skillParticleSplashPS;

    private Ball _ball;
    private ParticleSystem.EmissionModule _traceEmission;

    private void OnEnable()
    {
        _ball = GetComponent<Ball>();
        _traceEmission = tracePS.emission;
        Goal.GoalEvent += GoalSplash;
        Skill.SkillEvent += SkillSplash;
    }

    private void OnDisable()
    {
        Goal.GoalEvent -= GoalSplash;
        Skill.SkillEvent -= SkillSplash;
    }

    private void Update()
    {
        _traceEmission.rateOverDistance = _ball.currentSpeed * 0.1f;
    }

    private void GoalSplash(Side s)
    {
        Instantiate(goalSplash, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void SkillSplash(SkillName sName, Side s)
    {
        if (sName == SkillName.Faster || sName == SkillName.Slower || sName == SkillName.Reverse)
        {
            collisionSplashPS.Play();
            skillParticleSplashPS.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionSplashPS.Play();
    }
}
