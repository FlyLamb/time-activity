using UnityEngine;
using ElRaccoone.Tweens;

public class Blueprint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            GameManager.instance.NextArena();
        }
    }
}
