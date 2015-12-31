using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grabber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // get all removable dirves
            List<DriveInfo> colDrives = new List<DriveInfo>();
            colDrives = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Removable).ToList();

            // show in drop down
            List<string> colDriveLabels = new List<string>();
            foreach (DriveInfo dr in colDrives.Where(d => d.IsReady))
            {
                colDriveLabels.Add(dr.Name + " - " + dr.VolumeLabel);
            }
            ddlDrive.DataSource = colDriveLabels;
            InitializeForm();
        }

        private void InitializeForm()
        {
            pbStatus.Maximum = 100;
            lblStatus.Text = "";
            lblPic.Text = "";
            lblVid.Text = "";
            ddlPhone.Visible = false;
        }

        // get file info objects from removable drive
        private MediaInfo GetMediaInfo()
        {
            MediaInfo mi = new MediaInfo();
            mi.FilesImage = new List<FileInfo>();
            mi.FilesVideo = new List<FileInfo>();

            DirectoryInfo di = new DirectoryInfo(ddlDrive.Text.Split('-')[0].TrimEnd());
            List<FileInfo> colFilesIn = new List<FileInfo>(di.GetFiles("*.*", SearchOption.AllDirectories));
            List<string> colImageExtensions = new List<string>();
            List<string> colVideoExtensions = new List<string>();
            colImageExtensions = ConfigurationManager.AppSettings["ExtensionImage"].Split('|').ToList();
            colVideoExtensions = ConfigurationManager.AppSettings["ExtensionVideo"].Split('|').ToList();

            // filter out files that we don't care about
            foreach (FileInfo fi in colFilesIn)
            {
                if (colImageExtensions.Contains(fi.Extension.TrimStart('.').ToLower()))
                {
                    mi.FilesImage.Add(fi);
                }
                else if (colVideoExtensions.Contains(fi.Extension.TrimStart('.').ToLower()))
                {
                    mi.FilesVideo.Add(fi);
                }
            }

            return mi;
        }

        // set status labels
        private void SetStatusLabels(int Current, int Total, bool IsImage)
        {
            string msg = Current.ToString() + " of " + Total.ToString();

            if (IsImage)
            {
                lblPic.Text = "Copying image file: " + msg;
            }
            else
            {
                lblVid.Text = "Copying video file: " + msg;
            }
        }

        // button click
        private void btnGo_Click(object sender, EventArgs e)
        {
            // populate Copy Info object and pass to background worker
            CopyInfo ci = new CopyInfo();
            ci.FilesImage = new List<FileInfo>(GetMediaInfo().FilesImage);
            ci.FilesVideo = new List<FileInfo>(GetMediaInfo().FilesVideo);

            // start background thread
            bwCopy.RunWorkerAsync(ci);

            // wire up listener
            bwCopy.ProgressChanged += bwCopy_ProgressChanged;

            // disable controls
            ddlDrive.Enabled = !bwCopy.IsBusy;
            btnGo.Enabled = !bwCopy.IsBusy;
        }

        // handle progress updates from background worker
        void bwCopy_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CopyStatus cs = (CopyStatus)e.UserState;
            if (cs.IsImage)
            {
                SetStatusLabels(cs.ImageCurrent, cs.ImageTotal, true);
            }
            else
            {
                SetStatusLabels(cs.VideoCurrent, cs.VideoTotal, false);
            }
            pbStatus.Maximum = 100;
            pbStatus.Value = e.ProgressPercentage;
            lblStatus.Text = cs.Filename;
        }

        // update labels when drive is changed
        private void ddlDrive_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStatusLabels(0, GetMediaInfo().FilesImage.Count, true);
            SetStatusLabels(0, GetMediaInfo().FilesVideo.Count, false);
            lblStatus.Text = "";
            pbStatus.Value = 0;
        }

        // this is the background worker that does the file copies
        private void bwCopy_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;

            CopyInfo ci = (CopyInfo)e.Argument;

            CopyStatus cs = new CopyStatus();
            cs.ImageCurrent = 0;
            cs.ImageTotal = ci.FilesImage.Count;
            cs.VideoCurrent = 0;
            cs.VideoTotal = ci.FilesVideo.Count;

            // copy images
            foreach (FileInfo fi in ci.FilesImage)
            {
                cs.ImageCurrent++;

                //// target path should be C:\Users\Public\Pictures\YYYY\YYYY-MM-DD
                string targetDirPath = Path.Combine(ConfigurationManager.AppSettings["TargetPathImage"], fi.LastWriteTime.Year.ToString());
                targetDirPath = Path.Combine(targetDirPath, fi.LastWriteTime.Year.ToString() + "-" + fi.LastWriteTime.ToString("MM"));
                List<string> colImageRawExtensions = new List<string>();
                colImageRawExtensions = ConfigurationManager.AppSettings["ExtensionImageRaw"].Split('|').ToList();

                // put raw files in their own folder
                if (colImageRawExtensions.Contains(fi.Extension.TrimStart('.').ToLower()))
                {
                    targetDirPath = Path.Combine(targetDirPath, "raw");
                }
                bool targetPathExists = new DirectoryInfo(targetDirPath).Exists;
                if (!targetPathExists)
                {
                    new DirectoryInfo(targetDirPath).Create();
                }
                string targetFilePath = Path.Combine(targetDirPath, fi.Name);
                bool targetFileExists = new FileInfo(targetFilePath).Exists;
                cs.Filename = targetFilePath;
                
                if (!targetFileExists)
                {
                    fi.CopyTo(targetFilePath);
                }

                // update progress stats and send
                int progress = (int)((float)cs.ImageCurrent / (float)(cs.ImageTotal + cs.VideoTotal) * 100);
                cs.IsImage = true;
                bw.ReportProgress(progress, cs);
            }

            ////copy videos
            foreach (FileInfo fi in ci.FilesVideo)
            {
                cs.VideoCurrent++;

                // target path should be C:\Users\Public\Videos\YYYY\YYYY-MM-DD
                string targetDirPath = Path.Combine(ConfigurationManager.AppSettings["TargetPathVideo"], fi.LastWriteTime.Year.ToString());
                targetDirPath = Path.Combine(targetDirPath, fi.LastWriteTime.Year.ToString() + "-" + fi.LastWriteTime.ToString("MM"));

                bool targetPathExists = new DirectoryInfo(targetDirPath).Exists;
                if (!targetPathExists)
                {
                    new DirectoryInfo(targetDirPath).Create();
                }
                string targetFilePath = Path.Combine(targetDirPath, fi.Name);
                bool targetFileExists = new FileInfo(targetFilePath).Exists;
                cs.Filename = targetFilePath;
                if (!targetFileExists)
                {
                    fi.CopyTo(targetFilePath);
                }

                // update progress stats and send
                int progress = (int)((float)(cs.VideoCurrent + cs.ImageTotal) / (float)(cs.ImageTotal + cs.VideoTotal) * 100);
                cs.IsImage = false;
                bw.ReportProgress(progress, cs);
            }
        }

        // clean up when background worker completes
        private void bwCopy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ddlDrive.Enabled = !bwCopy.IsBusy;
            btnGo.Enabled = !bwCopy.IsBusy;
            lblStatus.Text = "Copy complete!";
        }
    }
}