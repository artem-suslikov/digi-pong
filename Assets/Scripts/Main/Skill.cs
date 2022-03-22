using UnityEngine;

public class Skill : MonoBehaviour
{
    public static event SkillAction SkillEvent;
    public static event Action SkillActivation;

    public bool aI;
    public Side side;
    public int pointsThreshold;
    public int lowPoints;
    public int highPoints;
    [Range(0, 5)]
    public int playerLevel;
    [HideInInspector]
    public int currentPoints;
    [HideInInspector]
    public SkillName currentSkill;
    [HideInInspector]
    public bool skillExists;

    private void OnEnable()
    {
        skillExists = false;
        Bat.HardHit += HighPoints;
        Bat.LightHit += LowPoints;
    }

    private void OnDisable()
    {
        Bat.HardHit -= HighPoints;
        Bat.LightHit -= LowPoints;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateSkill();
        }
    }

    public void ActivateSkill()
    {
        if (skillExists && !aI)
        {
            UseSkill();
        }
    }

    private void LowPoints(Side s)
    {
        if (s == side && !skillExists)
        {
            currentPoints += lowPoints;
            if (currentPoints >= pointsThreshold)
            {
                currentPoints = 0;
                GenerateSkill();
            }
        }
    }

    private void HighPoints(Side s)
    {
        if (s == side && !skillExists)
        {
            currentPoints += highPoints;
            if (currentPoints >= pointsThreshold)
            {
                currentPoints = 0;
                GenerateSkill();
            }
        }
    }

    private void GenerateSkill()
    {
        if (playerLevel == 0)
            currentSkill = SkillName.None;
        else
            currentSkill = (SkillName)Random.Range(1, playerLevel + 1);
        skillExists = true;
        SkillActivation?.Invoke();
    }

    public void UseSkill()
    {
        SkillEvent?.Invoke(currentSkill, side);
        skillExists = false;
        currentSkill = SkillName.None;
    }
}
