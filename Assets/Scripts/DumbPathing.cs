using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

// ill be fucking surprised if this doesnt crash all the time
// ~ baja 26.08.21

public class DumbPathing : MonoBehaviour { 

    public static DumbPathing instance;

    private void Awake() {
        if(instance != this && instance != null)
            Destroy(instance);
        instance = this;
    }



    [SerializeField] private List<Node> nodes;
    [SerializeField] private float[] distances;
    [SerializeField] private int[] parents;
    [SerializeField] public LayerMask mask;
    
    public List<Transform> makeIntoNodes;

    private List<int> unvisited = new List<int>();
    private List<int> visited = new List<int>();


    private List<int> currentPath;


[Serializable]
    public class Node {
        public int myId;
        public List<int> connectedNodes;
        public Vector3 position;
        public float myDst;

        public List<float> weights;

        public Node(int myId, Vector3 position) {
            this.myId = myId;
            this.position = position;
            connectedNodes = new List<int>();
            weights = new List<float>();
        }

        public void Weight(List<Node> nodegraph) {
            for (int i = 0; i < connectedNodes.Count; i++)
            {
                weights.Add(Vector3.Distance(position, nodegraph[connectedNodes[i]].position));
            }
            weights.Add(0);
        }
    }

    [ContextMenu("add children")]
    public void Children() {
        makeIntoNodes = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            makeIntoNodes.Add(transform.GetChild(i));
            transform.GetChild(i).gameObject.name = "" + i;
        }
    }

[ContextMenu("bake")]
    public void MakeNavMesh() {
        if(makeIntoNodes.Count <= 0) Children();
        if(makeIntoNodes.Count <= 0 ) return;
        print($"Creating Dijkstra Nav Mesh from {makeIntoNodes.Count}");
        nodes = new List<Node>(makeIntoNodes.Count);
        distances = new float[makeIntoNodes.Count];
        parents = new int[makeIntoNodes.Count];

        for(int i = 0; i < makeIntoNodes.Count; i++) nodes.Add(new Node(i, makeIntoNodes[i].position));

        for(int i = 0; i < nodes.Count; i++) {
            for(int j = 0; j < nodes.Count; j++)
            {
                if(j!=i) {
                    var p1 = nodes[i].position;
                    var p2 = nodes[j].position;
                    if(!Physics.Linecast(p1,p2, mask)) {// check if path traversable 
                        nodes[i].connectedNodes.Add(nodes[j].myId);
                        //Debug.DrawLine(p1,p2, Color.blue, 10);
                    }
                }
            }
            nodes[i].Weight(nodes);
        }
    }

    private void Start() {
        if(nodes.Count == 0)
            MakeNavMesh();
    }

    private int NearestNode(Vector3 p) { // implement chunking for performance
        int c = -1;
        float d = Mathf.Infinity;
        for (int i = 0; i < nodes.Count; i++)
        {
            float dst = Vector3.Distance(nodes[i].position, p);
            if(dst < d) { // check raycast
                c = i;
                d = dst;
            }
        }
        
        return c;
    }

    public List<Vector3> GetPath(Vector3 position, Vector3 targetPosition) { // buffer the path from the two points for the future. Will use memory, may increase performance
        int nn = NearestNode(position);
        int tt = NearestNode(targetPosition); // note that an octree would be wayy more optimal but we dont have time for that now

        Debug.DrawLine(position, nodes[nn].position, Color.blue, 10);
        Debug.DrawLine(targetPosition, nodes[tt].position, Color.blue, 10);

        if (nn == tt) return new List<Vector3>() { targetPosition };
        
        Dijkstra(nn,tt);
        List<Vector3> pth = new List<Vector3>();
        foreach (var item in currentPath)
        {
            pth.Add(nodes[item].position);
        }
        pth.Reverse();
        return pth;
    }

    public void Dijkstra(int nodeStart, int target) {
        unvisited = new List<int>();
        visited = new List<int>();
        foreach(Node a in nodes) unvisited.Add(a.myId);
        unvisited.Remove(nodeStart);
        visited.Add(nodeStart);
        for (int i = 0; i < nodes.Count; i++)
        {
            distances[i] = Mathf.Infinity;
        }
        distances[nodeStart] = 0;

        int n = nodeStart;
        int iterations = 0;
        while(unvisited.Count > 0 && iterations < 1000) {
            n = FindNextNode(n);
            Visit(n);
            iterations++;
        }
        
        n = target;
        iterations = 0;
        currentPath = new List<int>() {
            n
        };
        
        while(n != nodeStart && iterations < 1000) {
            //Debug.DrawLine(nodes[n].position, nodes[parents[n]].position, Color.red, 60);
            n = parents[n];
            currentPath.Add(n);
        }
        
    }

    

    private int FindNextNode(int node) {
        int nextNode = 0;
        float smallestDstPossible = Mathf.Infinity;
        for (int i = 0; i < nodes[node].connectedNodes.Count; i++)
        {
            
            int n = nodes[node].connectedNodes[i];
            

            float cost = nodes[node].weights[i] + distances[node];
            if(distances[n] > cost) {
                this.distances[n] = cost;
                parents[n] = node;
            }

            if(cost < smallestDstPossible && unvisited.Contains(n)) { // find the next node
                nextNode = n;
                
                smallestDstPossible = cost;
            }
        }

        return nextNode;
    }

    private void Visit(int n) {
        unvisited.Remove(n);
        if(!visited.Contains(n))
        visited.Add(n);
    }

    private void OnDrawGizmosSelected() {
        foreach(var n in nodes) {
            int i = 0;
            foreach(var c in n.connectedNodes) {
                #if UNITY_EDITOR
                Gizmos.DrawLine(n.position, nodes[c].position);
                
                Handles.color = Color.red;
                Handles.Label(n.position + Vector3.up, n.myId.ToString());
                try {
                    Handles.Label(Vector3.Lerp(n.position, nodes[c].position,0.5f), "W:" + n.weights[i].ToString("0.0"));
                } catch (Exception e){
                    print($"Weight null:: {e}");
                }
                #endif
                i++;
            }
        }
    }
}