using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1MdMnjWtkRkXVJ-RtGtABf4rEeAfjpwPzvTT-iH1J1is/export?format=tsv&gid=1545996921&range=A2:B";
    //const string URL = "https://script.google.com/macros/s/AKfycbxd_LOXjILbfDTR-v8jE0dOWHctRvlL4wL-cHuiNk6XXC1_132zczpQibGqDRiZQfHdVw/exec";

    //IEnumerator Start()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("value","값");

    //    UnityWebRequest www = UnityWebRequest.Post(URL, form);
    //    yield return www.SendWebRequest();

    //    string data = www.downloadHandler.text;
    //    print(data);
    //}
    
    IEnumerator GetDataSheet()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
    }

    public void GetData()
    {
        StartCoroutine(GetDataSheet());
        //UnityWebRequest www = UnityWebRequest.Get(URL);

        //www.SendWebRequest();

        //string data = www.downloadHandler.text;
        //Debug.Log(data);
    }

    public void CreatePost(WWWForm form)
    {
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        //yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        Debug.Log(data);
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();
            if (www.isDone) Debug.Log(www.downloadHandler.text);
            else Debug.Log("웹의 응답이 없습니다.");
        }
    }

}
