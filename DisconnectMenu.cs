using BepInEx;
using UnityEngine;

[BepInPlugin("com.yourname.disconnectmenu", "Disconnect Menu (Template)", "1.0.0")]
[BepInProcess("Gorilla Tag.exe")]
public class DisconnectPlugin : BaseUnityPlugin
{
    Menu.MenuController menuController;

    void Start()
    {
        menuController = new Menu.MenuController();
        menuController.Initialize();
        Logger.LogInfo("DisconnectMenu initialized.");
    }

    void Update()
    {
        menuController.Update();
    }
}