using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication11.Models;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using WebApplication11;
using WebApplication11.viewModel;
using Microsoft.EntityFrameworkCore;

namespace WebApplication11.Controllers
{
    public class HomeController : Controller
    {

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        const string SessionID = "_UserID";
        private DBContext dbContext;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _appEnvironment;



        public HomeController(DBContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment appEnvironment)
        {

            dbContext = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UserHomePage()
        {
            return View(dbContext.Users.ToList());
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                TempData["User"] = id;
                foreach (var Item in dbContext.Users)
                {
                    if (Item.Id.Equals(id))
                    {
                        TempData["User"] = Item.Id;
                        HttpContext.Session.SetInt32(SessionID, Item.Id);
                        TempData["UserEmail"] = Item.Email;
                        TempData["FUserName"] = Item.FName + " " + Item.LName;
                        TempData["UserName"] = Item.FName;
                        TempData["lUserName"] = Item.LName;
                        TempData["UserPassword"] = Item.Password;
                        TempData["City"] = Item.City;
                        TempData["Country"] = Item.Country;
                        TempData["Gender"] = Item.Gender;
                        TempData["UserMobile"] = Item.Mobile;
                        // TempData["Userpostimg"] = Item.postImg;
                        // TempData["postimg1"] = Item.postImg1;
                        TempData["img1"] = Item.postImg;


                    }
                }
            }
            return View();
        }
        
        public IActionResult editprofileimg(int? id)
        {
            if (id != null)
            {
                TempData["User"] = id;
                foreach (var Item in dbContext.Users)
                {
                    if (Item.Id.Equals(id))
                    {
                        TempData["User"] = Item.Id;
                        HttpContext.Session.SetInt32(SessionID, Item.Id);
                        TempData["UserEmail"] = Item.Email;
                        TempData["FUserName"] = Item.FName + " " + Item.LName;
                        TempData["UserName"] = Item.FName;
                        TempData["lUserName"] = Item.LName;
                        TempData["UserPassword"] = Item.Password;
                        TempData["City"] = Item.City;
                        TempData["Country"] = Item.Country;
                        TempData["Gender"] = Item.Gender;
                        TempData["UserMobile"] = Item.Mobile;
                        // TempData["Userpostimg"] = Item.postImg;
                        // TempData["postimg1"] = Item.postImg1;
                        TempData["img1"] = Item.postImg;


                    }
                }
            }

            return View();
        }


