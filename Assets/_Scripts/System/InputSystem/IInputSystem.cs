using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace HYH
{
    public interface IInputSystem : ISystem
    {
        float HorizontalInput { get; }
        float VerticalInput { get; }
        bool JumpDown { get; }
        bool Jump { get; }
        bool JumpUp { get; }

        bool ShootDown { get; }
        bool Shoot { get; }
        bool ShootUp { get; }
    }

    public class InputSystem : AbstractSystem, IInputSystem
    {
        protected override void OnInit()
        {

        }

        public float HorizontalInput => Input.GetAxisRaw("Horizontal");
        public float VerticalInput => Input.GetAxisRaw("Vertical");

        public bool JumpDown => Input.GetButtonDown("Jump");
        public bool Jump => Input.GetButton("Jump");
        public bool JumpUp => Input.GetButtonUp("Jump");

        public bool ShootDown => Input.GetButtonDown("Fire1");
        public bool Shoot => Input.GetButton("Fire1");
        public bool ShootUp => Input.GetButtonUp("Fire1");
    }
}