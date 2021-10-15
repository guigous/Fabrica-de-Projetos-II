/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */
 
using System;
using System.Collections.Generic;
using UnityEngine;
using V_AnimationSystem;
using CodeMonkey.Utils;

public class PlayerAim : MonoBehaviour {

    private CharacterAim_Base playerBase;
    private float nextShootTime;
    private State state;
    private Weapon weapon;

    private enum State {
        Normal,
        Reloading,
    }

    private void Awake() {
        playerBase = gameObject.GetComponent<CharacterAim_Base>();
        SetStateNormal();
    }

    private void Update() {
        switch (state) {
        case State.Normal:
            HandleAimShooting();
            break;
        case State.Reloading:
            HandleReloading();
            break;
        }
    }

    public void SetWeapon(Weapon weapon) {
        this.weapon = weapon;
    }

    private void SetStateNormal() {
        state = State.Normal;
    }

    private void HandleAimShooting() {
        Vector3 targetPosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDir = (targetPosition - GetPosition()).normalized;
        targetPosition += aimDir * 10f;
        playerBase.SetAimTarget(targetPosition);

        if (Time.time >= nextShootTime) {
            // Can shoot
            bool inputActivate = Input.GetMouseButtonDown(0);
            if (weapon.GetWeaponType() == Weapon.WeaponType.Rifle) inputActivate = Input.GetMouseButton(0);

            if (inputActivate) {
                if (weapon.TrySpendAmmo()) {
                    nextShootTime = Time.time + weapon.GetFireRate();
                    playerBase.ShootTarget(targetPosition);
                } else {
                    TryReload();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            TryReload();
        }
    }

    private void TryReload() {
        if (weapon.CanReload()) {
            state = State.Reloading;

            switch (weapon.GetWeaponType()) {
            case Weapon.WeaponType.Pistol:  Sound_Manager.PlaySound(Sound_Manager.Sound.Pistol_Reload); break;
            case Weapon.WeaponType.Rifle:   Sound_Manager.PlaySound(Sound_Manager.Sound.Rifle_Reload);  break;
            }
            
            playerBase.PlayIdleWeaponAimAnim();
            playerBase.PlayReloadAnim(() => {
                state = State.Normal;
                weapon.Reload();
            });
        }
    }

    private void HandleReloading() {
        Vector3 targetPosition = UtilsClass.GetMouseWorldPosition();
        playerBase.SetAimTarget(targetPosition);
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    private void OnEnable() {
        state = State.Normal;
    }

}