        [HttpPost]
        public IActionResult Register(User model)
        {
            if (CheckRegister(model))
            {
                model.postImg = "";
                dbContext.Users.Add(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["RegisterError"] = "Email is already exist";
                return RedirectToAction("Index");
            }
        }
        public Boolean CheckRegister(User model)
        {
            if (!dbContext.Users.Any(x => x.Email.Equals(model.Email)))
            {

                return true;
            }
            else
            {

                return false;
            }
        }

        [HttpPost]

        public IActionResult Login(User model)
        {

            if (CheckLoginEmail(model))
            {
                if (CheckLoginPassword(model))
                {
                    foreach (var Item in dbContext.Users)
                    {
                        if (Item.Email.Equals(model.Email))
                        {
                            TempData["User"] = Item.Id;
                            HttpContext.Session.SetInt32(SessionID, Item.Id);
                            TempData["UserEmail"] = Item.Email;
                            TempData["FUserName"] = Item.FName +" "+ Item.LName;
                            TempData["UserName"] = Item.FName;
                            TempData["lUserName"] = Item.LName;
                            TempData["UserPassword"] = Item.Password;
                            TempData["City"] = Item.City;
                            TempData["Country"] = Item.Country;
                            TempData["Gender"] = Item.Gender;
                            TempData["UserMobile"] = Item.Mobile;
                            TempData["img1"] = Item.postImg;

                            var friendUser = dbContext.friends.Where(x => x.userId == Item.Id);
                            var friendid = dbContext.friends.Where(x => x.friendId == Item.Id);
                            if (friendUser.Any() )
                            {
                                TempData["friends"] = dbContext.friends.Where(x => x.userId == Item.Id).Count();
                            }
                            if (friendid.Any())
                            {
                                TempData["friends"] = dbContext.friends.Where(x => x.friendId == Item.Id).Count();
                            }

                            //TempData["friends"] = dbContext.friends.Where(x=>x.userId==Item.Id).Count();
                            //TempData["friendid"] = dbContext.friends.Where(x => x.friendId == Item.Id).Count();

                        }
                    }
                    return RedirectToAction("personal");
                }
                else
                {
                    TempData["LoginError"] = "Email or password is incorrect";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Error"] = "Something was happened wrong, please try again";
                return RedirectToAction("Index");
            }
        }

        public Boolean CheckLoginEmail(User model)
        {
            var user = dbContext.Users.Where(x => x.Email == model.Email);
            if (user.Any())
            {
                CheckLoginPassword(model);
                return true;
            }
            else return false;

        }
        public Boolean CheckLoginPassword(User model)
        {
            var user = dbContext.Users.Where(x => x.Password == model.Password);
            if (user.Any())
            {
                return true;
            }
            else
            {

                return false;
            }

        }
        public async Task<IActionResult> EditInfo(User user)
        {
           // user.postImg = Convert.ToString(TempData["img"]);
            user.Email = Convert.ToString(TempData["UserEmail"]);
            user.Id = Convert.ToInt32(TempData["User"]);
            if (user != null)
            {
                if (user.postImg1 != null)
                {
                    string Folder = "image/";
                    Folder += Guid.NewGuid().ToString() + "_" + user.postImg1.FileName;
                    string serverFolder = Path.Combine(_appEnvironment.WebRootPath, Folder);
                    await user.postImg1.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    user.postImg = Folder;
                }
               


                TempData["proimg"]= Convert.ToString(TempData["img1"]);
                TempData["proimg"]=user.postImg;
                TempData["name"] = user.FName +" "+ user.LName;
                dbContext.Users.Update(user);
                dbContext.SaveChanges();
                return RedirectToAction("personal");
            }
            return RedirectToAction("Edit");
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult personal(int? id)
        {
            if(id == null)
            {
                id = @HttpContext.Session.GetInt32("_UserID");
                foreach (var Item in dbContext.Users)
                {
                    if (Item.Id.Equals(id))
                    {
                        TempData["User"] = Item.Id;

                        HttpContext.Session.SetInt32(SessionID, Item.Id);

                        TempData["FUserName"] = Item.FName + " " + Item.LName;

                        var friendUser = dbContext.friends.Where(x => x.userId == Item.Id);
                        var friendid = dbContext.friends.Where(x => x.friendId == Item.Id);
                        if (friendUser.Any())
                        {
                            TempData["friends"] = dbContext.friends.Where(x => x.userId == Item.Id).Count();
                        }
                        if (friendid.Any())
                        {
                            TempData["friends"] = dbContext.friends.Where(x => x.friendId == Item.Id).Count();
                        }
                    }
                }
            }
            else
            {
                 TempData["User"] = id;

                foreach (var Item in dbContext.Users)
                {
                    if (Item.Id.Equals(id))
                    {
                        TempData["User"] = Item.Id;
                        HttpContext.Session.SetInt32(SessionID, Item.Id);
                        TempData["UserEmail"] = Item.Email;
                        TempData["FUserName"] = Item.FName + " " + Item.LName;
                        TempData["UserName"] = Item.FName;
                        TempData["lUserName"] = Item.LName;
                        TempData["UserPassword"] = Item.Password;
                        TempData["City"] = Item.City;
                        TempData["Country"] = Item.Country;
                        TempData["Gender"] = Item.Gender;
                        TempData["UserMobile"] = Item.Mobile;
                        TempData["img1"] = Item.postImg;

                        var friendUser = dbContext.friends.Where(x => x.userId == Item.Id);
                        var friendid = dbContext.friends.Where(x => x.friendId == Item.Id);
                        if (friendUser.Any())
                        {
                            TempData["friends"] = dbContext.friends.Where(x => x.userId == Item.Id).Count();
                        }
                        if (friendid.Any())
                        {
                            TempData["friends"] = dbContext.friends.Where(x => x.friendId == Item.Id).Count();
                        }
                    }
                }
            }

            var sessionID = @HttpContext.Session.GetInt32("_UserID").ToString();
            Get(int.Parse(sessionID));
            //FriendRequest(int.Parse(sessionID),id);
            FriendRequest(int.Parse(sessionID));

            return View();
        }
        public async Task<IActionResult> PersonalNewPost(Newpost Model)
        {
            
            if (Model.postImg1 != null)
            {
                string Folder = "image/";
                Folder += Guid.NewGuid().ToString() + "_" + Model.postImg1.FileName;
                string serverFolder = Path.Combine(_appEnvironment.WebRootPath, Folder);
                await Model.postImg1.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                Model.postImg = Folder;
                
            }Model.DOB=DateTime.Now;
            dbContext.newpost.Add(Model);
            dbContext.SaveChanges();
            return RedirectToAction("personal", new { id=Model.userId });
        }
        public async Task<IActionResult> Personalphoto(User Model)
        {
            Model.Id = Convert.ToInt32(TempData["User"]);
           
            Model.Email = Convert.ToString(TempData["UserEmail"]);
            Model.City = Convert.ToString(TempData["City"]);
            Model.Country = Convert.ToString(TempData["Country"]);
            Model.Gender = Convert.ToString(TempData["Gender"]);
            Model.Mobile = Convert.ToString(TempData["UserMobile"]);
          //  Model.postImg = Convert.ToString(TempData["setimg"]);
            Model.LName = Convert.ToString(TempData["lUserName"]);
            Model.Password = Convert.ToString(TempData["UserPassword"]);
            Model.FName = Convert.ToString(TempData["FUserName"]);
            if (Model.postImg1 != null)
            {
                string Folder = "image/";
                Folder += Guid.NewGuid().ToString() + "_" + Model.postImg1.FileName;
                string serverFolder = Path.Combine(_appEnvironment.WebRootPath, Folder);
                await Model.postImg1.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                Model.postImg = Folder;
            }
            TempData["img"] = Model.postImg;
            dbContext.Users.Update(Model);
            dbContext.SaveChanges();
            return RedirectToAction("personal");
        }




        public IActionResult SearchAccount(string searchWord,int? id)
        {
            
            var sessionID = @HttpContext.Session.GetInt32("_UserID").ToString();
            List<User> users;


            users = dbContext.Users.Where(c => (c.Email.Contains(searchWord) || c.Mobile == searchWord) && c.Id != int.Parse(sessionID)).ToList();

            if (id != null)
            {
                TempData["User"] = id;
                
            }

            return View(users);
        }

        public IActionResult AddRequest(int id)
        {
            var sessionID = @HttpContext.Session.GetInt32("_UserID").ToString();
            friendRequest request = new friendRequest { UserID = int.Parse(sessionID), friendID = id, status = "requested" };

            dbContext.FriendRequests.Add(request);
            dbContext.SaveChanges();
           
            TempData["img11"] = Convert.ToString(TempData["proimg"]);
            TempData["profile"] = Convert.ToString(TempData["name"]);

            if (id != null)
            {
                TempData["User"] = int.Parse(sessionID);
                foreach (var Item in dbContext.Users)
                {
                    if (Item.Id.Equals(int.Parse(sessionID)))
                    {
                        TempData["User"] = Item.Id;
                        HttpContext.Session.SetInt32(SessionID, Item.Id);
                        TempData["UserEmail"] = Item.Email;
                        TempData["FUserName"] = Item.FName + " " + Item.LName;
                        TempData["UserName"] = Item.FName;
                        TempData["lUserName"] = Item.LName;
                        TempData["UserPassword"] = Item.Password;
                        TempData["City"] = Item.City;
                        TempData["Country"] = Item.Country;
                        TempData["Gender"] = Item.Gender;
                        TempData["UserMobile"] = Item.Mobile;
                        TempData["img1"] = Item.postImg;

                        var friendUser = dbContext.friends.Where(x => x.userId == Item.Id);
                        var friendid = dbContext.friends.Where(x => x.friendId == Item.Id);
                        if (friendUser.Any())
                        {
                            TempData["friends"] = dbContext.friends.Where(x => x.userId == Item.Id).Count();
                        }
                        if (friendid.Any())
                        {
                            TempData["friends"] = dbContext.friends.Where(x => x.friendId == Item.Id).Count();
                        }
                    }
                }
            }

            return RedirectToAction("SearchAccount");
        }


        public IActionResult Delete(int id)
        {
            var user=dbContext.Users.Find(id);
            dbContext.Remove(user);
            dbContext.SaveChanges();
            return RedirectToAction("UserHomePage");
        }

        private User getUserName()
        {
            int userID = int.Parse(@HttpContext.Session.GetInt32("_UserID").ToString());

            return dbContext.Users.Find(userID);
        }

      
        public async Task<IActionResult> CommentFun(CommentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var post = dbContext.newpost.Include(p => p.comments).FirstOrDefault(p => p.id == vm.postId);
                string Name = getUserName().FName;
                if (post != null)
                {
                    post.comments = post.comments ?? new List<Comment>();
                    post.comments.Add(new Comment
                    {
                        text = vm.text,
                        created = DateTime.Now,
                        UserName = Name,
                    });
                    post.comment = post.comments.Count();
                    dbContext.newpost.Update(post);
                }

            }
            else
                return RedirectToAction("personal");
            await dbContext.SaveChangesAsync();

            return RedirectToAction("personal");

        }
        public async Task<IActionResult> Like(LikeViewModel lm)
        {
            int x = 0;
            var like = dbContext.newpost.FirstOrDefault(p => p.id == lm.postId);
            if (like.like != null)
            {
                x = (int)like.like;
                x++;
                like.like = x;
            }
            else
            {
                x++;
                like.like = x;
            }
            dbContext.SaveChanges();
            return RedirectToAction("personal");
        }
        public async Task<IActionResult> Get(int userid)
        {
            var posts = dbContext.newpost.Include(p => p.comments).Include(a => a.user).ToList();
            ViewData["posts"] = posts;
            
            return View();
        }
        public async Task<IActionResult> FriendRequest(int userid)
        {
            var clear = dbContext.requestdetail.Where(a => a.id >= 0).ToList();
            if (clear != null)
            {
                foreach (var x in clear)
                {
                    dbContext.requestdetail.Remove(x);
                    dbContext.SaveChanges();
                }
            }
            var sessionID = @HttpContext.Session.GetInt32("_UserID").ToString();
            int id = int.Parse(sessionID);
            var friendRequests = dbContext.FriendRequests.Include(a => a.users).Where(p => p.status == "requested").ToList();

            if (friendRequests != null)
            {
                foreach (var friend in friendRequests)
                {
                    RequestDetail request = new RequestDetail();
                    //.id = friend.ID;
                    request.userID = friend.friendID;
                    request.FName = friend.users.FName;
                    request.LName = friend.users.LName;
                    request.friendID = friend.UserID;
                    dbContext.requestdetail.Add(request);
                    dbContext.SaveChanges();
                }

            }
            var friendRequest = dbContext.requestdetail.Where(p => p.userID == id).ToList();
           
            ViewData["requests"] = friendRequest;

            return View();
        }

        /*
        public async Task<IActionResult> FriendRequest(int userid,int? id)
        {

            var friendRequests = dbContext.requestdetail.FromSqlRaw("select  FriendRequests.ID as id , FriendRequests.friendID as userID ,FriendRequests.userID as friendID, Users.FName as FName , Users.LName as LName from Users join FriendRequests on FriendRequests.userID = Users.Id where FriendRequests.status ='requested' ").ToList();
            friendRequests.Select(p => p.userID == userid).ToList();
            ViewData["requests"] = friendRequests;
            if (id != null)
            {
                TempData["User"] = id;
                foreach (var Item in dbContext.Users)
                {
                    if (Item.Id.Equals(id))
                    {
                        TempData["User"] = Item.Id;
                        HttpContext.Session.SetInt32(SessionID, Item.Id);
                        TempData["UserEmail"] = Item.Email;
                        TempData["FUserName"] = Item.FName + " " + Item.LName;
                        TempData["UserName"] = Item.FName;
                        TempData["lUserName"] = Item.LName;
                        TempData["UserPassword"] = Item.Password;
                        TempData["City"] = Item.City;
                        TempData["Country"] = Item.Country;
                        TempData["Gender"] = Item.Gender;
                        TempData["UserMobile"] = Item.Mobile;
                        TempData["img1"] = Item.postImg;

                        var friendUser = dbContext.friends.Where(x => x.userId == Item.Id);
                        var friendid = dbContext.friends.Where(x => x.friendId == Item.Id);
                        if (friendUser.Any())
                        {
                            TempData["friends"] = dbContext.friends.Where(x => x.userId == Item.Id).Count();
                        }
                        if (friendid.Any())
                        {
                            TempData["friends"] = dbContext.friends.Where(x => x.friendId == Item.Id).Count();
                        }
                    }
                }
            }
            return View();
        }*/
        public async Task<IActionResult> Accept(int friendid)
        {
            var sessionID = @HttpContext.Session.GetInt32("_UserID").ToString();
            var accept = dbContext.FriendRequests.Where(a => a.friendID == int.Parse(sessionID) && a.UserID == friendid && a.status == "requested").ToList();
            foreach (var item in accept)
            {
                item.status = "Accepted";
                
            }
            dbContext.SaveChanges();
            AddFriend(int.Parse(sessionID), friendid);
            return RedirectToAction("FriendRequest");
        }
        public async Task<IActionResult> AddFriend(int userid, int friendid)
        {
            var friend = dbContext.Users.Include(p => p.friends).FirstOrDefault(p => p.Id == userid);
            if (friend != null)
            {
                friend.friends = friend.friends ?? new List<Friends>();
                friend.friends.Add(new Friends { friendId = friendid });
                dbContext.Users.Update(friend);
            }
            else
                return RedirectToAction("FriendRequest");
            dbContext.SaveChanges();
            return RedirectToAction("FriendRequest");
        }
        public async Task<IActionResult> Reject(int friendid, List<friendRequest> reject)
        {
            var sessionID = @HttpContext.Session.GetInt32("_UserID").ToString();
            var Reject = dbContext.FriendRequests.SingleOrDefault(a => a.friendID == int.Parse(sessionID) && a.UserID == friendid && a.status == "requested");
            if (Reject != null)
            {
                dbContext.FriendRequests.Remove(Reject);
            }

            else
                return RedirectToAction("FriendRequest");
            await dbContext.SaveChangesAsync();
            return RedirectToAction("FriendRequest");
        }
        public async Task<IActionResult> MainFriend()
        {
            return View();
        }

        public IActionResult personalFriend(int? id)
        {

            if (id != null)
            {
                TempData["User"] = id;
                foreach (var Item in dbContext.Users)
                {
                    if (Item.Id.Equals(id))
                    {
                        TempData["User"] = Item.Id;
                        HttpContext.Session.SetInt32(SessionID, Item.Id);
                        TempData["UserEmail"] = Item.Email;
                        TempData["FUserName"] = Item.FName + " " + Item.LName;
                        TempData["UserName"] = Item.FName;
                        TempData["lUserName"] = Item.LName;
                        TempData["UserPassword"] = Item.Password;
                        TempData["City"] = Item.City;
                        TempData["Country"] = Item.Country;
                        TempData["Gender"] = Item.Gender;
                        TempData["UserMobile"] = Item.Mobile;
                        TempData["img1"] = Item.postImg;

                        var friendUser = dbContext.friends.Where(x => x.userId == Item.Id);
                        var friendid = dbContext.friends.Where(x => x.friendId == Item.Id);
                        if (friendUser.Any())
                        {
                            TempData["friends"] = dbContext.friends.Where(x => x.userId == Item.Id).Count();
                        }
                        if (friendid.Any())
                        {
                            TempData["friends"] = dbContext.friends.Where(x => x.friendId == Item.Id).Count();
                        }
                    }
                }
            }

            var sessionID = @HttpContext.Session.GetInt32("_UserID").ToString();
            Get(int.Parse(sessionID));
            // FriendRequest(int.Parse(sessionID), id);
             FriendRequest(int.Parse(sessionID));

            return View();
        }

        public IActionResult showfriendData(int? id)
        {
            if (id != null)
            {
                TempData["User"] = id;
                foreach (var Item in dbContext.Users)
                {
                    if (Item.Id.Equals(id))
                    {
                        TempData["User"] = Item.Id;
                        HttpContext.Session.SetInt32(SessionID, Item.Id);
                        TempData["UserEmail"] = Item.Email;
                        TempData["FUserName"] = Item.FName + " " + Item.LName;
                        TempData["UserName"] = Item.FName;
                        TempData["lUserName"] = Item.LName;
                        TempData["UserPassword"] = Item.Password;
                        TempData["City"] = Item.City;
                        TempData["Country"] = Item.Country;
                        TempData["Gender"] = Item.Gender;
                        TempData["UserMobile"] = Item.Mobile;
                        TempData["img1"] = Item.postImg;

                        var friendUser = dbContext.friends.Where(x => x.userId == Item.Id);
                        var friendid = dbContext.friends.Where(x => x.friendId == Item.Id);
                        if (friendUser.Any())
                        {
                            TempData["friends"] = dbContext.friends.Where(x => x.userId == Item.Id).Count();
                        }
                        if (friendid.Any())
                        {
                            TempData["friends"] = dbContext.friends.Where(x => x.friendId == Item.Id).Count();
                        }
                    }
                }
            }

            return View();
        }
    }
}