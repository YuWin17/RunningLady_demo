using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UILink = NGUILink.UILink;

public class CExitGameUI : MonoBehaviour
{
    private GameObject Camera;
    private int escapeTimes = 0;
    void Awake()
    {
        NGUILink link = this.gameObject.GetComponent(typeof(NGUILink)) as NGUILink;
        Camera = CLauncherUI.Get(link,"Camera");
        Camera.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Camera.SetActive(true);
            escapeTimes++;

            StartCoroutine("resetTimes");
            if (escapeTimes > 1)
            {
                Camera.SetActive(false);
                Debug.Log("Quit!");
                Application.Quit();
            }
        }
    }

    IEnumerator resetTimes()
    {
        yield return new WaitForSeconds(2);
        escapeTimes = 0;
        Camera.SetActive(false);
    }
}

public class CLauncherUI : MonoBehaviour
{
    public Text ProgressLabel;
    public Text Tips;
    public GameObject Messagebox;
    public CText Msg;
    public Button Ok, Cancel;
    private System.Action mOKAction, mCancelAction;
    void Awake()
    {
        NGUILink link = this.gameObject.GetComponent(typeof(NGUILink)) as NGUILink;
        ProgressLabel = Get(link, "ProgressLabel").GetComponent<Text>();
        Tips = Get(link, "Tips").GetComponent<Text>();

        Messagebox = Get(link, "Messagebox");
        Messagebox.SetActive(false);
        link = this.Messagebox.GetComponent(typeof(NGUILink)) as NGUILink;
        Msg = Get(link, "msg").GetComponent<CText>();
        Ok = Get(link, "FirstBtn").GetComponent<Button>();
        Cancel = Get(link, "SecondBtn").GetComponent<Button>();
        Ok.onClick.AddListener(OnOkClick);
        Cancel.onClick.AddListener(OnCancelClick);
    }

    public static GameObject Get(NGUILink self, string name)
    {
        self.DoInitIfDont();
        UILink link = null;
        self.all_objs.TryGetValue(name, out link);
        if (link == null)
        {
            return null;
        }
        return link.LinkObj;
    }

    public void SetAction(System.Action ok, System.Action cancel = null)
    {
        this.mOKAction = ok;
        this.mCancelAction = cancel;
        this.Cancel.gameObject.SetActive(cancel != null);
    }


    private void Update()
    {
        this.SetProgress(1);
    }

    public void SetProgress(float progress)
    {
        

    }

    public void ShowMessagebox(string msg)
    {
        Messagebox.SetActive(true);
        Msg.text = msg;
    }

    private void OnOkClick()
    {
        if (this.mOKAction != null)
            this.mOKAction();
        this.mOKAction = null;
        Messagebox.SetActive(false);
    }

    private void OnCancelClick()
    {
        if (this.mCancelAction != null)
            this.mCancelAction();
        this.mCancelAction = null;
        Messagebox.SetActive(false);
    }
}
