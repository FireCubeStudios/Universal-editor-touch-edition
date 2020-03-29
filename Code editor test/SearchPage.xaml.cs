using Monaco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Input.Preview.Injection;
using Windows.UI.ViewManagement;
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
    public sealed partial class SearchPage : Page
    {
        Monaco.CodeEditor Editor = CodeEditor.UniversalEditor;
        public SearchPage()
        {
            this.InitializeComponent();
        }

        private async void UniversalButton_Click(object sender, RoutedEventArgs e)
        {
            var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            var x = pointerPosition.X;
            var y = pointerPosition.Y;
            var X = Window.Current.Bounds.Width - 50;
            var Y = Window.Current.Bounds.Height - 800;
            VirtualKey Key = VirtualKey.H;
            Button Item = sender as Button;
            string Name = Item.Name.ToString();
            switch (Name)
            {
                case "Find":
                    Key = VirtualKey.F;
                    break;
                case "Replace":
                    Key = VirtualKey.H;
                    break;
            }
            CodeEditor.UniversalEditor.Focus(FocusState.Pointer);
            await Task.Delay(100);

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
            InputInjector iiinputInjector = InputInjector.TryCreate();


            ////
            inputInjector.InjectKeyboardInput(new[] { shift, tab });
            shift.KeyOptions = InjectedInputKeyOptions.KeyUp;
            inputInjector.InjectKeyboardInput(new[] { shift });
        
            //  Window.Current.CoreWindow.PointerPosition = new Point(x, y);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage.TheimportantPage.FindName("SettingsPanel");
        }
    }
}
