using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    public bool UseExternalDB = false;
    string internalDomain = "http://localhost/UnityBackendTutorial/";
    string externalDomain = "https://backendtutorial.000webhostapp.com/";
    string urlHeader;

    void Start()
    {
        urlHeader = UseExternalDB ? externalDomain : internalDomain;

        //// A correct website page.
        //StartCoroutine(GetRequest("https://www.example.com"));

        //// A non-existing page.
        //StartCoroutine(GetRequest("https://error.html"));

        // our project
        //StartCoroutine(GetDate("http://localhost/UnityBackendTutorial/GetDate.php"));
        //StartCoroutine(GetUsers("http://localhost/UnityBackendTutorial/GetUsers.php"));
        //StartCoroutine(Login("testuser", "123456"));
        //StartCoroutine(RegisterUser("testuser3", "123456"));
    }

    //public void ShowUserItems()
    //{
    //    StartCoroutine(GetItemsIDs(Main.Instance.UserInfo.UserID));
    //}

    IEnumerator GetDate(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    IEnumerator GetUsers(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(urlHeader + "Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Attempting to login user id: " + www.downloadHandler.text);
                Main.Instance.UserInfo.SetCredentials(username, password);
                Main.Instance.UserInfo.SetID(www.downloadHandler.text);

                //If we logged in correctly
                if (www.downloadHandler.text.Contains("Wrong Credentials") || www.downloadHandler.text.Contains("Username does not exist"))
                {
                    Debug.Log("Try Again");
                }
                else
                {
                    Debug.Log("login success!");
                    Main.Instance.UserProfile.SetActive(true);
                    Main.Instance.Login.gameObject.SetActive(false);
                }
            }
        }
    }

    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(urlHeader + "RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetItemsIDs(string userID, System.Action<string> callback)
    {
        string uri = urlHeader + "GetItemsIDs.php";
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    // Show results as text
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    //Debug.Log(webRequest.downloadHandler.text);
                    string jsonArray = webRequest.downloadHandler.text;
                    // Call callback function to pass results
                    callback(jsonArray);
                    break;
            }
        }
    }

    public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {
        string uri = urlHeader + "GetItem.php";
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    // Show results as text
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    string jsonArray = webRequest.downloadHandler.text;
                    // Call callback function to pass results
                    callback(jsonArray);
                    break;
            }
        }
    }

    public IEnumerator GetItemIcon(string itemID, System.Action<byte[]> callback)
    {
        string uri = urlHeader + "GetItemIcon.php";
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);
        form.AddField("urlHeader", urlHeader);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("DOWNLOADING ICON: " + itemID);
                    // results as byte array
                    byte[] bytes = webRequest.downloadHandler.data;
                    callback(bytes);
                    break;
            }
        }
    }

    public IEnumerator SellItem(string ID, string itemID, string userID)
    {
        string uri = urlHeader + "SellItem.php";
        WWWForm form = new WWWForm();
        form.AddField("ID", ID);
        form.AddField("itemID", itemID);
        form.AddField("userID", userID);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    // Show results as text
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                    break;
            }
        }
    }
}
