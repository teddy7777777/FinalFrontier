using System;
using UnityEngine;
using KSPDev.GUIUtils;


namespace Nereid
{
   namespace FinalFrontier
   {
      class RibbonBrowser : AbstractWindow
      {
            #region Localizable UI strings

            static readonly Message EnableAllButtonText = new Message("#FF_RibbonBrowser_EnableAll", "Enable all");
            static readonly Message CloseButtonText = new Message("#FF_Button_Close", "Close");
            static readonly Message SearchText = new Message("#FF_RibbonBrowser_Search", "Search");
            static readonly Message TitleText = new Message("#FF_RibbonBrowser_Title", "Ribbons");
            static readonly Message NoneMatchText = new Message("#FF_RibbonBrowser_None", "NONE");
            static readonly Message NoRibbonsFoundText = new Message("#FF_RibbonBrowser_NoRibbonsFound", "no ribbons found");
            static readonly Message<String, String> RibbonDescriptionText = new Message<String, String>("#FF_RibbonBrowser_RibbonDescription", "<<1>>: <<2>>");
            static readonly Message<int, int> RibbonsCountText = new Message<int, int>("#FF_RibbonBrowser_RibbonsCount", "<<1>> ribbons in total (<<2>> custom ribbons)");
            
            #endregion

            private Vector2 scrollPosition = Vector2.zero;

         public static int WIDTH = 555;
         public static int HEIGHT = 600;

         private String search = "";


         public RibbonBrowser()
            : base(Constants.WINDOW_ID_RIBBONBROWSER, TitleText)
         {


         }

         protected override void OnWindow(int id)
         {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(EnableAllButtonText, FFStyles.STYLE_BUTTON))
            {
               FinalFrontier.configuration.EnableAllRibbons();
            }

            GUILayout.FlexibleSpace(); // Button(RibbonsText, GUIStyles.STYLE_LABEL);
            if (GUILayout.Button(CloseButtonText, FFStyles.STYLE_BUTTON))
            {
               SetVisible(false);
               // save configuration in case a ribbon was enabled/disabled
               FinalFrontier.configuration.Save();
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label(SearchText, HighLogic.Skin.label);
            search = GUILayout.TextField(search, FFStyles.STYLE_STRETCHEDTEXTFIELD);
            GUILayout.EndHorizontal();
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, FFStyles.STYLE_SCROLLVIEW, GUILayout.Height(HEIGHT));
            GUILayout.BeginVertical();
            int ribbonsFound = 0;
            foreach (Ribbon ribbon in RibbonPool.Instance())
            {
               String name = ribbon.GetName();
               String description = ribbon.GetDescription();
               if (search == null || search.Trim().Length == 0 || name.ContainsIgnoringCase(search) || description.ContainsIgnoringCase(search))
               {
                  GUILayout.BeginHorizontal(FFStyles.STYLE_RIBBON_AREA);
                  bool enabled = ribbon.enabled;
                  if(GUILayout.Toggle(enabled, "" , FFStyles.STYLE_NARROW_TOGGLE)!=enabled)
                  {
                     FinalFrontier.configuration.SetRibbonState(ribbon.GetCode(), !enabled);
                  }
                  GUILayout.Label(ribbon.GetTexture(), FFStyles.STYLE_SINGLE_RIBBON);
                  GUILayout.Label(RibbonDescriptionText.Format(name, description), FFStyles.STYLE_RIBBON_DESCRIPTION);
                  GUILayout.EndHorizontal();
                  ribbonsFound++;
               }
            }
            // no ribbons match search criteria
            if(ribbonsFound == 0)
            {
               GUILayout.BeginHorizontal(FFStyles.STYLE_RIBBON_AREA);
               GUILayout.Label(NoneMatchText, FFStyles.STYLE_SINGLE_RIBBON);
               GUILayout.Label(NoRibbonsFoundText, FFStyles.STYLE_RIBBON_DESCRIPTION);
               GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.Label(RibbonsCountText.Format(RibbonPool.Instance().Count(), RibbonPool.Instance().GetCustomRibbons().Count), FFStyles.STYLE_STRETCHEDLABEL);
           
            GUILayout.EndVertical();

            DragWindow();
         }

         public override int GetInitialWidth()
         {
            return WIDTH;
         }
      }
   }
}