using TodoApplication.Model;
using TodoApplication.Repository;
using TodoApplication.Services;
using TodoApplication.View;

namespace TodoApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var (userRepository, taskRepository) = InitializeRepositories();
            var userAuthService = new UserAuthService(userRepository);
            var taskService = new TaskService(taskRepository);
            var userView = new UserView(userAuthService);
            var taskView = new TaskView(taskService);
            var masterView = new MasterView(userView, taskView);
            masterView.Run();
        }

        private static (IUserRepository, ITaskRepository) InitializeRepositories()
        {
            StorageType storageType = StorageType.Invalid;
            while (storageType == StorageType.Invalid)
            {
                MasterView.RepoStorageMenu();
                storageType = MasterView.ReadStorageChoice();

                if (storageType == StorageType.Invalid)
                {
                    Helper.DisplayError("Invalid storage option. Please choose 1 or 2.");
                }
            }

            if (storageType == StorageType.Json)
            {
                Helper.DisplaySuccess("Storage initialized using Json mode.");
                return (new JsonUserRepository(), new JsonTaskRepository());
            }

            Helper.DisplaySuccess("Storage initialized using InMemory mode.");
            return (new UserRepository(), new TaskRepository());
        }
    }
}
