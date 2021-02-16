using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

namespace RR_Test_Proj
{
   [ExecuteInEditMode]
   public class SiblingRotator : MonoBehaviour
    {
        public int siblingStepZ = 5;
        /*public Vector2 upArcShiftLimits = new Vector2(0, 1);*/
        public AnimationCurve upArcShiftLimitsCurve = new AnimationCurve();
        public int maxCardsCount = 6;
        public RectTransform cardBody;

        private float _normalizedSibling; 
        private RectTransform _rectTransform;

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        void Update()
        {
            if (_rectTransform.parent.childCount == 1)
            {
                _normalizedSibling = 0.5f;
            }
            else
            {
                _normalizedSibling =
                    (float) (_rectTransform.GetSiblingIndex()) / (_rectTransform.parent.childCount - 1);
            }

            SetArcRotation(_normalizedSibling);
            SetArcShifting(_normalizedSibling);
        }

        private void SetArcRotation(float normalizedSibling)
        {
            var lRotQuaternion = _rectTransform.localRotation;
            var lRotVector = lRotQuaternion.eulerAngles;

            if (_rectTransform.parent.childCount == 2)
            {
                lRotVector.z = 0;
            }
            else
            {
                var sRot = Mathf.Lerp(-1 * maxCardsCount * siblingStepZ, maxCardsCount * siblingStepZ,
                    normalizedSibling);
                lRotVector.z = sRot;
            }
            
            lRotQuaternion.eulerAngles = lRotVector;
            _rectTransform.localRotation = lRotQuaternion;
        }
        
        private void SetArcShifting(float normalizedSibling)
        {
            var lPos = cardBody.anchoredPosition;
            lPos.y = upArcShiftLimitsCurve.Evaluate(normalizedSibling);
            cardBody.anchoredPosition = lPos;
        }
    }
}
