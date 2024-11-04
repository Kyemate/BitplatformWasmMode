﻿using Foundation;
using UIKit;

namespace BitplatformWasmMode.Client.Maui.Platforms.MacCatalyst;

[Register(nameof(AppDelegate))]
public partial class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {

        return base.FinishedLaunching(application, launchOptions!);
    }

}