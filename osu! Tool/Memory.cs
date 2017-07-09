using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace osu__Tool
{
    class Memory
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory
            (IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out] byte[] lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead);

        private Process process;

        public Memory(Process process)
        {
            this.process = process;
        }

        public int FindSignature(byte[] signature, int regionSize, int scanSize)
        {
            int startAddress = (int)process.MainModule.BaseAddress;
            int endAddress = startAddress + scanSize;

            int currentAddress = startAddress;
            int region = regionSize;

            byte[] buffer = new byte[region];

            while (currentAddress < endAddress)
            {
                buffer = ReadBytes(currentAddress, region + signature.Length);
                int index = FindPattern(buffer, signature);

                if (index != -1)
                    return currentAddress + index;

                currentAddress += region;
            }

            throw new Exception("Signature not found");
        }

        public byte[] ReadBytes(int address, int size)
        {
            byte[] buffer = new byte[size];
            ReadProcessMemory(process.Handle, (IntPtr)address, buffer, size, out IntPtr read);
            return buffer;
        }

        public Int32 ReadInt32(int address)
        {
            byte[] buffer = ReadBytes(address, sizeof(Int32));
            return BitConverter.ToInt32(buffer, 0);
        }

        public Boolean ReadBoolean(int address)
        {
            byte[] buffer = ReadBytes(address, sizeof(Boolean));
            return BitConverter.ToBoolean(buffer, 0);
        }

        private int FindPattern(byte[] source, byte[] pattern)
        {
            bool found = false;

            for (int i = 0; i < source.Length - pattern.Length; i++)
            {
                found = true;

                for (int j = 0; j < pattern.Length; j++)
                {
                    if (source[i + j] != pattern[j])
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                    return i;
            }

            return -1;
        }

        public Process Process { get => process; }
    }
}
