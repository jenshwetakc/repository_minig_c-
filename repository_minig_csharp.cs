using LibGit2Sharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace repository_minig_csharp
{
    public class repository_minig_csharp
    {
        static void Main(string[] args)
        {
            string repoPath = "C:/project/repository_mining";
            List<commitInfo> commitList = new List<commitInfo>();

            using (var repo = new Repository(repoPath))
            {
                var commits = repo.Commits;


                foreach (var commit in commits)
                {
                    string commitHash = commit.Id.Sha;
                    string author = commit.Author.Name;
                    DateTimeOffset commitDate = commit.Author.When;
                    string message = commit.Message;
                    commitInfo commitInfo = new commitInfo
                    {
                        CommitHash = commitHash,
                        Author = author,
                        CommitDate = commitDate,
                        Message = message
                    };
                    commitList.Add(commitInfo);
                }
                string projectPath = GetProjectPath();
                Console.WriteLine("Project Path: " + projectPath);

                int totalCommits = commits.Count();
                Console.WriteLine("Total no of commits:" + totalCommits);

            }
            var json = JsonConvert.SerializeObject(commitList, Formatting.Indented);
            Console.WriteLine(json);


        }
        static string GetProjectPath()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            string assemblyLocation = entryAssembly.Location;
            string projectPath = Path.GetDirectoryName(assemblyLocation);
            return projectPath;
        }

        class commitInfo
        {
            public string CommitHash { get; set; }
            public string Author { get; set; }
            public DateTimeOffset CommitDate { get; set; }
            public string Message { get; set; }

        }
    }




}

