using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SaveFile : MonoBehaviour
{
    public string saveName = "SaveData_";

    [Range(0, 10)]
    public int saveDataIndex = 1;
    public MapDataManager mapDataManager;

    public void SaveDataLocal(string dataToSave)
    {
        if (WriteToFile(saveName + saveDataIndex, dataToSave))
        {
            Debug.Log("Successfully saved data to file");
        }
    }

    public bool WriteToFile(string name, string content)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, name);

        try
        {
            File.WriteAllText(fullPath, content);
            Debug.Log("Filepath: " + fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving to a file " + e.Message);
        }
        return false;
    }

    public void SaveDataServer(string dataToSave)
    {
        StartCoroutine(PostRequestServer(dataToSave));
    }
    IEnumerator PostRequestServer(string data)
    {
        using (var request = new UnityWebRequest("http://localhost:3000", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Successfully saved data to server");
            }
            else
            {
                Debug.LogError("Save to server failed: " + request.error);
            }
        }
    }
    public string LoadDataLocal()
    {
        string data = "";
        if (ReadFromFile(saveName + saveDataIndex, out data))
        {
            Debug.Log("Successfully loaded data from file");
        }

        return data;
    }

    private bool ReadFromFile(string name, out string content)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, name);
        try
        {
            content = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error when loading the file " + e.Message);
            content = "";
        }
        return false;
    }

    public void LoadDataServer()
    {
        StartCoroutine(
            GetRequestServer(
                (UnityWebRequest request) =>
                {
                    // call to load game objects after recieved data from get request
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        print("Readfromserver recieved: " + request.downloadHandler.text);
                        Debug.Log("Successfully loaded data from server");
                        mapDataManager.ReDrawGameObjects(request.downloadHandler.text);
                    }
                    else
                    {
                        Debug.LogError("Load from server failed: " + request.error);
                    }
                }
            )
        );
    }

    public void SaveTilesLocal(string tilesData)
    {
        if (WriteToFile("Tiles_" + saveName + saveDataIndex, tilesData))
        {
            Debug.Log("Successfully saved tiles to file");
        }
    }

    public void SaveTilesServer(string tilesData)
    {
        StartCoroutine(PostTilesRequestServer(tilesData));
    }

    public string LoadTilesLocal()
    {
        string data = "";
        if (ReadFromFile("Tiles_" + saveName + saveDataIndex, out data))
        {
            Debug.Log("Successfully loaded tiles from file");
        }
        return data;
    }

    public void LoadTilesServer()
    {
        StartCoroutine(
            GetTilesRequestServer(
                (UnityWebRequest request) =>
                {
                // call to load tiles after receiving data from get request
                if (request.result == UnityWebRequest.Result.Success)
                    {
                        print("Tiles data received from server: " + request.downloadHandler.text);
                        Debug.Log("Successfully loaded tiles from server");
                        mapDataManager.DrawTilesFromJson(request.downloadHandler.text);
                    }
                    else
                    {
                        Debug.LogError("Load tiles from server failed: " + request.error);
                    }
                }
            )
        );
    }

    IEnumerator PostTilesRequestServer(string data)
    {
        using (var request = new UnityWebRequest("http://localhost:3001", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Successfully saved tiles to server");
            }
            else
            {
                Debug.LogError("Save tiles to server failed: " + request.error);
            }
        }
    }

    IEnumerator GetTilesRequestServer(Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            callback(request);
        }
    }


    IEnumerator GetRequestServer(Action<UnityWebRequest> callback)
    {
        // send get request to server, triggers callback after response recieved
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            callback(request);
        }
    }
}
