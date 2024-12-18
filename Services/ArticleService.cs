using Microsoft.EntityFrameworkCore;
using temuruang_be.Models;

namespace temuruang_be.Services;

public interface IArticleService
{
    Task<Article> AddArticle(Article article) ;
    Task UpdateArticle(int id, Article article) ;
    Task DeleteArticle(Article article);
    Task<Article?> FetchArticleByID(int id);
    Task<(IEnumerable<Article>, int totalPages)> FetchArticles(int pageNumber, int pageSize);
}

public sealed class ArticleService : IArticleService 
{
    private readonly ApplicationDbContext dbCtx;

    public ArticleService(ApplicationDbContext dbCtx) 
    {
        this.dbCtx = dbCtx;
    }

    public async Task<Article> AddArticle(Article article) 
    {
        dbCtx.Add(article);

        await dbCtx.SaveChangesAsync();

        return article;
    }

    public async Task UpdateArticle(int id, Article article) 
    {
        article.Id = id;
        dbCtx.Update(article);

        await dbCtx.SaveChangesAsync();
    }

    public async Task DeleteArticle(Article article)
    {
        dbCtx.Remove(article);

        await dbCtx.SaveChangesAsync();
    }

    public async Task<Article?> FetchArticleByID(int id)
    {
        Article? article = await dbCtx.Article.
            Where(a => a.Id ==id).
            AsNoTracking().
            Include(a => a.User).
            FirstOrDefaultAsync();

        if (article == null) 
        {
            return null;
        }

        return article;        
    }

    public async Task<(IEnumerable<Article>, int totalPages)> FetchArticles(int pageNumber, int pageSize)
    {
        IEnumerable<Article> users = await dbCtx.
            Article.
            AsNoTracking().
            Include(a => a.User).
            Skip((pageNumber - 1) * pageSize). 
            Take(pageSize). 
            ToListAsync();

        int totalCount = await dbCtx.Article.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        return (users, totalPages);
    }
}