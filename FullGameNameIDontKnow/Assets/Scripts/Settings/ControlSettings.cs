using System;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class ControlSettings : MonoBehaviour
    {
        private Slider slider;
        private QualityType graphicsSettings;
        private void Start()
        {
            slider = GetComponent<Slider>();
            
            Enum.TryParse(PlayerPrefs.GetString(SaveKeys.Graphics, QualityType.Medium.ToString()), out graphicsSettings);
            
            switch (graphicsSettings)
            {
                case QualityType.Low:
                    slider.value = 0;
                    break;
                case QualityType.Medium:
                    slider.value = 1;
                    break;
                case QualityType.High:
                    slider.value = 2;
                    break;
            }
        }

        public void ChangeSettings()
        {
            GameSettingsManager.ResolutionAndQuality((QualityType) slider.value);
        }
    }
}
