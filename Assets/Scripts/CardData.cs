using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor.Hardware;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace RR_Test_Proj
{
    public class CardData : MonoBehaviour
    {
        [Serializable]
        public class CValues
        {
            public Text text;

            [SerializeField] private int _value = 5;

            public void Start()
            {
                text.text = _value.ToString();
            }

            public int GetValue()
            {
                return _value;
            }

            public void SetNewValue(int valueNew)
            {
                OnValueChanged(_value = valueNew);
            }

            private void OnValueChanged(int valueNew)
            {
                text.text = valueNew.ToString();
            }
        }

        public Image image;
        public RawImage rawImage;
        public CValues helthValue;
        public CValues attackValue;
        public CValues manaValue;
        
        private CanvasGroup _canvasGroup;

        void Start()
        {
            _canvasGroup = gameObject.GetComponent<CanvasGroup>();
            helthValue.Start();
            attackValue.Start();
            manaValue.Start();
        }

        public void SetNewValueRandomly(int valueNew)
        {
            int getParam = Random.Range(1, 4);
            switch (getParam)
            {
                case 1 :
                    helthValue.SetNewValue(valueNew);
                    if (valueNew <= 0)
                        StartCoroutine("CardDie");
                    else
                    {
                        _canvasGroup.alpha = 1;
                        _canvasGroup.blocksRaycasts = true;
                    }
                    break;
                case 2 :
                    attackValue.SetNewValue(valueNew);
                    break;
                case 3 :
                    manaValue.SetNewValue(valueNew);
                    break;
            }
        }

        IEnumerator CardDie()
        {
            _canvasGroup.blocksRaycasts = false;
            while (_canvasGroup.alpha > .5f)
            {
                _canvasGroup.alpha -= Time.deltaTime;
                yield return null;
            }
        }

        public void SetImage(Texture2D getContent)
        {
            rawImage.texture = getContent;
            
            var r = image.rectTransform.rect;
            r.width = r.height = 128;
            var spr = Sprite.Create(getContent, r, image.rectTransform.pivot);
            image.sprite = spr;
        }
    }
}
