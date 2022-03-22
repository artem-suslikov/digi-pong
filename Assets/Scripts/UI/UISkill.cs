using UnityEngine;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
    public GameObject bat;
    public GameObject skillBar;
    public GameObject skillIcon;
    [Header("Skills icons")]
    public Sprite none;
    public Sprite slower;
    public Sprite faster;
    public Sprite expander;
    public Sprite squezer;
    public Sprite reverse;
    
    private RectTransform _skillBarRectTransform;
    private Image _skillImage;
    private Skill _skill;
    private float _barScaleCoef;

    private void Start() {
        _skillBarRectTransform = skillBar.GetComponent<RectTransform>();
        _skillImage = skillIcon.GetComponent<Image>();
        _skill = bat.GetComponent<Skill>();
    }

    private void Update() {
        _barScaleCoef = (float)_skill.currentPoints / (float)_skill.pointsThreshold;
        _skillBarRectTransform.localScale = new Vector3(1.0f, 1.0f * _barScaleCoef, 1.0f);
        switch (_skill.currentSkill)
        {
            case SkillName.Slower: _skillImage.sprite = slower; break;
            case SkillName.Faster: _skillImage.sprite = faster; break;
            case SkillName.Expander: _skillImage.sprite = expander; break;
            case SkillName.Squeezer: _skillImage.sprite = squezer; break;
            case SkillName.Reverse: _skillImage.sprite = reverse; break;
            default: _skillImage.sprite = none; break;
        }
    }
}
