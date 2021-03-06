# AppBuildSettings
### 概要
ひとつのプロジェクトで複数のアプリとして管理するときに便利なエディタ拡張です。  
各情報はビルド前処理**OnPreprocessBuild**で各プラットフォームのPlayerSettingsの内容を上書きます。  
現在の対象プラットフォームはiOS、Androidのみです。  

# 使い方
ツールバーの ***Tools/AppBuildSettings*** で設定ファイルを開くことができます。  
![Imgur](https://i.imgur.com/jeT5RaY.png)  
**AppBuildSettings**に対象となるプラットフォームの情報を入力し、ビルドするだけです。<br>
設定後に**Apply**を押すのをお忘れなく。  
<br>
![Imgur](https://i.imgur.com/XQy09o1.png)
<br>

### ActiveTitleの変更  
アプリ名やActiveTitleの数は**AppBuildSettings.cs**の中の ***User Editable Area*** 内の項目を変更してください。  
  
**AppBuildSettings.cs**
```
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
```

## ビルド環境
Unity 2017.3.1f1<br>
macOS High Sierra 10.13.4
