﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyExplorer.BasicElements;
using FlyExplorer.Core;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.IO;
using FlyExplorer.ControlElements;

namespace FlyExplorer.BasicElements
{
    static class Presenter
    {
        /// <summary>
        /// Возвращает массив элементов дерева, заполненый элементами дерева файловой системы.
        /// </summary>
        /// <returns>Массив элементов дерева</returns>
        static public TreeViewItem[] GetDirectorysTree()
        {
            return GetTreeViewItemsForLogicalDrives();
        }

        static public WrapPanel GetPanelWithFoldersAndFilesForContentArea(sbyte numberPosition)
        {
            string[] namesDirectories = AnalyzerFileSystem.GetDirectoriesNameFromPosition(numberPosition);
            string[] namesFiles = AnalyzerFileSystem.GetFilesNameFromPosition(numberPosition);

            WrapPanel panelWithFoldersAndFiles = new WrapPanel();

            for (int i = 0; i < namesDirectories.Length; i++)
            {
                panelWithFoldersAndFiles.Children.Add(new FolderButton(numberPosition) { TextFolder = namesDirectories[i], typeFolder = "folder", PathFolder = $@"{AnalyzerFileSystem.GetPosition(numberPosition)}\{namesDirectories[i]}" });
            }

            for (int i = 0; i < namesFiles.Length; i++)
            {
                panelWithFoldersAndFiles.Children.Add(new FolderButton(numberPosition) { TextFolder = namesFiles[i], typeFolder = "file" });
            }

            return panelWithFoldersAndFiles;
        }

        /// <summary>
        /// Возвращает текстовый блок с заданными в аргументах свойствами.
        /// </summary>
        /// <param name="text">Текст в текстовом блоке</param>
        /// <param name="fontSize">Размер шрифта</param>
        /// <param name="fontWeight">Тип шрифта</param>
        /// <returns>Текстовый блок</returns>
        static public TextBlock GetNewTextBox(string text, int fontSize, FontWeight fontWeight) => new TextBlock { Text = text, FontSize = fontSize, FontWeight = fontWeight };

        /// <summary>
        /// Возвращает массив элементов дерева, заполненый названиями логических дисков.
        /// </summary>
        /// <returns>Массив элементов дерева</returns>
        static private TreeViewItem[] GetTreeViewItemsForLogicalDrives()
        {
            DriveInfo[] disks = AnalyzerFileSystem.GetAllLogicDisk();

            TreeViewItem[] items = new TreeViewItem[disks.Length];

            for (int i = 0; i < disks.Length; i++)
            {
                items[i] = new TreeViewItem { Header = disks[i] + disks[i].VolumeLabel, Width = 150, };
            }

            return items;
        }



    }
}
