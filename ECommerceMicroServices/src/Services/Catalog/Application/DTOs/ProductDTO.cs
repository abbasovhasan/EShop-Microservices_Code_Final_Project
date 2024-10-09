
namespace Application.DTOs;

public class ProductDTO
{
    public int Id { get; set; } // Ürün güncelleme işlemleri için
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; }
}

