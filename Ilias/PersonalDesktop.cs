using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IliasSync.Ilias
{
    public class PersonalDesktop
    {
        public List<Course> Courses { get; set; }

        public int TotalCourses => Courses.Count;

        public int TotalFiles
        {
            get
            {
                var count = 0;

                foreach (var course in Courses)
                {
                    count += course.Containers.Where(x => x.GetType() == typeof(File)).ToList().Count;
                    foreach (var container in course.Containers)
                    {
                        count += CountFilesInFolderContainer(container);
                    }
                }

                return count;
            }
        }

        public PersonalDesktop(List<Course> courses)
        {
            Courses = courses;
        }

        private int CountFilesInFolderContainer(Container container)
        {
            return container.Containers.Where(x => x.GetType() == typeof(File)).ToList().Count + container.Containers.Sum(CountFilesInFolderContainer);
        }
    }
}
