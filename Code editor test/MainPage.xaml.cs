using Microsoft.UI.Xaml.Controls;
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
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using Windows.UI.Input.Preview.Injection;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MUXC = Microsoft.UI.Xaml.Controls;

namespace Code_editor_test
{
    public sealed partial class MainPage : Page
    {
    public static TextBlock UniversalStatusText { get; set; }
        public static MainPage TheimportantPage { get; set; }
        public static TabView TabsMain { get; set; }
        Point pointerPosition;
        Windows.Storage.ApplicationDataContainer localSettings;
        public MainPage()
        {
            this.InitializeComponent();
            
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
             Window.Current.SetTitleBar(TitleGrid);
            localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            UniversalStatusText = StatusText;
            TheimportantPage = TheMainPage as MainPage;
            InputPane currentInputPane = InputPane.GetForCurrentView();
            currentInputPane.Showing += OnShowing;
            currentInputPane.Hiding += OnHiding;
            TabsMain = Tabs;
            pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
        }
        public void Startup()
        {
            FindName("EditorNav");
            FindName("CodeMenuBar");
            FindName("CodeBar");
            CurlyBracketButton.Content = "{";
            CurlyBracketButtonEnd.Content = "}";
            OtherbracesButton.Content = "<";
            OtherbracesButtonEnd.Content = ">";
            OtherbracesButtonAlternativeEnd.Content = "/>";
            AndButton.Content = "&";
            var newTab = new TabViewItem();
            newTab.IconSource = new MUXC.SymbolIconSource() { Symbol = Symbol.Document };
            newTab.Header = "New Tab";
            Frame frame = new Frame();
            newTab.Content = frame;
            frame.Navigate(typeof(CodeEditor));
            Tabs.TabItems.Add(newTab);
        }
        public void OnShowing(InputPane sender, InputPaneVisibilityEventArgs e)
        {
            Thickness thickness = CodeBar.Margin;
            thickness.Bottom = 350;
            CodeBar.Margin = thickness;
            StatusTextGrid.VerticalAlignment = VerticalAlignment.Center;
        }

       public  void OnHiding(InputPane sender, InputPaneVisibilityEventArgs e)
        {
            Thickness thickness = CodeBar.Margin;
            thickness.Bottom = 0;
            CodeBar.Margin = thickness;
            StatusTextGrid.VerticalAlignment = VerticalAlignment.Bottom;
        }
        private void NavView_SelectionChanged(Windows.UI.Xaml.Controls.NavigationView sender, Windows.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            Windows.UI.Xaml.Controls.NavigationViewItem e = sender.SelectedItem as Windows.UI.Xaml.Controls.NavigationViewItem;
            String Name = e.Name.ToString();
            switch (Name)
            {   
                case "SearchFrame":
                    contentFrame.Navigate(typeof(SearchPage));
                    break;
                case "SettingsPage":
                    contentFrame.Navigate(typeof(CodeSettingsPage));
                    break;
                case "DocumentFrame":
                    contentFrame.Navigate(typeof(DocumentPage));
                    break;         
            }

        }
        public void MyFancyPanel_BackdropTapped(object sender, EventArgs e)
        {
            UnloadObject(SettingsPanel);
        }
        private void EditorNav_Loaded(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(SearchPage)); 
            EditorNav.SelectedItem = EditorNav.MenuItems[1];
        }
        public async Task NavigateMouse()
        {
            CodeEditor.UniversalEditor.Focus(FocusState.Pointer);
            await Task.Delay(100);
        }

        private async void UniversalMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            var x = pointerPosition.X;
            var y = pointerPosition.Y;
            VirtualKey Key = VirtualKey.H;
            MenuFlyoutItem Item = sender as MenuFlyoutItem;
            string Name = Item.Text.ToString();
            switch(Name)
            {
                case "Undo":
                    Key = VirtualKey.Z;
                    break;
                case "Redo":
                    Key = VirtualKey.Y;
                    break;
                case "Find":
                    Key = VirtualKey.F;
                    break;
                case "Replace":
                    Key = VirtualKey.H;
                    break;
                case "Select All":
                    Key = VirtualKey.A;
                    break;
                case "Cut":
                    Key = VirtualKey.X;
                    break;
                case "Copy":
                    Key = VirtualKey.C;
                    break;
                case "Paste":
                    Key = VirtualKey.V;
                    break;
            }
            await NavigateMouse();
            ////
            InputInjector inputInjector = InputInjector.TryCreate();
            var shift = new InjectedInputKeyboardInfo();
            shift.VirtualKey = (ushort)(VirtualKey.LeftControl);
            shift.KeyOptions = InjectedInputKeyOptions.None;

