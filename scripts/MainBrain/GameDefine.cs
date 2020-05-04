using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDefine
{
    #region 内部消息更新
    //能打击导弹的阈值
    public static float canFireValue = 100;

    //能否从数据库获取任务数据
    //true:能
    //false:不能
    public static bool CanGetTask = false;

    //轮次打完
    //true:打完
    //false:没打完
    public static bool RoundOver = false;

  //能否屏蔽射线
    //true:能
    //false:不能
    public static bool MuteWarningRay = false;
    public static bool MuteWatchRay = false;
    public static bool MuteFireRay = false;

    //能否更新任务树状态
    //true:能
    //false:不能
    public static bool canFreshTaskHeap = false;


    //栅格线缩放倍数
    public static int GridScale = 1;
    public const string Grid_Name = "Grid";

    //能否显示游戏运行日志log
    //true：能
    //false：不能
    public static bool ShowStatus = false;


    //能否显示游戏运行任务堆机制
    //true：能
    //false：不能
    public static bool canShowHeap = false;

    //能否显示游戏运行资源日志
    //true：能
    //false：不能
    public static bool ShowAssetsLog = false;




   //能否显示游戏分析结果
    //true：能
    //false：不能
    public static bool canShowGameAnalyse = false;



    //能否显示射线
    //true：能
    //false：不能
    public static bool canRay = true;

    //导弹毁伤的概率
    public static float percent = 0.6f;

    
    public static int fireAssets=1;
    public static int watchingAssets=3;
    public static int warningAssets=3;
    #endregion

    #region 所有不变类型的数据定义
    #endregion

    #region 场景sence
    public const string mainSence = "currentGame";
    #endregion

    #region 特效effect
    public const string HitBoomExplosion = "Prefabs/effect/FTEM_Explosion03";
    public const string HitWaterExplosion = "Prefabs/effect/FTEM_Explosion06_water";
    public const string FireOut = "Prefabs/effect/FTEM_ExplosionSmoke_Side01";
    public const string DaodanFly = "Prefabs/effect/FTEM_Explosion_Smoke03";
    public const string BeHitEffect = "Prefabs/effect/FTEM_Explosion05";
    public const string DaodanZhanjian = "Prefabs/fire/daodan_pre";
    public const string EnemyPlane = "Prefabs/plane/enemyPlane";
    public const string DaodanPlane = "Prefabs/fire/daodan_plane";
    public const string HitSound = "Prefabs/Sound/VeryLargeExplosion";
    public const string TravelLine = "Prefabs/line";
    public static bool MuteEffect = true;

    public static string ShotRay = "Prefabs/Materials/ShotRay";
    public static string WarningRay = "Prefabs/Materials/WarningRay";
    public static string WatchRay = "Prefabs/Materials/WatchRay";
    public static string FireRayName = "FireRay";
    public static string WarningRayName = "WarningRay";
    public static string WatchRayName = "WatchRay";
    public const string WatchRayRay= "Prefabs/UI/watching";

    #endregion

    #region GUI按钮name OnGUI
    public const string GUIFireAssets = "FireAssets";
    public const string GUIPause = "PauseInput";
    public const string GUIResume = "Resume";
    public const string GUIMuteEffect = "MuteEffect";
    public const string GUIMuteWarningRay = "WarnRay";
    public const string GUIMuteWatchRay = "WatchRay";
    public const string GUIMuteFireRay = "FireRay";
    public const string GUIPauseShow = "PauseShow";
    public const string GUIHeapStatus = "HeapStatus";
    public const string GUIShowLog = "ShowLog";
    public const string GUIAnalyse = "Analyse";
    #endregion

    #region 面板 panel
    public const string InputMenu = "Prefabs/UI/InputMenu";
    public const string AnalyseMenu = "Prefabs/UI/VictoryMenu";
    public const string Panel = "Prefabs/UI/Panel";
    public const string PausePanelName = "InputMenu";
    public const string AnalysePanelName = "AnalyseMenu";
    public const string ShowLogPanelName = "GameLog";
    public const string ShowAssets = "Assets";
    public const string ShowGameAnalyse = "VictoryMenu";
    public const string ShowHeap = "Heap";
    public const string BloodSlider = "Prefabs/UI/BloodSlider";
    public const string BloodSlider_main = "km_main_info";
    public const string BloodSlider_1 = "km_1_info";
    public const string BloodSlider_2 = "km_2_info";
    public const string BloodSlider_3 = "km_3_info";
    public const string BloodSlider_4 = "km_4_info";
    public const string BloodSlider_5 = "km_5_info";
    public const string BloodSlider_6 = "km_6_info";
    public const string BloodSlider_Plane_Warning = "plane_warning_info";
    #endregion

    #region 海洋Ocean
    public static GameObject Ocean=GameObject.Find("Ocean");
    #endregion

    #region 相机camera
    //栅格线
    public const string Grid = "Prefabs/UI/Grid";
    public const string CellLine = "Prefabs/UI/GridLine";
    public const string GridObj = "Prefabs/UI/GridObj";
    public const string Ray = "Prefabs/UI/Ray";
    public const string CellLineName = "GridLine";

    public const string Camera2D = "Prefabs/Camera/camera2d";
    //设置当前相机
    public static Camera CurrentCamera = Camera.main;
    //设置上一个视角的相机
    public static Camera prvsCamera = CurrentCamera;

    //切换当前相机
    public static void ChangeView(Camera CurrCamera)
    {
        prvsCamera = CurrentCamera;
        CurrentCamera = CurrCamera;
    }
    public static Vector3 middlepoint = Vector3.zero;
    public static bool ganglai = false;
    #endregion

    #region 武器weapon
    public const string Attack = "Prefabs/attack";
    public const string FireZhanjian = "Prefabs/firezhanjian";
    public static Vector3 daodanScale = new Vector3(3, 3, 3);
    #endregion

    #region 移动move
    public const string Move = "Prefabs/move";
    #endregion

    #region 添加舰船
    public const string addobj = "Prefabs/addobj";
    public const string addobjname = "Prefabs/ship/ship/";
    public const string Hangmu = "Prefabs/ship/ship/km_main";
    public const string huweijian = "Prefabs/ship/ship/km_huwei";
    public const string quzhujian = "Prefabs/ship/ship/km_quzhu";
    public const string xunyangjian = "Prefabs/ship/ship/km_xuyang";
    #endregion

    #region 敌军信息
    //enemy plane daodan count
    public const string EnemyPlaneDaodanCount = "EnemyPlaneDaodanCount";
    public const int enemyDistance = 10000;
    //enemyPlaneCount
    public static string EnemyPlaneCount = "EnemyPlaneCount";
    #endregion

    #region 枚举类
    public enum Tag
    {
        Camera,
        plane,
        zhanjian,
        daodan,
    }

    public enum TaskType
    {
        attack,
        fire,
        move,
        addobj,
    }

    public enum RayType
    {
        FireRay,
        WarningRay,
        WatchRay,
    }
    #endregion
}
