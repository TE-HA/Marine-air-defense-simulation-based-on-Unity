using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDefine
{
    #region 内部消息更新

    //能否从数据库获取任务数据
    //true:能
    //false:不能
    public static bool CanGetTask = false;

    //栅格线缩放倍数
    public static int GridScale = 1;
    public const string Grid_Name = "Grid_";

    //能否显示游戏运行日志log
    //true：能
    //false：不能
    public static bool ShowStatus = false;
    #endregion

    #region 所有不变类型的数据定义
    #endregion

    #region 场景sence
    public const string startSence = "startGame";
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
    public static bool MuteEffect = false;
    #endregion


    #region GUI按钮name OnGUI
    public const string GUICreate = "create";
    public const string GUIPause = "pause";
    public const string GUIResume = "resume";
    public const string GUIMuteEffect = "mute effect";
    public const string GUIClearEffect = "clear effect";
    public const string GUIUpdate = "update";
    public const string GUIShowLog = "showlog";
    #endregion


    #region 面板 panel
    public const string InputMenu = "Prefabs/UI/InputMenu";
    public const string Panel = "Prefabs/UI/Panel";
    public const string PausePanelName = "InputMenu";
    public const string ShowLogPanelName = "GameLog";
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

    #region 相机camera
    //栅格线
    public const string Grid = "Prefabs/UI/Grid";
    public const string CellLine = "Prefabs/UI/GridLine";
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

    #endregion


    #region 武器weapon
    public const string Weapon = "Prefabs/weapon";
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

    //enemyPlaneCount
    public static string EnemyPlaneCount = "EnemyPlaneCount";
    #endregion

    #region 枚举类
    #endregion
    public enum Tag
    {
        Camera,
        plane,
        zhanjian,
        daodan,
    }

    public enum GameObjType
    {
        Plane_Warning,
        km_1,
        km_2,
    }
}
