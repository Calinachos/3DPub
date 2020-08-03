using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public PlayerStats ps;
    public int points;

    [SerializeField]
    private Skills[] skills;

    [SerializeField]
    private Skills[] unclockedDefault;

    [SerializeField]
    private Text talentPointText;
    // Start is called before the first frame update
    
    public int MyPoints
    {
        get
        {
            return points;
        }

        set
        {
            points = value;
            UpdateTalentPointText();
        }
    }
    
    void Start()
    {
        //points = ps.skillPoints;
        ResetTalents(); 
    }

    public void TryUseSkill(Skills skill)
    {
        if (MyPoints > 0 && skill.Click())
        {
            MyPoints--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTalentPointText();
    }

    private void ResetTalents()
    {
        UpdateTalentPointText();

        foreach (Skills skill in skills)
        {
            skill.Lock();
        }

        foreach (Skills skill in unclockedDefault)
        {
            skill.Unlock();
        }
    }

    private void UpdateTalentPointText()
    {
        talentPointText.text = points.ToString();
    }
}
