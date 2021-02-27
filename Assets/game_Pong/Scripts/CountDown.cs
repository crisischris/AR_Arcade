using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public int countDownTime;
    public Text countDownDisplay;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }
    IEnumerator CountdownToStart()
    {
        while(countDownTime > 0)
        {
            countDownDisplay.text = countDownTime.ToString();
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }
        countDownDisplay.text = "GO";
        // Wait 1 second and turn off GO
        yield return new WaitForSeconds(1f);
        countDownDisplay.gameObject.SetActive(false);
    }
}
