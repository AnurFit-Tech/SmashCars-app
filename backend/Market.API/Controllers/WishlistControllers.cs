using Microsoft.AspNetCore.Mvc;
using Market.Domain.Entities.Sales;
using Market.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Market.Application.Modules.Wishlist;
namespace Market.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous] // Privremeno dozvoljeno svima dok testiraš
public class WishlistController : ControllerBase
{
    private readonly DatabaseContext _context;

    public WishlistController(DatabaseContext context)
    {
        _context = context;
    }

    /* // 1. Dohvati listu za određenog korisnika preko njegovog ID-a
     // Uključuje i podatke o proizvodima (Name, Price) iz Catalog foldera
     [HttpGet("{userId}")]
     public IActionResult GetByUserId(int userId)
     {
         var wishlist = _context.Wishlists
             .Include(w => w.WishlistProducts)
             .ThenInclude(wp => wp.Product) // Ovo spaja sa ProductEntity tabelom
             .FirstOrDefault(w => w.UserId == userId);

         if (wishlist == null)
             return NotFound("Korisnik nema kreiranu listu želja.");

         // Mapiramo podatke da vratimo čist JSON sa listom proizvoda
         var result = new
         {
             WishlistId = wishlist.Id,
             UserId = wishlist.UserId,
             Products = wishlist.WishlistProducts.Select(wp => new
             {
                 ProductId = wp.Product.Id,
                 ProductName = wp.Product.Name,
                 Price = wp.Product.Price,
                 Description = wp.Product.Description
             }).ToList()
         };

         return Ok(result);
     }
     */

    //Get sa Backend Paging, Filter i Total Count:
    [HttpGet]
    public async Task<IActionResult> GetWishlist(
    [FromQuery] string? searchTerm,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        //CountAsync
        var query = _context.WishlistProducts
            .Include(wp => wp.Product)
            .AsQueryable();

        // Backend Filtering
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(wp => wp.Product.Name.Contains(searchTerm));
        }

        var totalItems = await query.CountAsync();

        //  Backend  Paging (Skip i Take)
        var items = await query
            .OrderByDescending(wp => wp.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new
        {
            TotalCount = totalItems,
            PageSize = pageSize,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            Items = items
        });
    }


    // 2. Dodaj proizvod u listu želja
    // Koristimo DTO klasu da Swagger bude čist i da POST ne puca
    [HttpPost]
    public IActionResult Add([FromBody] WishlistAddRequest request)
    {
        // Provjeri postoji li korisnik i proizvod u bazi prije dodavanja
        var userExists = _context.Users.Any(u => u.Id == request.UserId);
        var productExists = _context.Products.Any(p => p.Id == request.ProductId);

        if (!userExists || !productExists)
            return BadRequest("Korisnik ili proizvod ne postoje u bazi.");

        // Nađi postojeću listu ili kreiraj novu ako je korisnik nema
        var wishlist = _context.Wishlists.FirstOrDefault(w => w.UserId == request.UserId);
        if (wishlist == null)
        {
            wishlist = new WishlistEntity { UserId = request.UserId };
            _context.Wishlists.Add(wishlist);
            _context.SaveChanges();
        }

        // Provjeri da proizvod već nije u toj listi
        var existsInWishlist = _context.WishlistProducts
            .Any(wp => wp.WishlistId == wishlist.Id && wp.ProductId == request.ProductId);

        if (existsInWishlist)
            return BadRequest("Proizvod je već u listi želja.");

        // Dodaj u spojnu tabelu
        var wishlistProduct = new WishlistProductEntity
        {
            WishlistId = wishlist.Id,
            ProductId = request.ProductId
        };

        _context.WishlistProducts.Add(wishlistProduct);
        _context.SaveChanges();

        return Ok("Uspješno dodano u listu želja!");
    }

    //3. Obriši konkretan proizvod iz liste želja
    [HttpDelete("remove-product/{wishlistId:int}/{productId:int}")]
    public async Task<IActionResult> RemoveProduct(int wishlistId, int productId)
    {
        var item = await _context.WishlistProducts
            .FirstOrDefaultAsync(wp => wp.WishlistId == wishlistId && wp.ProductId == productId);

        if (item == null)
            return NotFound("Proizvod nije pronađen u toj listi.");
        _context.Entry(item).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        return Ok("Proizvod je trajno uklonjen. Sada ga možete ponovo dodati.");
    }

    [HttpPost("wizzard-complete")]
    public async Task<IActionResult> CompleteWizzard([FromBody] WishlistWizzardRequest request)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == request.UserId);
        if (!userExists) return BadRequest("Korisnik ne postoji.");

        var wishlist = await _context.Wishlists.FirstOrDefaultAsync(w => w.UserId == request.UserId);
        if (wishlist == null)
        {
            wishlist = new WishlistEntity { UserId = request.UserId };
            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();
        }

        var wishlistProduct = new WishlistProductEntity
        {
            WishlistId = wishlist.Id,
            ProductId = request.ProductId
        };

        _context.WishlistProducts.Add(wishlistProduct);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "Wizzard uspješno završen!",
            Summary = $"Dodali ste proizvod ID {request.ProductId} sa prioritetom {request.Priority} i napomenom: {request.Note}."
        });
    }
}

// DTO klasa za POST zahtjev (da Swagger ne šalje nepotrebne podatke)
public class WishlistAddRequest
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
}