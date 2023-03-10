using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SliderDisplay : MonoBehaviour
    {
        public enum TextDisplayMode
        {
            SingleValue,
            Percentage,
            OutOf,
        }
        [SerializeField] private TMP_Text _sliderValueText;
        [SerializeField] private Slider _slider;
        [SerializeField] private TextDisplayMode displayMode;


        public float MaxValue { get; set; }


        public void SetToMax()
        {
            _slider.maxValue = MaxValue;
            SetValues(MaxValue);
        }

        public void SetValues(float newValue)
        {
            _slider.value = newValue;

            switch (displayMode)
            {
                case TextDisplayMode.SingleValue:
                {
                    _sliderValueText.text = newValue.ToString("#00");
                    break;
                }
                case TextDisplayMode.Percentage:
                {
                    var percentageValue = ((float)newValue / (float)MaxValue) * 100f;
                    _sliderValueText.text = newValue.ToString("#00%");
                    break;
                }
                case TextDisplayMode.OutOf:
                {
                    _sliderValueText.text = $"{newValue:#00} / {MaxValue:#00}";
                    break;
                }
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
