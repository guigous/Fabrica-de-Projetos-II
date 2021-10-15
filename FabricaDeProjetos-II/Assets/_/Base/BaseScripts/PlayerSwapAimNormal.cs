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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using V_AnimationSystem;

/*
 * Swaps between Normal and Aim animation logic
 * Handles all Animations
 * */
public class PlayerSwapAimNormal : MonoBehaviour {
    
    public event EventHandler<CharacterAim_Base.OnShootEventArgs> OnShoot;

    #region Private
    private enum SkeletonType {
        Normal,
        Aim
    }

    private CharacterAim_Base characterAimBase;
    private PlayerAim playerAim;

    private Character_Base playerBase;
    private PlayerPunch playerPunch;
    private PlayerSword playerSword;

    private Weapon weapon;
    private SkeletonType skeletonType;

    private void Awake() {
        characterAimBase = GetComponent<CharacterAim_Base>();
        playerAim = GetComponent<PlayerAim>();
        playerBase = GetComponent<Character_Base>();
        playerPunch = GetComponent<PlayerPunch>();
        playerSword = GetComponent<PlayerSword>();

        playerBase.enabled = false;
        playerPunch.enabled = false;
        playerSword.enabled = false;

        skeletonType = SkeletonType.Aim;

        characterAimBase.OnShoot += CharacterAimBase_OnShoot;
    }

    private void CharacterAimBase_OnShoot(object sender, CharacterAim_Base.OnShootEventArgs e) {
        OnShoot?.Invoke(this, e);
    }

    private void EnableAimSkeleton() {
        playerBase.enabled = false;
        playerPunch.enabled = false;
        playerSword.enabled = false;

        characterAimBase.SetVObjectEnabled(true);
        characterAimBase.enabled = true;
        characterAimBase.RefreshBodySkeletonMesh();
        playerAim.enabled = true;

        skeletonType = SkeletonType.Aim;
    }

    private void EnableNormalSkeleton() {
        characterAimBase.SetVObjectEnabled(false);
        characterAimBase.enabled = false;
        playerAim.enabled = false;

        playerBase.enabled = true;
        playerPunch.enabled = true;
        playerSword.enabled = true;
        playerBase.RefreshBodySkeletonMesh();
        
        skeletonType = SkeletonType.Normal;
    }
    #endregion


    public Weapon GetWeapon() {
        return weapon;
    }

    public void SetWeapon() {
        SetWeapon(weapon);
    }

    public void SetWeapon(Weapon weapon) {
        this.weapon = weapon;

        switch (weapon.GetWeaponType()) {
        default:
        case Weapon.WeaponType.Pistol:
            EnableAimSkeleton();
            characterAimBase.SetWeaponType(CharacterAim_Base.WeaponType.Pistol);
            playerAim.SetWeapon(weapon);
            break;
        case Weapon.WeaponType.Shotgun:
            EnableAimSkeleton();
            characterAimBase.SetWeaponType(CharacterAim_Base.WeaponType.Shotgun);
            playerAim.SetWeapon(weapon);
            break;
        case Weapon.WeaponType.Rifle:
            EnableAimSkeleton();
            characterAimBase.SetWeaponType(CharacterAim_Base.WeaponType.Rifle);
            playerAim.SetWeapon(weapon);
            break;
        case Weapon.WeaponType.Punch:
            EnableNormalSkeleton();
            playerPunch.enabled = true;
            playerSword.enabled = false;
            break;
        case Weapon.WeaponType.Sword:
            EnableNormalSkeleton();
            playerPunch.enabled = false;
            playerSword.enabled = true;
            break;
        }
    }

    public void PlayIdleAnim() {
        characterAimBase.PlayIdleAnim();
        playerBase.PlayIdleAnim();
    }

    public void PlayMoveAnim(Vector3 moveDir) {
        characterAimBase.PlayMoveAnim(moveDir);
        playerBase.PlayMoveAnim(moveDir);
    }
    
    public void PlayDodgeAnimation(Vector3 dir) {
        EnableNormalSkeleton();
        playerPunch.enabled = false;
        playerSword.enabled = false;

        playerBase.PlayDodgeAnimation(dir);
    }
    
    public void PlayWinAnimation() {
        EnableNormalSkeleton();
        playerPunch.enabled = false;
        playerSword.enabled = false;

        playerBase.PlayWinAnimation();
    }

}