using zBus.Models;

namespace zBus.Data
{
    public class AppDbInitializer
    {

        public static void seed(IApplicationBuilder AppBuilder)
        {
            using (var serviceScope = AppBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                
                if (!context.Buses.Any())
                {
                    context.Buses.AddRange(new List<Bus>()
                    {
                        new Bus()
                        {
                            BusPicture = "https://inkasarmored.com/wp-content/uploads/INKAS-Armored-Buses.jpg",
                            BusModel = "Model X",
                            NumberOfSeats = 50,
                            AirConditioningAvailable = true,
                            WifiAvailable = true,
                            RestroomAvailable = false,
                            DriverId = 3 
                        },
                        new Bus()
                        {
                            BusPicture = "https://intercity-buses.com/wp-content/uploads/2021/08/About_sec02_002.png",
                            BusModel = "Model Y",
                            NumberOfSeats = 40,
                            AirConditioningAvailable = true,
                            WifiAvailable = false,
                            RestroomAvailable = true,
                            DriverId = 2 
                        },

                    });
                    
                    context.SaveChanges();

                }
                if (!context.Stations.Any())
                {
                    context.Stations.AddRange(new List<Station>()
                    {
                        new Station()
                        {
                            StationCity = "City A",
                            StationAddress = "Address 1",
                            StationName = "Station 1",
                            ContactNumber = "123-456-7890"
                        },
                        new Station()
                        {
                            StationCity = "City B",
                            StationAddress = "Address 2",
                            StationName = "Station 2",
                            ContactNumber = "987-654-3210"
                        },

                    });

                    //context.SaveChanges();
                }

            }
        }
    }
}
