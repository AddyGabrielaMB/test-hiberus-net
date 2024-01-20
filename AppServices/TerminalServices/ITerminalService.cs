using TestHiberusNet.DTOs.TerminalDTOs;

namespace TestHiberusNet.AppServices.TerminalServices
{
    public interface ITerminalService
    {
        Task<List<TerminalDto>> GetList();
    }
}