            ////
            var tab = new InjectedInputKeyboardInfo();
            tab.VirtualKey = (ushort)Key;
            tab.KeyOptions = InjectedInputKeyOptions.None;
            inputInjector.InjectKeyboardInput(new[] { shift, tab });
            ////
            inputInjector.InjectKeyboardInput(new[] { shift, tab });
            shift.KeyOptions = InjectedInputKeyOptions.KeyUp;
            inputInjector.InjectKeyboardInput(new[] { shift });
           // Window.Current.CoreWindow.PointerPosition = new Point(x, y);
        }
        private async void UniversalSelectionMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            var x = pointerPosition.X;
            var y = pointerPosition.Y;
            MenuFlyoutItem Item = sender as MenuFlyoutItem;
            string Tag = Item.Tag.ToString();
            VirtualKey FirstKeyV = VirtualKey.A;
            VirtualKey SecondKeyV = VirtualKey.Menu;
            VirtualKey ThirdKeyV = VirtualKey.A;
            if (Tag.Contains("Ctrl"))
            {
                FirstKeyV = VirtualKey.LeftControl;
            }
            else if(Tag.Contains("Shift"))
            {
                FirstKeyV = VirtualKey.Shift;
            }
            else
            {
                FirstKeyV = VirtualKey.None;
            }

            if(Tag.Contains("UpArrow"))
            {
                ThirdKeyV = VirtualKey.Up;
            }
            else if (Tag.Contains("DownArrow"))
            {
                ThirdKeyV = VirtualKey.Down;
            }
            else if (Tag.Contains("RightArrow"))
            {
                ThirdKeyV = VirtualKey.Down;
            }
            else if (Tag.Contains("L"))
            {
                ThirdKeyV = VirtualKey.L;
            }
            else if (Tag.Contains("I"))
            {
                ThirdKeyV = VirtualKey.I;
            }
            else
            {
                ThirdKeyV = VirtualKey.Left;
            }
            if(Tag.Contains("Shift") && FirstKeyV == VirtualKey.LeftControl)
            {
                SecondKeyV = VirtualKey.RightShift;
            }
            await NavigateMouse();
            ////
            InputInjector inputInjector = InputInjector.TryCreate();
            var FirstKey = new InjectedInputKeyboardInfo();
            FirstKey.VirtualKey = (ushort)FirstKeyV;
            FirstKey.KeyOptions = InjectedInputKeyOptions.None;

            ////
            var SecondKey = new InjectedInputKeyboardInfo();
            SecondKey.VirtualKey = (ushort)SecondKeyV;
            SecondKey.KeyOptions = InjectedInputKeyOptions.None;
            var ThirdKey = new InjectedInputKeyboardInfo();
            ThirdKey.VirtualKey = (ushort)ThirdKeyV;
            ThirdKey.KeyOptions = InjectedInputKeyOptions.None;
            inputInjector.InjectKeyboardInput(new[] { FirstKey, SecondKey, ThirdKey });
            ////
            inputInjector.InjectKeyboardInput(new[] { FirstKey, SecondKey, ThirdKey });
            FirstKey.KeyOptions = InjectedInputKeyOptions.KeyUp;
            SecondKey.KeyOptions = InjectedInputKeyOptions.KeyUp;
            ThirdKey.KeyOptions = InjectedInputKeyOptions.KeyUp;
            inputInjector.InjectKeyboardInput(new[] { FirstKey, SecondKey, ThirdKey });
          //  Window.Current.CoreWindow.PointerPosition = new Point(x, y);
        }
        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            var x = pointerPosition.X;
            var y = pointerPosition.Y;
            await NavigateMouse();
            InputInjector inputInjector = InputInjector.TryCreate();
            var shift = new InjectedInputKeyboardInfo();
            shift.VirtualKey = (ushort)(VirtualKey.Shift);
            shift.KeyOptions = InjectedInputKeyOptions.None;

            ////
            var tab = new InjectedInputKeyboardInfo();
            tab.VirtualKey = (ushort)(VirtualKey.Menu);
            tab.KeyOptions = InjectedInputKeyOptions.None;

            var Tab = new InjectedInputKeyboardInfo();
            Tab.VirtualKey = (ushort)(VirtualKey.A);
            Tab.KeyOptions = InjectedInputKeyOptions.None;

            inputInjector.InjectKeyboardInput(new[] { shift, tab, Tab });
            InputInjector iiinputInjector = InputInjector.TryCreate();
      
            shift.KeyOptions = InjectedInputKeyOptions.KeyUp;
            tab.KeyOptions = InjectedInputKeyOptions.KeyUp;
            Tab.KeyOptions = InjectedInputKeyOptions.KeyUp;
            inputInjector.InjectKeyboardInput(new[] { shift, tab, Tab });
          //  Window.Current.CoreWindow.PointerPosition = new Point(x, y);

        }

        private async void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            var x = pointerPosition.X;
            var y = pointerPosition.Y;
            await NavigateMouse();
            InputInjector inputInjector = InputInjector.TryCreate();
            var info = new InjectedInputKeyboardInfo();
            info.VirtualKey = (ushort)((VirtualKey)Enum.Parse(typeof(VirtualKey),"193", true));
            info.KeyOptions = InjectedInputKeyOptions.None;
            var tab = new InjectedInputKeyboardInfo();
            tab.VirtualKey = (ushort)VirtualKey.LeftControl;
            tab.KeyOptions = InjectedInputKeyOptions.None;
            inputInjector.InjectKeyboardInput(new[] { tab, info });
            tab.KeyOptions = InjectedInputKeyOptions.KeyUp;

            //and generate the key up event next, doing it this way avoids
            //the need for a delay like in Martin's sample code.
            info.KeyOptions = InjectedInputKeyOptions.KeyUp;
                inputInjector.InjectKeyboardInput(new[] { info, tab });
           // Window.Current.CoreWindow.PointerPosition = new Point(x, y);
        }

        private async  void UniversalToolBarButton_Click(object sender, RoutedEventArgs e)
        {
            var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            var x = pointerPosition.X;
            var y = pointerPosition.Y;
            string TextSelected;
            Button b = sender as Button;
            TextSelected = b.Content.ToString();          
            CodeEditor.UniversalEditor.SelectedText = TextSelected;
            await NavigateMouse();
            InputInjector inputInjector = InputInjector.TryCreate();
            var tab = new InjectedInputKeyboardInfo();
            tab.VirtualKey = (ushort)VirtualKey.Left;
            tab.KeyOptions = InjectedInputKeyOptions.None;
            inputInjector.InjectKeyboardInput(new[] { tab });
            tab.KeyOptions = InjectedInputKeyOptions.KeyUp;
            inputInjector.InjectKeyboardInput(new[] { tab });
        }
        // Remove the requested tab from the TabView
        private void Tabs_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            if (sender.TabItems.Count == 1)
            {
                sender.TabItems.Remove(args.Tab);
                var newTab = new TabViewItem();
                newTab.IconSource = new MUXC.SymbolIconSource() { Symbol = Symbol.Document };
                newTab.Header = "New Tab";
                newTab.IsTapEnabled = true;
                Frame frame = new Frame();
                newTab.Content = frame;
                frame.Navigate(typeof(CodeEditor));
                sender.TabItems.Add(newTab);
            }
            else
            {
                sender.TabItems.Remove(args.Tab);
            }              
        }

        private async void TabView_AddTabButtonClick(TabView sender, object args)
        {
            if(sender.TabItems.Count < 3)
            { 
            var newTab = new TabViewItem();
            newTab.IconSource = new MUXC.SymbolIconSource() { Symbol = Symbol.Document };
            newTab.Header = "New Tab";
            newTab.IsTapEnabled = true;
            Frame frame = new Frame();
            newTab.Content = frame;
            frame.Navigate(typeof(CodeEditor));
            sender.TabItems.Add(newTab);
            }
            else
            {
                var messageDialog = new MessageDialog("Maximum tab limit (only beta version)");
                    await messageDialog.ShowAsync(); 
            }
        }

        private async void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            var x = pointerPosition.X;
            var y = pointerPosition.Y;
            await NavigateMouse();
            InputInjector inputInjector = InputInjector.TryCreate();
            var info = new InjectedInputKeyboardInfo();
            info.VirtualKey = (ushort)VirtualKey.RightShift;
            info.KeyOptions = InjectedInputKeyOptions.None;
            var tab = new InjectedInputKeyboardInfo();
            tab.VirtualKey = (ushort)VirtualKey.F10;
            tab.KeyOptions = InjectedInputKeyOptions.None;
            inputInjector.InjectKeyboardInput(new[] { tab, info });
            tab.KeyOptions = InjectedInputKeyOptions.KeyUp;

            //and generate the key up event next, doing it this way avoids
            //the need for a delay like in Martin's sample code.
            info.KeyOptions = InjectedInputKeyOptions.KeyUp;
            inputInjector.InjectKeyboardInput(new[] { info, tab });
          //  Window.Current.CoreWindow.PointerPosition = new Point(x, y);
        }

        private async void Tab_Click(object sender, RoutedEventArgs e)
        {
              var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
                  var x = pointerPosition.X;
                  var y = pointerPosition.Y;
              await NavigateMouse();
              InputInjector inputInjector = InputInjector.TryCreate();
              var tab = new InjectedInputKeyboardInfo();
              tab.VirtualKey = (ushort)VirtualKey.Tab;
              tab.KeyOptions = InjectedInputKeyOptions.None;
              inputInjector.InjectKeyboardInput(new[] { tab });
              tab.KeyOptions = InjectedInputKeyOptions.KeyUp;
              inputInjector.InjectKeyboardInput(new[] { tab });
            //  Window.Current.CoreWindow.PointerPosition = new Point(x, y);
        }


        private async void StatusTextGrid_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000);
            Startup();
        }

        private async void OpenFileItem_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".js");
            picker.FileTypeFilter.Add(".cs");
            picker.FileTypeFilter.Add(".html");
            picker.FileTypeFilter.Add(".scss");
            picker.FileTypeFilter.Add(".fs");
            picker.FileTypeFilter.Add(".css");
            picker.FileTypeFilter.Add(".xml");
            picker.FileTypeFilter.Add(".ts");
            picker.FileTypeFilter.Add(".json");
            picker.FileTypeFilter.Add(".py");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
               CodeEditor.UniversalEditor.Text = await FileIO.ReadTextAsync(file);
                CodeEditor.UniversalEditor.Text = await FileIO.ReadTextAsync(file);
                CodeEditor.LocalFile = file;
                TabViewItem ee = Tabs.SelectedItem as TabViewItem;
                ee.Header = file.Name;
            }
            else
            {
                var messageDialog = new MessageDialog("Cancelled");
                await messageDialog.ShowAsync();
            }
        }

        private async void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            var x = pointerPosition.X;
            var y = pointerPosition.Y;
            if(UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Touch)
            { 
              var X = Window.Current.Bounds.Width - 50;
              var Y = Window.Current.Bounds.Height - 800;
         Window.Current.CoreWindow.PointerPosition = new Point(X, Y);
          var down = new InjectedInputMouseInfo();
           down.MouseOptions = InjectedInputMouseOptions.LeftDown;
               var up = new InjectedInputMouseInfo();
                up.MouseOptions = InjectedInputMouseOptions.LeftUp;
                  InputInjector IIInputInjector = InputInjector.TryCreate();
               IIInputInjector.InjectMouseInput(new[] { down, up });
               await Task.Delay(100);
                IIInputInjector.InjectMouseInput(new[] { down, up });
            Window.Current.CoreWindow.PointerPosition = new Point(x, y);
            }
            else
            {
                await Task.Delay(150);
                Window.Current.CoreWindow.PointerPosition = new Point(500, 500);
                var down = new InjectedInputMouseInfo();
                down.MouseOptions = InjectedInputMouseOptions.LeftDown;
                var up = new InjectedInputMouseInfo();
                up.MouseOptions = InjectedInputMouseOptions.LeftUp;
                InputInjector IIInputInjector = InputInjector.TryCreate();
                IIInputInjector.InjectMouseInput(new[] { down, up });
                await Task.Delay(100);
                IIInputInjector.InjectMouseInput(new[] { down, up });
                Window.Current.CoreWindow.PointerPosition = new Point(x, y);
            }
        }

        private async void SaveFileItem_Click(object sender, RoutedEventArgs e)
        {
            String code = CodeEditor.UniversalEditor.Text;
            code = CodeEditor.UniversalEditor.Text;
            if (CodeEditor.LocalFile != null)
                {            
                await Windows.Storage.FileIO.WriteTextAsync(CodeEditor.LocalFile, code);
                await Windows.Storage.FileIO.WriteTextAsync(CodeEditor.LocalFile, code);
                 var messageDialog = new MessageDialog("Saved: " + code);
                await messageDialog.ShowAsync();
                CodeEditor.LocalFile = null;
                }
               else if (CodeEditor.LocalFile == null)
               {
                if(string.IsNullOrEmpty(code))
                    {
                    var messageDialog = new MessageDialog("Nothing to save");
                    await messageDialog.ShowAsync();
                }
                else
                {
                    var savePicker = new Windows.Storage.Pickers.FileSavePicker();
                    savePicker.SuggestedStartLocation =
                        Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                    // Dropdown of file types the user can save the file as
                    savePicker.FileTypeChoices.Add(CodeEditor.Fileextension, new List<string>() { CodeEditor.Fileextension });
                    // Default file name if the user does not type one in or select a file to replace
                    savePicker.SuggestedFileName = CodeEditor.NameOfDocument;
                    Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
                    if (file != null)
                    {
                        // Prevent updates to the remote version of the file until
                        // we finish making changes and call CompleteUpdatesAsync.
                        Windows.Storage.CachedFileManager.DeferUpdates(file);
                        // write to file
                        await Windows.Storage.FileIO.WriteTextAsync(file, code);
                        // Let Windows know that we're finished changing the file so
                        // the other app can update the remote version of the file.
                        // Completing updates may require Windows to ask for user input.
                        Windows.Storage.Provider.FileUpdateStatus status =
                            await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                        if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                        {
                            var messageDialog = new MessageDialog("Saved: " + code);
                            await messageDialog.ShowAsync();
                        }
                        else
                        {
                            var messageDialog = new MessageDialog("FailedToSave: " + code);
                            await messageDialog.ShowAsync();
                        }
                    }
                    else
                    {
                        var messageDialog = new MessageDialog("Cancelled");
                        await messageDialog.ShowAsync();
                    }
                }
               }
            code = "";
        }

        private async void SaveAsFileItem_Click(object sender, RoutedEventArgs e)
        {
            String code = CodeEditor.UniversalEditor.Text;
            code = CodeEditor.UniversalEditor.Text;
            code = "";
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add(CodeEditor.Fileextension, new List<string>() { CodeEditor.Fileextension });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = CodeEditor.NameOfDocument;
            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                // write to file
                await Windows.Storage.FileIO.WriteTextAsync(file, code);
                Windows.Storage.Provider.FileUpdateStatus status =
                    await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    var messageDialog = new MessageDialog("Saved: " + code);
                    await messageDialog.ShowAsync();
                }
                else
                {
                    var messageDialog = new MessageDialog("FailedToSave: " + code);
                    await messageDialog.ShowAsync();
                }
            }
            else
            {
                var messageDialog = new MessageDialog("Cancelled");
                await messageDialog.ShowAsync();
            }
        }

            private async void NewFileItem_Click(object sender, RoutedEventArgs e)
            {
                if (Tabs.TabItems.Count < 3)
                {
                    var newTab = new TabViewItem();
                    newTab.IconSource = new MUXC.SymbolIconSource() { Symbol = Symbol.Document };
                    newTab.Header = "New Tab";
                    newTab.IsTapEnabled = true;
                    Frame frame = new Frame();
                    newTab.Content = frame;
                    frame.Navigate(typeof(CodeEditor));
                    Tabs.TabItems.Add(newTab);
                }
                else
                {
                    var messageDialog = new MessageDialog("Maximum tab limit");
                    await messageDialog.ShowAsync();
                }
            }
        }
    }
