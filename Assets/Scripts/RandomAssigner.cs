using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RR_Test_Proj
{
    public class RandomAssigner : MonoBehaviour
    {
        public Button button;
        public List<CardData> cards;
    
        
        private int _index;
        // Start is called before the first frame update
        void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (_index == cards.Count)
                _index = 0;
            var crd = cards[_index];
            crd.SetNewValueRandomly(Random.Range(-2, 9));
            _index++;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
