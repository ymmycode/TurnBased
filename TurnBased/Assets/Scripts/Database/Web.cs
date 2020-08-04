using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    public Login login;
    public Register register;
    public string nowDate, userID, lastPlayed, lastScene, userNameNew, saveMessage;

    public float savedVolumeValue;
    
    // Start is called before the first frame update
    void Start()
    {
        // A correct website page.
    //    StartCoroutine(GetDate("http://127.0.0.1/gamedatabases/GetDate.php"));

        // A non-existing page.
    //    StartCoroutine(GetUsers("http://127.0.0.1/gamedatabases/GetUsers.php"));

        //login
    //    StartCoroutine(Login("ymmy", "170998"));

        //Register
    //    StartCoroutine(Register("fauzi", "qwerty", "fauzi@gmail.com"));
    }

    public IEnumerator UpdateDate(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", id);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/gamedatabases/UpdateDate.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                nowDate = www.downloadHandler.text;
                Debug.Log(nowDate);
            }
        }
    }

    public IEnumerator UpdateLastScene()
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        form.AddField("lastScene", SceneManager.GetActiveScene().name);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/gamedatabases/UpdateLastScene.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                saveMessage = www.downloadHandler.text;
            }
        }
    }
    public IEnumerator GetDate(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                nowDate = webRequest.downloadHandler.text;
            }
        }
    }

    public IEnumerator GetUserID(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/gamedatabases/GetUserID.php", form))
        {
            
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                userID = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    public IEnumerator UserLastScene(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", id);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/gamedatabases/GetLastScene.php", form))
        {
            
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                lastScene = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    public IEnumerator GetUsers(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/gamedatabases/Login.php", form))
        {
            
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                login.loginMessageNew = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);
                userNameNew = username;
            }
        }
    }
    public IEnumerator Register(string username, string password, string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("loginEmail", email);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/gamedatabases/Register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                register.regMessage = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator SavedData(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", id);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/gamedatabases/InputDataEntry.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    public IEnumerator SavedOption(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", id);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/gamedatabases/InputOptionEntry.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator UpdateVolume(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", id);
        form.AddField("volume", savedVolumeValue.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/gamedatabases/SyncOption.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetVolume(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", id);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/gamedatabases/GetVolume.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                savedVolumeValue = float.Parse(www.downloadHandler.text, CultureInfo.InvariantCulture.NumberFormat);
            }
        }
    }
}
