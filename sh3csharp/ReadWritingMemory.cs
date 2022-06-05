using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic; // Install-Package Microsoft.VisualBasic

internal static partial class ReadWritingMemory
{
    [DllImport("kernel32")]
    private static extern int OpenProcess(int dwDesiredAccess, int bInheritHandle, int dwProcessId);

    [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
    private static extern int WriteProcessMemory1(int hProcess, int lpBaseAddress, ref int lpBuffer, int nSize, ref int lpNumberOfBytesWritten);
    [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
    private static extern float WriteProcessMemory2(int hProcess, int lpBaseAddress, ref float lpBuffer, int nSize, ref int lpNumberOfBytesWritten);
    [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
    private static extern long WriteProcessMemory3(int hProcess, int lpBaseAddress, ref long lpBuffer, int nSize, ref int lpNumberOfBytesWritten);

    [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
    private static extern int ReadProcessMemory1(int hProcess, int lpBaseAddress, ref int lpBuffer, int nSize, ref int lpNumberOfBytesWritten);
    [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
    private static extern float ReadProcessMemory2(int hProcess, int lpBaseAddress, ref float lpBuffer, int nSize, ref int lpNumberOfBytesWritten);
    [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
    private static extern long ReadProcessMemory3(int hProcess, int lpBaseAddress, ref long lpBuffer, int nSize, ref int lpNumberOfBytesWritten);

    private const int PROCESS_ALL_ACCESS = 0x1F0FF;

    public static bool WriteDMAInteger(string Process, int Address, int[] Offsets, int Value, int Level, int nsize = 4)
    {
        try
        {
            int lvl = Address;
            for (int i = 1, loopTo = Level; i <= loopTo; i++)
                lvl = ReadInteger(Process, lvl, nsize) + Offsets[i - 1];
            WriteInteger(Process, lvl, Value, nsize);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static int ReadDMAInteger(string Process, int Address, int[] Offsets, int Level, int nsize = 4)
    {
        try
        {
            int lvl = Address;
            for (int i = 1, loopTo = Level; i <= loopTo; i++)
                lvl = ReadInteger(Process, lvl, nsize) + Offsets[i - 1];
            int vBuffer;
            vBuffer = ReadInteger(Process, lvl, nsize);
            return vBuffer;
        }
        catch (Exception ex)
        {

        }

        return default;
    }

    public static bool WriteDMAFloat(string Process, int Address, int[] Offsets, float Value, int Level, int nsize = 4)
    {
        try
        {
            int lvl = Address;
            for (int i = 1, loopTo = Level; i <= loopTo; i++)
                lvl = (int)Math.Round(ReadFloat(Process, lvl, nsize) + Offsets[i - 1]);
            WriteFloat(Process, lvl, Value, nsize);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static float ReadDMAFloat(string Process, int Address, int[] Offsets, int Level, int nsize = 4)
    {
        try
        {
            int lvl = Address;
            for (int i = 1, loopTo = Level; i <= loopTo; i++)
                lvl = (int)Math.Round(ReadFloat(Process, lvl, nsize) + Offsets[i - 1]);
            float vBuffer;
            vBuffer = ReadFloat(Process, lvl, nsize);
            return vBuffer;
        }
        catch (Exception ex)
        {

        }

        return default;
    }

    public static bool WriteDMALong(string Process, int Address, int[] Offsets, long Value, int Level, int nsize = 4)
    {
        try
        {
            int lvl = Address;
            for (int i = 1, loopTo = Level; i <= loopTo; i++)
                lvl = (int)(ReadLong(Process, lvl, nsize) + Offsets[i - 1]);
            WriteLong(Process, lvl, Value, nsize);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static long ReadDMALong(string Process, int Address, int[] Offsets, int Level, int nsize = 4)
    {
        try
        {
            int lvl = Address;
            for (int i = 1, loopTo = Level; i <= loopTo; i++)
                lvl = (int)(ReadLong(Process, lvl, nsize) + Offsets[i - 1]);
            long vBuffer;
            vBuffer = ReadLong(Process, lvl, nsize);
            return vBuffer;
        }
        catch (Exception ex)
        {

        }

        return default;
    }

    public static void WriteNOPs(string ProcessName, long Address, int NOPNum)
    {
        int C;
        int B;
        if (ProcessName.EndsWith(".exe"))
        {
            ProcessName = ProcessName.Replace(".exe", "");
        }
        var MyP = Process.GetProcessesByName(ProcessName);
        if (MyP.Length == 0)
        {

            return;
        }
        IntPtr hProcess = (IntPtr)OpenProcess(PROCESS_ALL_ACCESS, 0, MyP[0].Id);
        if (hProcess == IntPtr.Zero)
        {

            return;
        }

        B = 0;
        var loopTo = NOPNum;
        for (C = 1; C <= loopTo; C++)
        {
            int arglpBuffer = 0x90;
            int arglpNumberOfBytesWritten = 0;
            WriteProcessMemory1((int)hProcess, (int)(Address + B), ref arglpBuffer, 1, ref arglpNumberOfBytesWritten);
            B = B + 1;
        }
    }

    public static void WriteXBytes(string ProcessName, long Address, string Value)
    {
        if (ProcessName.EndsWith(".exe"))
        {
            ProcessName = ProcessName.Replace(".exe", "");
        }
        var MyP = Process.GetProcessesByName(ProcessName);
        if (MyP.Length == 0)
        {

            return;
        }
        IntPtr hProcess = (IntPtr)OpenProcess(PROCESS_ALL_ACCESS, 0, MyP[0].Id);
        if (hProcess == IntPtr.Zero)
        {

            return;
        }

        int C;
        int B;
        int D;
        byte V;

        B = 0;
        D = 1;
        var loopTo = (int)Math.Round(Math.Round(Strings.Len(Value) / 2d));
        for (C = 1; C <= loopTo; C++)
        {
            V = (byte)Math.Round(Conversion.Val("0x" + Strings.Mid(Value, D, 2)));
            int arglpBuffer = V;
            int arglpNumberOfBytesWritten = 0;
            WriteProcessMemory1((int)hProcess, (int)(Address + B), ref arglpBuffer, 1, ref arglpNumberOfBytesWritten);
            V = (byte)arglpBuffer;
            B = B + 1;
            D = D + 2;
        }

    }

    public static void WriteInteger(string ProcessName, int Address, int Value, int nsize = 4)
    {
        if (ProcessName.EndsWith(".exe"))
        {
            ProcessName = ProcessName.Replace(".exe", "");
        }
        var MyP = Process.GetProcessesByName(ProcessName);
        if (MyP.Length == 0)
        {

            return;
        }
        IntPtr hProcess = (IntPtr)OpenProcess(PROCESS_ALL_ACCESS, 0, MyP[0].Id);
        if (hProcess == IntPtr.Zero)
        {

            return;
        }

        int hAddress, vBuffer;
        hAddress = Address;
        vBuffer = Value;
        int arglpBuffer = vBuffer;
        int arglpNumberOfBytesWritten = 0;
        WriteProcessMemory1((int)hProcess, hAddress, ref arglpBuffer, nsize, ref arglpNumberOfBytesWritten);
    }

    public static void WriteFloat(string ProcessName, int Address, float Value, int nsize = 4)
    {
        if (ProcessName.EndsWith(".exe"))
        {
            ProcessName = ProcessName.Replace(".exe", "");
        }
        var MyP = Process.GetProcessesByName(ProcessName);
        if (MyP.Length == 0)
        {

            return;
        }
        IntPtr hProcess = (IntPtr)OpenProcess(PROCESS_ALL_ACCESS, 0, MyP[0].Id);
        if (hProcess == IntPtr.Zero)
        {

            return;
        }

        int hAddress;
        float vBuffer;

        hAddress = Address;
        vBuffer = Value;
        int arglpNumberOfBytesWritten = 0;
        WriteProcessMemory2((int)hProcess, hAddress, ref vBuffer, nsize, ref arglpNumberOfBytesWritten);
    }

    public static void WriteLong(string ProcessName, int Address, long Value, int nsize = 4)
    {
        if (ProcessName.EndsWith(".exe"))
        {
            ProcessName = ProcessName.Replace(".exe", "");
        }
        var MyP = Process.GetProcessesByName(ProcessName);
        if (MyP.Length == 0)
        {

            return;
        }
        IntPtr hProcess = (IntPtr)OpenProcess(PROCESS_ALL_ACCESS, 0, MyP[0].Id);
        if (hProcess == IntPtr.Zero)
        {

            return;
        }

        int hAddress;
        long vBuffer;

        hAddress = Address;
        vBuffer = Value;
        int arglpNumberOfBytesWritten = 0;
        WriteProcessMemory3((int)hProcess, hAddress, ref vBuffer, nsize, ref arglpNumberOfBytesWritten);
    }

    public static int ReadInteger(string ProcessName, int Address, int nsize = 4)
    {
        if (ProcessName.EndsWith(".exe"))
        {
            ProcessName = ProcessName.Replace(".exe", "");
        }
        var MyP = Process.GetProcessesByName(ProcessName);
        if (MyP.Length == 0)
        {

            return default;
        }
        IntPtr hProcess = (IntPtr)OpenProcess(PROCESS_ALL_ACCESS, 0, MyP[0].Id);
        if (hProcess == IntPtr.Zero)
        {

            return default;
        }

        int hAddress, vBuffer = default;
        hAddress = Address;
        int arglpNumberOfBytesWritten = 0;
        ReadProcessMemory1((int)hProcess, hAddress, ref vBuffer, nsize, ref arglpNumberOfBytesWritten);
        return vBuffer;
    }

    public static float ReadFloat(string ProcessName, int Address, int nsize = 4)
    {
        if (ProcessName.EndsWith(".exe"))
        {
            ProcessName = ProcessName.Replace(".exe", "");
        }
        var MyP = Process.GetProcessesByName(ProcessName);
        if (MyP.Length == 0)
        {

            return default;
        }
        IntPtr hProcess = (IntPtr)OpenProcess(PROCESS_ALL_ACCESS, 0, MyP[0].Id);
        if (hProcess == IntPtr.Zero)
        {

            return default;
        }

        int hAddress;
        var vBuffer = default(float);

        hAddress = Address;
        int arglpNumberOfBytesWritten = 0;
        ReadProcessMemory2((int)hProcess, hAddress, ref vBuffer, nsize, ref arglpNumberOfBytesWritten);
        return vBuffer;
    }

    public static long ReadLong(string ProcessName, int Address, int nsize = 4)
    {
        if (ProcessName.EndsWith(".exe"))
        {
            ProcessName = ProcessName.Replace(".exe", "");
        }
        var MyP = Process.GetProcessesByName(ProcessName);
        if (MyP.Length == 0)
        {

            return default;
        }
        IntPtr hProcess = (IntPtr)OpenProcess(PROCESS_ALL_ACCESS, 0, MyP[0].Id);
        if (hProcess == IntPtr.Zero)
        {

            return default;
        }

        int hAddress;
        var vBuffer = default(long);

        hAddress = Address;
        int arglpNumberOfBytesWritten = 0;
        ReadProcessMemory3((int)hProcess, hAddress, ref vBuffer, nsize, ref arglpNumberOfBytesWritten);
        return vBuffer;
    }

}