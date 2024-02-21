using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponHitbox : MonoBehaviour {

    [SerializeField] protected List<Enemy> m_enemyColliders = new List<Enemy>();

    private void OnTriggerEnter(Collider other) {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null && !m_enemyColliders.Contains(enemy))
            m_enemyColliders.Add(enemy);
    }

    private void OnTriggerStay(Collider other) {
        var enemy = other.GetComponent<Enemy>(); //laggy!
        if (enemy != null && !m_enemyColliders.Contains(enemy))
            m_enemyColliders.Add(enemy);
    }

    private void OnTriggerExit(Collider other) {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null && m_enemyColliders.Contains(enemy))
            m_enemyColliders.Remove(other.GetComponent<Enemy>());
    }

    public void Hit(float dmg, Vector3 knockback, DamageType type = DamageType.Normal) {
        m_enemyColliders.RemoveAll((e) => e == null); // remove destroyed objects from the list
        foreach (var w in m_enemyColliders) {
            w.Hit(dmg, type);
            var rb = w.GetComponent<Rigidbody>();
            if (rb != null) rb.AddForce(knockback, ForceMode.Impulse);
        }
    }
}