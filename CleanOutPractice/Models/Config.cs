namespace CleanOutPractice.Models;

public class Config
{
    public string UserName { get; set; } = null!;

    public string GameName
    {
        get => $"昊緣 l {UserName}";
    }

    public string StartAppName { get; set; } = null!;

    public double DelaySecond { get; set; }

    public int StartAppTimeOut { get; set; }

    public int StartGameTimeOut { get; set; }

    public int OpenGameDelaySecond { get; set; }

    public string PurpleLauncherPath { get; set; } = null!;

    public bool CloseGame { get; set; }
}