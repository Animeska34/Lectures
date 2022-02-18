using System;
using System.Text.Json;

namespace HTMLGenerator
{
    class Program
    {
        class Config
        {
            public String path = String.Empty;
            public String pageTemplate = "PageTemplate.html";
            public String directoryTemplate = "DirectoryTemplate.html";
            public String sourceLink = String.Empty;
            public String linkTemplate = "<li><a href = \"%link%\">%name%</a></li>";
            public String unsupportedDataTemplate = "<li><a href = \"%link%\">%name%</a></li>";
            public List<String> ignoreFolders = new List<String>();
            public List<String> ignoreFiles = new List<String>();
            public Boolean output = false;
            public String rawLink = String.Empty;
            public Dictionary<String, String> rawDataTemplates = new Dictionary<String, String>();
        }

        static Config config = new();
        static string tmphtml = String.Empty;
        static string tmppage = String.Empty;

        static List<String> generated = new();
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
            foreach (var item in config.ignoreFolders)
            {
                if (item == dir.Name)
                    return true;
            }
            return false;
        }

        static Boolean Ignore(FileInfo file)
        {
            foreach (var item in config.ignoreFiles)
            {
                if (item == file.Extension || file.Name == "index.html")
                    return true;
            }
            return false;
        }

        static string GenerateItem(DirectoryInfo itemPath, String link)
        {
            Inform($"\tAdding \"{itemPath.Name}\" to item list");
            String result = String.Empty;
            if(File.Exists(itemPath.FullName + "/index.html"))
                result = config.linkTemplate.Replace("%link%", itemPath.Name + "/index.html");
            else
                result = config.linkTemplate.Replace("%link%", link + itemPath.Name + "/");
            result = result.Replace("%name%", itemPath.Name);
            return result;
        }

        static Boolean TryGeneratePage(DirectoryInfo directory, String rawLink)
        {
            if (File.Exists(directory.FullName + "/index.html") && !generated.Contains(directory.Name))
                return false;
            Inform($"\t\tStarting generating page for {directory.Name}", ConsoleColor.Blue);
            if (!generated.Contains(directory.Name))
            {
                Inform($"\t\tDirectory {directory.Name} missing in registry. Adding...", ConsoleColor.DarkYellow);
                generated.Add(directory.Name);
            }
            String list = String.Empty;
            foreach (var item in Directory.GetFiles(directory.FullName))
            {
                FileInfo file = new FileInfo(item);
                if(Ignore(file))
                {
                    Inform($"\t\tIgnoring {file.Name}", ConsoleColor.Red);
                    continue;
                }
                Inform($"\t\tProcessing {file.Name}");
                if (config.rawDataTemplates.ContainsKey(file.Extension))
                {
                    list += config.rawDataTemplates[file.Extension].Replace("%link%", file.Name).Replace("%name%", file.Name).Replace("%content%", File.ReadAllText(file.FullName)).Replace("%rawLink%", rawLink + directory.Name + "/" + file.Name);
                }
                else
                {
                    Inform($"\t\tTemplate for {file.Extension} not found, using universal template", ConsoleColor.DarkYellow);
                    list += config.unsupportedDataTemplate.Replace("%link%", file.Name).Replace("%name%", file.Name).Replace("%content%", File.ReadAllText(file.FullName)).Replace("%rawLink%", rawLink + directory.Name + "/" + file.Name);
                }
            }
            Inform($"\t\tSaving \"{directory.Name}/index.html\"", ConsoleColor.Green);
            var html = tmppage;
            html = html.Replace("%list%", list);
            html = html.Replace("%root%", directory.Name);
            html = html.Replace("%timestamp%", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            File.WriteAllText(directory.FullName + "/index.html", html);
            return true;
        }
        static void Generate()
        {
            String[] rootDirs = Directory.GetDirectories(config.path);
            tmphtml = File.ReadAllText(config.directoryTemplate);
            tmppage = File.ReadAllText(config.pageTemplate);

            foreach (var root in rootDirs)
            {
                
                DirectoryInfo rootInfo = new(root);
                if (Ignore(rootInfo))
                {
                    Inform($"Ignoring directory \"{root}\"", ConsoleColor.Red);
                    continue;
                }
                String link = config.sourceLink + rootInfo.Name + "/";
                String rawLink = config.rawLink + rootInfo.Name + "/";
                Inform($"Analyzing sudirectories for \"{root}\"", ConsoleColor.Blue);
                String list = String.Empty;
                String[] subDirs = Directory.GetDirectories(root);
                foreach (var sub in subDirs)
                {
                    DirectoryInfo subInfo = new(sub);
                    if (Ignore(subInfo))
                    {
                        Inform($"\tIgnoring sub directory \"{sub}\"", ConsoleColor.Red);
                        continue;
                    }
                    if(Directory.GetFiles(subInfo.FullName).Length == 0)
                    {
                        if (Ignore(subInfo))
                        {
                            Inform($"\tSub directory \"{sub}\" is empty, ignoring", ConsoleColor.DarkYellow);
                            continue;
                        }
                    }
                    TryGeneratePage(subInfo, rawLink);
                    list += GenerateItem(subInfo, link);
                }
                Inform($"Saving \"{root}/index.html\"",  ConsoleColor.Green);
                var html = tmphtml;
                html = html.Replace("%list%", list);
                html = html.Replace("%root%", new DirectoryInfo(root).Name);
                html = html.Replace("%timestamp%", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                File.WriteAllText(root + "/index.html", html);
            }
        }
        static void Main()
        {
            
            if (File.Exists("config.json"))
            {
                var options = new JsonSerializerOptions();
                options.IncludeFields = true;
                var tmp = JsonSerializer.Deserialize<Config>(File.ReadAllText("config.json"), options);
                
                if (tmp!= null)
                    config = tmp;

                if (File.Exists("generated.json"))
                {
                    var genTmp = JsonSerializer.Deserialize<List<string>>(File.ReadAllText("generated.json"), options);
                    if (genTmp != null)
                        generated = genTmp;
                }
                if (File.Exists(config.pageTemplate) && File.Exists(config.directoryTemplate) && Directory.Exists(config.path))
                {
                    Generate();
                    File.WriteAllText("generated.json", JsonSerializer.Serialize(generated, options));
                }
                else
                {
                    Console.WriteLine($"ERROR: \"{config.path}\" or \"{config.pageTemplate}\" or \"{config.directoryTemplate}\" not exists");
                    Console.ReadKey();
                    return;
                }
                if (config.output)
                {
                    Inform("All tasks complete. Press any key to continue...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("ERROR: \"config.json\" not exists");
                Console.ReadKey();
            }
        }
    }
}
