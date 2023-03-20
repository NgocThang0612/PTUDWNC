using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;

namespace TatBlog.Data.Seeders;

public class DataSeeder : IDataSeeder
{
    private readonly BlogDBContext _dbContext;

    public DataSeeder(BlogDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Initialize()
    {
        _dbContext.Database.EnsureCreated();

        if (_dbContext.Posts.Any()) return;

        var authors = AddAuthors();
        var categories = AddCategories();
        var tags = AddTags();
        var posts = AddPosts(authors, categories, tags);
    }

    private IList<Author> AddAuthors() 
    {
        var authors = new List<Author>()
        {
            new()
            {
                FullName = "NgocThang",
                UrlSlug = "ngoc-thang",
                Email = "ngocthang00@gmail.con",
                JoinedDate= new DateTime(2022, 2, 22)
                
            },
            new()
            {
                FullName = "TanThanh",
                UrlSlug = "tan-thanh",
                Email = "tanthanh01@motip.com",
                JoinedDate = new DateTime(2023, 2, 21)
                
            },
            new()
            {
                FullName = "TrongVy",
                UrlSlug = "trong-vy",
                Email = "trongvy02@gmail.com",
                JoinedDate = new DateTime(2021, 2, 20)
                
            },
            new()
            {
                FullName = "TuanKiet",
                UrlSlug = "tuan-kiet",
                Email = "tuankiet03@motip.com",
                JoinedDate = new DateTime(2020, 2, 19)
                
            },
            new()
            {
                FullName = "VanDu",
                UrlSlug = "van-du",
                Email = "vandu04@gmail.com",
                JoinedDate = new DateTime(2019, 2, 18)
                
            },
            new()
            {
                FullName = "NhatHa",
                UrlSlug = "nhat-ha",
                Email = "nhatha05@motip.com",
                JoinedDate = new DateTime(2018, 2, 17)
                
            },

            new()
            {
                FullName = "ABC",
                UrlSlug = "a-b-c",
                Email = "abc01@gmail.com",
                JoinedDate = new DateTime(2017, 3, 18)

            },

            new()
            {
                FullName = "BCA",
                UrlSlug = "b-c-a",
                Email = "bca02@yahoo.com",
                JoinedDate = new DateTime(2016, 4, 19)

            },

            new()
            {
                FullName = "CBA",
                UrlSlug = "c-b-a",
                Email = "cba03@gmail.com",
                JoinedDate = new DateTime(2015, 5, 15)

            },

            new()
            {
                FullName = "CAB",
                UrlSlug = "c-a-b",
                Email = "cab04@motip.com",
                JoinedDate = new DateTime(2014, 6, 20)

            },

            new()
            {
                FullName = "NuocCam",
                UrlSlug = "nuoc-cam",
                Email = "nuoccam05@gmail.com",
                JoinedDate = new DateTime(2023, 7, 21)

            },

            new()
            {
                FullName = "ComGa",
                UrlSlug = "com-ga",
                Email = "comga06@email.com",
                JoinedDate = new DateTime(2022, 8, 22)

            },

            new()
            {
                FullName = "ComVit",
                UrlSlug = "com-vit",
                Email = "comvit07@motip.com",
                JoinedDate = new DateTime(2021, 9, 23)

            },

            new()
            {
                FullName = "MeoMeo",
                UrlSlug = "meo-meo",
                Email = "meomeo08@yahoo.com",
                JoinedDate = new DateTime(2020, 10, 24)

            },

            new()
            {
                FullName = "GauGau",
                UrlSlug = "gau-gau",
                Email = "gaugau09@gmail.com",
                JoinedDate = new DateTime(2019, 11, 25)

            }
        };

        foreach (var author in authors)
        {
            if (!_dbContext.Authors.Any(a => a.UrlSlug == author.UrlSlug))
                _dbContext.Authors.Add(author);
        }
        //_dbContext.Authors.AddRange(authors);
        _dbContext.SaveChanges();
     
        return authors;
    }

    private IList<Category> AddCategories() 
    {
        var categories = new List<Category>()
        {
            new() {Name = ".NET Core", Description = ".NET Core", UrlSlug = ".net-core", ShowOnMenu = true},
            new() {Name = "Architecture", Description = "Architecture", UrlSlug = "architecture", ShowOnMenu = true},
            new() {Name = "Messaging", Description = "Messaging", UrlSlug = "messaging", ShowOnMenu = true},
            new() {Name = "OOP", Description = "OOP", UrlSlug = "OOP", ShowOnMenu = true},
            new() {Name = "Design Patterns", Description = "Design Patterns", UrlSlug = "design-patterns", ShowOnMenu = true},
            new() {Name = "NodeJs", Description = "NodeJs", UrlSlug = "NodeJs", ShowOnMenu = true},
            new() {Name = "HTML/CSS/JS", Description = "HTML/CSS/JS", UrlSlug = "html-css-js", ShowOnMenu = true},
            new() {Name = "React", Description = "React", UrlSlug = "react", ShowOnMenu = true},
            new() {Name = "C++", Description = "C++", UrlSlug = "c++", ShowOnMenu = true},
            new() {Name = "Python", Description = "Python", UrlSlug = "python", ShowOnMenu = true},
        };
        foreach (var category in categories)
        {
            if (!_dbContext.Categories.Any(a => a.UrlSlug == category.UrlSlug))
                _dbContext.Categories.Add(category);
        }
        //_dbContext.AddRange(categories);
        _dbContext.SaveChanges();

        return categories;
    }

    private IList<Tag> AddTags() 
    {
        var tags = new List<Tag>()
        {
            new() {Name = "Google", Description = "Google applications", UrlSlug = "google"},
            new() {Name = "ASP .NET MVC", Description = "ASP .NET MVC", UrlSlug = "asp-net-mvc"},
            new() {Name = "Razor Page", Description = "Razor Page", UrlSlug = "razor-page"},
            new() {Name = "Blazor", Description = "Blazor", UrlSlug = "blazor"},
            new() {Name = "Deep Learning", Description = "Deep Learning", UrlSlug = "deep-learning"},
            new() {Name = "Neural Network", Description = "Neural Network", UrlSlug = "neural-network"},
            new() {Name = "Virtual Machine", Description = "Virtual Machine", UrlSlug = "virtual-machine"},
            new() {Name = "Amazon", Description = "Amazon", UrlSlug = "amazon"},
            new() {Name = "Microsoft", Description = "Microsoft", UrlSlug = "microsoft"},
            new() {Name = "Programming For Beginners", Description = "Programming For Beginners", UrlSlug = "programming-for-beginners"},
            new() {Name = "IOT", Description = "IOT", UrlSlug = "iot"},
            new() {Name = "Spring Boot", Description = "Spring Boot", UrlSlug = "spring-boot"},
            new() {Name = "Tester", Description = "Tester", UrlSlug = "tester"},
            new() {Name = "Cisco", Description = "Cisco", UrlSlug = "cisco"},
            new() {Name = "Machine Learning", Description = "Machine Learning", UrlSlug = "machine-learning"},
            new() {Name = "React Native", Description = "React Native", UrlSlug = "react-native"},
            new() {Name = "API", Description = "API", UrlSlug = "api"},
            new() {Name = "OOP", Description = "OOP", UrlSlug = "oop"},
            new() {Name = "SQLServer", Description = "SQLServer", UrlSlug = "sql-server"},
            new() {Name = "Data Science", Description = "Data Science", UrlSlug = "data-science"},

        };
        foreach (var tag in tags)
        {
            if (!_dbContext.Tags.Any(a => a.UrlSlug == tag.UrlSlug))
                _dbContext.Tags.Add(tag);
        }
        //_dbContext.AddRange(tags);
        _dbContext.SaveChanges();

        return tags;
    }

    private IList<Post> AddPosts(
        IList<Author> authors,
        IList<Category> categories,
        IList<Tag> tags)
    {
        var posts = new List<Post>()
        {
            new()
            {
                Title = "ASP .NET Core Diagnostic Scenarios",
                ShortDescription = "David and friends has a great repository",
                Description = "Here's a few great DONT'T and DO exemples",
                Meta = "David and friends has a great reponsitory filled",
                UrlSlug = "aspnet-core-diagnostic-scenarios",
                Published = true,
                PostedDate = new DateTime(2021, 9, 3, 7, 2, 0),
                ModifiedDate = null,
                ViewCount = 10,
                Author = authors[0],
                Category = categories[0],
                Tags = new List<Tag>()
                {
                    tags[0]
                }
            },
            new()
            {
                Title = "Model-View-Controller",
                ShortDescription = "This is the design pattern used in software engineering",
                Description = "MVC is a software architecture pattern for creating user interfaces on computers",
                Meta = "The Principles of Clean Architecture by Uncle Bob Martin",
                UrlSlug = "model-view-controller",
                Published = true,
                PostedDate = new DateTime(2015, 10, 4, 5, 3, 0),
                ModifiedDate = null,
                ViewCount = 9,
                Author = authors[1],
                Category = categories[1],
                Tags = new List<Tag>()
                {
                    tags[1]
                }
            },
            new()
            {
                Title = "NodeJs Application Developer’s Guide",
                ShortDescription = "NodeJS is an open-source and cross-platform JavaScript runtime environment that is used to run web applications outside of the client's browser",
                Description = "Initially, the author named the project web.js for the sole purpose of using it as a web application instead of Apache or other servers",
                Meta = " The Node Ahead: JavaScript leaps from browser into future",
                UrlSlug = "nodeJs-application-developers-guid",
                Published = true,
                PostedDate = new DateTime(2021, 11, 8, 11, 4, 8),
                ModifiedDate = null,
                ViewCount = 8,
                Author = authors[2],
                Category = categories[2],
                Tags = new List<Tag>()
                {
                    tags[2]
                }
            },
            new()
            {
                Title = "JavaScript For Impatient Programmers",
                ShortDescription = "JavaScript , in its current version, is an interpreted programming language developed from the concept of prototypes",
                Description = "This language is widely used for websites ( user side) as well as server side (with Nodejs). It was originally developed by Brendan Eich at Netscape Communications as Mocha , then renamed LiveScript , and finally JavaScript",
                Meta = "Nigel McFarlane: Rapid Application Development with Mozilla , Prentice Hall Professional Technical References, ISBN 0-13-142343-6",
                UrlSlug = "javaScript-for-impatient-programmerst",
                Published = true,
                PostedDate = new DateTime(2015, 10, 1, 10, 4, 8),
                ModifiedDate = null,
                ViewCount = 7,
                Author = authors[3],
                Category = categories[3],
                Tags = new List<Tag>()
                {
                    tags[3]
                }
            },
            new()
            {
                Title = "Simple and Efficient Programming with C#",
                ShortDescription = "C# is a powerful, general-purpose object-oriented programming language developed by Microsoft",
                Description = "C# is in some ways a programming language that most directly reflects the .NET Framework on which all .NET programs run, and it relies heavily on this framework",
                Meta = "Welcome to C# 9.0” . Microsoft Docs . Microsoft . Retrieved November 13, 2020 .",
                UrlSlug = "programming-with-C#",
                Published = true,
                PostedDate = new DateTime(2020, 11, 2, 11, 5, 9),
                ModifiedDate = null,
                ViewCount = 6,
                Author = authors[4],
                Category = categories[4],
                Tags = new List<Tag>()
                {
                    tags[4]
                }
            },
            new()
            {
                Title = "CLEAN CODE: A HANDBOOK OF AGILE SOFTWARE CRAFTSMANSHIP",
                ShortDescription = "If you study and work in programming, you must have heard more or less about  the concept of Clean Code,  right?",
                Description = "Clean code, if translated, means clean source code, but in simple terms, clean code includes: how to organize the source code, how to deploy the source code so that it is scientific, easy to understand and brings performance. high for the program.",
                Meta = "Robert C.Martin",
                UrlSlug = "clean-code",
                Published = true,
                PostedDate = new DateTime(2016, 12, 3, 10, 9, 12),
                ModifiedDate = null,
                ViewCount = 5,
                Author = authors[5],
                Category = categories[5],
                Tags = new List<Tag>()
                {
                    tags[5]
                }
            },
            new()
            {
                Title = "Nuoc cam ngon siu cap vip pro max",
                ShortDescription = "Nuoc cam dinh cua choppppp ",
                Description = "Nuoc cam Nuoc cam Nuoc cam Nuoc cam Nuoc cam Nuoc cam Nuoc cam Nuoc cam Nuoc cam Nuoc cam  ",
                Meta = "Ver01",
                UrlSlug = "nuoc-cam",
                Published = true,
                PostedDate = new DateTime(2017, 10, 4, 11, 10, 12),
                ModifiedDate = null,
                ViewCount = 6,
                Author = authors[6],
                Category = categories[6],
                Tags = new List<Tag>()
                {
                    tags[6]
                }
            },
            new()
            {
                Title = "Com ga ngon siu cap vip pro max",
                ShortDescription = "Com ga VN dinh cua choppppp ",
                Description = "Com ga Com ga Com ga Com ga Com ga Com ga Com ga Com ga Com ga Com ga Com ga Com ga Com ga Com ga Com ga",
                Meta = "Ver02",
                UrlSlug = "com-ga",
                Published = true,
                PostedDate = new DateTime(2018, 11, 5, 11, 11, 11),
                ModifiedDate = null,
                ViewCount = 7,
                Author = authors[7],
                Category = categories[7],
                Tags = new List<Tag>()
                {
                    tags[7]
                }
            },
            new()
            {
                Title = "Com vit ngon siu cap vip pro max",
                ShortDescription = "Com vit VN dinh cua choppppp ",
                Description = "Com vit Com vit Com vit Com vit Com vit Com vit Com vit Com vit Com vit Com vit Com vit Com vit Com vit Com vit Com vit Com vit Com vit Com vit",
                Meta = "Ver03",
                UrlSlug = "com-vit",
                Published = true,
                PostedDate = new DateTime(2019, 9, 6, 9, 12, 9),
                ModifiedDate = null,
                ViewCount = 8,
                Author = authors[8],
                Category = categories[8],
                Tags = new List<Tag>()
                {
                    tags[8]
                }
            },
            new()
            {
                Title = "Com tron ngon siu cap vip pro max",
                ShortDescription = "Com tron VN dinh cua choppppp ",
                Description = "Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron Com tron ",
                Meta = "Ver04",
                UrlSlug = "com-tron",
                Published = true,
                PostedDate = new DateTime(2015, 8, 5, 8, 5, 8),
                ModifiedDate = null,
                ViewCount = 9,
                Author = authors[9],
                Category = categories[9],
                Tags = new List<Tag>()
                {
                    tags[9]
                }
            },
            new()
            {
                Title = "Con meo cute siu cap vip pro max",
                ShortDescription = "Meo co VN dinh cua choppppp ",
                Description = "Meo meo Meo meo Meo meo Meo meo Meo meo Meo meo Meo meo Meo meo Meo meo Meo meo Meo meo Meo meo Meo meo Meo meo Meo meo Meo meo Meo meo Meo meo ",
                Meta = "Ver05",
                UrlSlug = "meo-meo",
                Published = true,
                PostedDate = new DateTime(2020, 7, 7, 10, 11, 10),
                ModifiedDate = null,
                ViewCount = 10,
                Author = authors[10],
                Category = categories[5],
                Tags = new List<Tag>()
                {
                    tags[10]
                }
            },
            new()
            {
                Title = "Con cho cute siu cap vip pro max",
                ShortDescription = "Cho co VN dinh cua choppppp ",
                Description = "Gau gau Gau gau Gau gau Gau gau Gau gau Gau gauGau gau Gau gau Gau gau Gau gau Gau gau Gau gau Gau gau Gau gau Gau gau Gau gau Gau gau Gau gau Gau gau Gau gau",
                Meta = "Ver06",
                UrlSlug = "gau-gau",
                Published = true,
                PostedDate = new DateTime(2021, 6, 8, 1, 10, 1),
                ModifiedDate = null,
                ViewCount = 11,
                Author = authors[11],
                Category = categories[6],
                Tags = new List<Tag>()
                {
                    tags[11]
                }
            },
            new()
            {
                Title = "ABC",
                ShortDescription = "ABCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC",
                Description = "ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC  ",
                Meta = "Ver07",
                UrlSlug = "a-b-c",
                Published = true,
                PostedDate = new DateTime(2022, 3, 9, 10, 9, 18),
                ModifiedDate = null,
                ViewCount = 12,
                Author = authors[12],
                Category = categories[7],
                Tags = new List<Tag>()
                {
                    tags[12]
                }
            },
            new()
            {
                Title = "BCA",
                ShortDescription = "BCAAAAAAAAAAAAAAAAAAAAAAAAA",
                Description = "BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA BCA ",
                Meta = "Ver08",
                UrlSlug = "b-c-a",
                Published = true,
                PostedDate = new DateTime(2023, 2, 10, 10, 8, 17),
                ModifiedDate = null,
                ViewCount = 13,
                Author = authors[13],
                Category = categories[8],
                Tags = new List<Tag>()
                {
                    tags[13]
                }
            },
            new()
            {
                Title = "CAB",
                ShortDescription = "CABBBBBBBBBBBBBBBBB",
                Description = "CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB CAB  ",
                Meta = "Ver09",
                UrlSlug = "c-a-b",
                Published = true,
                PostedDate = new DateTime(2022, 1, 9, 10, 7, 15),
                ModifiedDate = null,
                ViewCount = 14,
                Author = authors[14],
                Category = categories[9],
                Tags = new List<Tag>()
                {
                    tags[14]
                }
            },
            new()
            {
                Title = "CBA",
                ShortDescription = "CBAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                Description = "CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA CBA ",
                Meta = "Ver10",
                UrlSlug = "c-b-a",
                Published = true,
                PostedDate = new DateTime(2021, 4, 8, 5, 6, 14),
                ModifiedDate = null,
                ViewCount = 15,
                Author = authors[14],
                Category = categories[8],
                Tags = new List<Tag>()
                {
                    tags[15]
                }
            },
        };
        foreach (var post in posts)
        {
            if (!_dbContext.Posts.Any(a => a.UrlSlug == post.UrlSlug))
                _dbContext.Posts.Add(post);
        }
        //_dbContext.AddRange(posts);
        _dbContext.SaveChanges();
        return posts;
    }
}
