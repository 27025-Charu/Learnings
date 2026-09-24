using MultiUserNotification.Repository;
using MultiUserNotification.Services;
using MultiUserNotification.View;

namespace MultiUserNotification
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InMemoryRepository repository = new InMemoryRepository();
            Service service = new Service(repository);
            UserView view = new UserView(service);
            view.UserInputData();
            Console.ReadKey();
        }
    }
}
