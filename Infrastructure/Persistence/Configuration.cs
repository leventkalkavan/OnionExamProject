using Microsoft.Extensions.Configuration;

namespace Persistence;

public class Configuration
{
    public static string? GetConnectionString
    {
        get
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
                "/Users/leventkalkavan/Desktop/Projeler/ExamProjectOnionArchitecture/Presentation/WebAPI"));
            configurationManager.AddJsonFile("appsettings.json");

            return configurationManager.GetConnectionString("DefaultConnection");
        }
    }
}