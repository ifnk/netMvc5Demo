using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using netMvcTest.Dto.Article;
using netMvcTest.IDAL;
using netMvcTest.Model.Entity;
using netMvcTest.Model.Entity.HelpEntity;
using netMvcTest.MVC.Filters;
using Webdiyer.WebControls.Mvc;

namespace netMvcTest.MVC.Controllers
{
    [BlogFilterAuth]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly IArticleToCategoryService _articleToCategoryService;

        public ArticleController(
            IArticleService articleService,
            IBlogCategoryService blogCategoryService,
            IArticleToCategoryService articleToCategoryService)
        {
            _articleService = articleService;
            _blogCategoryService = blogCategoryService;
            _articleToCategoryService = articleToCategoryService;
        }

        // GET : Article
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET : CreateCategory 创建 文章分类  
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        // POST : CreateCategory 创建 文章分类  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory(AddBlogCategoryDto addBlogCategoryDto)
        {
            if (addBlogCategoryDto == null) throw new ArgumentNullException(nameof(addBlogCategoryDto));
            if (ModelState.IsValid)
            {
                var blogCategoryEntity = AutoMapper.Mapper.Map<BlogCategory>(addBlogCategoryDto);
                // 先将session 里面 的userId 转换 成 string ,然后 用 guid.parse 强转成 guid类型 
                Guid userId = new Guid(Session["userId"].ToString());
                await _blogCategoryService.AddBlogCategoryAsync(userId, blogCategoryEntity);
                return RedirectToAction(nameof(CategoryList)); // 跳转到首页
            }

            ModelState.AddModelError("", @"文章分类名有误!");
            return View(addBlogCategoryDto);
        }

        [HttpGet]
        public async Task<ActionResult> CategoryList(QueryParameter queryParameter)
        {
            var blogCategoryDtos = await GetBlogCategoryDtosAsync(queryParameter);
            return View(blogCategoryDtos);
        }

        private async Task<IEnumerable<BlogCategoryDto>> GetBlogCategoryDtosAsync(QueryParameter queryParameter)
        {
            var blogCategoryEntities =
                await _blogCategoryService.GetCategoriesAsync(Guid.Parse(Session["userId"].ToString()), queryParameter);
            var blogCategoryDtos = AutoMapper.Mapper.Map<IEnumerable<BlogCategoryDto>>(blogCategoryEntities);
            return blogCategoryDtos;
        }

        // GET : CreateArticle 创建 文章  
        [HttpGet]
        public async Task<ActionResult> CreateArticle()
        {
            IEnumerable<BlogCategoryDto> blogCategoryDtos =
                await GetBlogCategoryDtosAsync(new QueryParameter {PageSize = 100});
            ViewBag.blogCategoryDtos = blogCategoryDtos;
            return View();
        }

        // POST : CreateArticle 创建 文章  
        [HttpPost]
        [ValidateInput(false)] // 不做录入校验 
        public async Task<ActionResult> CreateArticle(AddArticleDto addArticleDto)
        {
            if (ModelState.IsValid)
            {
                // 添加 到 article 实体
                var articleEntity = AutoMapper.Mapper.Map<Article>(addArticleDto);
                await _articleService.AddArticleAsync(Guid.Parse(Session["userId"].ToString()), articleEntity);
                Guid articleId = articleEntity.Id;
                // 添加 到 ArticleToBlogCategory 实体
                foreach (Guid categoryId in addArticleDto.CategoryIds)
                {
                    await _articleToCategoryService.AddAsync(new ArticleToCategory
                        {ArticleId = articleId, BlogCategoryId = categoryId}, false);
                    // 每次添加的时候不保存 
                }

                await _articleToCategoryService.SaveAsync(); // 累积起来保存

                return RedirectToAction(nameof(ArticleList)); // 跳转到首页
            }

            ModelState.AddModelError("", "添加失败!");

            return View(addArticleDto);
        }

        // GET : ArticleList 文章列表 
        [HttpGet]
        public async Task<ActionResult> ArticleList(QueryParameter queryParameter)
        {
            var articleEntities =
                await _articleService.GetArticlesAsync( queryParameter);
            // 当前用户第n 页 数据 
            var articleDtos = AutoMapper.Mapper.Map<IEnumerable<ArticleDto>>(articleEntities);
            return View(new PagedList<ArticleDto>(
                articleDtos,
                articleEntities.PageIndex,
                articleEntities.PageSize,
                articleEntities.TotalCount
            ));
        }

