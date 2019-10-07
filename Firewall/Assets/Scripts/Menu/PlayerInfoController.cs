using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoController : MonoBehaviour
{
    public void OnUsername(string username) {
        if(PlayerInfo.playerInfo != null) {
            PlayerInfo.playerInfo.username = username;
            PlayerPrefs.SetString("username", username);
        }
    }
}
