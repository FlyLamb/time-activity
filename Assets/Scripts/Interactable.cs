using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Interactable : MonoBehaviour {
    public UnityEvent onInteract = new UnityEvent();
    public Action<Interactable> onInteractAction;
    public void Interact() {
        
        onInteractAction.Invoke(this);
        onInteract.Invoke();
        print("interact");
    }
}
