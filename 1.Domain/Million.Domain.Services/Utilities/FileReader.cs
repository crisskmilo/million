using System;
using System.IO;

namespace Million.Domain.Services.Utilities
{
    public static class FileReader
    {
        /// <summary>
        /// Reads the entire content of a text file.
        /// </summary>
        /// <param name="filePath">Full path to the file.</param>
        /// <returns>File content as a string.</returns>
        public static string ReadAllText(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("The file path cannot be null or empty.", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("The file does not exist.", filePath);

            return File.ReadAllText(filePath);
        }
    }
}