using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Globalization;

namespace KoboSync
{
    public partial class MainForm : Form
    {
        private string sql_query;
        private string books_path;
        private string dir_path;

        public MainForm()
        {
            InitializeComponent();
        }

        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings[key] ?? "";
            }
            catch (ConfigurationErrorsException ex)
            {
                return ex.Message;
            }
        }

        static string AddUpdateAppSettings(string[,] setts)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;

                for (int i = 0; i < setts.Length / 2; i++)
                {
                    if (settings[setts[0, i]] == null)
                    {
                        settings.Add(setts[0, i], setts[1, i]);
                    }
                    else
                    {
                        settings[setts[0, i]].Value = setts[1, i];
                    }
                }


                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException ex)
            {
                return ex.Message;
            }
            return "OK";
        }

        private List<string> DetectDrive()
        {
            var list_dr = new List<string>();
            //var drives = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.DriveType == DriveType.Removable);
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo dr in drives)
            {
                if (File.Exists(dr + ".kobo/KoboReader.sqlite"))
                {
                    list_dr.Add(dr.ToString());
                }
            }
            return list_dr;
        }

        private string SelectFolder()
        {
            DialogResult dialog_result = folderBrowserDialogMain.ShowDialog();
            string folder_name = "";
            if (dialog_result == DialogResult.OK)
            {
                //Извлечение имени папки
                folder_name = folderBrowserDialogMain.SelectedPath;
            }
            return folder_name;
        }

        private void buttonSync_Click(object sender, EventArgs e)
        {
            ButtonSync.Enabled = false;
            books_path = "file:///mnt/sd/";
            if (textBoxTo.Text.StartsWith(comboBoxDriveList.Text))
            {
                books_path = "file:///mnt/onboard/";
            }

            richTextBoxLog.Clear();
            richTextBoxLog.SelectionFont = new Font(richTextBoxLog.Font, FontStyle.Bold);
            richTextBoxLog.AppendText("Start synchronizing" + Environment.NewLine);
            richTextBoxLog.ScrollToCaret();
            richTextBoxLog.Select(0, richTextBoxLog.TextLength);
            Application.DoEvents();
            string return_message = Copy(textBoxFrom.Text, textBoxTo.Text, checkBoxClearDiSource.Checked);
            richTextBoxLog.AppendText(return_message + Environment.NewLine);
            if (!return_message.Equals("OK"))
            {
                ButtonSync.Enabled = true;
                return;
            }
            Application.DoEvents();
            return_message = CreateShelf();
            richTextBoxLog.AppendText(return_message + Environment.NewLine);
            if (!return_message.Equals("OK"))
            {
                ButtonSync.Enabled = true;
                return;
            }
            Application.DoEvents();
            string[,] settings = { { "FromFolder", "ToFolder", "ClearDiSource", "LongShelfNames" }, { textBoxFrom.Text, textBoxTo.Text, checkBoxClearDiSource.Checked.ToString(), checkBoxLongSheldNames.Checked.ToString() } };
            //string[] value = { textBoxFrom.Text, textBoxTo.Text };
            return_message = AddUpdateAppSettings(settings);
            if (!return_message.Equals("OK"))
            {
                richTextBoxLog.AppendText(return_message + Environment.NewLine);
                ButtonSync.Enabled = true;
                return;
            }
            richTextBoxLog.AppendText("---" + Environment.NewLine);
            richTextBoxLog.SelectionFont = new Font(richTextBoxLog.Font, FontStyle.Bold);
            richTextBoxLog.Select(richTextBoxLog.TextLength, 7);
            richTextBoxLog.AppendText("Success");
            ButtonSync.Enabled = true;
        }

        private void buttonFrom_Click(object sender, EventArgs e)
        {
            textBoxFrom.Text = SelectFolder();
        }

        private void buttonTo_Click(object sender, EventArgs e)
        {
            textBoxTo.Text = SelectFolder();
        }

        private string Copy(string sourceDirectory, string targetDirectory, Boolean clear_diSource = false)
        {
            DirectoryInfo diSource;
            DirectoryInfo diTarget;
            try
            {
                diSource = new DirectoryInfo(sourceDirectory);
                diTarget = new DirectoryInfo(targetDirectory);

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            sql_query = "DELETE FROM Shelf;" + Environment.NewLine + "DELETE FROM ShelfContent;" + Environment.NewLine;
            if (clear_diSource)
            {
                richTextBoxLog.AppendText("Clear destination folder... ");
                Application.DoEvents();
                FileInfo[] dst;
                try
                {
                    dst = diTarget.GetFiles();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                foreach (FileInfo file in dst)
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                    richTextBoxLog.SelectionFont = new Font(richTextBoxLog.Font, FontStyle.Bold);
                    richTextBoxLog.SelectionColor = Color.Green;
                    richTextBoxLog.Select(richTextBoxLog.TextLength, 2);
                    richTextBoxLog.AppendText("OK" + Environment.NewLine);
                }
                foreach (DirectoryInfo dir in diTarget.GetDirectories())
                {
                    try
                    {
                        dir.Delete(true);
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
                richTextBoxLog.SelectionFont = new Font(richTextBoxLog.Font, FontStyle.Bold);
                richTextBoxLog.SelectionColor = Color.Green;
                richTextBoxLog.Select(richTextBoxLog.TextLength, 2);
                richTextBoxLog.AppendText("OK" + Environment.NewLine);
                Application.DoEvents();
            }
            richTextBoxLog.AppendText("Copy folders: ");
            Application.DoEvents();
            string return_message = CopyAll(diSource, diTarget);
            if (return_message == "OK")
            {
                richTextBoxLog.SelectionFont = new Font(richTextBoxLog.Font, FontStyle.Bold);
                richTextBoxLog.SelectionColor = Color.Green;
                richTextBoxLog.Select(richTextBoxLog.TextLength, 2);
            }
            return return_message;
        }

        private string CopyAll(DirectoryInfo source, DirectoryInfo target, string di_path = "")
        {
            // Copy each file into the new directory.
            FileInfo[] src;
            try
            {
                src = source.GetFiles();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            foreach (FileInfo fi in src)
            {
                try
                {
                    fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                sql_query += "INSERT INTO ShelfContent(ShelfName, ContentId, DateModified, _IsDeleted, _IsSynced) "
                            + "VALUES ('" + di_path + "', '" + Regex.Replace(Regex.Replace(target.FullName, @"^\w:\\", books_path), @"\\", "/") + "/" + fi.Name.Replace("'", "''") + "', '" + DateTime.Now.ToString("yyy-MM-ddTHH:mm:ssZ") + "', 'false', 'false');" + Environment.NewLine;
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                try
                {
                    DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                    if (!checkBoxLongSheldNames.Checked)
                    {
                        di_path = "";
                    }
                    if (di_path.Equals(""))
                    {
                        dir_path = diSourceSubDir.Name;
                    }
                    else
                    {
                        dir_path = di_path + " " + comboBoxSpace.SelectedItem + " " + diSourceSubDir.Name;

                    }
                        richTextBoxLog.AppendText(Environment.NewLine+dir_path+" ");
                        Application.DoEvents();
                    if (diSourceSubDir.GetFiles().Length > 0)
                    {
                        sql_query += "INSERT INTO Shelf(CreationDate, Id, InternalName, LastModified, Name, Type, _IsDeleted, _IsVisible, _IsSynced, _SyncTime) "
                                        + "VALUES ('" + DateTime.Now.ToString("yyy-MM-ddTHH:mm:ssZ") + "', '" + Guid.NewGuid() + "', '" + dir_path + "', '" + DateTime.Now.ToString("yyy-MM-ddTHH:mm:ssZ") + "', '" + dir_path + "', 'UserTag', 'false', 'true', 'true', NULL);" + Environment.NewLine;
                    }

                        CopyAll(diSourceSubDir, nextTargetSubDir, dir_path);

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "OK";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            comboBoxSpace.Text = "-";
            var drives = DetectDrive();
            comboBoxDriveList.Items.AddRange(drives.ToArray());
            if (drives.Count == 1)
            {
                comboBoxDriveList.Text = drives[0];
            }
            textBoxFrom.Text = ReadSetting("FromFolder");
            textBoxTo.Text = ReadSetting("ToFolder");
            if (ReadSetting("ClearDiSource").Equals("True"))
            {
                checkBoxClearDiSource.Checked = true;
            }
            if (ReadSetting("LongShelfNames").Equals("True"))
            {
                checkBoxLongSheldNames.Checked = true;
            }
            //default help text
            string base_text = "";
            CultureInfo ci = CultureInfo.InstalledUICulture;

            if (ci.TwoLetterISOLanguageName.Equals("ru") | ci.TwoLetterISOLanguageName.Equals("uk") | ci.TwoLetterISOLanguageName.Equals("be"))
            {
                base_text = "Перенос библиотеки на Kobo Aura One, Kobo Glo, Kobo H2O и создание для каждой папки с книгами отдельной полки (коллекции) на устройстве." + Environment.NewLine +
                    "---" + Environment.NewLine +
                    labelFrom.Text + " -- папка на компьютере, в которой хранится ваша библиотека. " +
                    "Для каждой подпапки, независимо от уровня вложенности, будет создана коллекция (полка)." + Environment.NewLine +
                    labelTo.Text + " -- папка в электронной книге, в которую будет скопирована бибилиотека." + Environment.NewLine +
                    labelDriveLetter.Text + @" -- внутренний диск электронной книги с файлом \.kobo\KoboReader.sqlite.";

            }
            richTextBoxLog.AppendText(base_text);
            richTextBoxLog.Select(richTextBoxLog.Find(labelFrom.Text), labelFrom.Text.Length);
            richTextBoxLog.SelectionFont = new Font(richTextBoxLog.Font, FontStyle.Bold);
            richTextBoxLog.Select(richTextBoxLog.Find(labelTo.Text), labelTo.Text.Length);
            richTextBoxLog.SelectionFont = new Font(richTextBoxLog.Font, FontStyle.Bold);
            richTextBoxLog.Select(richTextBoxLog.Find(labelDriveLetter.Text), labelDriveLetter.Text.Length);
            richTextBoxLog.SelectionFont = new Font(richTextBoxLog.Font, FontStyle.Bold);
        }

        private void comboBoxDriveList_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> drives = DetectDrive();
            //var dr = DriveInfo.GetDrives().Where(drive => drive.Name == comboBoxDriveList.SelectedItem);

            //var dr = driveinfo.getdrives().where()
            //labelDriveName.Text = drives.ToArray()[comboBoxDriveList.SelectedIndex].VolumeLabel.ToString();
        }
        private void comboBoxDriveList_Click(object sender, EventArgs e)
        {
            List<string> drives = DetectDrive();
            comboBoxDriveList.Items.Clear();
            comboBoxDriveList.Items.AddRange(drives.ToArray());
        }

        private string CreateShelf()
        {
            richTextBoxLog.AppendText("Create shelves... ");
            Application.DoEvents();
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + comboBoxDriveList.SelectedItem + "\\.kobo\\KoboReader.sqlite; Version=3; FailIfMissing=True;"))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sql_query;
                    using (SQLiteTransaction tr = conn.BeginTransaction())
                    {
                        cmd.ExecuteNonQuery();
                        tr.Commit();
                    }
                    conn.Close();
                }
                catch (SQLiteException ex)
                {
                    return ex.Message;
                }
                richTextBoxLog.SelectionFont = new Font(richTextBoxLog.Font, FontStyle.Bold);
                richTextBoxLog.SelectionColor = Color.Green;
                richTextBoxLog.Select(richTextBoxLog.TextLength, 2);
                return "OK";
            }
        }

    }
}
