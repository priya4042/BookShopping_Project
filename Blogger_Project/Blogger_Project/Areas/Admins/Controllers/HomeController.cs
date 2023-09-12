
using Blog_DataAccess.FileManager;
using Blog_DataAccess.Repository;
using Blog_Models.Comments;
using Blog_Models.ViewModels;
using Blogger_Project.Models;
using Blogger_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger_Project.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class HomeController : Controller
    {
        
            private IRepository _repo;
            private IFileManager _fileManager;

            public HomeController(
                IRepository repo,
                IFileManager fileManager
                )
            {
                _repo = repo;
                _fileManager = fileManager;
            }

            public IActionResult Index(int pageNumber, string category, string search)
            {
                if (pageNumber < 1)
                    return RedirectToAction("Index", new { pageNumber = 1, category });

                var vm = _repo.GetAllPosts(pageNumber, category, search);

                return View(vm);
            }

            public IActionResult Post(int id) =>
                View(_repo.GetPost(id));

            [HttpGet("/Image/{image}")]
            [ResponseCache(CacheProfileName = "Monthly")]
            public IActionResult Image(string image) =>
                 new FileStreamResult(
                     _fileManager.ImageStream(image),
                     $"image/{image.Substring(image.LastIndexOf('.') + 1)}");

            [HttpPost]
            public async Task<IActionResult> Comment(CommentViewModel vm)
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Post", new { id = vm.PostId });

                var post = _repo.GetPost(vm.PostId);
                if (vm.MainCommentId == 0)
                {
                    post.MainComments = post.MainComments ?? new List<MainComment>();

                    post.MainComments.Add(new MainComment
                    {
                        Message = vm.Message,
                        Created = DateTime.Now,
                    });

                    _repo.UpdatePost(post);
                }
                else
                {
                    var comment = new SubComment
                    {
                        MainCommentId = vm.MainCommentId,
                        Message = vm.Message,
                        Created = DateTime.Now,
                    };
                    _repo.AddSubComment(comment);
                }

                await _repo.SaveChangesAsync();

                return RedirectToAction("Post", new { id = vm.PostId });
            }
        }
    }
