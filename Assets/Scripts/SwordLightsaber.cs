using UnityEngine;
using UnityEngine.Serialization;

public class SwordLightsaber : WeaponSword
{
    public override void Fire1()
    {
        base.Fire1();
        Attack();

    }
}