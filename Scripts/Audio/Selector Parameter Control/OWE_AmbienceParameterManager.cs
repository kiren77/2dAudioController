using System.Collections.Generic;
using UnityEngine;

namespace OWE_AmbienceControl
{
    public class OWE_AmbienceParameterManager : MonoBehaviour
    {
        public FMODUnity.StudioEventEmitter biomeEmitter;
        public FMODUnity.StudioEventEmitter manmadeEmitter;

        private float biomeSelectorValue = 0.0f;
        private float manmadeSelectorValue = 0.0f;

        public float BiomeSelectorValue
        {
            get => biomeSelectorValue;
            set
            {
                biomeSelectorValue = Mathf.Clamp(value, 0f, 1f);
                SetBiomeParameter();
            }
        }

        public float ManmadeSelectorValue
        {
            get => manmadeSelectorValue;
            set
            {
                manmadeSelectorValue = Mathf.Clamp(value, 0f, 1f);
                SetManmadeParameter();
            }
        }

        private void SetBiomeParameter()
        {
            int biomeLabelIndex = Mathf.FloorToInt(biomeSelectorValue * 4);
            biomeEmitter.SetParameter("OWE Biome Selector", biomeLabelIndex);
        }

        private void SetManmadeParameter()
        {
            int manmadeLabelIndex = Mathf.FloorToInt(manmadeSelectorValue * 1);
            manmadeEmitter.SetParameter("OWE Manmade Selector", manmadeLabelIndex);
        }

        public void SetBiomeToDesert()
        {
            BiomeSelectorValue = 0f / 3f;
        }

        public void SetBiomeToForest()
        {
            BiomeSelectorValue = 1f / 3f;
        }

        public void SetBiomeToJungle()
        {
            BiomeSelectorValue = 2f / 3f;
        }

        public void SetBiomeToOcean()
        {
            BiomeSelectorValue = 3f / 3f;
        }

        public void SetManmadeToMarket()
        {
            ManmadeSelectorValue = 0f;
        }
    }
}
