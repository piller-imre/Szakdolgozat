using System.IO;
using UnityEngine;

public class myLogger : MonoBehaviour {

    //static string path = Application.dataPath + "Log\\";
    static string path = "Logs\\";
    static string LogFileName;
    static string date;
    static StreamWriter LogFile;
    static MapGenerator mapGenerator;
    //static StreamWriter LogFile = new StreamWriter(path + "Szakdoga_Log_" + System.DateTime.Now + ".txt");
    //static StreamWriter LogFile = new StreamWriter("@C:\\Users\\ASD\\Desktop\\Log.txt");

    public static void CreateLogFile()
    {
        mapGenerator = MapGenerator.Instance;

        if (mapGenerator.LoggerEnabled)
        {
            date = System.DateTime.Now.Year + ".";
            date += System.DateTime.Now.Month + ".";
            date += System.DateTime.Now.Day + "_";
            date += System.DateTime.Now.Hour + ".";
            date += System.DateTime.Now.Minute + ".";
            date += System.DateTime.Now.Second;

            LogFileName = path + "Szakdoga_Log_" + date + ".txt";
            Debug.Log("Log File Created: " + LogFileName);

            bool isLogFileCreated;
            LogFileCreator(out isLogFileCreated);
        }   
    }

    private static void LogFileCreator(out bool isCreated)
    {
        isCreated = false;

        if (mapGenerator.LoggerEnabled)
        {
            LogFile = new StreamWriter(LogFileName);

            LogFile.WriteLine("Project name: Szakdoga");
            LogFile.WriteLine("Date: " + date);
            LogFile.WriteLine("---------------");
            LogFile.Flush();

            isCreated = true;
        }          
    }

	public static void AddToLogFile(string message, bool spaceBefore = false)
    {
        if (mapGenerator.LoggerEnabled)
        {
            if (spaceBefore)
            {
                LogFile.WriteLine("");
            }

            LogFile.WriteLine(message);
            LogFile.Flush();
        }      
    }

    public static void AddToLogFile(GameObject obj, string message, bool spaceBefore = false)
    {
        if (mapGenerator.LoggerEnabled)
        {
            if (spaceBefore)
            {
                LogFile.WriteLine("");
            }

            LogFile.WriteLine(obj + ": " + message);
            LogFile.Flush();
        }   
    }

    public static void AddToLogFile(string obj, string message, bool spaceBefore = false)
    {
        if (mapGenerator.LoggerEnabled)
        {
            if (spaceBefore)
            {
                LogFile.WriteLine("");
            }

            LogFile.WriteLine(obj + ": " + message);
            LogFile.Flush();
        }    
    }

    public static void CloseLogFile()
    {
        if (mapGenerator.LoggerEnabled)
        {
            LogFile.WriteLine("---------------");
            LogFile.WriteLine("app is closed normally");
            LogFile.Close();
        }
    }
}
