using CleanOutPractice.Extensions;

namespace CleanOutPractice.Services;

public class FileService
{
    public virtual void WriteLog(string logCont, string logPath, string logFileName)
    {
        try
        {
            logCont = JsonConvert.Serialize(new { LogTime = DateTime.Now.ToString("O"), Message = logCont });

            ChkFolderPath(logPath, true);

            OutPutTxt(logPath, logFileName, logCont);
        }
        catch
        {
            OutPutTxt(logPath.Replace($".txt", $"-{DateTime.Now:HHmmsss}.txt"), logFileName, logCont);
        }
    }

    public virtual bool ChkFolderPath(string path, bool createTag)
    {
        var result = Directory.Exists(path);

        if (!result && createTag)
            Directory.CreateDirectory(path);

        return result;
    }

    public virtual void OutPutTxt(string folderPath, string fileName, string outStr)
    {
        string filePath = Path.Combine(folderPath, $@"{fileName}");

        ChkFolderPath(folderPath, true);

        using (FileStream fs = new(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
        {
            using (StreamWriter outputFile = new(fs))
            {
                outputFile.WriteLine(outStr);
            }
        }
    }

    public virtual T? ReadJson<T>(string jsonPath) where T : new()
    {
        string json = string.Empty;

        using (StreamReader read = new(jsonPath))
        {
            json = read.ReadToEnd();
        }

        return JsonConvert.Deserialize<T>(json);
    }
}
