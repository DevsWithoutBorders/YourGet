using System;
using System.Threading.Tasks;
using System.Web;

namespace YourGet.Core.Services
{
    public interface IContentService
    {
        Task<IHtmlString> GetContentItemAsync(string name, TimeSpan expiresIn);
        Task<IHtmlString> GetContentItemAsync(string name, string extension, TimeSpan expiresIn);
        void ClearCache();
    }
}
