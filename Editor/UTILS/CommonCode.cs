using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class CommonCode
{
    public static string API_HOST = "https://api-backend-gla.herokuapp.com/";
    public CommonCode() { }
    public static IEnumerator Post(string url, string json)
    {
        Debug.Log(url);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.isHttpError)
        {
            throw new Exception(request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Dados enviados com sucesso!");
            Debug.Log("Pode acessa - los através da rota: " + url);
        }

    }

    public static async Task<string> Get(string url)
    {
        try
        {
            using (var www = UnityWebRequest.Get(url))
            {

                www.SetRequestHeader("Content-Type", "application/json");

                var operation = www.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                if (www.isHttpError)
                    throw new Exception(www.downloadHandler.text);

                if (www.downloadHandler.text.Contains("null"))
                {
                    return null;
                }

                return www.downloadHandler.text;
            }

        }
        catch (Exception ex)
        {
            Debug.LogError($"{nameof(Get)} failed: {ex.Message}");
            return default;
        }
    }
}