using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");



//create the data context
var db = new DataContext();
var x = 1;
while (x != 5)
{
  Console.WriteLine("Press 1 to display all blogs \n" +
                    "press 2 to add a new blog \n " +
                    "press 3 to add a new post\n" +
                    "press 4 to display all posts\n" +
                    "press 5 to quit\n");

  x = int.Parse(Console.ReadLine());

  if (x == 2)
  {
// Create and save a new Blog


    Console.Write("Enter a name for a new Blog: ");


    var name = Console.ReadLine();





    var blog = new Blog { Name = name };








    db.AddBlog(blog);


    logger.Info("Blog added - {name}", name);


  }

  if (x == 1)
  {
// Display all Blogs from the database


    var query = db.Blogs.OrderBy(b => b.Name);





    Console.WriteLine("All blogs in the database:");


    foreach (var item in query)


    {


      Console.WriteLine(item.Name);


    }
  }

  if (x == 3)
  {
//create post
    Console.WriteLine("what Blog are you posting to");

    var targetBlog = db.Blogs.Where(b => b.Name == Console.ReadLine());
    Console.WriteLine("Enter a title for a new Post: ");

    var postTitle = Console.ReadLine();

    Console.WriteLine("Enter content for a new Post:");
    var postContent = Console.ReadLine();

    var post = new Post { Title = postTitle, Content = postContent };
    db.AddPost(targetBlog.First(), post);
  }

  if (x == 4)
  {
//display all posts

    Console.WriteLine("what blogs posts do you want to see");
    var targBlog = db.Blogs.Where(b => b.Name == Console.ReadLine());

    var postsquery = db.Posts.Where(b => b.Blog == targBlog.First());

    Console.WriteLine($"All posts in the database: {postsquery.Count()}");


    foreach (var item in postsquery)


    {


      Console.WriteLine(item.Blog.Name, item.Title, item.Content);


    }


  }
}

logger.Info("Program ended");
