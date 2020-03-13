using UnityEngine;
using UnityEngine.UI;

public class CheckPointTrigger : MonoBehaviour
{
    public GameObject[] checkPoints;
    public Text lapText;
    public Text totLapsText;


    private GameObject checkPoint;
    private int currentCheckPointIndex = 0;
    private int totalLaps = 3;
    private int lap = 1;

    private void Start()
    {
        checkPoint = checkPoints[currentCheckPointIndex];
        totLapsText.text = totalLaps.ToString();
        lapText.text = lap.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            if (other.gameObject == checkPoint)
            {
                currentCheckPointIndex++;

                currentCheckPointIndex %= checkPoints.Length;
                checkPoint = checkPoints[currentCheckPointIndex];

                if (currentCheckPointIndex == 0)
                {
                    lap++;
                    lapText.text = lap.ToString();
                }
            }
        }
    }
}
