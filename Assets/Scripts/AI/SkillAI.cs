using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Skill))]
[RequireComponent(typeof(Bat))]

public class SkillAI : MonoBehaviour
{
    private Skill _skill;
    private Bat _bat;
    private Side _side;

    private void OnEnable()
    {
        Bat.HardHit += UseSkill;
        Bat.LightHit += UseSkill;
    }

    private void OnDisable()
    {
        Bat.HardHit -= UseSkill;
        Bat.LightHit -= UseSkill;
    }

    private void Start()
    {
        _skill = GetComponent<Skill>();
        _bat = GetComponent<Bat>();
        _side = _bat.side;
    }

    private void UseSkill(Side s)
    {
        if (_skill.skillExists)
        {
            switch (_skill.currentSkill)
            {
                case SkillName.Slower: if (s != _side)
                        StartCoroutine("ApplySkill"); break;
                case SkillName.Faster: if (s == _side)
                        StartCoroutine("ApplySkill"); break;
                case SkillName.Expander: if (s != _side)
                        StartCoroutine("ApplySkill"); break;
                case SkillName.Squeezer: if (s == _side)
                        StartCoroutine("ApplySkill"); break;
                case SkillName.Reverse: if (s != _side)
                        StartCoroutine("ApplySkill"); break;
                default: break;
            }
        }
    }

    private IEnumerator ApplySkill()
    {
        yield return new WaitForSeconds(1.0f);
        _skill.UseSkill();
    }
}
