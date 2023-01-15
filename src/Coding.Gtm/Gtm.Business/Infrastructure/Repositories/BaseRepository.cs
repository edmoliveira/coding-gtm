using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Text;

namespace Gtm.Business.Infrastructure.Repositories
{
    /// <summary>
    /// Repositories base class
    /// </summary>
    internal abstract class BaseRepository
    {
        #region Fields 

        /// <summary>
        /// Provides information about the web hosting environment an application is running in.
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Infrastructure.Repositories.BaseRepository class.
        /// </summary>
        /// <param name="webHostEnvironment">Provides information about the web hosting environment an application is running in.</param>
        public BaseRepository(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        #endregion

        #region Methods

        #region protected

        /// <summary>
        /// Combines two strings into a path => ContentRootPath + tablePath.
        /// </summary>
        /// <param name="tablePath">Table path</param>
        /// <returns>The combined paths.</returns>
        protected string CombinePath(string tablePath)
        {
            return Path.Combine(_webHostEnvironment.ContentRootPath, tablePath);
        }

        /// <summary>
        /// Asynchronously opens a text file, reads all the text in the file, and then closes the file.
        /// </summary>
        /// <param name="filePath">A relative or absolute path of the file.</param>
        /// <returns>A task that represents the asynchronous read operation, which wraps the string containing all text in the file.</returns>
        protected static async Task<T> ReadTextAsync<T>(string filePath)
        {
            var text = await File.ReadAllTextAsync(filePath).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(text);
        }

        /// <summary>
        /// Asynchronously writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="filePath">A relative or absolute path of the file.</param>
        /// <param name="text">File text</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        protected static async Task WriteTextAsync<T>(string filePath, T obj)
        {
            string text = JsonConvert.SerializeObject(obj, Formatting.Indented);

            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using var sourceStream =
                new FileStream(
                    filePath,
                    FileMode.Create, FileAccess.Write, FileShare.None,
                    bufferSize: 4096, useAsync: true);

            await sourceStream.WriteAsync(encodedText, 0, encodedText.Length).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
