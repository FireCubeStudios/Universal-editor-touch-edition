using System;
using Monaco;
using Monaco.Editor;
using Monaco.Helpers;
using Monaco.Languages;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using Microsoft.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Code_editor_test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CodeEditor : Page
    {
        private ContextKey _myCondition;
        public string CodeContent

        {

            get { return (string)GetValue(CodeContentProperty); }

            set { SetValue(CodeContentProperty, value); }

        }



        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...

        public static readonly DependencyProperty CodeContentProperty =

            DependencyProperty.Register("CodeContent", typeof(string), typeof(MainPage), new PropertyMetadata(""));

        public static Monaco.CodeEditor UniversalEditor { get; set; }
        public static Boolean Saved { get; set; }
        public static string NameOfDocument { get; set; } 
        public static string Fileextension { get; set; }
        public static Windows.Storage.StorageFile LocalFile { get; set; }
        private string eeeeeeee;
        public CodeEditor()
        {
            this.InitializeComponent();
            UniversalEditor = Editor;
            Editor.Loaded += Editor_Loaded;
            Editor.InternalException += Editor_InternalException;
            Saved = false;
            Fileextension = ".txt";
        }
        private void Editor_InternalException(Monaco.CodeEditor sender, Exception args)

        {

            // This shouldn't happen, if it does, then it's a bug.

        }



        private async void Editor_Loaded(object sender, RoutedEventArgs e)

        {
            LoadingControl.IsLoading = false;
            if(string.IsNullOrEmpty(eeeeeeee) == true)
            {
                await StartDialog.ShowAsync();
            }
            //TO-DO: REPLACE WITH BETTER SOLUTION
            // Ready for Display
            var loop = 10;
            while (loop == 10)
            {
                try
                {
                    var range = await Editor.GetModel().GetFullModelRangeAsync();
                    var line = await Editor.GetPositionAsync();
                    var count = await Editor.GetModel().GetLineCountAsync();
                    MainPage.UniversalStatusText.Text = "Range: " + range.ToString() + "  Ln: " + line + "  All Ln: " + count;
                }
                catch
                {
                    return;
                }

            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Editor.CodeLanguage = ComboLanguageBox.SelectedItem.ToString();
            switch (ComboLanguageBox.SelectedItem.ToString())
            {
                case "css":
                    Fileextension = ".css";
                    break;
                case "scss":
                    Fileextension = ".scss";
                    break;
                case "json":
                    Fileextension = ".json";
                    break;
                case "javascript":
                    Fileextension = ".js";
                    break;
                case "typescript":
                    Fileextension = ".ts";
                    break;
                case "html":
                    Fileextension = ".html";
                    break;
                case "python":
                    Fileextension = ".py";
                    break;
                case "xml":
                    Fileextension = ".xml";
                    break;
                case "csharp":
                    Fileextension = ".cs";
                    break;
                case "fsharp":
                    Fileextension = ".fs";
                    break;
                default:
                    Fileextension = ".js";
                    break;
            }
            if (string.IsNullOrEmpty(DocumentName.Text) == true)
            {
                NameOfDocument = "New Document";
                TabViewItem Tab = MainPage.TabsMain.SelectedItem as TabViewItem;
                Tab.Header = "New Document.js";
            }
            else
            {
                NameOfDocument = DocumentName.Text;
                TabViewItem Tab = MainPage.TabsMain.SelectedItem as TabViewItem;
                Tab.Header = DocumentName.Text + Fileextension;
            }
            eeeeeeee = "bruh";
        }

        private void DocumentName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DocumentName.Text) == true)
            {
                StartDialog.IsPrimaryButtonEnabled = false;
            }
            else
            {
                StartDialog.IsPrimaryButtonEnabled = true;
            }
        }
    }
}
