using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RR_Test_Proj
{

    public class ImageLoader : MonoBehaviour
    {
        public string baseUrl = "https://picsum.photos/200/300";
        public int count = 10;

        public List<Texture> loadedTextures;
        public List<CardData> cards;
        //public List<Sprite> loadedSprites;
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < count; i++)
            {
                StartCoroutine("GetTexture", cards[i]);
            }
        }

        IEnumerator GetTexture(CardData card)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(baseUrl);
            yield return www.SendWebRequest();

            loadedTextures.Add(DownloadHandlerTexture.GetContent(www));
            card.SetImage(DownloadHandlerTexture.GetContent(www));
        }
    }
}