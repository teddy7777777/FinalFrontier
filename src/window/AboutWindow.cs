using System;
using UnityEngine;
using KSPDev.GUIUtils;

namespace Nereid
{
   namespace FinalFrontier
   {
      class AboutWindow : PositionableWindow
      {
            #region Localizable UI strings

            static readonly Message CloseButtonText = new Message("#FF_Button_Close", "Close");
            static readonly Message TitleText = new Message("#FF_About_Title", "About");
            static readonly Message InfoLine1Text = new Message("#FF_About_InfoLine1", "Final Frontier - written by Nereid (A.Kolster)");
            static readonly Message InfoLine2Text = new Message("#FF_About_InfoLine2", "Some ribbons and graphics are inspired and/or created by Unistrut.");
            static readonly Message InfoLine3Text = new Message("#FF_About_InfoLine3", "The First-In-Space and First-EVA-In-Space ribbons are created by SmarterThanMe.");
            static readonly Message InfoLine4Text = new Message("#FF_About_InfoLine4", "The toolbar was created by blizzy78.");
            static readonly Message InfoLine5Text = new Message("#FF_About_InfoLine5", "Some custom ribbons are created/provided by nothke, SmarterThanMe, helldiver and Wyrmshadow.");
            static readonly Message InfoLine6Text = new Message("#FF_About_InfoLine6", "Special thanks to Unistrut for giving permissions to use his ribbon graphics.");
            static readonly Message InfoLine7Text = new Message("#FF_About_InfoLine7", "In memory of our beloved Cira. We will miss you.");
            
            #endregion

         public AboutWindow()
            : base(Constants.WINDOW_ID_ABOUT, TitleText)
         {

         }

         protected override void OnWindow(int id)
         {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(FFStyles.STYLE_RIBBON_DESCRIPTION);
            GUILayout.Label(InfoLine1Text, FFStyles.STYLE_STRETCHEDLABEL);
            GUILayout.Label("");
            GUILayout.Label(InfoLine2Text, FFStyles.STYLE_STRETCHEDLABEL);
            GUILayout.Label(InfoLine3Text, FFStyles.STYLE_STRETCHEDLABEL);
            GUILayout.Label(InfoLine4Text, FFStyles.STYLE_STRETCHEDLABEL);
            GUILayout.Label(InfoLine5Text, FFStyles.STYLE_STRETCHEDLABEL);
            GUILayout.Label("");
            GUILayout.Label(InfoLine6Text, FFStyles.STYLE_STRETCHEDLABEL);
            GUILayout.Label("");
            GUILayout.Label(InfoLine7Text);
            GUILayout.Label("");
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            if (GUILayout.Button(CloseButtonText, FFStyles.STYLE_BUTTON)) SetVisible(false);
            GUILayout.EndHorizontal();
            DragWindow();
         }

         public override int GetInitialWidth()
         {
            return 350;
         }
      }
   }
}
