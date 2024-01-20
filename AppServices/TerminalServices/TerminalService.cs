using AutoMapper;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using TestHiberusNet.DTOs.TerminalDTOs;
using TestHiberusNet.Models;

namespace TestHiberusNet.AppServices.TerminalServices
{
    public class TerminalService : ITerminalService
    {
        private readonly HiberusTestDBContext _dbContext;
        private readonly IMapper _mapper;   
        public TerminalService( 
            HiberusTestDBContext dbContext,
            IMapper mapper
         ) 
        { 
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<TerminalDto>> GetList()
        {
           List<Terminales> TerminalList = await _dbContext.Terminales
                .Include(t => t.IdEstadoNavigation)
                .Include(t => t.IdFabNavigation)                
                .AsNoTracking()
                .Cacheable()
                .ToListAsync();

           return _mapper.Map<List<TerminalDto>>(TerminalList);
        }
    }
}
