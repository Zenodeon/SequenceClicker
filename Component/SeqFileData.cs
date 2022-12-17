using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ookii.Dialogs.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using SequenceClicker.View;

namespace SequenceClicker.Component
{
    public class SeqFileData
    {
        public BasicSequencerPanel.SaveData basicSeqData;

        private string filePath = "";
        private static string fileExtention = "sqd";

        public static SeqFileData CreateFromFile()
        {
            string path = "";

            VistaOpenFileDialog openFileDialog = new VistaOpenFileDialog();

            if ((bool)openFileDialog.ShowDialog())
                path = openFileDialog.FileName;
            else
                return null;

            return CreateFromFile(path);
        }

        public static SeqFileData CreateFromFile(string path)
        {
            if (!File.Exists(path))
                return null;

            SeqFileData fileData = null;

            try
            {
                string data = File.ReadAllText(path);
                fileData = JsonConvert.DeserializeObject<SeqFileData>(data);
            }
            catch(Exception e)
            {
                DLog.Warn(e.Message);
            }

            if (fileData != null)
                fileData.filePath = path;

            return fileData;
        }

        public void SaveData()
        {
            if (filePath == "")
            {
                SaveDataAs();
                return;
            }

            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            string data = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filePath, data);
        }

        public void SaveDataAs()
        {
            string newPath = GetVaildFilePath();
            if (newPath == "")
                return;
            else
                filePath = newPath;

            SaveData();
        }

        private string GetVaildFilePath()
        {
            string path;

            VistaSaveFileDialog saveFileDialog = new VistaSaveFileDialog();
            saveFileDialog.FileName = $"InputSequence.{fileExtention}";

            saveFileDialog.OverwritePrompt = false;

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
