// =================================
//
//	AppBuildSettingsEditor.cs
//	Created by Takuya Himeji
//
// ==============================

using UnityEngine;
using UnityEditor;
using UnityEditor.Build;

[CustomEditor(typeof(AppBuildSettings))]
public class AppBuildSettingsEditor : Editor, IPreprocessBuild
{
	// Inspector更新
    public override void OnInspectorGUI()
    {
        var buildSettings = target as AppBuildSettings;

		// ビルド対象のタイトルを更新
        buildSettings.buildTargetTitle = (GlobalConfig.AppTitle)EditorGUILayout.EnumPopup("Build Target Title", buildSettings.buildTargetTitle);
        EditorGUILayout.Space();

        for(int i=0; i<buildSettings.settings.Length; i++)
        {
        	AppBuildSettings.Settings stg = buildSettings.settings[i]; // AppSetting取得
			stg.isEditorOpen = EditorGUILayout.Foldout(stg.isEditorOpen, stg.productName); // タイトル名表示

            if (stg.isEditorOpen)
            {
                // ProductName, BundleIDの設定
                EditorGUI.indentLevel++;  // インデント
                stg.productName = (string)EditorGUILayout.TextField("Product Name", stg.productName);
                stg.bundleId = (string)EditorGUILayout.TextField("Bundle ID", stg.bundleId);

                EditorGUILayout.Space();
                foreach(AppBuildSettings.PlatformItem item in stg.items)
                {
                    EditorGUILayout.LabelField(item.platform.ToString());
                    EditorGUI.indentLevel++;  // インデント
                    item.version = (string)EditorGUILayout.TextField("Version", item.version);

                    if(item.platform == AppBuildSettings.Platform.iOS)
                    {
                        item.buildVersion = (string)EditorGUILayout.TextField("Build Version", item.buildVersion);
                    }
                    else
                    {
                        item.versionCode = (int)EditorGUILayout.IntField("Version Code", item.versionCode);
                    }

                    item.icon = (Texture2D)EditorGUILayout.ObjectField("App Icon", item.icon, typeof(Texture2D), false);
                    EditorGUI.indentLevel--;  // インデント戻し
                }
                EditorGUI.indentLevel--;  // インデント戻し
            }
        }

        this.serializedObject.ApplyModifiedProperties();
	}


	// ------------
    // ビルド前処理
	// PlayerSettingsの更新
    public void OnPreprocessBuild(BuildTarget buildTarget, string path)
    {
        var buildSettings = Resources.Load<AppBuildSettings>("AppBuildSettings");
        
        for (int i = 0; i < buildSettings.settings.Length; i++)
        {
            // Setting情報取得
            AppBuildSettings.Settings settings = buildSettings.settings[i];            
            // アプリタイトルの設定
            PlayerSettings.productName = settings.productName;

            // 設定されたビルドターゲットタイトルである場合
            if (settings.appTitle == buildSettings.buildTargetTitle)
            {
                foreach (AppBuildSettings.PlatformItem item in settings.items)
                {
                    PlayerSettings.bundleVersion = item.version;    // アプリバージョン
                    // 対象のプラットフォーム
                    if (buildTarget == BuildTarget.iOS)             // iOS
                    {
                        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, settings.bundleId);       // BundleID設定
                        PlayerSettings.iOS.buildNumber = item.buildVersion;                                     // ビルドバージョンの設定
                        SetIcons(BuildTargetGroup.iOS, item.icon);                                              // アイコンの設定
                        break;
                    }
                    else if (buildTarget == BuildTarget.Android)    // Android
                    {
                        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, settings.bundleId);   // BundleID設定
                        PlayerSettings.Android.bundleVersionCode = item.versionCode;                            // ビルドバージョンの設定
                        SetIcons(BuildTargetGroup.Android, item.icon);                                          // アイコンの設定
                        break;
                    }
                }
                // 設定の保存
                AssetDatabase.SaveAssets();
                break;
            }
        }
    }

    // アイコン設定
    private void SetIcons(BuildTargetGroup buildTargetGroup, Texture2D icon)
    {
        // アイコンの反映
        if (icon != null)
        {
            // プラットフォームのアイコンを取得
            Texture2D[] icons = PlayerSettings.GetIconsForTargetGroup(buildTargetGroup);
            // アイコン配列にTexuterを格納
            for (int i = 0; i < icons.Length; i++)
            {
                icons[i] = icon;
            }
            // 取得したアイコン画像をPlayerSettingsに反映
            PlayerSettings.SetIconsForTargetGroup(buildTargetGroup, icons);
        }
    }

    // 実行順
    public int callbackOrder { get { return 0; } }


    // ------------
    // AppBuildSettingsアセットピックアップ
    [MenuItem("Tools/AppBuildSettings")]
    static void SelectionAsset()
    {
        var guids = AssetDatabase.FindAssets("t:AppBuildSettings");
        if (guids.Length == 0)
        {
            throw new System.IO.FileNotFoundException("AppBuildSettings does not found");
        }

        var path = AssetDatabase.GUIDToAssetPath(guids[0]);
        var obj = AssetDatabase.LoadAssetAtPath<AppBuildSettings>(path);
        EditorGUIUtility.PingObject(obj);
        Selection.activeObject = obj;
    }
}