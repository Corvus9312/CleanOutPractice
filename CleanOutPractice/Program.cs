using NetAutoGUI;
using System.Diagnostics;
using CleanOutPractice.Models;
using CleanOutPractice.Services;

namespace CleanOutPractice;

internal class Program
{
    private static FileService _fileService = new();

    private static Config _config = new();

    private static readonly string _imagesFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

    static async Task Main(string[] args)
    {
        _config = _fileService.ReadJson<Config>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs", "config.json"))
            ?? throw new NullReferenceException("Config is null");

        try
        {
            Window? window = GUI.Application.FindWindowByTitle(_config.GameName);
            if (window is null)
            {
                window = GUI.Application.FindWindowByTitle(_config.StartAppName);
                if (window is null)
                {
                    Process.Start(_config.PurpleLauncherPath);
                    window = GUI.Application.WaitForWindowByTitle(_config.StartAppName, _config.StartAppTimeOut * 1000);
                    await Task.Delay((int)(_config.DelaySecond * 15 * 1000));
                }

                window.Activate();
                Console.WriteLine($"開啟{_config.StartAppName}");

                await Task.Delay((int)(_config.DelaySecond * 1000));

                var width = window.Rectangle.Width;
                var height = window.Rectangle.Height;

                Console.WriteLine($"進入{_config.StartAppName}昊緣頁面");
                await MouseClick(window, (int)(0.125 * width), (int)(0.275 * height));

                await Task.Delay((int)(_config.DelaySecond * 1000));
                
                Console.WriteLine("執行遊戲");
                await MouseClick(window, (int)(0.9 * width), (int)(0.875 * height));

                window = GUI.Application.WaitForWindowByTitle(_config.GameName, _config.StartGameTimeOut * 1000);
                await Task.Delay(_config.OpenGameDelaySecond * 1000);
            }

            window.Activate();
            Console.WriteLine($"開啟昊緣");

            await Task.Delay((int)(_config.DelaySecond * 1000));

            await 掃蕩(window);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static async Task 掃蕩(Window window)
    {
        var width = window.Rectangle.Width;
        var height = window.Rectangle.Height;

        Console.WriteLine("若在省電模式中需喚醒");
        await MouseClick(window, (int)(0.5 * width), (int)(0.825 * height));
        await MouseClick(window, (int)(0.5 * width), (int)(0.5 * height));

        await Task.Delay((int)(_config.DelaySecond * 5 * 1000));

        Console.WriteLine("須關閉月卡介面");
        await MouseClick(window, (int)(0.5 * width), (int)(0.825 * height));

        Console.WriteLine("開啟戰役介面");
        await MouseClick(window, (int)(0.94 * width), (int)(0.03 * height));

        Console.WriteLine("思想修練");
        await MouseClick(window, (int)(0.9 * width), (int)(0.15 * height));

        Console.WriteLine("無限模式");
        await MouseClick(window, (int)(0.55 * width), (int)(0.25 * height));

        Console.WriteLine("掃蕩");
        await MouseClick(window, (int)(0.7 * width), (int)(0.7 * height));

        Console.WriteLine("MAX");
        await MouseClick(window, (int)(0.6 * width), (int)(0.425 * height));

        Console.WriteLine("掃蕩開始");
        await MouseClick(window, (int)(0.55 * width), (int)(0.675 * height));

        Console.WriteLine("關閉思想修練");
        await MouseClick(window, (int)(0.975 * width), (int)(0.05 * height));
        await MouseClick(window, (int)(0.975 * width), (int)(0.05 * height));

        if (width <= 1600 && _config.CloseGame)
        {
            Console.WriteLine("關閉遊戲");
            await MouseClick(window, (int)(0.985 * width), (int)-25);

            Console.WriteLine("確認關閉遊戲");
            await MouseClick(window, (int)(0.55 * width), (int)(0.525 * height));
        }
    }

    static async Task MouseClick(Window window, int x, int y)
    {
        await Task.Delay((int)(_config.DelaySecond * 1000));
        Console.WriteLine($"MouseClick:{x}, {y}");
        window.MoveMouseTo(x, y);

        window.MouseDown();
        window.MouseUp();
    }
}