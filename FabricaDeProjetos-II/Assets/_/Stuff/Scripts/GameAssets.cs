/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */
 
using UnityEngine;
using System.Reflection;
using V_AnimationSystem;

public class GameAssets : MonoBehaviour {

    private static GameAssets _i;

    public static GameAssets i {
        get {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }



    public int ignoreRaycastLayer;
    public int playerLayer;
    public int enemyLayer;
    public int wallLayer;
    public int doorLayer;
    public int shieldLayer;


    public Sprite s_ShootFlash;
    public Sprite s_ShieldTransformerDestroyed;
    public Sprite s_PistolIcon;
    public Sprite s_ShotgunIcon;
    public Sprite s_RifleIcon;
    
    public Transform pfSwordSlash;
    public Transform pfEnemy;
    public Transform pfEnemyCharger;
    public Transform pfEnemyArcher;
    public Transform pfEnemyFlyingBody;
    public Transform pfEnemyShuriken;
    public Transform pfImpactEffect;
    public Transform pfDamagePopup;
    public Transform pfPickupHealth;

    public Material m_WeaponTracer;
    public Material m_MarineSpriteSheet;

    public Material m_DoorRed;
    public Material m_DoorGreen;
    public Material m_DoorBlue;
    public Material m_DoorKeyHoleRed;
    public Material m_DoorKeyHoleGreen;
    public Material m_DoorKeyHoleBlue;

    public Material m_LineEmissionRed;
    public Material m_LineEmissionYellow;
    public Material m_SpritesDefault;
    public Material m_PlayerWinOutline;





    
    public static class UnitAnimTypeEnum {

        static UnitAnimTypeEnum() {
            V_Animation.Init();
            FieldInfo[] fieldInfoArr = typeof(UnitAnimTypeEnum).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fieldInfoArr) {
                if (fieldInfo != null) {
                    fieldInfo.SetValue(null, UnitAnimType.GetUnitAnimType(fieldInfo.Name));
                }
            }
        }

        public static UnitAnimType dSwordTwoHandedBack_Idle;
        public static UnitAnimType dSwordTwoHandedBack_Walk;
        public static UnitAnimType dSwordTwoHandedBack_Sword;
        public static UnitAnimType dSwordTwoHandedBack_Sword2;

        public static UnitAnimType dMinion_Idle;
        public static UnitAnimType dMinion_Walk;
        public static UnitAnimType dMinion_Attack;

        public static UnitAnimType dShielder_Idle;
        public static UnitAnimType dShielder_Walk;

        public static UnitAnimType dSwordShield_Idle;
        public static UnitAnimType dSwordShield_Walk;

        public static UnitAnimType dMarine_Idle;
        public static UnitAnimType dMarine_Walk;
        public static UnitAnimType dMarine_Attack;

        public static UnitAnimType dBareHands_Idle;
        public static UnitAnimType dBareHands_Walk;
        

    }




    public static class UnitAnimEnum {

        static UnitAnimEnum() {
            V_Animation.Init();
            FieldInfo[] fieldInfoArr = typeof(UnitAnimEnum).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fieldInfoArr) {
                if (fieldInfo != null) {
                    fieldInfo.SetValue(null, UnitAnim.GetUnitAnim(fieldInfo.Name));
                }
            }
        }
        
        public static UnitAnim dMarine_AimWeaponRight;
        public static UnitAnim dMarine_AimWeaponRightInvertV;
        public static UnitAnim dMarine_ShootWeaponRight;
        public static UnitAnim dMarine_ShootWeaponRightInvertV;
        
    }

}
