using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveManager : MonoBehaviour {

    public static WaveManager Instance {
        get {
            if(instance == null)
                instance = GameObject.FindObjectOfType<WaveManager>();
            return instance;
        }
    }

    private static WaveManager instance;


    [System.Serializable]
    public class Wave {
        public List<GameObject> enemies;
    }

    public int waveNum = 0;
    
    [SerializeField]
    private List<Transform> spawnPoints;
    private List<int> usedSpawns = new List<int>();

    public List<Wave> waves;

    private List<GameObject> enemiesAlive = new List<GameObject>();

[SerializeField]
    private Billboard[] billboards;

    [ContextMenu("add children")]
    public void Children() {
        spawnPoints = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints.Add(transform.GetChild(i));
            transform.GetChild(i).gameObject.name = "" + i;
        }
    }

    public Vector3 GetRandomSpawn() {
        int r = Random.Range(0,spawnPoints.Count);
        while(usedSpawns.Contains(r) && usedSpawns.Count != spawnPoints.Count) {
            r = Random.Range(0,spawnPoints.Count);
        }
        usedSpawns.Add(r);
        return spawnPoints[r].position;
    }

    public void UnregisterEnemy(GameObject e) {
        if(enemiesAlive.Contains(e))
        enemiesAlive[enemiesAlive.IndexOf(e)] = null; // the cleanup will deal with it, this way is less likely to cause glitches
    }

    public void RegisterEnemy(GameObject e) {
        if(enemiesAlive.Contains(e)) return;
        enemiesAlive.Add(e);
    }

    private void Start() {
        Display(@"Interact to start wave.
Come here, little one",true);
        foreach (var item in billboards) {
            item.gameObject.AddComponent<Interactable>().onInteractAction += Interact;
        }
    }

    public void Interact(Interactable i) {
        if(enemiesAlive.Count <= 0) SpawnWave();
    }

    

[ContextMenu("Spawn Wave")]
    public void SpawnWave() {
        MusicManager.Instance.StartWave();
        usedSpawns.Clear();
        enemiesAlive.Clear();
        foreach (var item in waves[waveNum].enemies) {
            enemiesAlive.Add(Instantiate(item, GetRandomSpawn(), Quaternion.identity));
        }
        UIManager.Instance.AnnounceNewWave(waveNum, enemiesAlive.Count);
    }

    private void WaveFinished() {
        print("Wave finished");
        Display(@"Wave complete. 
Interact to continue", true);
        waveNum++;
        MusicManager.Instance.StopWave();
        UIManager.Instance.Announce("Wave finished!");
    }

    private void FixedUpdate() {
        if(enemiesAlive.Count > 0)
            CleanupList();
        
    }

    private void CleanupList() {
        enemiesAlive.RemoveAll((w)=>w==null);
        Display($"<size=72>Wave {waveNum}</size><br>{enemiesAlive.Count} remaining");
        if(enemiesAlive.Count <= 0) WaveFinished(); 

    }

    private void Display(string txt, bool scr = false) {
        foreach (var item in billboards) {
            item.SetText(txt,scr);
        }
    }
}