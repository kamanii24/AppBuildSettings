// =================================
//
//	AppBuildSettings.cs
//	Created by Takuya Himeji
//
// =================================

using UnityEngine;

public class AppBuildSettings : ScriptableObject
{
    public GlobalConfig.AppTitle buildTargetTitle = GlobalConfig.AppTitle.App1;

    // プラットフォーム
    public enum Platform
    {
        iOS, Android
    }

    [System.Serializable]
    public class PlatformItem
    {
        public Platform platform;
        public string version       = "1.0.0";
        public string buildVersion  = "0";
        public int versionCode = 0;
        public Texture2D icon;
    }

    [System.Serializable]
    public class Settings
    {
        // Active Title
        public GlobalConfig.AppTitle appTitle;
        // General Settings
        public string productName       = "MyApps";
        public string bundleId          = "com.mycompany.myapps";
        public PlatformItem[] items = new PlatformItem[2]
        {
            new PlatformItem { platform = Platform.iOS },
            new PlatformItem { platform = Platform.Android }
        };

        public bool isEditorOpen = false;   // Editor Support
    }
    public Settings[] settings = new Settings[4]
    {
        new Settings { appTitle = GlobalConfig.AppTitle.App1 },
        new Settings { appTitle = GlobalConfig.AppTitle.App2 },
        new Settings { appTitle = GlobalConfig.AppTitle.App3 },
        new Settings { appTitle = GlobalConfig.AppTitle.App4 }
    };
}
