using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserProfile : MonoBehaviour
{
    public TMP_Text userName, lastScene, lastPlayed;

    private void Update()
    {
        userName.text = Main.Instance.Web.userNameNew;
        lastScene.text = Main.Instance.Web.lastScene;
        lastPlayed.text = Main.Instance.Web.nowDate;
    }
}
