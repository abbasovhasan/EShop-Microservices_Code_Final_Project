using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public class Payment : BaseEntities
{
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }

    // Birebir ilişki için OrderId ve Order tanımları
    public int OrderId { get; set; }
    public Order Order { get; set; }
}