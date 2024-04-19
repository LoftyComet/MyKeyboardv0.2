using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerVisualize : MonoBehaviour
{
    public GameObject pressableButton;
    private TMP_Text tMP_Text;
    // Start is called before the first frame update
    void Start()
    {
        tMP_Text=GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        tMP_Text.text=pressableButton.GetComponent<NewQuickTap>().timer.ToString();
    }
}
