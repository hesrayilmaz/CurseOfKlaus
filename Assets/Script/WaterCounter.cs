using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaterCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waterText;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
       waterText.text=0+"/20";
    }

    public void IncreaseScore()
    {
        score++;
        waterText.text = score + "/20";
    }
}
