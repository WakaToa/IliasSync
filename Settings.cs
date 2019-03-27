using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using IliasSync.Ilias;
using Newtonsoft.Json;

namespace IliasSync
{
    public class Settings
    {
        private static Settings _instance;
        public static Settings Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                _instance = System.IO.File.Exists("settings.json") ? JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText("settings.json")) : new Settings();
                
                return _instance;
            }
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public string SyncPath { get; set; }
        public List<Course> Courses { get; set; }

        public Settings()
        {
            SyncPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Courses = new List<Course>();
        }

        public Course GetCourse(int id)
        {
            return Courses.Find(x => x.Id == id);
        }

        public void UpdateCourse(Course course)
        {
            Courses.RemoveAll(x => x.Id == course.Id);
            Courses.Add(course);
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }

        public void Save()
        {
            var data = JsonConvert.SerializeObject(this);
            System.IO.File.WriteAllText("settings.json", data);
        }
    }
}
