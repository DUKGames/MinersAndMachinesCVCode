using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class Newsletter : MonoBehaviour
{
    private const string wwwTriggerEN = "https://waslink.net";
    private const string wwwTriggerPL = "https://waslink.net";

    public event EventHandler<EmailEventArgs> OnTriedEmailAdd;

    public class EmailEventArgs : EventArgs
    {
        public bool isAddedProperly;
    }

    public bool CanAddEmail()
    {
        if (SteamCloudDataManager.i.SteamCloudPrefs.AddedMailsCount < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddEmailToNewsletter(string emailToAdd, bool isEnglish) 
    {
        StartCoroutine(AddEmail(emailToAdd, isEnglish));
    }

    private IEnumerator AddEmail(string emailToAdd, bool isEnglish)
    {
        WWWForm form = new WWWForm();
        
        form.AddField("email", emailToAdd);
        form.AddField("game", "MANPC");

        UnityWebRequest www = UnityWebRequest.Post(isEnglish ? wwwTriggerEN : wwwTriggerPL, form);

        yield return www.SendWebRequest();

        if(www.downloadHandler.data != null)
        {
            if (www.downloadHandler.text == "ok")
            {
                SteamCloudDataManager.i.SteamCloudPrefs.AddedMailsCount++;
                OnTriedEmailAdd?.Invoke(this, new EmailEventArgs { isAddedProperly = true });
            }
            else
            {
                OnTriedEmailAdd?.Invoke(this, new EmailEventArgs { isAddedProperly = false });
            }
        }
        else
        {
            OnTriedEmailAdd?.Invoke(this, new EmailEventArgs { isAddedProperly = false });
        }
    }
}
