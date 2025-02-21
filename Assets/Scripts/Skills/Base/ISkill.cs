using System;
using UnityEngine;
public abstract class Skill
{

    public abstract void ActivateSkill();
    public abstract void LevelUpSkill();
}

[Serializable]
public abstract class ActiveSkill : Skill
{

}

[Serializable]
public abstract class PassiveSkill : Skill
{

}