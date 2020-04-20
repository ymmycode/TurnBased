using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class MathScript : MonoBehaviour
{

    public int num1, num2, num3, total, answer;

    public Text questionText, buttonAnswer1Text, buttonAnswer2Text, buttonAnswer3Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetQuestion()
    {
        num1 = Random.Range(0, 10);
        num2 = Random.Range(0, 10);
        num3 = Random.Range(0, 10);

        questionText.text = num1.ToString() + " + " + num2.ToString() +" x "+ num3.ToString();

        total = num1 + num2 * num3;

        buttonAnswer1Text.text = Random.Range(total-2, total+1).ToString();
        buttonAnswer2Text.text = Random.Range(total-2, total+1).ToString();
        buttonAnswer3Text.text = Random.Range(total-2, total+1).ToString();
    }
}
