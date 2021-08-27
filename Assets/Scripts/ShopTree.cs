using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ShopTree : MonoBehaviour {
    [Serializable]
    public struct Item {
        public Weapon weapon;
        public int price;
    }

[Serializable]
    public class Column {
        public Sprite icon;
        public List<Item> weapons; 
    }

    public List<Column> tree;

    public float spacing;
    public Vector3 startPos;

    public GameObject treeElement;

    private List<GameObject> existing = new List<GameObject>();

    private void Start() {
        Refresh();
    }

    public void Refresh() {
        foreach (var item in existing)
        {
            Destroy(item);
        }
        existing.Clear();


        for(int i = 0; i < tree.Count; i++) {
            var a = Instantiate(treeElement, Vector3.zero, Quaternion.identity, transform);
            a.GetComponent<ShopTreeElement>().SetIcon(tree[i].icon);
            a.GetComponent<RectTransform>().localPosition = new Vector3(i * spacing, 0) + startPos;
            existing.Add(a);
            for(int j = 0; j < tree[i].weapons.Count; j++) {
                var w = Instantiate(treeElement, Vector3.zero, Quaternion.identity, transform);
                w.GetComponent<RectTransform>().localPosition = new Vector3(i * spacing, (-j-1)*spacing) + startPos;
                w.GetComponent<ShopTreeElement>().Set(tree[i].weapons[j].price, tree[i].weapons[j].weapon);
                existing.Add(w);
            }
        }
    }
}
