using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalkingPathfinder : Enemy {
    public Transform target;
    private NavMeshPath path;
    private float elapsed = 0.0f;

    protected Vector3 nextPoint;
    private int cp = 0;
    [SerializeField]
    protected EnemyController enemyController;
    [SerializeField]
    protected float nearDistance = 2f;


    protected float repathEvery = 1.0f;

    protected bool nearTarget = false;

    public bool lookAtTarget = true;

    override protected void Spawn() {
        base.Spawn();
        path = new NavMeshPath();
        elapsed = 0.0f;
    }



    override protected void Life() {
        base.Life();
        Navigation();
    }

    protected void Navigation() {
        // Update the way to the goal every second.
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f && enemyController.isGrounded) {
            elapsed -= 1.0f;
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);

            // NavMeshHit hit;
            // NavMesh.SamplePosition(transform.position, out hit, 10000, NavMesh.AllAreas);


            if (path.status == NavMeshPathStatus.PathInvalid) {
                enemyController.SetJump(true);
                enemyController.rb.AddForce(transform.forward * 10);
            } else {
                enemyController.SetJump(false);
            }

            cp = 0;
            if (cp < path.corners.Length) {
                nextPoint = path.corners[cp];
                nearTarget = false;
            } else {
                nextPoint = target.position;
            }

        }
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 10f);

        var wy = nextPoint;
        wy.y = 0;
        var py = transform.position;
        py.y = 0;
        if (Vector3.Distance(py, wy) < nearDistance) {
            if (cp + 1 < path.corners.Length) {
                cp++;
                nextPoint = path.corners[cp];
                nearTarget = false;
            } else {
                nearTarget = true;
            }
        }

        var bdir = (nextPoint - transform.position);

        bdir.y = 0;
        bdir.Normalize();
        enemyController.rb.rotation = Quaternion.LookRotation(bdir, Vector3.up);
        if (!nearTarget)
            enemyController.SetInput(bdir);
        else
            enemyController.SetInput(Vector2.zero);
    }


    public static Vector3 GetRandomPoint(Vector3 center, float maxDistance) {
        // Get Random Point inside Sphere which position is center, radius is maxDistance
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMeshHit hit; // NavMesh Sampling Info Container

        // from randomPos find a nearest point on NavMesh surface in range of maxDistance
        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }

}