using Godot;
using System;

// When using remember to write
//  CustomIndivScript_DegugLogs customIndivScript_DegugLogs = new CustomIndivScript_DegugLogs();

// The main coupling present here is that the script needs to inherit from Node somewhere along the chain.
public class CustomIndivScript_DegugLogs
{
    bool debugMode = false;
    public CustomIndivScript_DegugLogs(bool DebugMode)
    {
        debugMode = DebugMode;
    }

    string baseLogFilesDir = "";
    string logFileDir = "";
    string logFileName = "";
    string pathToLog = "";

    int numFiles = 0;

    public void new_DebugLogFIle(string LogPurpose, Node scriptFile)
    {
        if (debugMode == false)
        {
            return;
        }

        string currentTime = DateTime.Now.ToString("MMMdd_HHmm");
        string currentFile = scriptFile.GetType().ToString();

        baseLogFilesDir = "user://custom_logs/ScriptSpecificLogs";
        logFileDir = "Logs_" + currentFile;
        logFileName = currentFile + "_Log_" + currentTime + ".txt";
        pathToLog = baseLogFilesDir + "/" + logFileDir + "/" + logFileName;

        var logDirectory = DirAccess.Open(baseLogFilesDir + "/" + logFileDir);

        if (logDirectory == null)
        {
            logDirectory = DirAccess.Open(baseLogFilesDir);
            logDirectory.MakeDir(logFileDir);
        }
        else
        {
            logDirectory = DirAccess.Open(baseLogFilesDir + "/" + logFileDir);
            numFiles = logDirectory.GetFiles().Length;
        }

        if (numFiles > 10)
        {
            return;
        }

        using var file = FileAccess.Open(pathToLog, FileAccess.ModeFlags.Write);
        file.StoreString(LogPurpose);
    }

    public void update_DebugLogFile(string LogContents)
    {
        if (debugMode == false || numFiles > 10)
        {
            return;
        }

        using var moveState_LogFile = FileAccess.Open(pathToLog, FileAccess.ModeFlags.ReadWrite);
        moveState_LogFile.SeekEnd();
        moveState_LogFile.StoreString(LogContents);
    }
}