        // GET : EditArticle 编辑文章 
        [HttpGet]
        [Route("Article/EditArticle/{articleId}")]
        public async Task<ActionResult> EditArticle(Guid articleId)
        {
            return await GetArticleDtoAsync(articleId);
        }

        // POST : EditArticle 编辑文章 
        [HttpPost]
        [Route("Article/EditArticle/{articleId}")]
        public async Task<ActionResult> EditArticle(Guid articleId, EditArticleDto editArticleDto)
        {
            if (articleId == Guid.Empty) throw new ArgumentNullException(nameof(editArticleDto));
            if (editArticleDto == null) throw new ArgumentNullException(nameof(editArticleDto));
            if (ModelState.IsValid)
            {
                Article articleEntity = await _articleService.GetArticleAsync(articleId);
                //将 article 转换 为 EditArticleDto
                //把 传 进来 的 EditArticleDto 的值 更新 到 EditArticleDto
                //将 EditArticleDto 映射回去
                AutoMapper.Mapper.Map(editArticleDto, articleEntity); // 3步全搞定
                await _articleService.UpdateAsync(articleEntity);
                Guid[] oldCategoryIds = articleEntity.ArticleToCategories.Select(x => x.BlogCategoryId).ToArray();
                Guid[] newCategoryIds = editArticleDto.CategoryIds;
                Guid[] delCategoryIds = oldCategoryIds.Except(newCategoryIds).ToArray(); //old中有new中没有的 （结果：a,b）
                Guid[] addCategoryIds = newCategoryIds.Except(oldCategoryIds).ToArray(); //new中有old中没有的 （结果：e）
                List<ArticleToCategory> removeArticleToCategories = articleEntity.ArticleToCategories
                    .Where(x => delCategoryIds.Contains(x.BlogCategoryId))
                    .ToList();
                // 如果被选中了，原来没有的要添加；
                foreach (Guid addCategoryId in addCategoryIds)
                {
                    await _articleToCategoryService.AddAsync(
                        new ArticleToCategory
                        {
                            ArticleId = articleId,
                            BlogCategoryId = addCategoryId
                        }, false
                    );
                }

                // 如果没被选中，原来有的要删除。
                foreach (ArticleToCategory removeArticleToCategory in removeArticleToCategories)
                {
                    await _articleToCategoryService.RemoveAsync(removeArticleToCategory.Id, false);
                }

                await _articleToCategoryService.SaveAsync();
                return RedirectToAction(nameof(ArticleList)); // 跳转到文章列表
            }

            return View(editArticleDto);
        }


        // GET : ArticleDetails 文章详情 
        [HttpGet]
        [Route("Article/ArticleDetails/{articleId}")]
        public async Task<ActionResult> ArticleDetails(Guid articleId)
        {
            return await GetArticleDtoAsync(articleId);
        }

        private async Task<ActionResult> GetArticleDtoAsync(Guid articleId)
        {
            Article articleEntity = await _articleService.GetArticleAsync(articleId);
            ArticleDto articleDto = AutoMapper.Mapper.Map<ArticleDto>(articleEntity);
            IEnumerable<BlogCategoryDto> blogCategoryDtos =
                await GetBlogCategoryDtosAsync(new QueryParameter {PageSize = 100});
            ViewBag.blogCategoryDtos = blogCategoryDtos;
            return View(articleDto);
        }

        //点赞数量+1 或者 反对数量+1
        [HttpPost]
        [Route("Article/addLikeCount/{articleId}/{isLike}")]
        public async Task<ActionResult> addLikeCount(Guid articleId, bool isLike)
        {
            Article articleEntity = await _articleService.GetArticleAsync(articleId);
            if (isLike)
            {
                articleEntity.LikeCount = articleEntity.LikeCount + 1;
            }
            else
            {
                articleEntity.OpposeCount = articleEntity.OpposeCount + 1;
            }

            await _articleService.UpdateAsync(articleEntity);
            return isLike
                ? Json(new MsgReturn<string> {Msg = "已赞同"})
                : Json(new MsgReturn<string> {Msg = "已反对"});
        }
    }
}