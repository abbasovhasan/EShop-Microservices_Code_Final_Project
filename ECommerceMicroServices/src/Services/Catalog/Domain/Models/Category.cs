using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public class Category : BaseEntities
{
    public string Name { get; set; }

    // Her kategori birden fazla ürüne sahip olabilir
    public ICollection<Product> Products { get; set; }
}

