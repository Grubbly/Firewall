using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo playerInfo;
    public string username;

    // Create singleton
    private void OnEnable() {
        if(PlayerInfo.playerInfo == null) {
            PlayerInfo.playerInfo = this;
        } 
        else {
            if(PlayerInfo.playerInfo != this) {
                Destroy(PlayerInfo.playerInfo.gameObject);
                PlayerInfo.playerInfo = this;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("username")) {
            username = PlayerPrefs.GetString("username");
        }
    }
}
