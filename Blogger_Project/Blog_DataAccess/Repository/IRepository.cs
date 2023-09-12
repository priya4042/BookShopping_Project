using Blog_Models;
using Blog_Models.Comments;
using Blog_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_DataAccess.Repository
{
   public interface IRepository
    {
        Post GetPost(int id);
        FrontPostViewModel GetFrontPost(int id);
        List<Post> GetAllPosts();
        IndexViewModel GetAllPosts(int pageNumber, string category, string search);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);
        void AddSubComment(SubComment comment);
        Task<bool> SaveChangesAsync();
    }
}
