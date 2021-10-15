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

public class MainMenu : MonoBehaviour {

    private void Awake() {
        transform.Find("playBtn").GetComponent<Button_UI>().ClickFunc = () => Loader.Load(Loader.Scene.GameScene);
        transform.Find("websiteBtn").GetComponent<Button_UI>().ClickFunc = () => Application.OpenURL("https://unitycodemonkey.com");
    }

}
