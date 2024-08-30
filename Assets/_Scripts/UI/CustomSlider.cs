using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GrassVsFps
{
    public class CustomSlider : Slider, IEndDragHandler
    {
        private float _lastValue;
        [SerializeField] private CustomSliderEvent _onEndDragAndValueChanged = new CustomSliderEvent();
        /// <summary>
        /// Callback when the slider drag end to new value
        /// </summary>
        public CustomSliderEvent OnEndDragAndValueChanged { get { return _onEndDragAndValueChanged; } set { _onEndDragAndValueChanged = value; } }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _lastValue = value;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (value != _lastValue)
            {
                _onEndDragAndValueChanged?.Invoke(value);
            }
        }

        [Serializable]
        public class CustomSliderEvent : UnityEvent<float> { }
    }
}
