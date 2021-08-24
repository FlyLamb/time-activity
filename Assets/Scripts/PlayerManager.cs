using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private static PlayerManager instance;
    public static PlayerManager Instance {
        get {
            if(instance == null) {
                instance = GameObject.FindObjectOfType<PlayerManager>();
            }
            return instance;
        }
    }

    public BajtixPlayerController controller;
    public new PlayerCamera camera;

}
