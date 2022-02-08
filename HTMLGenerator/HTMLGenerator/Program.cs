using System;
using System.Text.Json;

namespace HTMLGenerator
{
    //realizuoti struktura kur galima realizuti vet klinikos db kr nurokyta kaip su kokiu guvunu zaisti
    class Program
    {
        class Config
        {
            public String path = "";
            public String pageTemplate = "PageTemplate.html";
            public String sourceLink = "";
            public String itemTemplate = "<li><a href = \"%link%\">%name%</a></li>";
            public List<String> ignore = new List<String>();
            public Boolean output = false;
        }

        static Config config = new();
        static void Inform(string content, ConsoleColor color = ConsoleColor.White)
        {

            if (config.output)
            {
                var defcol = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(content);
                Console.ForegroundColor = defcol;
            }
        }
        static Boolean Ignore(DirectoryInfo dir)
        {
            foreach (var item in config.ignore)
            {
                if (item == dir.Name)
                    return true;
            }
            return false;
        }

        static string GenerateItem(DirectoryInfo itemPath, String link)
        {
            Inform($"Adding \"{itemPath.Name}\" to item list");
            String result = String.Empty;
            if(File.Exists(itemPath.FullName + "/index.html"))
                result = config.itemTemplate.Replace("%link%", itemPath.Name + "/index.html");
            else
                result = config.itemTemplate.Replace("%link%", link + itemPath.Name + "/");
            result = result.Replace("%name%", itemPath.Name);
            return result;
        }
        static void Generate()
        {
            String[] rootDirs = Directory.GetDirectories(config.path);
            foreach (var root in rootDirs)
            {
                
                DirectoryInfo rootInfo = new(root);
                if (Ignore(rootInfo))
                {
                    Inform($"Ignoring directory \"{root}\"", ConsoleColor.Red);
                    continue;
                }
                String link = config.sourceLink + rootInfo.Name + "/";
                Inform($"Analyzing sudirectories for \"{root}\"", ConsoleColor.Blue);
                String list = String.Empty;
                String[] subDirs = Directory.GetDirectories(root);
                foreach (var sub in subDirs)
                {
                    DirectoryInfo subInfo = new(sub);
                    if (Ignore(subInfo))
                    {
                        Inform($"Ignoring sub directory \"{sub}\"", ConsoleColor.Red);
                        continue;
                    }
                    list += GenerateItem(subInfo, link);
                }
                Inform($"Saving \"{root}/index.html\"",  ConsoleColor.Green);
                File.WriteAllText(root + "/index.html", File.ReadAllText(config.pageTemplate).Replace("%list%", list).Replace("%root%", new DirectoryInfo(root).Name));
            }
        }
        static void Main()
        {
            
            if (File.Exists("config.json"))
            {
                var options = new JsonSerializerOptions();
                options.IncludeFields = true;
                var tmp = JsonSerializer.Deserialize<Config>(File.ReadAllText("config.json"), options);
                if(tmp!= null)
                    config = tmp;
                if (File.Exists(config.pageTemplate) && Directory.Exists(config.path))
                    Generate();
                else
                {
                    Console.WriteLine($"ERROR: \"{config.path}\" or \"{config.pageTemplate}\" not exists");
                    Console.ReadKey();
                    return;
                }
                if (config.output)
                    Console.ReadKey();
            }
            else
            {
                Console.WriteLine("ERROR: \"config.json\" not exists");
                Console.ReadKey();
            }
        }
    }
}
