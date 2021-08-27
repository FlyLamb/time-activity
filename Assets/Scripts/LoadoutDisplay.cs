using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutDisplay : MonoBehaviour {

    public GameObject displayItem;

    public float offset = 160;

    private List<GameObject> existing = new List<GameObject>();

    private void Start() {
        Refresh();
    }

    public void Refresh() {
        List<Weapon> weapons = WeaponManager.Instance.weapons;
        if(existing != null) foreach (var item in existing) {
            Destroy(item);
        }
        existing.Clear();
        for(int i = 0; i < weapons.Count; i++) {
            var a = Instantiate(displayItem, Vector3.forward, Quaternion.identity, transform);
            a.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-(i+0.5f) * offset);
            a.GetComponent<LoadoutDisplayElement>().Load(weapons[i]);
            existing.Add(a);
        }

        WeaponManager.Instance.Select(0);
    }

    

    
}
