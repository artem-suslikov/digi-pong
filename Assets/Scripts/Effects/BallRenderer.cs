using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Ball))]

public class BallRenderer : MonoBehaviour
{
    public Material playerMaterial;
    public Material enemyMaterial;
    private MeshRenderer _meshRend;
    private Ball _ball;
    private Side _lastBat;
    private Side _currentSide;

    private void OnEnable()
    {
        _meshRend = GetComponent<MeshRenderer>();
        _ball = GetComponent<Ball>();
        Goal.GoalEvent += BatChange;
        Bat.HardHit += BatChange;
        Bat.LightHit += BatChange;
        Skill.SkillEvent += BatChange;

        _lastBat = _ball.side;
        ChangeMaterial();
    }

    private void OnDisable()
    {
        Goal.GoalEvent -= BatChange;
        Bat.HardHit -= BatChange;
        Bat.LightHit -= BatChange;
        Skill.SkillEvent -= BatChange;
    }

    private void Update()
    {
        if (_lastBat != _currentSide)
        {
            ChangeMaterial();
            _currentSide = _lastBat;
        }
    }

    private void ChangeMaterial()
    {
        if (_lastBat == Side.Player)
            _meshRend.material = playerMaterial;
        else
            _meshRend.material = enemyMaterial;
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
}
