using Monaco;
using Monaco.Editor;
using Monaco.Helpers;
using Monaco.Languages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using Windows.UI.Input.Preview.Injection;
using Windows.UI.Popups;
using Windows.UI.Shell;
using Windows.UI.StartScreen;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MUXC = Microsoft.UI.Xaml.Controls;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Code_editor_test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CodeSettingsPage : Page
    {
        Monaco.CodeEditor Editor = CodeEditor.UniversalEditor;
        Windows.Storage.ApplicationDataContainer localSettings;

        public CodeSettingsPage()
        {
            this.InitializeComponent();
            localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if(localSettings.Values["Automaticclosingbrackets"] == null)
            { 
            localSettings.Values["Automaticclosingbrackets"] = Automaticclosingbrackets.IsOn;
            localSettings.Values["Automaticindent"] = Automaticindent.IsOn;
            localSettings.Values["Codelens"] = Codelens.IsOn;
            localSettings.Values["Disablelayerhinting"] = Disablelayerhinting.IsOn;
            localSettings.Values["Disablemonospaceoptimizations"] = Disablemonospaceoptimizations.IsOn;
            localSettings.Values["Draganddrop"] = Draganddrop.IsOn;
            localSettings.Values["Emptyselectionclipboard"] = Emptyselectionclipboard.IsOn;
            localSettings.Values["Fixedoverflowwidgets"] = Fixedoverflowwidgets.IsOn;
            localSettings.Values["Fontligatures"] = Fontligatures.IsOn;
            localSettings.Values["Formatonpaste"] = Formatonpaste.IsOn;
            localSettings.Values["Glyphmargin"] = Glyphmargin.IsOn;
            localSettings.Values["Hidecursorinoverviewruler"] = Hidecursorinoverviewruler.IsOn;
            localSettings.Values["Hover"] = Hover.IsOn;
            localSettings.Values["Iconsinsuggestions"] = Iconsinsuggestions.IsOn;
                localSettings.Values["Links"] = Links.IsOn;
            localSettings.Values["Matchbrackets"] = Matchbrackets.IsOn;
            localSettings.Values["Mousewheelzoom"] = Mousewheelzoom.IsOn;
            localSettings.Values["Togglecodefolding"] = Togglecodefolding.IsOn;
            localSettings.Values["Occurenceshighlight"] = Occurenceshighlight.IsOn;
            localSettings.Values["Overviewrulerborder"] = Overviewrulerborder.IsOn;
            localSettings.Values["Parameterhints"] = Parameterhints.IsOn;
            localSettings.Values["Quicksuggestions"] = Quicksuggestions.IsOn;
            localSettings.Values["RenderControlCharacters"] = RenderControlCharacters.IsOn;
            localSettings.Values["RenderIndentGuides"] = RenderIndentGuides.IsOn;
            localSettings.Values["RoundedSelection"] = RoundedSelection.IsOn;
            localSettings.Values["ScrollBeyondLastLine"] = ScrollBeyondLastLine.IsOn;
            localSettings.Values["Selectionclipboard"] = Selectionclipboard.IsOn;
            localSettings.Values["Selectionhighlight"] = Selectionhighlight.IsOn;
            localSettings.Values["Selectonlinenumbers"] = Selectonlinenumbers.IsOn;
            localSettings.Values["Suggestontriggercharacters"] = Suggestontriggercharacters.IsOn;
            localSettings.Values["UseTabStops"] = UseTabStops.IsOn;
            localSettings.Values["WordBasedSuggestions"] = WordBasedSuggestions.IsOn;
            localSettings.Values["WordwrapMinified"] = WordwrapMinified.IsOn;
        }
        else
        {
                Automaticclosingbrackets.IsOn = (bool) localSettings.Values["Automaticclosingbrackets"];
                Automaticindent.IsOn = (bool) localSettings.Values["Automaticindent"];
                Codelens.IsOn = (bool) localSettings.Values["Codelens"];
             /*   Disablelayerhinting.IsOn = (bool) localSettings.Values["Disablelayerhinting"];
                Disablemonospaceoptimizations.IsOn = (bool) localSettings.Values["Disablemonospaceoptimizations"];*/
                Draganddrop.IsOn = (bool) localSettings.Values["Draganddrop"];
                Emptyselectionclipboard.IsOn = (bool) localSettings.Values["Emptyselectionclipboard"];
                Fixedoverflowwidgets.IsOn = (bool) localSettings.Values["Fixedoverflowwidgets"];
                Fontligatures.IsOn = (bool) localSettings.Values["Fontligatures"];
                Formatonpaste.IsOn = (bool) localSettings.Values["Formatonpaste"];
               // Glyphmargin.IsOn = (bool) localSettings.Values["Glyphmargin"];
                Hidecursorinoverviewruler.IsOn = (bool) localSettings.Values["Hidecursorinoverviewruler"];
                Hover.IsOn = (bool) localSettings.Values["Hover"];
                Iconsinsuggestions.IsOn = (bool) localSettings.Values["Iconsinsuggestions"];
                Links.IsOn = (bool) localSettings.Values["Links"];
                Matchbrackets.IsOn = (bool) localSettings.Values["Matchbrackets"];
               Mousewheelzoom.IsOn = (bool) localSettings.Values["Mousewheelzoom"];
                Togglecodefolding.IsOn = (bool) localSettings.Values["Togglecodefolding"];
              //  Occurenceshighlight.IsOn = (bool) localSettings.Values["Occurenceshighlight"];
                Overviewrulerborder.IsOn = (bool) localSettings.Values["Overviewrulerborder"];
                Parameterhints.IsOn = (bool) localSettings.Values["Parameterhints"];
                Quicksuggestions.IsOn = (bool) localSettings.Values["Quicksuggestions"];
                RenderControlCharacters.IsOn = (bool) localSettings.Values["RenderControlCharacters"];
                RenderIndentGuides.IsOn = (bool) localSettings.Values["RenderIndentGuides"];
                RoundedSelection.IsOn = (bool) localSettings.Values["RoundedSelection"];
               ScrollBeyondLastLine.IsOn = (bool) localSettings.Values["ScrollBeyondLastLine"];
                Selectionclipboard.IsOn = (bool) localSettings.Values["Selectionclipboard"];
                Selectionhighlight.IsOn = (bool) localSettings.Values["Selectionhighlight"];
                Selectonlinenumbers.IsOn = (bool) localSettings.Values["Selectonlinenumbers"];
                Suggestontriggercharacters.IsOn = (bool) localSettings.Values["Suggestontriggercharacters"];
                UseTabStops.IsOn = (bool) localSettings.Values["UseTabStops"];
                WordBasedSuggestions.IsOn = (bool) localSettings.Values["WordBasedSuggestions"];
                WordwrapMinified.IsOn = (bool) localSettings.Values["WordwrapMinified"];
            }
        }
        private async void FeedbackLink_Click(object sender, RoutedEventArgs e)

        {

            //This launcher is part of the Store Services SDK https://docs.microsoft.com/en-us/windows/uwp/monetize/microsoft-store-services-sdk

            var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();

            await launcher.LaunchAsync();

        }

        private async void Rate_Click(object sender, RoutedEventArgs e)

        {

            await Launcher.LaunchUriAsync(

    new Uri($"ms-windows-store://review/?PFN={Package.Current.Id.FamilyName}"));

        }

        private async void PinAppToTaskbar_Click(object sender, RoutedEventArgs e)

        {

            bool isPinningAllowed = TaskbarManager.GetDefault().IsPinningAllowed;

            if (isPinningAllowed)

            {

                if (ApiInformation.IsTypePresent("Windows.UI.Shell.TaskbarManager"))

                {

                    bool isPinned = await TaskbarManager.GetDefault().IsCurrentAppPinnedAsync();



                    if (isPinned)

                    {

                        await new MessageDialog("If not you can manually pin the app to the taskbar", "You already have the app pinned in your taskbar").ShowAsync();

                    }

                    else

                    {

                        bool IsPinned = await TaskbarManager.GetDefault().RequestPinCurrentAppAsync();

                    }

                }



                else

                {

                    await new MessageDialog("Update your device to the Fall creators update or higher to pin this app", "Update your device").ShowAsync();

                }

            }







            else

            {

                var t = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;

                switch (t)

                {

                    case "Windows.Desktop":

                        await new MessageDialog("It seems you are using a computer. Group policy disabled pinning of app in taskbar", "Taskbar pin failed").ShowAsync();

                        break;

                    case "Windows.Mobile":

                        await new MessageDialog("It seems you are using a Windows 10 on ARM device or mobile device. Group policy disabled pinning of the app", "Taskbar pin failed").ShowAsync();

                        break;

                    case "Windows.IoT":

                        await new MessageDialog("It seems you are using a IoT device which doesn't support taskbar pin API", "Taskbar pin failed").ShowAsync();

                        break;

                    case "Windows.Team":

                        break;

                    case "Windows.Holographic":

                        await new MessageDialog("It seems you are using hololens. Hololens doesn't have a taskbar", "Taskbar pin failed").ShowAsync();

                        break;

                    case "Windows.Xbox":

                        await new MessageDialog("It seems you are using a xbox. Xbox doesn't have a taskbar", "Taskbar pin failed").ShowAsync();

                        break;

                    default:

                        await new MessageDialog("It seems you are using a " + t + " device. This device does not support taskbar API or Group policy disabled pinning of the app", "Taskbar pin failed").ShowAsync();

                        break;

                }

            }

        }

        private async void LivePin(object sender, RoutedEventArgs e)

        {

            // Get your own app list entry

            // (which is always the first app list entry assuming you are not a multi-app package)

            AppListEntry entry = (await Package.Current.GetAppListEntriesAsync())[0];



            // Check if Start supports your app

            bool isSupported = StartScreenManager.GetDefault().SupportsAppListEntry(entry);

            if (isSupported)

            {

                if (ApiInformation.IsTypePresent("Windows.UI.StartScreen.StartScreenManager"))

                {

                    // Primary tile API's supported!

                    bool isPinned = await StartScreenManager.GetDefault().ContainsAppListEntryAsync(entry);

                    if (isPinned)

                    {

                        await new MessageDialog("If not you can manually put the live tile on to the StartScreen", "You already have the live tile in your StartScreen").ShowAsync();

                    }

                    else

                    {

                        bool IsPinned = await StartScreenManager.GetDefault().RequestAddAppListEntryAsync(entry);

                    }

                }

                else

                {

                    await new MessageDialog("You need to update your device to enable automatic pinning", "Update your device").ShowAsync();

                }

            }

            else

            {

                var t = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;

                switch (t)

                {

                    case "Windows.IoT":

                        await new MessageDialog("It seems you are using a IoT device which doesn't support Primary tile API", "live tile failed").ShowAsync();

                        break;

                    case "Windows.Team":

                        break;

                    case "Windows.Holographic":

                        await new MessageDialog("It seems you are using hololens. Hololens doesn't support live tile", "live tile failed").ShowAsync();

                        break;

                    case "Windows.Xbox":

                        await new MessageDialog("It seems you are using a xbox. Xbox doesn't support live tile", "live tile failed").ShowAsync();

                        break;

                    default:

                        await new MessageDialog("It seems you are using a " + t + " device. This device does not support Primary tile API", "live tile failed").ShowAsync();

                        break;

                }

            }

        }
        private void ButtonClearHighlights_Click(object sender, RoutedEventArgs e)
        {
            Editor.Decorations.Clear();
        }
        private void ButtonFolding_Click(object sender, RoutedEventArgs e)
        {
            Editor.Options.Folding = !Editor.Options.Folding ?? true;
            CodeEditor.UniversalEditor.Focus(FocusState.Programmatic);
        }

        private void ButtonChangeLanguage_Click(object sender, RoutedEventArgs e)

        {

            Editor.CodeLanguage = (Editor.CodeLanguage == "csharp") ? "xml" : "csharp";
            Editor.Options.ContextMenu = true;
        }



        //ok so i made this at 2:52 p.m which is the latest i have ever coded because i sleep so this is weird but it works 
        private void Universal_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggle = sender as ToggleSwitch;
            String Name = toggle.Name;
            localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
           /* Boolean Toggle;
            if(toggle.IsOn == true)
            {
                Toggle = true;
            }
            else
            {
                Toggle = false;
            }*/
            localSettings.Values[Name] = toggle.IsOn;
            switch (Name)
            {
                case "Autoclosingbrackets":
                    CodeEditor.UniversalEditor.Options.AutoClosingBrackets = toggle.IsOn;
                    break;
                case "Automaticindent":
                    CodeEditor.UniversalEditor.Options.AutoIndent = toggle.IsOn;
                    break;
                case "Codelens":
                    CodeEditor.UniversalEditor.Options.CodeLens = toggle.IsOn;
                    break;
                case "Disablelayerhinting":
                    CodeEditor.UniversalEditor.Options.DisableLayerHinting = toggle.IsOn;
                    break;
                case "Disablemonospaceoptimizations":
                    CodeEditor.UniversalEditor.Options.DisableMonospaceOptimizations = toggle.IsOn;
                    break;
                case "Draganddrop":
                    CodeEditor.UniversalEditor.Options.DragAndDrop = toggle.IsOn;
                    break;
                case "Emptyselectionclipboard":
                    CodeEditor.UniversalEditor.Options.EmptySelectionClipboard = toggle.IsOn;
                    break;
                case "Fixedoverflowwidgets":
                    CodeEditor.UniversalEditor.Options.FixedOverflowWidgets = toggle.IsOn;
                    break;
                case "Fontligatures":
                    CodeEditor.UniversalEditor.Options.FontLigatures = toggle.IsOn;
                    break;
                case "Formatonpaste":
                    CodeEditor.UniversalEditor.Options.FormatOnPaste = toggle.IsOn;
                    break;
                case "Glyphmargin":
                    CodeEditor.UniversalEditor.Options.GlyphMargin = toggle.IsOn;
                    break;
                case "Hidecursorinoverviewruler":
                    CodeEditor.UniversalEditor.Options.HideCursorInOverviewRuler = toggle.IsOn;
                    break;
                case "Hover":
                    CodeEditor.UniversalEditor.Options.Hover = toggle.IsOn;
                    break;
                case "Iconsinsuggestions":
                    CodeEditor.UniversalEditor.Options.IconsInSuggestions = toggle.IsOn;
                    break;
                case "Links":
                    CodeEditor.UniversalEditor.Options.Links = toggle.IsOn;
                    break;
                case "Matchbrackets":
                    CodeEditor.UniversalEditor.Options.MatchBrackets = toggle.IsOn;
                    break;
                case "Mousewheelzoom":
                    CodeEditor.UniversalEditor.Options.MouseWheelZoom = toggle.IsOn;
                    break;
                case "Occurenceshighlight":
                    CodeEditor.UniversalEditor.Options.OccurrencesHighlight = toggle.IsOn;
                    break;
                case "Overviewrulerborder":
                    CodeEditor.UniversalEditor.Options.OverviewRulerBorder = toggle.IsOn;
                    break;
                case "Parameterhints":
                    CodeEditor.UniversalEditor.Options.ParameterHints = toggle.IsOn;
                    break;
                case "Quicksuggestions":
                    CodeEditor.UniversalEditor.Options.QuickSuggestions = toggle.IsOn;
                    break;
                case "RenderControlCharacters":
                    CodeEditor.UniversalEditor.Options.RenderControlCharacters = toggle.IsOn;
                    break;
                case "RenderIndentGuides":
                    CodeEditor.UniversalEditor.Options.RenderIndentGuides = toggle.IsOn;
                    break;
                case "RoundedSelection":
                    CodeEditor.UniversalEditor.Options.RoundedSelection = toggle.IsOn;
                    break;
                case "ScrollBeyondLastLine":
                    CodeEditor.UniversalEditor.Options.ScrollBeyondLastLine = toggle.IsOn;
                    break;
                case "Selectionclipboard":
                    CodeEditor.UniversalEditor.Options.SelectionClipboard = toggle.IsOn;
                    break;
                case "Suggestontriggercharacters":
                    CodeEditor.UniversalEditor.Options.SuggestOnTriggerCharacters = toggle.IsOn;
                    break;
                case "Usetabstops":
                    CodeEditor.UniversalEditor.Options.UseTabStops = toggle.IsOn;
                    break;
                case "Wordbasedsuggestions":
                    CodeEditor.UniversalEditor.Options.WordBasedSuggestions = toggle.IsOn;
                    break;
                case "Wordwrapminified":
                    CodeEditor.UniversalEditor.Options.WordWrapMinified= toggle.IsOn;
                    break;
                case "Togglecodefolding":
                    CodeEditor.UniversalEditor.Options.Folding = toggle.IsOn;
                    break;
            }
        }
    }
}
