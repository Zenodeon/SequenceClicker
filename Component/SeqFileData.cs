using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ookii.Dialogs.Wpf;
using SequenceClicker.View;

namespace SequenceClicker.Component
{
    public class SeqFileData
    {
        private string filePath = "";

        public BasicSequencerPanel.SaveData basicSeqData;

        public static string fileExtention = "sqd";

        public void SaveData()
        {
            if (filePath == "")
            {
                string newPath = GetVaildFilePath();
                if (newPath == "")
                    return;
                else
                    filePath = newPath;
            }

            DLog.Log(filePath);
        }

        public void SaveDataAs()
        {

        }

        private string GetVaildFilePath()
        {
            string path;

            VistaSaveFileDialog saveFileDialog = new VistaSaveFileDialog();
            saveFileDialog.FileName = $"InputSequence.{fileExtention}";

            if ((bool)saveFileDialog.ShowDialog())
                path = saveFileDialog.FileName;
            else
                return "";

            if (!path.Contains('.'))
                path += $".{fileExtention}";

            if(File.Exists(path))
                path = GetUniqueFilePath(path);

            return path;
        }

        private string GetUniqueFilePath(string path)
        {
            string defaultPathStructure = path.Split('.')[0];
            int count = 1;

            while(File.Exists(path))
            {
                path = $"{defaultPathStructure}{count}.{fileExtention}";
                count++;
            }

            return path;
        }
    }
}
