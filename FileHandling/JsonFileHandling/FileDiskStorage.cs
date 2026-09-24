namespace JsonFileHandling
{
    public class FileDiskStorage
    {
        private readonly string _filePath;

        public FileDiskStorage(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be empty.", nameof(filePath));

            string? directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            _filePath = filePath;
        }

        public bool Exists() => File.Exists(_filePath);

        public async Task<string> ReadAllTextAsync()
        {
            if (Exists())
            {
                return await File.ReadAllTextAsync(_filePath);
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task WriteAllTextAsync(string content)
        {
            await File.WriteAllTextAsync(_filePath, content);
        }
    }
}
