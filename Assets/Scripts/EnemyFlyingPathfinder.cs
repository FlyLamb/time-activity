using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemyFlyingPathfinder : Enemy {
    
    protected Vector3 next;
    public Transform target;

    public List<Vector3> path;
    private int pathPoint;


    private int repathCountdown = 50;

    protected override void Life() {   
        base.Life();
        if(next == Vector3.zero) {
            Repath();
        }

        
    }

    protected virtual void FixedUpdate() {
        if(repathCountdown < 0) {
            Repath();
            repathCountdown = 50;
        }
        repathCountdown--;

        for (int i = 0; i < path.Count - 1; i++) {
            Debug.DrawLine(path[i], path[i+1], Color.red, Time.fixedDeltaTime);
        }
    }

    protected void Repath() {
        path = DumbPathing.instance.GetPath(transform.position, target.position + Vector3.up * 10);
        
        if(path.Count > 1 && !Physics.Linecast(transform.position, path[1], DumbPathing.instance.mask))
            next = path[1];
        else if(path.Count == 1)
            next = path[0];
        
    }
}