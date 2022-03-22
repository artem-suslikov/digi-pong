using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]

public class FieldPanel : MonoBehaviour
{
    public Material idleMaterial;
    public Material playerMaterial;
    public Material enemyMaterial;
    [Range(0.001f, 0.01f)]
    public float fadeSpeed = 0.005f;
    private Side _lastBat;
    private MeshRenderer _meshRend;

    private void OnEnable()
    {
        _meshRend = GetComponent<MeshRenderer>();
        Goal.GoalEvent += BatChange;
        Bat.HardHit += BatChange;
        Bat.LightHit += BatChange;
        Skill.SkillEvent += BatChange;
    }

    private void OnDisable()
    {
        Goal.GoalEvent -= BatChange;
        Bat.HardHit -= BatChange;
        Bat.LightHit -= BatChange;
        Skill.SkillEvent -= BatChange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_lastBat == Side.Player)
            _meshRend.material = playerMaterial;
        else
            _meshRend.material = enemyMaterial;
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine("Fade");
    }

    private void BatChange(Side s)
    {
        _lastBat = s;
    }

    private void BatChange(SkillName skill, Side s)
    {
        if (skill == SkillName.Reverse)
            _lastBat = s;
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
