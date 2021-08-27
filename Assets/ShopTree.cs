using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    private void Start() {
        
        for(int i = 0; i < tree.Count; i++) {
            for(int j = 0; j < tree[i].weapons.Count; j++) {
                var w = Instantiate(treeElement, Vector3.zero, Quaternion.identity, transform);
                w.GetComponent<RectTransform>().localPosition = new Vector3(i * spacing, -j*spacing) + startPos;
                w.GetComponent<ShopTreeElement>().Set(tree[i].weapons[j].price, tree[i].weapons[j].weapon.icon);
            }
        }
    }
}
