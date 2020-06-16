using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTweener : MonoBehaviour
{


    public void PopUp()
    {
        //LeanTween.scale(gameObject, new Vector3(1,1,1), 1f);
        GetComponent<Animator>().SetBool("isOpen", true);
    }

    public void ClosePanel()
    {
        //LeanTween.scale(gameObject, new Vector3(1,1,1), 1f);
        GetComponent<Animator>().Play("CloseAnim");
    }
}
