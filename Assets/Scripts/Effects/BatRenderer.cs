using UnityEngine;

[RequireComponent(typeof(Bat))]
[RequireComponent(typeof(MeshRenderer))]

public class BatRenderer : MonoBehaviour
{
    public Material lightMaterial;
    public Material hardMaterial;
    private Bat _bat;
    private MeshRenderer _meshRend;

    private void OnEnable()
    {
        _bat = GetComponent<Bat>();
        _meshRend = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (_bat.highSpeed)
            _meshRend.material = hardMaterial;
        else
            _meshRend.material = lightMaterial;
    }
}
