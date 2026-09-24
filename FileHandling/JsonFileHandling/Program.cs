namespace JsonFileHandling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var storage = new FileDiskStorage("Persons.json");
            var jsonFile = new JsonFile();
            var repository = new PersonRepository(storage, jsonFile);
            Person p1 = new Person()
            {
                Name = "Charu",
                Age = 20,
                Occupation = "intern",
                Id = 19,
            };
            Person p2 = new Person()
            {
                Name = "Charu",
                Age = 20,
                Occupation = "intern",
                Id = 19,
            };
            repository.Add(p1);
            repository.Add(p2);
            repository.SaveAsync();
            repository.LoadAsync();
            var currentData = repository.GetAllInMemory();
            foreach (var item in currentData)
            {
                Console.WriteLine($"Item {item.Id} : {item.Name} - {item.Age} - {item.Occupation}");
            }
            Console.ReadKey();
        }
    }
}
