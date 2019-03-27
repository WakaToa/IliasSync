using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IliasSync.Ilias;
using Container = IliasSync.Ilias.Container;
using File = System.IO.File;

namespace IliasSync
{
    public partial class FrmMain : Form
    {
        private IliasClient _iliasClient;

        public FrmMain()
        {
            InitializeComponent();
            gbSettings.Enabled = false;
            cmdSync.Enabled = false;
            gbPersonalDesktop.Enabled = false;
            txtSyncPath.Text = Settings.Instance.SyncPath;
            txtUsername.Text = Settings.Instance.Username;
            txtPassword.Text = Crypto.Decrypt(Settings.Instance.Password);
        }

        private async void cmdLogin_Click(object sender, EventArgs e)
        {
            await LoginAsync();
        }

        private async void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                await LoginAsync();
            }
        }

        private async Task LoginAsync()
        {
            gbLogin.Enabled = false;
            _iliasClient = new IliasClient(txtUsername.Text, txtPassword.Text);

            var result = await _iliasClient.LoginAsync();
            if (!result)
            {
                MessageBox.Show("Login fehlgeschlagen!\r\nBenutzername oder Passwort ist falsch!", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gbLogin.Enabled = true;
            }
            else
            {
                gbSettings.Enabled = true;
                cmdSync.Enabled = true;
                gbPersonalDesktop.Enabled = true;
                Settings.Instance.Username = txtUsername.Text;
                Settings.Instance.Password =Crypto.Encrypt(txtPassword.Text);
                Settings.Instance.Save();
                await RefreshPersonalDesktop();
            }
        }

        private async Task RefreshPersonalDesktop()
        {
            var desktop = await _iliasClient.GetPersonalDesktopAsync();

            foreach (var desktopCourse in desktop.Courses)
            {
                var savedCourse = Settings.Instance.GetCourse(desktopCourse.Id);
                if (savedCourse != null)
                {
                    dgvCourses.Rows.Add(savedCourse.Sync, savedCourse.Semester, savedCourse.Id, savedCourse.Name, savedCourse.Path);
                }
                else
                {
                    dgvCourses.Rows.Add(false, "", desktopCourse.Id, desktopCourse.Name, desktopCourse.Path);
                    Settings.Instance.AddCourse(desktopCourse);
                }
            }

            Settings.Instance.Save();
        }

        private async Task SyncAsync()
        {
            var coursesFromSettings = Settings.Instance.Courses.Where(x => x.Sync).ToList();

            var courses = new List<Course>();
            foreach (DataGridViewRow dgvCoursesRow in dgvCourses.Rows)
            {
                var courseId = int.Parse(dgvCoursesRow.Cells["clmnId"].Value.ToString());
                var matchingCourse = coursesFromSettings.FirstOrDefault(x => x.Id == courseId);
                if (matchingCourse != null)
                {
                    courses.Add(matchingCourse);
                }
            }
            foreach (var course in courses)
            {
                Log($"Synchronisiere Kurs {course.Name}", Color.Aqua);
                await DownloadFolderAsync(course, course.Id);
                Log($"Synchronisation von Kurs {course.Name} abgeschlossen!", Color.Green);
            }

            Log("Erfolgreich alle Kurse synchronisiert!", Color.Green);
        }

        private async Task DownloadFolderAsync(Course course, int containerId)
        {
            foreach (var file in await _iliasClient.GetFilesFromContainerAsync(containerId))
            {
                var path = Path.Combine(course.Path, file.Path, $"{file.Name}.{file.Extension}");

                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }

                if (File.Exists(path))
                {
                    Log($"Datei existiert bereits: {Path.Combine(file.Path.Trim(), $"{file.Name}.{file.Extension}")}", Color.Olive);
                }
                else
                {
                    Log($"Lade Datei {Path.Combine(file.Path, $"{file.Name}.{file.Extension}")}", Color.Blue);
                    var fileBytes = await _iliasClient.DownloadFileAsync(file.Id);
                  
                    System.IO.File.WriteAllBytes(path, fileBytes);
                }
            }

            foreach (var container in await _iliasClient.GetContainersFromContainerAsync(containerId))
            {
                Log($"Synchronisiere Ordner {container.Name}", Color.Aqua);
                await DownloadFolderAsync(course, container.Id);
            }
        }

        public void Log(string text, Color color = new Color()) //new Color() == Color.Black
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.BeginInvoke(new Action(delegate {
                    Log(text, color);
                }));
                return;
            }

            var dt = $"[{DateTime.Now:HH:mm:ss}] ";

            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.SelectionColor = color;

            if (rtbLog.Lines.Length == 0)
            {
                rtbLog.AppendText(dt + text);
                rtbLog.ScrollToCaret();
                rtbLog.AppendText(Environment.NewLine);
            }
            else
            {
                rtbLog.AppendText(dt + text + Environment.NewLine);
                rtbLog.ScrollToCaret();
            }
        }

        private void dgvCourses_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvCourses.IsCurrentCellDirty)
            {
                dgvCourses.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCourses.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvCourses_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var courseId = int.Parse(dgvCourses.Rows[e.RowIndex].Cells["clmnId"].Value.ToString());
                var courseName = dgvCourses.Rows[e.RowIndex].Cells["clmnName"].Value.ToString();
                var courseSync = bool.Parse(dgvCourses.Rows[e.RowIndex].Cells["clmnSync"].Value.ToString());
               
                var courseSemester = dgvCourses.Rows[e.RowIndex].Cells["clmnSemester"].Value.ToString();

                var coursePath = Path.Combine(Settings.Instance.SyncPath, courseSemester != "0" ? courseSemester : "", courseName);
                dgvCourses.Rows[e.RowIndex].Cells["clmnPath"].Value = coursePath;

                var course = new Course(courseId, courseName, courseSync) { Path = coursePath, Semester = courseSemester };

                Settings.Instance.UpdateCourse(course);

                Settings.Instance.Save();
            }
            catch (Exception exception)
            {
            }

        }

        private void txtSyncPath_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.SyncPath = txtSyncPath.Text;
            Settings.Instance.Save();

            foreach (DataGridViewRow row in dgvCourses.Rows)
            {
                var courseId = int.Parse(row.Cells["clmnId"].Value.ToString());
                var courseName = row.Cells["clmnName"].Value.ToString();
                var courseSync = bool.Parse(row.Cells["clmnSync"].Value.ToString());
                
                var courseSemester = row.Cells["clmnSemester"].Value.ToString();

                var coursePath = Path.Combine(Settings.Instance.SyncPath, courseSemester != "0" ? courseSemester : "", courseName);

                row.Cells["clmnPath"].Value = coursePath;

                var course = new Course(courseId, courseName, courseSync) { Path = coursePath, Semester = courseSemester };

                Settings.Instance.UpdateCourse(course);

                Settings.Instance.Save();
            }
        }

        private async void cmdSync_Click(object sender, EventArgs e)
        {
            await SyncAsync();
        }
    }
}
