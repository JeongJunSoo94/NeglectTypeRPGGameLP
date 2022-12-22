using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class GoogleSheetManager : MonoBehaviour
{
    // const string URL = "https://docs.google.com/spreadsheets/d/1MdMnjWtkRkXVJ-RtGtABf4rEeAfjpwPzvTT-iH1J1is/export?format=tsv";
    const string URL = "https://script.google.com/macros/s/AKfycbxd_LOXjILbfDTR-v8jE0dOWHctRvlL4wL-cHuiNk6XXC1_132zczpQibGqDRiZQfHdVw/exec";

    IEnumerator Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("value","°ª");

        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
    }
}
