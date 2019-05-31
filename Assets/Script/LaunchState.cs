using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

// LAUNCH状态基类
public abstract class LaunchState
{
    public string DownLoad { protected set; get; }
    public float ProgressValue { protected set; get; }
    public string Tips { protected set; get; }
    protected Launcher launcher = null;
    public LaunchState(Launcher launcher) { this.launcher = launcher; }
    public bool pause { get; private set; }
    public void SetPause(bool pause) { this.pause = pause; }
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract LaunchState CheckTransition();

    public static LaunchState Create(Launcher launcher) { return new InitState(launcher); }
}

// 初始化资源
internal class InitState : LaunchState
{
   

    public InitState(Launcher launcher) : base(launcher) { }

    public override void Enter()
    {
      
    }

    public override void Update()
    {
      
    }

    private void CheckCopyQueue(string file)
    {
       
    }

    public override LaunchState CheckTransition()
    {
        return new EnterGameState(this.launcher);
    }

    public override void Exit() { }
}


//检查并更新资源服务器资源
internal class CheckVersionState : LaunchState {
    public CheckVersionState(Launcher launcher) : base(launcher) { }

    public override void Enter() {
     
    }

    public override void Update() {
      
    }

    public override void Exit() { }

    public override LaunchState CheckTransition() {
       return null;
    }

    private void OnComplete() {
       
    }

}

/// <summary>
/// 进入游戏
/// </summary>
internal class EnterGameState : LaunchState
{
    private IGame game;
    public EnterGameState(Launcher lau) : base(lau) { }
    public override void Enter()
    {
        InitEventSystem();

        LoadGameData();
    }

    public override void Exit() { }
    public override void Update() { }
    public override LaunchState CheckTransition() { return null; }

    private void InitEventSystem()
    {
        GameObject eventsystem = new GameObject("EventSystem");
        Object.DontDestroyOnLoad(eventsystem);
        eventsystem.AddComponent<EventSystem>();
        StandaloneInputModule sim = eventsystem.AddComponent<StandaloneInputModule>();
        sim.forceModuleActive = true;
    }

    private void LoadGameData()
    {
        PCLoadAssembly();
    }
    void PCLoadAssembly()
    {
        this.game=new CGame();
        this.game.StartGame();
    }

}


