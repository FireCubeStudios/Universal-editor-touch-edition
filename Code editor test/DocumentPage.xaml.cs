using System;
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
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Popups;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Code_editor_test
{
    public sealed partial class DocumentPage : Page
    {
        public DocumentPage()
        {
            this.InitializeComponent();
            ComboLanguageBox.PlaceholderText = CodeEditor.UniversalEditor.Language;
            FileName.Text = CodeEditor.NameOfDocument + CodeEditor.Fileextension;
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
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
                if (string.IsNullOrEmpty(code))
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

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
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

        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            await EditDialog.ShowAsync();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
                    CodeEditor.UniversalEditor.CodeLanguage = ComboLanguageBox.SelectedItem.ToString();
                    switch (ComboLanguageBox.SelectedItem.ToString())
                    {
                        case "css":
                            CodeEditor.Fileextension = ".css";
                            break;
                        case "scss":
                            CodeEditor.Fileextension = ".scss";
                            break;
                        case "json":
                            CodeEditor.Fileextension = ".json";
                            break;
                        case "javascript":
                            CodeEditor.Fileextension = ".js";
                            break;
                        case "typescript":
                            CodeEditor.Fileextension = ".ts";
                            break;
                        case "html":
                            CodeEditor.Fileextension = ".html";
                            break;
                        case "python":
                            CodeEditor.Fileextension = ".py";
                            break;
                        case "xml":
                            CodeEditor.Fileextension = ".xml";
                            break;
                        case "csharp":
                            CodeEditor.Fileextension = ".cs";
                            break;
                        case "fsharp":
                            CodeEditor.Fileextension = ".fs";
                            break;
                    }
            if (string.IsNullOrEmpty(DocumentName.Text) == true)
            {
                CodeEditor.NameOfDocument = "New Document";
                TabViewItem Tab = MainPage.TabsMain.SelectedItem as TabViewItem;
                Tab.Header = "New Document" + CodeEditor.Fileextension;
            }
            else
            {
                CodeEditor.NameOfDocument = DocumentName.Text;
                TabViewItem Tab = MainPage.TabsMain.SelectedItem as TabViewItem;
                Tab.Header = DocumentName.Text + CodeEditor.Fileextension;
            }
        }

        private void DocumentName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DocumentName.Text) == true)
            {
                EditDialog.IsPrimaryButtonEnabled = false;
            }
            else
            {
                EditDialog.IsPrimaryButtonEnabled = true;
            }
        }
    }
}
