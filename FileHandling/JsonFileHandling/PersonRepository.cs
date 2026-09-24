namespace JsonFileHandling
{
    public class PersonRepository
    {
        private readonly FileDiskStorage _diskStorage;
        private readonly JsonFile _jsonProcessor;
        private List<Person> _memoryCache;
        private readonly SemaphoreSlim _fileLock = new SemaphoreSlim(1, 1);
        public PersonRepository(FileDiskStorage diskStorage, JsonFile jsonProcessor)
        {
            _diskStorage = diskStorage;
            _jsonProcessor = jsonProcessor;
            _memoryCache = new List<Person>();
        }
        public void Add(Person person)
        {
            lock (_memoryCache)
            {
                _memoryCache.Add(person);
            }
        }
        public List<Person> GetAllInMemory()
        {
            lock (_memoryCache)
            {
                return _memoryCache;
            }
        }
        public async Task LoadAsync()
        {
            await _fileLock.WaitAsync();
            try
            {
                string rawJson = await _diskStorage.ReadAllTextAsync();
                lock (_memoryCache)
                {
                    _memoryCache = _jsonProcessor.Deserialize<List<Person>>(rawJson);
                }
            }
            finally
            {
                _fileLock.Release();
            }
        }
        public async Task SaveAsync()
        {
            string rawJson;

            lock (_memoryCache)
            {
                rawJson = _jsonProcessor.Serialize(_memoryCache);
            }
            await _fileLock.WaitAsync();
            try
            {
                await _diskStorage.WriteAllTextAsync(rawJson);
            }
            finally
            {
                _fileLock.Release();
            }
        }
        public void Dispose()
        {
            _fileLock.Dispose();
        }
    }
}
