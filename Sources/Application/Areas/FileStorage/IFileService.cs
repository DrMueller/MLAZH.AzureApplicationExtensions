using System.Threading.Tasks;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.FileStorage
{
    public interface IFileService
    {
        Task AppendAsync(string text);
    }
}