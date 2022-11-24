using UnityEngine;
using System.Net;
using System.IO;


public class DOS : MonoBehaviour
{
   public static Voice_Command RecognisedText(string text)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://d5dgmkq0ivv72u343ps2.apigw.yandexcloud.net/dos?q=" + text);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<Voice_Command>(json);
    }
}
