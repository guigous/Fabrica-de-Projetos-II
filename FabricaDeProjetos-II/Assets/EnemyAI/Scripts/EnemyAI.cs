/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EnemyAI : MonoBehaviour {

    private enum State {
        Roaming,
        ChaseTarget,
        ShootingTarget,
        GoingBackToStart,
    }

    private IAimShootAnims aimShootAnims;
    private EnemyPathfindingMovement pathfindingMovement;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private float nextShootTime;
    private State state;

    private void Awake() {
        pathfindingMovement = GetComponent<EnemyPathfindingMovement>();
        aimShootAnims = GetComponent<IAimShootAnims>();
        state = State.Roaming;
    }

    private void Start() {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update() {
        switch (state) {
        default:
        case State.Roaming:
            pathfindingMovement.MoveToTimer(roamPosition);

            float reachedPositionDistance = 10f;
            if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance) {
                // Reached Roam Position
                roamPosition = GetRoamingPosition();
            }

            FindTarget();
            break;
        case State.ChaseTarget:
            pathfindingMovement.MoveToTimer(Player.Instance.GetPosition());

            aimShootAnims.SetAimTarget(Player.Instance.GetPosition());

            float attackRange = 30f;
            if (Vector3.Distance(transform.position, Player.Instance.GetPosition()) < attackRange) {
                // Target within attack range
                if (Time.time > nextShootTime) {
                    pathfindingMovement.StopMoving();
                    state = State.ShootingTarget;
                    aimShootAnims.ShootTarget(Player.Instance.GetPosition(), () => {
                        state = State.ChaseTarget;
                    });
                    float fireRate = .15f;
                    nextShootTime = Time.time + fireRate;
                }
            }

            float stopChaseDistance = 80f;
            if (Vector3.Distance(transform.position, Player.Instance.GetPosition()) > stopChaseDistance) {
                // Too far, stop chasing
                state = State.GoingBackToStart;
            }
            break;
        case State.ShootingTarget:
            break;
        case State.GoingBackToStart:
            pathfindingMovement.MoveToTimer(startingPosition);
            
            reachedPositionDistance = 10f;
            if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance) {
                // Reached Start Position
                state = State.Roaming;
            }
            break;
        }
    }

    private Vector3 GetRoamingPosition() {
        return startingPosition + UtilsClass.GetRandomDir() * Random.Range(10f, 70f);
    }

    private void FindTarget() {
        float targetRange = 50f;
        if (Vector3.Distance(transform.position, Player.Instance.GetPosition()) < targetRange) {
            // Player within target range
            state = State.ChaseTarget;
        }
    }

}
