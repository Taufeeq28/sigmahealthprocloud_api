using BAL.Repository;
using BAL.RequestModels;
using Data.Models;
using Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Implementation
{
    public class FilterRepository 

    {
        private SigmaproIisContext context;
        private readonly SigmaproIisContextUdf _dbContextudf;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public FilterRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
            
        }
    }
}
