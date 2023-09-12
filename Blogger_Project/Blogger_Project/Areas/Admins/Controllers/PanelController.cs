using Blog_DataAccess.FileManager;
using Blog_DataAccess.Repository;

using Blog_Models;
using Blog_Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger_Project.Areas.Admins.Controllers
{
    [Area("Admins")]
        [Authorize(Roles = "Admin")]
        public class PanelController : Controller
        {
            private IRepository _repo;
            private IFileManager _fileManager;

            public PanelController(
                IRepository repo,
                IFileManager fileManager
                )
            {
                _repo = repo;
                _fileManager = fileManager;
            }
            public IActionResult Index()
            {
                var posts = _repo.GetAllPosts();
                return View(posts);
            }

            [HttpGet]
            public IActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return View(new PostViewModel());
                }
                else
                {
                    var post = _repo.GetPost((int)id);
                    return View(new PostViewModel
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Body = post.Body,
                        CurrentImage = post.Images,
                        Description = post.Description,
                        Category = post.Category,
                        Tags = post.Tags
                    });
                }
            }

            [HttpPost]
            public async Task<IActionResult> Edit(PostViewModel vm)
            {
                var post = new Post
                {
                    Id = vm.Id,
                    Title = vm.Title,
                    Body = vm.Body,
                    Description = vm.Description,
                    Category = vm.Category,
                    Tags = vm.Tags,
                };

                if (vm.Image == null)
                    post.Images = vm.CurrentImage;
                else
                {
                    if (!string.IsNullOrEmpty(vm.CurrentImage))
                        _fileManager.RemoveImage(vm.CurrentImage);

                    post.Images = await _fileManager.SaveImage(vm.Image);
                }

                if (post.Id > 0)
                    _repo.UpdatePost(post);
                else
                    _repo.AddPost(post);


                if (await _repo.SaveChangesAsync())
                    return RedirectToAction("Index");
                else
                    return View(vm);
            }

            [HttpGet]
            public async Task<IActionResult> Remove(int id)
            {
                _repo.RemovePost(id);
                await _repo.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
    }
