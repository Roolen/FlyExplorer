using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlyExplorer.BasicElements;
using FlyExplorer.ControlElements;
using FlyExplorer.Core;
using System.IO;

namespace FlyExplorer
{
    public delegate void DeleteTab();

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        /// <summary>
        /// Индекс последней вкладки.
        /// </summary>
        private sbyte currentNumberTab = -1;
        /// <summary>
        /// Коллекция ссылок на вкладки.
        /// </summary>
        private List<TabItem> tabs = new List<TabItem>();
        /// <summary>
        /// Делегат на метод удаления вкладки.
        /// </summary>
        DeleteTab methodDeleteTab;
        #endregion


        public MainWindow()
        {
            InitializeComponent();

            #region Subscribe to delegates
            AnalyzerFileSystem.UpdateHandler += OutputtingDateForContentArea;
            Presenter.NewTabHandler += NewTab;
            methodDeleteTab = TabItem_Delete;
            #endregion

            OutputTreeElement();

            #region Output for default tab
            NewTab();
            #endregion
        }

        /// <summary>
        /// Выводит данные в область контента.
        /// </summary>
        private void OutputtingDateForContentArea(sbyte numberPosition)
        {
            OutputtingFoldersAndFilesForContentArea(numberPosition);
            OutputtingAddressLine(numberPosition);
        }

        /// <summary>
        /// Выводит папки и файлы, в виде кнопок, в область контенета, из указанной позиции.
        /// </summary>
        /// <param name="numberPosition">Позиция анализатора файловой системы</param>
        private void OutputtingFoldersAndFilesForContentArea(sbyte numberPosition)
        {
            ScrollViewer viewer = new ScrollViewer { Content = Presenter.GetPanelWithFoldersAndFilesForContentArea(numberPosition) };

            tabs[numberPosition].Content = viewer;
            tabs[numberPosition].Header = new TabButton(GetNameTab(AnalyzerFileSystem.GetPosition(numberPosition)), methodDeleteTab);

        }

        /// <summary>
        /// Выводит данные адресной строки.
        /// </summary>
        /// <param name="numberPosition">Позиция вкладки к которой принадлежит адрессная строка</param>
        private void OutputtingAddressLine(sbyte numberPosition)
        {
            string path = AnalyzerFileSystem.GetPosition(numberPosition);

            AdressLine.Children.RemoveRange(2, AdressLine.Children.Capacity);

            foreach (ButtonAddressLine button in GetButtonsAddressLine(path, numberPosition))
            {
                AdressLine.Children.Add(button);
            }
        }

        /// <summary>
        /// Возвращает массив кнопок для адрессной строки.
        /// </summary>
        /// <param name="path">Путь адресной строки</param>
        /// <param name="numberPosition">Позиция вкладки к которой принадлежит адрессная строка</param>
        /// <returns>Массив кнопок для адрессной строки</returns>
        private ButtonAddressLine[] GetButtonsAddressLine(string path, sbyte numberPosition)
        {
            string[] pathElements = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            ButtonAddressLine[] buttons = new ButtonAddressLine[pathElements.Length - 1];

            for (int i = 0; i < pathElements.Length - 1; i++)
            {
                buttons[i] = new ButtonAddressLine(pathElements, numberPosition, buttons.Length - i);
                buttons[i].buttonAddressLine.Content = pathElements[i];
            }

            return buttons;
        }

        /// <summary>
        /// Создаёт новую вкладку и выводит в области контента, файлы и папки.
        /// </summary>
        private void NewTab(string path = null)
        {

            if(path == null) AnalyzerFileSystem.CreateNewPosition("C:\\");
            if (path != null) AnalyzerFileSystem.CreateNewPosition(path);

            currentNumberTab++;

            ScrollViewer viewer = new ScrollViewer() { Content = Presenter.GetPanelWithFoldersAndFilesForContentArea(currentNumberTab) };

            TabItem tab = new TabItem { Header = new TabButton ( GetNameTab( AnalyzerFileSystem.GetPosition(currentNumberTab) ), methodDeleteTab ),
                                        Content = viewer };

            tab.GotFocus += TabItem_GotFocus;
            TabControl.Items.Insert(TabControl.Items.Count - 1, tab);
            tabs.Add(tab);

            OutputtingAddressLine(currentNumberTab);

            TabControl.SelectedIndex = currentNumberTab - 1;
        }

        private string GetNameTab(string pathTab)
        {
            string[] elementsPath = pathTab.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

            return elementsPath.Last();
        }

        /// <summary>
        /// Открывает окно лога.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilesElementTheLog_Copy_Click(object sender, RoutedEventArgs e)
        {
            WindowLog winLog = new WindowLog();
            winLog.Show();
        }

        /// <summary>
        /// Выводит все элементы дерева файловой системы.
        /// </summary>
        private void OutputTreeElement()
        {
            if(treeView.Items != null) { treeView.Items.Clear(); }

            treeView.Items.Add(Presenter.GetNewTextBox("Favorites", 24, FontWeights.Bold));

            OutputFavoritesOnTreeView();

            treeView.Items.Add(Presenter.GetNewTextBox("Computer", 24, FontWeights.Bold));

            OutputDrivesOnTreeView();

            treeView.Items.Add(Presenter.GetNewTextBox("Network", 24, FontWeights.Bold));
        }

        private void OutputFavoritesOnTreeView()
        {
            TreeViewButton[] itemsFavorites = Presenter.GetFavoritesTree();

            foreach (TreeViewButton favoriteItem in itemsFavorites)
            {
                treeView.Items.Add(favoriteItem);
            }
        }

        /// <summary>
        /// Выводит логические диски, в качестве элементов дерева файловой системы.
        /// </summary>
        private void OutputDrivesOnTreeView()
        {
            TreeViewButton[] itemsDriveSystem = Presenter.GetDirectorysTree();

            foreach (TreeViewButton driveItem in itemsDriveSystem)
            {
                treeView.Items.Add(driveItem);
            }
        }

        /// <summary>
        /// Создает новую вкладку, при нажатии на кнопку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonForCreateNewTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewTab();
        }

        /// <summary>
        /// Выделяет предыдущую вкладку, при фокусе на кнопке создания новой вкладки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonForCreateNewTab_GotFocus(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedIndex = TabControl.SelectedIndex - 1;
        }

        /// <summary>
        /// Показывает необходимую адресную строку при фокусе на вкладку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            OutputtingAddressLine(Convert.ToSByte(TabControl.SelectedIndex));
        }

        /// <summary>
        /// Событие удаления вкладки.
        /// </summary>
        private void TabItem_Delete()
        {
            if (currentNumberTab != 0)
            {
            TabControl.Items.RemoveAt(TabControl.SelectedIndex);
            tabs.RemoveAt(TabControl.SelectedIndex);
            AnalyzerFileSystem.DeleteLastPosition();
            currentNumberTab--;
            TabControl.SelectedIndex = currentNumberTab;
            }
            else
            {
                Log.Write("Presenter: don't can remove tab, this tab is last.");
                WindowMessage winMessage = new WindowMessage("don't can", "Don't can remove the tab, this tab is last.");
                winMessage.Show();
            }
        }

        private void ButtonForCreateInFavorite_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> items = new Dictionary<string, string>();
            TabItem tab = tabs.Last();

            items.Add((string)tab.Header, AnalyzerFileSystem.GetPosition( (sbyte)TabControl.SelectedIndex) );

            Configurator.SetFavoritesValueRegistry(items);

            OutputTreeElement();
        }
    }
}
