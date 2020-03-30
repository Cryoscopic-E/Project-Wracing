using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointTrigger : MonoBehaviour
{
    
    public Text lapText;
    public Text totLapsText;
    

    private List<GameObject> checkPoints;
    private GameObject checkPoint;
    private int currentCheckPointIndex = 0;
    private int totalLaps = 3;
    private int lap = 1;

    private void Awake()
    {
        //checkPoints = new List<GameObject>();
        //foreach(Transform t in GameObject.Find("CheckPoints").transform)
        //{
        //    checkPoints.Add(t.gameObject);
        //}
        //checkPoint = checkPoints[currentCheckPointIndex];
        //totLapsText.text = totalLaps.ToString();
        //lapText.text = lap.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            if (other.gameObject == checkPoint)
            {
                currentCheckPointIndex++;

                currentCheckPointIndex %= checkPoints.Count;
                checkPoint = checkPoints[currentCheckPointIndex];

                if (currentCheckPointIndex == 0)
                {
                    lap++;
                    lapText.text = lap.ToString();
                }
            }
        }
    }

    public GameObject GetCurrentTargetCheckPoint()
    {
        return checkPoint;
    }
}
