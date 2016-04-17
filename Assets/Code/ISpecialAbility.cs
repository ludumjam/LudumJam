using UnityEngine;
using System.Collections;

public interface ISpecialAbility
{
    void TriggerAbility();

    float CoolDown
    {
        get;
    }
}
