using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]

public class GoalRenderer : MonoBehaviour
{
    public Material idleMaterial;
    public Material sideMaterial;
    [Range(0.001f, 0.01f)]
    public float fadeSpeed = 0.005f;
    private MeshRenderer _meshRend;


    private void OnEnable()
    {
        _meshRend = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _meshRend.material = sideMaterial;
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        for (float i = 0f; i <= 1; i += fadeSpeed)
        {
            _meshRend.material.Lerp(_meshRend.material, idleMaterial, i);
            yield return null;
        }
    }
}
