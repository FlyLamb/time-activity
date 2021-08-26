using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveManager : MonoBehaviour {

    [System.Serializable]
    public class Wave {
        public List<GameObject> enemies;
    }

    public int waveNum = 0;
    
    public Transform[] spawnPoints;
    private List<int> usedSpawns = new List<int>();

    public List<Wave> waves;

    private List<GameObject> enemiesAlive = new List<GameObject>();

    public Billboard[] billboards;

    public Vector3 GetRandomSpawn() {
        int r = 0;
        while(usedSpawns.Contains(r) && usedSpawns.Count != spawnPoints.Length) {
            r = Random.Range(0,spawnPoints.Length);
        }
        usedSpawns.Add(r);
        return spawnPoints[r].position;
    }

    private void Start() {
        Display("Interact to start wave.");
        foreach (var item in billboards)
        {
            item.gameObject.AddComponent<Interactable>().onInteractAction = Interact;
        }
    }

    public void Interact(Interactable i) {
        if(enemiesAlive.Count <= 0) SpawnWave();
    }

    

[ContextMenu("Spawn Wave")]
    public void SpawnWave() {
        usedSpawns.Clear();
        enemiesAlive.Clear();
        foreach (var item in waves[waveNum].enemies) {
            enemiesAlive.Add(Instantiate(item, GetRandomSpawn(), Quaternion.identity));
        }
    }

    private void WaveFinished() {
        print("Wave finished");
        Display("Wave complete. Interact to continue");
        waveNum++;
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

    private void Display(string txt) {
        foreach (var item in billboards) {
            item.SetText(txt);
        }
    }
}