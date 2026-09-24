using VehicleWash.Repository;
using VehicleWash.Services;
using VehicleWash.Views;

namespace VehicleWash
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userRepository = new UserRepository();
            var vehicleRepository = new VehicleRepository();
            var vehicleRecordRepository = new VehicleRecordRepository();

            var userAuthService = new UserAuthService(userRepository);
            var vehicleService = new VehicleService(vehicleRepository, userAuthService);
            var vehicleWashService = new VehicleWashService(vehicleRepository, vehicleRecordRepository);

            var userView = new UserView(userAuthService);
            var vehicleView = new VehicleView(vehicleService);
            var vehicleWashView = new VehicleWashView(vehicleWashService);

            var masterView = new MasterView(userView, vehicleView, vehicleWashView);
            masterView.Run();
        }
    }
}