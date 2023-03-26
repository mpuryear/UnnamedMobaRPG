using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity.MobaRPG.Gameplay.UI
{
    public class UIHUDButton : Button, IPointerDownHandler, IPointerUpHandler
    {
        // We apply a uniform 95% scale to buttons when pressed
        static readonly Vector3 k_DownScale = new Vector3(0.95f, 0.95f, 0.95f);

        /// <summary>
        /// Called when the user clicks down on the button (but hasn't released the button yet)
        /// </summary>
        public Action OnPointerDownEvent;

        /// <summary>
        /// Called when the user clicks up on the button (completing a click event)
        /// </summary>
        public Action OnPointerUpEvent;

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (!IsInteractable()) { return; }
            base.OnPointerDown(eventData);
            transform.localScale = k_DownScale;
            OnPointerDownEvent?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (!IsInteractable()) { return; }
            base.OnPointerUp(eventData);
            transform.localScale = Vector3.one;
            OnPointerUpEvent?.Invoke();
        }
    }
}
