using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IliasSync.Ilias
{
    public class File : Container
    {
        public string Path { get; set; }
        public string Extension { get; set; }

        public string DownloadUri => $"https://www.ilias.fh-dortmund.de/ilias/goto_ilias-fhdo_file_{Id}_download.html";

        public File(int id, string name, string path, string ext) : base(id, name)
        {
            Path = path;
            Extension = ext;
        }
    }
}
