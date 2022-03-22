using UnityEngine;

public class GameControllerSFX : MonoBehaviour
{
    public AudioClip goalHit;
    public AudioClip skillEnable;
    public AudioClip skillSlower;
    public AudioClip skillFaster;
    public AudioClip skillExpander;
    public AudioClip skillSqueezer;
    public AudioClip skillReverse;
    public AudioClip menuEnter;
    public AudioClip menuExit;
    public AudioClip playerWin;
    public AudioClip playerLose;
    private AudioSource _source;

    private void OnEnable()
    {
        _source = GetComponent<AudioSource>();
        Goal.GoalEvent += PlayGoalHit;
        Skill.SkillActivation += PlaySkillEnable;
        Skill.SkillEvent += PlaySkill;
        UIController.MenuEnter += PlayMenuEnter;
        UIController.MenuExit += PlayMenuExit;
        GameController.End += PlayEnd;
    }

    private void OnDisable()
    {
        Goal.GoalEvent -= PlayGoalHit;
        Skill.SkillActivation -= PlaySkillEnable;
        Skill.SkillEvent -= PlaySkill;
        UIController.MenuEnter -= PlayMenuEnter;
        UIController.MenuExit -= PlayMenuExit;
        GameController.End -= PlayEnd;
    }

    private void PlayGoalHit(Side s)
    {
        _source.PlayOneShot(goalHit);
    }

    private void PlaySkillEnable()
    {
        _source.PlayOneShot(skillEnable);
    }

    private void PlaySkill(SkillName skillName, Side s)
    {
        switch (skillName)
        {
            case SkillName.Slower: _source.PlayOneShot(skillSlower); break;
            case SkillName.Faster: _source.PlayOneShot(skillFaster); break;
            case SkillName.Expander: _source.PlayOneShot(skillExpander); break;
            case SkillName.Squeezer: _source.PlayOneShot(skillSqueezer); break;
            case SkillName.Reverse: _source.PlayOneShot(skillReverse); break;
        }
    }

    private void PlayMenuEnter()
    {
        _source.PlayOneShot(menuEnter);
    }

    private void PlayMenuExit()
    {
        _source.PlayOneShot(menuExit);
    }

    private void PlayEnd(Side s)
    {
        switch (s)
        {
            case Side.Player: _source.PlayOneShot(playerWin); break;
            case Side.Enemy: _source.PlayOneShot(playerLose); break;
        }
    }
}
