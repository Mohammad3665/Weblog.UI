using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;
using Weblog.Core.Domain.RepositoryContracts;

namespace Weblog.Core.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostRepository _postRepository;
        public CategoryService(ICategoryRepository categoryRepository, IPostRepository postRepository)
        {
            _categoryRepository = categoryRepository;
            _postRepository = postRepository;
        }


        public async Task<Guid> CreateCategoryAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
            return category.Id;
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }
        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            var posts = await _postRepository.GetAllAsync();
            if (posts.Any(p => p.CategoryId == categoryId))
            {
                throw new InvalidOperationException("Cannot delete category that has posts");
            }

            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category != null)
            {
                await _categoryRepository.DeleteAsync(category);
            }
        }
        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
    }
}
