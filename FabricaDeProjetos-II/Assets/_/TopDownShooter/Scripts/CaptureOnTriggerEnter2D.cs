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

public class CaptureOnTriggerEnter2D : MonoBehaviour {

    public event EventHandler OnCapturedTriggerEnter2D;
    public event EventHandler OnPlayerTriggerEnter2D;

    private void OnTriggerEnter2D(Collider2D collider) {
        OnCapturedTriggerEnter2D?.Invoke(collider, EventArgs.Empty);

        Player player = collider.GetComponent<Player>();
        if (player != null) {
            OnPlayerTriggerEnter2D?.Invoke(player, EventArgs.Empty);
        }
    }

}
