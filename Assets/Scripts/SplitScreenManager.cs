using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitScreenManager : MonoBehaviour
{
    public List<GameObject> cameras;
    public List<RenderTexture> renderTextures;
    public Vector2 screenDimensions = new Vector2(1024, 768);

    GameObject canvas;
    int numOfPlayers;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            cameras.Add(gameObject.transform.GetChild(i).gameObject);
            gameObject.transform.GetChild(i).gameObject.GetComponent<Camera>().targetTexture = renderTextures[i];
            Destroy(gameObject.transform.GetChild(i).gameObject.GetComponent<AudioListener>());
        }

        numOfPlayers = cameras.Count;
        Debug.Log(numOfPlayers);
        SetUpScreen();
    }

    void SetUpScreen()
    {
        if(numOfPlayers == 1)
        {
            CreateRawImage(0, new Vector3(0, 0, 0), new Vector2(screenDimensions.x, screenDimensions.y));
        }else if(numOfPlayers == 2)
        {
            CreateRawImage(0, new Vector3(0, 150, 0), new Vector2(screenDimensions.x, screenDimensions.y /2));
            CreateRawImage(1, new Vector3(0, -150, 0), new Vector2(screenDimensions.x, screenDimensions.y / 2));
        }else if(numOfPlayers == 3)
        {
            CreateRawImage(0, new Vector3(-200, 150, 0), new Vector2(screenDimensions.x /2, screenDimensions.y / 2));
            CreateRawImage(1, new Vector3(200, 150, 0), new Vector2(screenDimensions.x /2, screenDimensions.y / 2));
            CreateRawImage(2, new Vector3(0, -150, 0), new Vector2(screenDimensions.x, screenDimensions.y / 2));
        }
        else
        {
            CreateRawImage(0, new Vector3(-200, 150, 0), new Vector2(screenDimensions.x / 2, screenDimensions.y / 2));
            CreateRawImage(1, new Vector3(200, 150, 0), new Vector2(screenDimensions.x / 2, screenDimensions.y / 2));
            CreateRawImage(2, new Vector3(-200, -150, 0), new Vector2(screenDimensions.x / 2, screenDimensions.y / 2));
            CreateRawImage(3, new Vector3(200, -150, 0), new Vector2(screenDimensions.x / 2, screenDimensions.y / 2));

        }



    }


    void CreateRawImage(int playerNum, Vector3 position, Vector2 scale)
    {
        GameObject rawImage = new GameObject("Player" + playerNum + "Image");
        rawImage.transform.SetParent(canvas.gameObject.transform);
        rawImage.AddComponent<RawImage>();
        RawImage rawImageTemp = rawImage.GetComponent<RawImage>();
        rawImageTemp.rectTransform.anchoredPosition = position;
        rawImageTemp.rectTransform.sizeDelta = scale;
        rawImageTemp.texture = renderTextures[playerNum];
    }

}
