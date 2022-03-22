using UnityEngine;

public class BallSFX : MonoBehaviour
{
    public AudioClip ballToBatLightHit;
    public AudioClip ballToBatHardHit;
    public AudioClip ballInstantiation;
    private AudioSource _source;

    private void OnEnable()
    {
        Bat.LightHit += PlayLightHit;
        Bat.HardHit += PlayHardHit;
        _source = GetComponent<AudioSource>();
        _source.PlayOneShot(ballInstantiation);
    }

    private void OnDisable()
    {
        Bat.LightHit -= PlayLightHit;
        Bat.HardHit -= PlayHardHit;
    }

    private void PlayLightHit(Side s)
    {
        _source.PlayOneShot(ballToBatLightHit);
    }

    private void PlayHardHit(Side s)
    {
        _source.PlayOneShot(ballToBatHardHit);
    }
}
