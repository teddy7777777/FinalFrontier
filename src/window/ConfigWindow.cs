using System;
using UnityEngine;
using FinalFrontierAdapter;
using KSPDev.GUIUtils;

namespace Nereid
{
   namespace FinalFrontier
   {
      class ConfigWindow : PositionableWindow
      {
            #region Localizable UI strings

            static readonly Message CloseButtonText = new Message("#FF_Button_Close", "Close");
            static readonly Message BrowseButtonText = new Message("#FF_Config_Browse", "Browse Ribbon Codes");
            static readonly Message TitleText = new Message("#FF_Config_Title", "Final Frontier Configuration");
            static readonly Message LogLevelText = new Message("#FF_Config_LogLevel", "Log Level");
            static readonly Message ResetWindowPostionsButtonText = new Message("#FF_Config_ResetWindowPostions", "Reset Window Positions");
            static readonly Message HallOfFameWindowTitleText = new Message("#FF_Config_HallOfFameWindowTitle", "Hall Of Fame window title");
            static readonly Message DecorationBoardWindowTitleText = new Message("#FF_Config_DecorationBoardWindowTitle", "Decoration Board window title");
            static readonly Message MissionSummaryWindowTitleText = new Message("#FF_Config_MissionSummaryWindowTitle", "Mission Summary window title");
            static readonly Message WindowTitlesNeedsRestartText = new Message("#FF_Config_WindowTitlesNeedsRestart", "(window titles needs a restart to take effect)");
            static readonly Message SettingsText = new Message("#FF_Config_Settings", "Settings");
            static readonly Message CustomRibbonAtSpaceCenterText = new Message("#FF_Config_CustomRibbonAtSpaceCenter", "Custom ribbons at space center");
            static readonly Message RevocationOfRibbonsText = new Message("#FF_Config_RevocationOfRibbons", "Revocation of ribbons enabled");
            static readonly Message AutoExpandText = new Message("#FF_Config_AutoExpand", "Expand ribbons in hall of fame");
            static readonly Message PermadeathText = new Message("#FF_Config_Permadeath", "Permadeath enabled");
            static readonly Message HotkeyText = new Message("#FF_Config_Hotkey", "Hotkey enabled");
            static readonly Message KerbinTimeText = new Message("#FF_Config_KerbinTime", "Use kerbin time");
            static readonly Message MissionSummaryText = new Message("#FF_Config_MissionSummary", "Show summary when vessel is recovered");
            static readonly Message UseStockToolbarText = new Message("#FF_Config_UseStockToolbarText", "Use Stock Toolbar (needs a restart to take effect)");
            static readonly Message UseFARCalculationsText = new Message("#FF_Config_UseFARCalculations", "Use FAR calculations");
            static readonly Message SqueezeSciencePointsText = new Message("#FF_Config_SqueezeSciencePoints", "Squeeze science points");
            static readonly Message AlwaysUseDirectTextureLoadText = new Message("#FF_Config_AlwaysUseDirectTextureLoad", "Allways use direct texture load (restart required)");
            static readonly Message LogRibbonAwardsText = new Message("#FF_Config_LogRibbonAwards", "Log ribbon awards");
            static readonly Message HotKeyLabelText = new Message("#FF_Config_HotKeyLabel", "Hotkey: LEFT-ALT + ");
            static readonly Message HotKeySelectorText = new Message("#FF_Config_HotKeySelector", "press key");

            #endregion

         private readonly CodeBrowser codeBrowser = new CodeBrowser();

         private GUIStyle STYLE_TEXTFIELD_WIDOWTITLE = new GUIStyle(HighLogic.Skin.textField);

         private bool hotkeyInput = false;

         public ConfigWindow()
            : base(Constants.WINDOW_ID_CONFIG, TitleText)
         {
            STYLE_TEXTFIELD_WIDOWTITLE.stretchWidth = false;
            STYLE_TEXTFIELD_WIDOWTITLE.fixedWidth = 190;
         }

