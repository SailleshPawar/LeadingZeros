using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadingZeros.Interface
{
    public interface ISave
    {
        void SaveFile(string content, string folderPath, string fileName);
    }
}
