using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IliasSync.Ilias
{
    public class Course : Container
    {
        public bool Sync { get; set; }

        public string Semester { get; set; }

        public string Path { get; set; }

        public Course()
        {
            
        }

        public Course(int id, string name) : this(id, name, false)
        {
        }

        public Course(int id, string name, bool sync) : base(id, name)
        {
            Sync = sync;
            Semester = "";
            Path = System.IO.Path.Combine(Settings.Instance.SyncPath, Semester, name.RemoveIlegalPathCharacters());
        }
    }
}
