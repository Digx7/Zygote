using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    
    public abstract class ColorHelper : MonoBehaviour
    {
        public List<Color> colors;
        private const float LERPTIME = 0.1f;
        private Color ogColor;
        private Color newColor;

        private int _currentIndex = 0;

        public void SetColorIndex(int index)
        {
            if (index >= colors.Count) return;
            if (gameObject.activeInHierarchy == false) return;

            _currentIndex = index;

            ogColor = GetOGColor();
            newColor = colors[index];

            StopAllCoroutines();
            StartCoroutine(ColorFade());
        }

        public void IncrementColorIndex()
        {
            int newIndex = _currentIndex + 1;
            if(newIndex >= colors.Count) newIndex = 0;
            if(newIndex < 0) newIndex = 0;

            SetColorIndex(newIndex);
        }

        IEnumerator ColorFade()
        {
            OnSetColorIndex();

            float time = 0f;
            while (time < LERPTIME)
            {
                time += Time.deltaTime;
                float t = time;

                t = t.Remap(0f, LERPTIME, 0f, 1f);
                Color updatingColor = Color.Lerp(ogColor, newColor, t);
                UpdateColor(updatingColor);
                yield return null;
            }
        }

        protected virtual void OnSetColorIndex()
        {

        }

        protected virtual Color GetOGColor()
        {
            return Color.white;
        }

        protected virtual void UpdateColor(Color newColor)
        {
            // Will be overriden by children
        }
    }
}
