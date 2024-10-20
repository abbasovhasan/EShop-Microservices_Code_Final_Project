﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoriesAbstraction;

public interface IRepository<T> where T : BaseEntities
{
    DbSet<T> Table { get; }
}
