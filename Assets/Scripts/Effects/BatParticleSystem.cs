using UnityEngine;

[RequireComponent(typeof(Bat))]

public class BatParticleSystem : MonoBehaviour
{
    public ParticleSystem tracePS;
    public GameObject borderSplash;
    public ParticleSystem skillCircleSplashPS;

    private Bat _bat;
    private ParticleSystem.EmissionModule _traceEmission;

    private void OnEnable()
    {
        _bat = GetComponent<Bat>();
        _traceEmission = tracePS.emission;
        Skill.SkillEvent += SkillSplash;
    }

    private void OnDisable()
    {
        Skill.SkillEvent -= SkillSplash;
    }

    private void Update()
    {
        if (_bat.highSpeed)
            tracePS.Play();
        else
            tracePS.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "border")
            Instantiate(borderSplash, collision.GetContact(0).point, Quaternion.identity);
    }

    private void SkillSplash(SkillName sName, Side s)
    {
        if (sName == SkillName.Expander && _bat.side == s)
            skillCircleSplashPS.Play();
        if (sName == SkillName.Squeezer && _bat.side != s)
            skillCircleSplashPS.Play();
    }
}
