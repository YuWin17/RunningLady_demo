
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using System;
using Object = UnityEngine.Object;
using System.Net;
using System.Threading;

public interface IGame
{
    void StartGame();
}


public class Launcher : MonoBehaviour {
    protected internal CLauncherUI cLauncherUI = null;
    protected LaunchState state = null;

    void Start() {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //设置屏幕自动旋转， 并置支持的方向
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        //加载UI
        GameObject launcherui = UnityEngine.Object.Instantiate(Resources.Load("UI/Login/UIPrefab/Launcher")) as GameObject;
        this.cLauncherUI = launcherui.AddComponent<CLauncherUI>();

        if(Application.isMobilePlatform) {
            GameObject exitui = UnityEngine.Object.Instantiate(Resources.Load("UI/Login/UIPrefab/ExitGame")) as GameObject;
            exitui.AddComponent<CExitGameUI>();
            Object.DontDestroyOnLoad(exitui);
        }

        StartLaunche();
    }

    void StartLaunche() {
        this.state = LaunchState.Create(this);
        this.state.Enter();
    }

    void Update() {
        if (this.state == null) 
            return;

        if (this.state.pause)
            return;

        var s = this.state.CheckTransition();
        if (s != null) {
            this.state.Exit();
            s.Enter();
            this.state = s;
            return;
        }
        this.state.Update();
    }

    void OnDestroy() {
        if (this.state != null) {
            this.state.Exit();
            this.state = null;
        }
    }
}

