using System.IO;

namespace YourGet.Core.Services
{
    public interface IFileReference
    {
        /// <summary>
        /// Gets the content ID suitable for use in the ifNoneMatch parameter of <see cref="IFileStorageService.GetFileReferenceAsync"/>
        /// </summary>
        string ContentId
        { get; }

        Stream OpenRead();
    }
}