using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;
using System.Linq;


public class EnemyEyeGuy : EnemyFlyingPathfinder {
    private new Rigidbody rigidbody;

    public float laserRegenTime = 10;
    private float laserLoaded;

    [SerializeField]
    private GameObject eyeLaserExplosion;

    public Animator animator;

[SerializeField]
    private LineRenderer laser;

    [SerializeField] private GameObject bigLaser;

    override protected void Spawn() {
        base.Spawn();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        laserLoaded = laserRegenTime;
        bigLaser.SetActive(false);

        target = PlayerManager.Instance.controller.transform;
        laserLoaded = Random.Range(5,12);
    }

    [System.Obsolete] // fuck this shit
    
    protected override void Life() {
        base.Life();
        if(laserLoaded > 0) {
            laserLoaded -= Time.deltaTime;
            laser.enabled = false;
        }
        else {
            laser.enabled = true;
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, transform.position + transform.forward * -1000);
            RaycastHit hit;
            Debug.DrawRay(transform.position,transform.forward * -100);
            
            if(Physics.Raycast(transform.position,transform.forward * -1, out hit)) {
                laser.SetPosition(1, hit.point);
                Collider[] ws = Physics.OverlapSphere(hit.point, 2);
                foreach(Collider aa in ws) {
                    if(aa.gameObject.layer == 6) {
                        print("HIT THE PLAYER");
                        laserLoaded = laserRegenTime;
                        animator.SetTrigger("Attack");
                        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                        gameObject.TweenDelayedInvoke(0.5f, () => {
                            bigLaser.SetActive(true);
                            Instantiate(eyeLaserExplosion, hit.point, Quaternion.LookRotation(hit.normal));
                        }).TweenDelayedInvoke(0.7f, () => {
                            bigLaser.SetActive(false); 
                            rigidbody.constraints = RigidbodyConstraints.None;
                        });
                        break;
                    }
                }
            }
        }
    }

    override protected void FixedUpdate() {
        if(health < 0) return;

        base.FixedUpdate();
        
        Vector3 direction = next - transform.position;
        Vector3 playerDirection = target.position - transform.position;
        playerDirection.Normalize();
        direction.Normalize();

        if(rigidbody.velocity.magnitude < 5)
            rigidbody.AddForce(direction, ForceMode.Acceleration);

        Vector3 lookDir = laserLoaded <= 0 ? playerDirection : direction;

        Vector3 torque = Vector3.Cross(lookDir, transform.forward);
        rigidbody.AddTorque(torque * Mathf.Clamp(10 - rigidbody.velocity.magnitude,0.2f,2), ForceMode.Acceleration);
    }
}
