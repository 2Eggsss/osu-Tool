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
using System.Diagnostics;

namespace osu__Tool
{
    public partial class Form : System.Windows.Forms.Form
    {
        private Osu osu;
        private OsuBeatmap beatmap;

        public Form()
        {
            InitializeComponent();
        }

        private void SetBeatmapLabelText(string text)
        {
            if (InvokeRequired)
                Invoke(new Action(() => beatmapLabel.Text = text));
            else
                beatmapLabel.Text = text;
        }

        private void SearchSongs(string text)
        {
            foreach (string folder in Directory.EnumerateDirectories(osu.SongsPath, "*", SearchOption.TopDirectoryOnly))
            {
                foreach (string file in Directory.EnumerateFiles(folder, "*.osu", SearchOption.TopDirectoryOnly))
                {
                    string fileName = file.Substring(file.LastIndexOf("\\") + 1);

                    string songArtistSep = " - ";
                    int songArtistIndex = fileName.IndexOf(songArtistSep);

                    string artist = fileName.Remove(songArtistIndex);

                    string name = fileName.Substring(songArtistIndex + songArtistSep.Length);
                    name = name.Remove(name.LastIndexOf(" ("));

                    string creator = fileName.Substring(fileName.LastIndexOf('(') + 1);
                    creator = creator.Remove(creator.IndexOf(')'));

                    string difficulty = fileName.Substring(fileName.LastIndexOf('[') + 1);
                    difficulty = difficulty.Remove(difficulty.IndexOf(']'));

                    string searchText = text.ToLower();

                    if (artist.ToLower().Contains(searchText) ||
                        name.ToLower().Contains(searchText) ||
                        creator.ToLower().Contains(searchText) ||
                        difficulty.ToLower().Contains(searchText))
                    {
                        DataGridViewRow row = new DataGridViewRow()
                        {
                            // Keep track of all file paths.
                            Tag = file
                        };

                        row.CreateCells(songsGridView, artist, name, creator, difficulty);

                        if (InvokeRequired)
                            Invoke(new Action(() => songsGridView.Rows.Add(row)));
                        else
                            songsGridView.Rows.Add(row);
                    }
                }
            }
        }

        private async Task SearchSongsAsync(string text)
        {
            await Task.Run(() => SearchSongs(text));
        }

        private void LoadSettings()
        {
            ezCheckBox.Checked = Settings.Default.Easy;
            hrCheckBox.Checked = Settings.Default.HardRock;
            hitScanCheckBox.Checked = Settings.Default.HitScan;
            hitScanPixelOffsetNumeric.Value = Settings.Default.HitScanPixelOffset;
            randomizeKeyTimingsCheckBox.Checked = Settings.Default.RandomizeKeyTimings;
            timingOffsetNumeric.Value = Settings.Default.TimingOffset;
            tappingStyleComboBox.SelectedIndex = Settings.Default.TappingStyle;
            singleTapMaxBPMNumeric.Value = Settings.Default.SingleTapMaxBPM;
            dtCheckBox.Checked = Settings.Default.DoubleTime;
            htCheckBox.Checked = Settings.Default.HalfTime;
        }

        public void StartCheckThread()
        {
            // Hopefully the application exits before we get any errors about reading memory.
            Task.Run(() =>
            {
                while (true)
                {
                    if (!osu.IsRunning())
                        Application.Exit();

                    Task.Delay(1).Wait();
                }
            });
        }

        public void StartPlayThread()
        {
            Play play = new Play();

            // GetScaledX and GetScaledY depend on window size and position.
            // Doesn't seem to visibly increase CPU usage.
            Task.Run(() =>
            {
                while (true)
                {
                    play.UpdateWindow(osu.MainWindowHandle);
                    Task.Delay(500).Wait();
                }
            });

            // This thread will check if a beatmap is loaded and the user is in play mode.
            Task.Run(() =>
            {
                while (true)
                {
                    play.Run(ref osu, ref beatmap);
                    Task.Delay(1).Wait();
                }
            });
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            LoadSettings();
            await Task.Run(() => osu = new Osu());
            StartCheckThread();
            StartPlayThread();
            await SearchSongsAsync(String.Empty);
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private async void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            // Not fully initialized yet.
            if (osu == null)
                return;

            songsGridView.Rows.Clear();
            await SearchSongsAsync(searchTextBox.Text);
        }

        private void EzCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ezCheckBox.Checked && hrCheckBox.Checked)
                hrCheckBox.Checked = false;

            Settings.Default.Easy = ezCheckBox.Checked;
        }

        private void HrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (hrCheckBox.Checked && ezCheckBox.Checked)
                ezCheckBox.Checked = false;

            Settings.Default.HardRock = hrCheckBox.Checked;
        }

        private void HitScanCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.HitScan = hitScanCheckBox.Checked;
        }

        private void HitScanPixelOffsetNumeric_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.HitScanPixelOffset = (int)hitScanPixelOffsetNumeric.Value;
        }

        private void RandomizeKeyHoldTime_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.RandomizeKeyTimings = randomizeKeyTimingsCheckBox.Checked;
        }

        private void TimingOffsetNumeric_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.TimingOffset = (int)timingOffsetNumeric.Value;
        }

        private void TappingStyleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.TappingStyle = tappingStyleComboBox.SelectedIndex;
        }

        private void SingleTapMaxBPMNumeric_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.SingleTapMaxBPM = (int)singleTapMaxBPMNumeric.Value;
        }

        private void DtCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (dtCheckBox.Checked && htCheckBox.Checked)
                htCheckBox.Checked = false;

            Settings.Default.DoubleTime = dtCheckBox.Checked;
        }

        private void HtCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (htCheckBox.Checked && dtCheckBox.Checked)
                dtCheckBox.Checked = false;

            Settings.Default.HalfTime = htCheckBox.Checked;
        }

        private async void SongsGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            DataGridViewRow currentRow = dgv.CurrentRow;

            if (!currentRow.Selected)
                return;

            await Task.Run(() =>
            {
                string beatmapPath = String.Empty;
                Invoke(new Action(() => beatmapPath = (string)currentRow.Tag));

                beatmap = new OsuBeatmap(beatmapPath, ezCheckBox.Checked, hrCheckBox.Checked);

                string beatmapFileName = beatmapPath.Substring(beatmapPath.LastIndexOf("\\") + 1); // Remove full folder path.
                beatmapFileName = beatmapFileName.Remove(beatmapFileName.IndexOf(".osu")); // Remove extension.
                SetBeatmapLabelText(beatmapFileName);

                //osu.MaximizeWindow();
                //osu.FocusWindow();
            });
        }
    }
}
