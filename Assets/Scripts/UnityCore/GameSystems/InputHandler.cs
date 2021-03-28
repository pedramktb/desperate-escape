using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore.GameSystems
{
    public class InputHandler : MonoBehaviour
    {
        #region Public Functions
        public bool IsMouseClicked()
        {
            return Input.GetButtonDown("Fire1");
        }

        public bool IsMouseHeldDown()
        {
            return Input.GetButton("Fire1");
        }

        public Vector2 GetMousePos()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } 
        #endregion
    }
}