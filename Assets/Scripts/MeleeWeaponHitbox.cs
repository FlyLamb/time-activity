using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponHitbox : MonoBehaviour
{

    protected List<Enemy> colliders = new List<Enemy>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
            colliders.Add(other.GetComponent<Enemy>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>())
            colliders.Remove(other.GetComponent<Enemy>());
    }

    public void Hit(float dmg, DamageType type = DamageType.Normal, float knockback = 1)
    {
        colliders.RemoveAll((e) => e == null); // remove destroyed objects from the list
        foreach (var w in colliders)
        {
            w.Hit(dmg, type);
            if (w.GetComponent<Rigidbody>()) w.GetComponent<Rigidbody>().AddForce(PlayerManager.Instance.controller.transform.forward * knockback, ForceMode.Impulse);
            colliders.Remove(w.GetComponent<Enemy>());
        }
    }
}