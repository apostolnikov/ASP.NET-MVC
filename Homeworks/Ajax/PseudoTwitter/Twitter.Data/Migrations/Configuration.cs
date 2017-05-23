namespace Twitter.Data.Migrations
{
    using Models;

    using System;
    using System.Web;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.IO;
    using System.Text.RegularExpressions;

    //using System.Security.Claims;

    public sealed class Configuration : DbMigrationsConfiguration<Twitter.Data.TwitterContext>
    {
        private List<string> solPathParts = new List<string>();

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Twitter.Data.TwitterContext context)
        {
            if (!context.Users.Any())
            {
                solPathParts = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\\", @"\").Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                var manager = new UserManager<User>(new UserStore<User>(context));

                if (!context.Roles.Any(r => r.Name == "Admin"))
                {
                    var store = new RoleStore<IdentityRole>(context);
                    var roleManager = new RoleManager<IdentityRole>(store);
                    var role = new IdentityRole { Name = "Admin" };

                    roleManager.Create(role);
                }

                if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
                {
                    var user = new User { UserName = "admin@admin.com", Email = "admin@admin.com", FirstName = "Admin", LastName = "Adminov" };

                    manager.Create(user, "Temp_123");
                    manager.AddToRole(user.Id, "Admin");
                }

                var userHolder = new Queue<User>();
                var rnd = new Random();

                using (var reader = new StreamReader(this.PathBuilder("Twitter.Data", @"/Resources/Users.txt")))
                {
                    string line = reader.ReadLine();
                    int i = 0;

                    while (!string.IsNullOrEmpty(line.Trim()))
                    {
                        var userData = Regex.Split(line, @"\s+\|\s+");

                        var address = new Address()
                        {
                            Country = "Not Important",
                            CountryCode = rnd.Next(1000, 10000).ToString(),
                            City = "SomeCity",
                            ZipCode = rnd.Next(1000, 10000).ToString(),
                            Street = "Lorem ipsum",
                            Number = rnd.Next(1, 56)
                        };

                        context.Address.Add(address);

                        User user = new User()
                        {
                            UserName = userData[0] + userData[1],
                            Email = userData[0] + userData[1] + "@somemail.com",
                            FirstName = userData[0],
                            LastName = userData[1],
                            Gender = (Gender)Enum.Parse(typeof(Gender), userData[2]),
                            CreatedAt = DateTime.Now,
                            LastUpdated = DateTime.Now,
                            UserPicture = userData[3] == "None" ? "https://www.dropbox.com/s/225jip7nvs2gwt6/images%20%283%29.jpg?raw=1" : userData[3],
                            ProfileTheme = userData[4] == "None" ? "https://www.dropbox.com/s/y429g37i0p3yzxt/106838.jpg?raw=1" : userData[4],
                            DateOfBirth = DateTime.ParseExact(userData[6], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                            AboutMe = userData[5],
                            Address = address
                        };

                        manager.Create(user, "Temp_123" + i++);
                        userHolder.Enqueue(user);

                        line = reader.ReadLine();
                    }

                    context.SaveChanges();
                }

                for (int i = 0; i < 4; i++)
                {
                    var user = userHolder.Dequeue();
                    userHolder.Enqueue(user);

                    for (int j = 0; j < 2; j++)
                    {
                        var follower = userHolder.Dequeue();
                        userHolder.Enqueue(follower);

                        user.Followers.Add(follower);
                        context.Users.AddOrUpdate(user);

                        context.SaveChanges();
                    }
                }
            }

            var allusers = context.Users.Where(u => !u.Roles.Any()).OrderBy(u => u.UserName);

            if (!context.Messages.Any())
            {
                for (int i = 1; i <= 16; i++)
                {
                    var sender = allusers.Skip(i % 4).FirstOrDefault();
                    var recipient = allusers.Skip((i - 1) / 4).FirstOrDefault();
                    var message = new Message()
                    {
                        Content = "Message" + i,
                        Date = DateTime.Now,
                        Sender = sender,
                        Recipient = recipient
                    };
                    context.Messages.Add(message);
                }

                context.SaveChanges();
            }

            if (!context.Notifications.Any())
            {
                for (int i = 1; i <= 8; i++)
                {
                    var recipient = allusers.Skip(i % 4).FirstOrDefault();
                    var sender = allusers.Skip(i % 2).FirstOrDefault();
                    var notification = new Notification()
                    {
                        Content = "Notification" + i,
                        Date = DateTime.Now,
                        SendTo = recipient,
                        TriggeredBy = sender
                    };
                    context.Notifications.Add(notification);
                }
            }

            if (!context.Tweets.Any())
            {
                for (int i = 1; i <= 16; i++)
                {
                    var sender = allusers.Skip(i % 4).FirstOrDefault();

                    var tweet = new Tweet()
                    {
                        Text = "Tweet" + i,
                        CreatedAt = DateTime.Now,
                        Author = sender
                    };

                    context.Tweets.Add(tweet);
                }

                context.SaveChanges();
            }

            var allTweets = context.Tweets.OrderBy(u => u.Text);

            if (!context.Reports.Any())
            {
                for (int i = 1; i <= 4; i++)
                {
                    var sender = allusers.Skip(i - 1).FirstOrDefault();
                    var tweet = allTweets.Skip((i + 3) % 4).FirstOrDefault();
                    var report = new Report()
                    {
                        ReportText = "Report" + i,
                        DateOfReport = DateTime.Now,
                        ReportedTweet = tweet,
                        ReportedBy = sender
                    };
                    context.Reports.Add(report);
                }
            }

            if (!context.Tags.Any())
            {
                for (int i = 0; i < 4; i++)
                {
                    var tag = new Tag()
                    {
                        TagName = "Tag" + i
                    };

                    context.Tags.Add(tag);
                }

                context.SaveChanges();
            }

            if (!context.Cities.Any())
            {
                using (var reader = new StreamReader(this.PathBuilder("Twitter.Data", @"/Resources/Cities.txt")))
                {
                    string line = reader.ReadLine();
                    string pattern = @"([a-zA-Z]+)";

                    while (!string.IsNullOrEmpty(line))
                    {
                        Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                        Match m = r.Match(line);

                        if (m.Success)
                        {
                            var city = new City()
                            {
                                CityName = m.Captures[0].Value.Trim()
                            };

                            context.Cities.Add(city);
                        }

                        line = reader.ReadLine();
                    }
                    context.SaveChanges();
                }
            }
        }

        private string PathBuilder(string project, string appending)
        {
            var pathParts = this.solPathParts;
            pathParts[pathParts.Count() - 1] = project;
            string path = string.Join("\\", pathParts) + appending;

            return path;
        }
    }
}
