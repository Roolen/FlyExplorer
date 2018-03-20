using System;
using System.IO;

namespace FlyExplorer.BasicElements
{
    internal class LogicDisk
    {
        private DriveInfo logicDisk;
        private string nameDisk;
        private ulong size;
        private ulong freeVolume;
        private ulong occupiedVolume;
        private string labelDisk;
        private DriveType typeFileSystem;


        public string NameDisk
        {
            get
            {
                if (nameDisk != null) return nameDisk;
                return "";
            }
        }

        public string LabelDisk
        {
            get
            {
                if (labelDisk != null) return labelDisk;
                return ""; // todo add exception;
            }
        }
        public DriveInfo Disk
        {
            set { logicDisk = value; }
        }

        /// <summary>
        /// Представляет собой логический диск файловой системой.
        /// </summary>
        /// <param name="name">Имя логического диска</param>
        /// <param name="label">Метка логического диска</param>
        public LogicDisk(string name)
        {
            nameDisk = name;
            logicDisk = new DriveInfo(nameDisk);
        }

        /// <summary>
        /// Обновляет данные логического диска.
        /// </summary>
        public void UpdateDisk()
        {
            UpdateLabel();
            UpdateSize();
            UpdateTypeFileSystem();
        }

        /// <summary>
        /// Обновляет метку тома.
        /// </summary>
        private void UpdateLabel()
        {
            labelDisk = logicDisk.VolumeLabel;
        }

        /// <summary>
        /// Обновляет общий, занятый и свободный объем логического диска.
        /// </summary>
        private void UpdateSize()
        {
            size = (ulong)logicDisk.TotalSize;
            freeVolume = (ulong)logicDisk.TotalFreeSpace;
            occupiedVolume = (ulong)logicDisk.AvailableFreeSpace;
        }

        /// <summary>
        /// Обновляет тип файловой системы логического диска.
        /// </summary>
        private void UpdateTypeFileSystem()
        {
            typeFileSystem = logicDisk.DriveType;
        }

        /// <summary>
        /// Возвращает общий объем логического диска.
        /// </summary>
        /// <returns>Размер</returns>
        public ulong GetSizeDisk()
        {
            return size;
        }

        /// <summary>
        /// Возвращает свободный объем логического диска.
        /// </summary>
        /// <returns>объем</returns>
        public ulong GetFreeVolume()
        {
            return freeVolume;
        }

        /// <summary>
        /// Возвращает занятый объем логического диска.
        /// </summary>
        /// <returns>Объем</returns>
        public ulong GetOccupiedVolume()
        {
            return occupiedVolume;
        }

        /// <summary>
        /// Возвращает тип файловой системы логического диска.
        /// </summary>
        /// <returns>Тип ФС</returns>
        public string GetTypeFileSystem()
        {
            return Convert.ToString(typeFileSystem);
        }

        /// <summary>
        /// Изменяет метку логического диска.
        /// </summary>
        /// <param name="newLabel">Новая метка</param>
        public void SetLabel(string newLabel)
        {
            labelDisk = newLabel;
        }
    }
}