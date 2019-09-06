using LeadingZeros.Interface;
using System;
using System.IO;

namespace LeadingZeros.Models
{
    public class FileOperation : ISave
    {
        //Save file to a directory
        public void SaveFile(string content, string folderPath, string fileName)
        {

            var path = Path.Combine(folderPath, fileName + "_" + DateTime.Now.ToString("yyymmdd") + "_" + DateTime.Now.ToString("HHMMss") + ".csv");
            File.WriteAllText(path, content);

        }

    }
}
