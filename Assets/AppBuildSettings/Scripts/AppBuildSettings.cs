// =================================
//
//	AppBuildSettings.cs
//	Created by Takuya Himeji
//
// =================================

using UnityEngine;

public class AppBuildSettings : ScriptableObject
{
    public AppTitle buildTargetTitle = AppTitle.App1;
    
    public static AppTitle ActiveTitle
    {
        get
        {
            var settings = Resources.Load<AppBuildSettings>("AppBuildSettings");
            return settings.buildTargetTitle;
        }
    }

    // プラットフォーム
    public enum Platform
    {
        iOS, Android
    }

    [System.Serializable]
    public class PlatformItem
    {
        public Platform platform;
        public string bundleId = "com.mycompany.myapps";
        public string version = "1.0.0";
        public string buildVersion = "0";
        public int versionCode = 0;
        public Texture2D icon;
    }

    [System.Serializable]
    public class Settings
    {
        // Active Title
        public AppTitle appTitle;
        // General Settings
        public string productName = "MyApps";
        public PlatformItem[] items = new PlatformItem[2]
        {
            new PlatformItem { platform = Platform.iOS },
            new PlatformItem { platform = Platform.Android }
        };

        public bool isEditorOpen = false;   // Editor Support
    }


    // ------------
    #region User Editable Area
    // アプリタイトル
    public enum AppTitle
    {
        App1,
        App2,
        App3,
        App4
    }

    public Settings[] settings = new Settings[4]
    {
        new Settings { appTitle = AppTitle.App1 },
        new Settings { appTitle = AppTitle.App2 },
        new Settings { appTitle = AppTitle.App3 },
        new Settings { appTitle = AppTitle.App4 }
    };
    #endregion User Editable Area
    // ------------
}
