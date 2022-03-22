using UnityEngine;

public class BorderSFX : MonoBehaviour
{
    public AudioClip ballToWallHit;
    public AudioClip batToWallHit;
    private AudioSource _source;

    private void OnEnable()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "bat") _source.PlayOneShot(batToWallHit);
        if (collision.collider.tag == "ball") _source.PlayOneShot(ballToWallHit);
    }
}
