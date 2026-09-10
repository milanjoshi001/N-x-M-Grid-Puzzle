using System;
using NMGrid.Grid;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NMGrid.Gameplay
{
    public class Input : MonoBehaviour
    {
        public Action<MoveDirection> OnMove;

        private void Update()
        {
            //PC controls
            
            if(Keyboard.current == null) return;

            if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                OnMove?.Invoke(MoveDirection.Up);
            }
            
            if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                OnMove?.Invoke(MoveDirection.Down);
            }
            
            if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                OnMove?.Invoke(MoveDirection.Left);
            }
            
            if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                OnMove?.Invoke(MoveDirection.Right);
            }
        }
    }
}