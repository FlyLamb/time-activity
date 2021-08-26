using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Interactable : MonoBehaviour {
    public UnityEvent onInteract;
    public Action<Interactable> onInteractAction;
    public void Interact() {
        onInteract.Invoke();
        onInteractAction.Invoke(this);
        print("interact");
    }
}
