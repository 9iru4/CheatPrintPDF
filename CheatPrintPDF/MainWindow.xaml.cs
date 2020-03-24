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
        /// <summary>
        /// Путь к файлу
        /// </summary>
        string FilePath;
        /// <summary>
        /// Результат обработки пдф файла
        /// </summary>
        List<PagesResult> result = new List<PagesResult>();

        /// <summary>
        /// Инициализируем объекты
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            PdfCommon.Initialize();
        }

        /// <summary>
        /// Событие нажатия кнопки Открыть
        /// </summary>
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            //Диалог выбора файла
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

        /// <summary>
        /// Событие нажатия кнопки запуск
        /// </summary>
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            //Отключаем кнопки
            StartButton.IsEnabled = false;
            OpenFileButton.IsEnabled = false;

            try
            {
                //Класс для работы с пдф файлом
                MyPDFFileWorker fileWorker = new MyPDFFileWorker();

                //Результат обработки файла
                result = fileWorker.GetPDFPagesTypes(await fileWorker.GetPdfPages(FilePath));

                //Обновляем датагрид
                pdfDataGrid.ItemsSource = result;
            }
            catch (System.Exception ex)
            {
                MyMessageBox.Show(ex.ToString(), this);
            }

            //Включаем кнопки
            StartButton.IsEnabled = true;
            OpenFileButton.IsEnabled = true;
        }

        /// <summary>
        /// Событие двойного клика по ДатаГриду
        /// </summary>
        private void pdfDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText((pdfDataGrid.SelectedItem as PagesResult).CopyPages); //(выбранная строка копируется в буфер)
        }
    }
}
