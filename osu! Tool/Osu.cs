using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace osu__Tool
{
    public sealed class Osu
    {
        private Memory memory;
        private string songsPath;
        private int audioTimeAddress;
        private int audioPlayingAddress;

        public string SongsPath { get => songsPath; }
        public IntPtr MainWindowHandle { get => memory.Process.MainWindowHandle; }

        public Osu()
        {
            GetProcess();
            GetSongsPath();
            GetAddresses();
        }

        public bool IsRunning()
        {
            return !memory.Process.HasExited;
        }

        public bool IsMapPlaying()
        {
            // MainWindowTitle stays the same after creating the process object, so refresh everything.
            memory.Process.Refresh();
            return memory.Process.MainWindowTitle.Contains('-');
        }

        public void MaximizeWindow()
        {
            ShowWindowAsync(MainWindowHandle, SW_SHOWMAXIMIZED);
        }

        public void FocusWindow()
        {
            SetForegroundWindow(MainWindowHandle);
        }

        public int GetAudioTime()
        {
            return memory.ReadInt32(audioTimeAddress);
        }

        public bool IsAudioPlaying()
        {
            return memory.ReadBoolean(audioPlayingAddress);
        }

        private void GetProcess()
        {
            const string processName = "osu!";
            Process[] processes = Process.GetProcessesByName(processName);

            // Process is not running yet, keep checking.
            while (processes.Length == 0)
            {
                processes = Process.GetProcessesByName(processName);
                Task.Delay(1000).Wait();
            }

            memory = new Memory(processes.First());
        }

        private void GetSongsPath()
        {
            string processPath = memory.Process.MainModule.FileName;
            string osuPath = processPath.Substring(0, processPath.LastIndexOf("\\") + 1);
            string beatmapDirectory = String.Empty;

            foreach (string i in File.ReadLines(osuPath + "osu!." + Environment.UserName + ".cfg"))
            {
                if (i.Contains("BeatmapDirectory"))
                {
                    beatmapDirectory = i.Split(new string[] { " = " }, StringSplitOptions.None).Last();
                    break;
                }
            }

            // Is a full path.
            if (beatmapDirectory.Contains(':'))
                songsPath = beatmapDirectory;
            else
                songsPath = osuPath + beatmapDirectory;
        }

        private void GetAddresses()
        {
            int addressPtr = memory.FindSignature(new byte[] { 0xDB, 0x5D, 0xE8, 0x8B, 0x45, 0xE8, 0xA3 }, 0x1000, 0x10000000);

            audioTimeAddress = memory.ReadInt32(addressPtr + 0x7);
            audioPlayingAddress = audioTimeAddress + 0x24;
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
    }
}
