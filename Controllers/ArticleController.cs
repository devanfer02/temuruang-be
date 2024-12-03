using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using temuruang_be.Dtos.ArticleDTO;
using temuruang_be.Models;
using temuruang_be.Services;

namespace temuruang_be.Controllers;

[ApiController]
[Route("api/articles")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _artSvc;
    private readonly ILogger<ArticleController> _logger;

    public ArticleController(IArticleService articleService, ILogger<ArticleController> logger)
    {
        _artSvc = articleService;
        _logger = logger;
    }

    [HttpGet]
    
    public async Task<IActionResult> FetchAllArticles()
    {
        try 
        {   
            var articles = await _artSvc.FetchArticles();

            return Ok(ApiResponse<IEnumerable<Article>>.Create(200, "successfully fetch articles", articles));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);            
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FetchArticleByID(int id)
    {
        try 
        {
            var article = await _artSvc.FetchArticleByID(id);

            if (article == null)
            {
                return NotFound(ApiResponse<Article?>.Create(404, "failed to find article", null));
            }

            return Ok(ApiResponse<Article>.Create(200, "successfully fetch article by id", article));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddArticle(ArticleRequestDTO dto)
    {
        try 
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;

            if (userId == null) 
            {
                return Unauthorized(ApiResponse<string?>.Create(401, "unauthorized", null));
            }

            Article article = ArticleRequestDTO.ToArticle(dto);
            article.UserId = Guid.Parse(userId);

            await _artSvc.AddArticle(article);
            
            return Ok(ApiResponse<Article>.Create(200, "successfully add article", article));
        } 
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateArticle(int id, ArticleRequestDTO dto)
    {
        try 
        {
            Article? article = await _artSvc.FetchArticleByID(id);

            if (article == null) 
            {
                return NotFound(ApiResponse<Article?>.Create(404, "failed to update article", null));   
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;

            if (userId == null) 
            {
                return Unauthorized(ApiResponse<string?>.Create(401, "unauthorized", null));
            }

            if (!article.UserId.ToString().Equals(userId))
            {
                return Unauthorized(ApiResponse<string?>.Create(401, "unauthorized", null));
            }

            Article updated = ArticleRequestDTO.ToArticle(dto);
            updated.UserId = Guid.Parse(userId);

            await _artSvc.UpdateArticle(id, updated);

            return Ok(ApiResponse<string?>.Create(200, "successfully update article", null));
        } 
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        try 
        {
            Article? article = await _artSvc.FetchArticleByID(id);

            if (article == null) 
            {
                return NotFound(ApiResponse<Article?>.Create(404, "failed to update article", null));   
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;

            if (userId == null) 
            {
                return Unauthorized(ApiResponse<string?>.Create(401, "unauthorized", null));
            }

            if (!article.UserId.ToString().Equals(userId))
            {
                return Unauthorized(ApiResponse<string?>.Create(401, "unauthorized", null));
            }

            await _artSvc.DeleteArticle(article);

            return Ok(ApiResponse<string?>.Create(200, "successfully delete user", null));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}