         public override void SetVisible(bool visible)
         {
            if (!IsVisible() && visible) hotkeyInput = false;
            base.SetVisible(visible);
         }

         protected override void OnWindow(int id)
         {
            Configuration config = FinalFrontier.configuration;

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(BrowseButtonText, FFStyles.STYLE_BUTTON))
            {
               if (!codeBrowser.IsVisible()) MoveWindowAside(codeBrowser);
               codeBrowser.SetVisible(!codeBrowser.IsVisible());
            }

            GUILayout.FlexibleSpace();
            if (GUILayout.Button(CloseButtonText, FFStyles.STYLE_BUTTON))
            {
               SetVisible(false);
               config.Save();
            }
            GUILayout.EndHorizontal();
            GUILayout.Label(LogLevelText, FFStyles.STYLE_STRETCHEDLABEL);
            GUILayout.BeginHorizontal();
            LogLevelButton(Log.LEVEL.OFF, "OFF");
            LogLevelButton(Log.LEVEL.ERROR, "ERROR");
            LogLevelButton(Log.LEVEL.WARNING, "WARNING");
            LogLevelButton(Log.LEVEL.INFO, "INFO");
            LogLevelButton(Log.LEVEL.DETAIL, "DETAIL");
            LogLevelButton(Log.LEVEL.TRACE, "TRACE");
            GUILayout.EndHorizontal();
            // Reset Window Postions
            if (GUILayout.Button(ResetWindowPostionsButtonText, FFStyles.STYLE_BUTTON))
            {
               PositionableWindow.ResetAllWindowPositions();
            }
            // Window Titles
            GUILayout.BeginHorizontal();
            GUILayout.Label(HallOfFameWindowTitleText, FFStyles.STYLE_STRETCHEDLABEL);
            String hallOfFameWindowTitle = FinalFrontier.configuration.GetHallOfFameWindowTitle();
            hallOfFameWindowTitle = GUILayout.TextField(hallOfFameWindowTitle, STYLE_TEXTFIELD_WIDOWTITLE);
            FinalFrontier.configuration.SetHallOfFameWindowTitle(hallOfFameWindowTitle);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label(DecorationBoardWindowTitleText, FFStyles.STYLE_STRETCHEDLABEL);
            String decorationBoardWindowTitle = FinalFrontier.configuration.GetDecorationBoardWindowTitle();
            decorationBoardWindowTitle = GUILayout.TextField(decorationBoardWindowTitle, STYLE_TEXTFIELD_WIDOWTITLE);
            FinalFrontier.configuration.SetDecorationBoardWindowTitle(decorationBoardWindowTitle);
            GUILayout.EndHorizontal();
            // Mission-Summary is not working in KSP 1.2
            //GUILayout.BeginHorizontal();
            //GUILayout.Label(MissionSummaryWindowTitleText, FFStyles.STYLE_STRETCHEDLABEL);
            //String missionSummaryWindowTitle = FinalFrontier.configuration.GetMissionSummaryWindowTitle();
            //missionSummaryWindowTitle = GUILayout.TextField(missionSummaryWindowTitle, STYLE_TEXTFIELD_WIDOWTITLE);
            //FinalFrontier.configuration.SetMissionSummaryWindowTitle(missionSummaryWindowTitle);
            //GUILayout.EndHorizontal();
            GUILayout.Label(WindowTitlesNeedsRestartText, FFStyles.STYLE_RLABEL);
            //
            //
            GUILayout.Label(SettingsText, FFStyles.STYLE_STRETCHEDLABEL);
            // CUSTOM RIBBONS AT SPACE CENTER
            config.SetCustomRibbonAtSpaceCenterEnabled( GUILayout.Toggle(config.IsCustomRibbonAtSpaceCenterEnabled(), CustomRibbonAtSpaceCenterText, FFStyles.STYLE_TOGGLE) );
            // REVOCATION OF RIBBONS
            config.SetRevocationOfRibbonsEnabled (GUILayout.Toggle(config.IsRevocationOfRibbonsEnabled(), RevocationOfRibbonsText, FFStyles.STYLE_TOGGLE) );
            // AUTO EXPAND RIBBONS
            config.SetAutoExpandEnabled( GUILayout.Toggle(config.IsAutoExpandEnabled(), AutoExpandText, FFStyles.STYLE_TOGGLE) );
            // PERMADEATH
            GameUtils.SetPermadeathEnabled( GUILayout.Toggle(GameUtils.IsPermadeathEnabled(), PermadeathText, FFStyles.STYLE_TOGGLE) );
            // HOTKEY
            GUILayout.BeginHorizontal();
            config.SetHotkeyEnabled( GUILayout.Toggle(config.IsHotkeyEnabled(), HotkeyText, FFStyles.STYLE_TOGGLE) );
            GUILayout.FlexibleSpace();
            DrawHotKeyField();
            //GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            // KERBIN TIME
            GameUtils.SetKerbinTimeEnabled( GUILayout.Toggle(GameUtils.IsKerbinTimeEnabled(), KerbinTimeText, FFStyles.STYLE_TOGGLE) );
            // MISSION SUMMARY POPUP WINDOW 
            config.SetMissionSummaryEnabled(GUILayout.Toggle(config.IsMissionSummaryEnabled(), MissionSummaryText, FFStyles.STYLE_TOGGLE));
            // Stock Toolbar
            if(ToolbarManager.ToolbarAvailable)
            {
               config.SetUseStockToolbar(GUILayout.Toggle(config.UseStockToolbar(), UseStockToolbarText, FFStyles.STYLE_TOGGLE));
            }
            // FAR Calculations
            if(FinalFrontier.farAdapter.IsInstalled())
            {
               config.UseFARCalculations = GUILayout.Toggle(config.UseFARCalculations, UseFARCalculationsText, FFStyles.STYLE_TOGGLE);
            }
            // squeeze science points (just a single logbook entry per kerbal)
            config.squeezeSciencePoints = GUILayout.Toggle(config.squeezeSciencePoints, SqueezeSciencePointsText, FFStyles.STYLE_TOGGLE);
            // direct texture load
            config.alwaysUseDirectTextureLoad = GUILayout.Toggle(config.alwaysUseDirectTextureLoad, AlwaysUseDirectTextureLoadText, FFStyles.STYLE_TOGGLE);
            // Log Ribbon Aawrds
            config.logRibbonAwards = GUILayout.Toggle(config.logRibbonAwards, LogRibbonAwardsText, FFStyles.STYLE_TOGGLE);

            GUILayout.EndVertical();
            DragWindow();
         }

         private void DrawHotKeyField()
         {
            String text = hotkeyInput ? HotKeySelectorText.ToString() : FinalFrontier.configuration.hotkey.ToString();
            GUILayout.Label(HotKeyLabelText);
            if (GUILayout.Button(text, FFStyles.STYLE_BUTTON_HOTYKEY))
            {
               hotkeyInput = true;
            }
            if(hotkeyInput)
            {
               if (Input.anyKeyDown)
               {
                  hotkeyInput = false;
                  KeyCode[] keys = Utils.GetPressedKeys();
                  if (keys != null && keys.Length > 0)
                  {
                     FinalFrontier.configuration.hotkey = keys[0];
                  }
               }
            }
         }

         private void LogLevelButton(Log.LEVEL level, String text)
         {
            if (GUILayout.Toggle(Log.GetLevel() == level, text, FFStyles.STYLE_BUTTON) && Log.GetLevel() != level)
            {
               FinalFrontier.configuration.SetLogLevel(level);
               Log.SetLevel(level);
            }
         }
      }
   }
}
