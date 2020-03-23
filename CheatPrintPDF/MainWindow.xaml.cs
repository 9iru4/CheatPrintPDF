using MahApps.Metro.Controls;
using Microsoft.Win32;
using Patagames.Pdf.Net;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CheatPrintPDF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        string FilePath;
        List<PagesResult> result = new List<PagesResult>();

        public MainWindow()
        {
            InitializeComponent();
            PdfCommon.Initialize();
            pdfDataGrid.ItemsSource = result;
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if ((bool)openFileDialog.ShowDialog())
            {
                FilePath = openFileDialog.FileName;

                FilePathTextBox.Text = FilePath;
                StartButton.IsEnabled = true;
            }
            else
            {
                MyMessageBox.Show("Файл не выбран.", this);
            }
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            OpenFileButton.IsEnabled = false;
            MyPDFFileWorker fileWorker = new MyPDFFileWorker();
            result = fileWorker.GetPDFPagesTypes(await fileWorker.GetPdfPages(FilePath));
            pdfDataGrid.ItemsSource = result;
            StartButton.IsEnabled = true;
            OpenFileButton.IsEnabled = true;
        }

        private void pdfDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText((pdfDataGrid.SelectedItem as PagesResult).CopyPages);
        }
    }
}
