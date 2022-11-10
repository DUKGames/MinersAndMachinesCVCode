using UnityEngine;

public class CodeTools : MonoBehaviour
{
    public static CodeTools i;

    private Camera mainCamera;

    private void Awake()
    {
        if (CodeTools.i == null)
        {
            i = this;
            SetCamera();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector3 GetWorldToScreenVector(Vector3 vector)
    {
        return mainCamera.WorldToScreenPoint(vector);
    }

    public Vector3 GetWorldToScreenVectorNoZ(Vector3 vector)
    {
        Vector3 pos = mainCamera.WorldToScreenPoint(vector);
        pos.z = 0;
        return pos;
    }

    public Vector3 GetScreenToWorldVectorFromClick(Vector3 clickPosition)
    {
        return mainCamera.ScreenToWorldPoint(clickPosition);
    }

    public Vector3 GetScreenToWorldVectorFromClickNoZ(Vector3 clickPosition)
    {
        Vector3 pos = mainCamera.ScreenToWorldPoint(clickPosition);
        pos.z = 0;
        return pos;
    }

    public void SetPlayerPrefs(string name, string value)
    {
        PlayerPrefs.SetString(name, value);
    }

    public void SetPlayerPrefs(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }

    public string GetPlayerPrefsStringValue(string name)
    {
        if (PlayerPrefs.HasKey(name)) return PlayerPrefs.GetString(name);
        return "-1";
    }

    public int GetPlayerPrefsIntValue(string name)
    {
        if (PlayerPrefs.HasKey(name)) return PlayerPrefs.GetInt(name);
        return -1;
    }

    public void DeletePlayerPrefs(string name)
    {
        PlayerPrefs.DeleteKey(name);
    }

    public void SetCamera()
    {
        mainCamera = Camera.main;
    }

}
