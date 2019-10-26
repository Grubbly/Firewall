using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForceTextWrap : MonoBehaviour
{
    TextMeshProUGUI TMP;

    // Start is called before the first frame update
    void Start()
    {
        TMP = GetComponent<TextMeshProUGUI>();
        TMP.enableWordWrapping = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!TMP.enableWordWrapping) {
            TMP.enableWordWrapping = true;
        }
    }


}
