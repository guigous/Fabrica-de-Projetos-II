using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using V_AnimationSystem;
using V_ObjectSystem;

/*
 * Manages the Composite Skeleton to Aim and Shoot a Weapon
 * Doesnt manage Feet body parts
 * */
public class V_UnitSkeleton_Composite_WeaponReload : V_IActiveInactive {

    private V_Object parentObject;

    private V_UnitSkeleton unitSkeleton;
    private UnitAnim animAimWeaponReloadRight;
    private UnitAnim animAimWeaponReloadLeft;
    private UnitAnim activeAnimAimWeaponReload;
    private Vector3 aimTargetPosition;
    private bool isActive;
    private bool usingSkeletonRight;
    private bool isReloading;
    private Vector3 positionOffset;

    public V_UnitSkeleton_Composite_WeaponReload(V_Object parentObject, V_UnitSkeleton unitSkeleton, UnitAnim animAimWeaponReloadRight, UnitAnim animAimWeaponReloadLeft) {
        this.parentObject = parentObject;
        this.unitSkeleton = unitSkeleton;
        this.animAimWeaponReloadRight = animAimWeaponReloadRight.Clone();
        this.animAimWeaponReloadLeft = animAimWeaponReloadLeft.Clone();

        SetPositionOffset(new Vector3(0, -2));

        isReloading = false;
        SetInactive();
    }

    public void SetActive() {
        isActive = true;
        usingSkeletonRight = true;
        activeAnimAimWeaponReload = animAimWeaponReloadRight;
        unitSkeleton.ReplaceAllBodyPartsInAnimation(activeAnimAimWeaponReload);
        unitSkeleton.GetSkeletonUpdater().SetHasVariableSortingOrder(true);
    }

    public void SetInactive() {
        isActive = false;
    }

    public void SetPositionOffset(Vector3 positionOffset) {
        this.positionOffset = positionOffset;
    }

    public void SetAimTarget(Vector3 aimTargetPosition) {
        this.aimTargetPosition = aimTargetPosition;
        
        Vector3 aimDir = (aimTargetPosition - parentObject.GetPosition()).normalized;
        
        // Decide if should use Right or Left Body Part
        if (!isReloading) {
            switch (UnitAnim.GetAnimDirFromVector(aimDir)) {
            default:
            case UnitAnim.AnimDir.Down:
            case UnitAnim.AnimDir.DownRight:
            case UnitAnim.AnimDir.Right:
            case UnitAnim.AnimDir.UpRight:
            case UnitAnim.AnimDir.Up:
                if (!usingSkeletonRight) {
                    // Switch sides
                    usingSkeletonRight = true;
                    activeAnimAimWeaponReload = animAimWeaponReloadRight;
                    unitSkeleton.ReplaceAllBodyPartsInAnimation(activeAnimAimWeaponReload);
                }
                break;
            case UnitAnim.AnimDir.UpLeft:
            case UnitAnim.AnimDir.Left:
            case UnitAnim.AnimDir.DownLeft:
                if (usingSkeletonRight) {
                    // Switch sides
                    usingSkeletonRight = false;
                    activeAnimAimWeaponReload = animAimWeaponReloadLeft;
                    unitSkeleton.ReplaceAllBodyPartsInAnimation(activeAnimAimWeaponReload);
                }
                break;
            }
        }

        // Show on top of Body for all except Up
        bool weaponOnTopOfBody = UnitAnim.GetAnimDirFromVectorLimit4Directions(aimDir) != UnitAnim.AnimDir.Up;
        
        int bonusOffset = 2000;

        if (usingSkeletonRight) {
            activeAnimAimWeaponReload.ApplyAimDir(aimDir, positionOffset, weaponOnTopOfBody ? +bonusOffset : -bonusOffset);
        } else {
            activeAnimAimWeaponReload.ApplyAimDir(CodeMonkey.Utils.UtilsClass.ApplyRotationToVector(aimDir, 180), positionOffset, weaponOnTopOfBody ? +bonusOffset : -bonusOffset);
        }
    }

    public void Reload(Vector3 aimTargetPosition, Action onReloadCompleted) {
        SetAimTarget(aimTargetPosition);
        Action<V_Skeleton_Anim> reloadCompleted = (V_Skeleton_Anim skeletonAnim) => {
            isReloading = false;
            activeAnimAimWeaponReload.GetAnims()[0].onAnimComplete = null;
            //CodeMonkey.CMDebug.TextPopupMouse("reload");
            onReloadCompleted();
        };
        isReloading = true;
        activeAnimAimWeaponReload.ResetAnims();
        activeAnimAimWeaponReload.GetAnims()[0].onAnimComplete = reloadCompleted;
        unitSkeleton.ReplaceAllBodyPartsInAnimation(activeAnimAimWeaponReload);
    }

    public void SetAnims(UnitAnim animAimWeaponReloadRight, UnitAnim animAimWeaponReloadLeft) {
        this.animAimWeaponReloadRight = animAimWeaponReloadRight.Clone();
        this.animAimWeaponReloadLeft = animAimWeaponReloadLeft.Clone();
        SetActive();
    }

}
