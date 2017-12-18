using System;
using System.IO;

namespace FlyExplorer.BasicElements
{
    internal class LogicDisk
    {
        private string nameDisk;
        private ulong Size;
        private ulong FreeVolume;
        private ulong OccupiedVolume;
        private string labelDisk;
        private string typeFileSystem;


        public string NameDisk
        {
            get
            {
                if (nameDisk != null) return nameDisk;
                return ""; // todo add exception.
            }
            set
            {
                if (value != null) nameDisk = value;
                // todo add exception.
            }
        }

        public string LabelDisk
        {
            get
            {
                if (labelDisk != null) return labelDisk;
                return ""; // todo add exception;
            }
            set
            {
                if (value != null) labelDisk = value;
                // todo add exception.
            }
        }

        /// <summary>
        /// Представляет собой логический диск фойловой системой.
        /// </summary>
        /// <param name="name">Имя логического диска</param>
        /// <param name="label">Метка логического диска</param>
        public LogicDisk(string name, string label)
        {
            NameDisk = name;
            LabelDisk = label;
        }

        /// <summary>
        /// Обновляет данные логического диска.
        /// </summary>
        public void UpdateDisk()
        {
            
        }

        private void SetNameDisk()
        {

        }

        private void UpdateSize()
        {

        }

        private void UpdateTypeFileSystem()
        {

        }

        /// <summary>
        /// Возвращает общий объем логического диска.
        /// </summary>
        /// <returns>Размер</returns>
        public ulong GetSizeDisk()
        {
            return Size;
        }

        /// <summary>
        /// Возвращает свободный объем логического диска.
        /// </summary>
        /// <returns>объем</returns>
        public ulong GetFreeVolume()
        {
            return FreeVolume;
        }

        /// <summary>
        /// Возвращает занятый объем логического диска.
        /// </summary>
        /// <returns>Объем</returns>
        public ulong GetOccupiedVolume()
        {
            return OccupiedVolume;
        }

        /// <summary>
        /// Возвращает тип файловой системы логического диска.
        /// </summary>
        /// <returns>Тип ФС</returns>
        public string GetTypeFileSystem()
        {
            return typeFileSystem;
        }
    }
}