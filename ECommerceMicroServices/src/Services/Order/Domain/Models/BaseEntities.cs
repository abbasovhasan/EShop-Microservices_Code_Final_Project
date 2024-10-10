using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public class BaseEntities
{
    public int Id { get; set; }

    // Opsiyonel olarak oluşturulma ve güncellenme tarihleri de eklenebilir
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
}
