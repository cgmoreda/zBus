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
                if (!context.Drivers.Any())
                {
                    context.Drivers.AddRange(new List<Driver>()
                     {
                        new Driver()
                        {
                            ProfilePicturePath ="https://assets-global.website-files.com/602a97fb7ff89c083c49cc06/64ae6f120f19a375e4fb8867_untitled-63.png",
                            FullName = "John Smith",
                            Contact = "+201120202020",
                            Age = 32,
                            YearsOfExperience = 5
                        },
                        new Driver()
                        {
                            ProfilePicturePath ="https://www.workitdaily.com/media-library/image.jpg?id=19298040&width=980&quality=85",
                            FullName = "Marcus Johnson",
                            Contact = "+201112345920",
                            Age = 36,
                            YearsOfExperience = 7
                        },
                        new Driver()
                        {
                            ProfilePicturePath ="https://t3.ftcdn.net/jpg/06/35/34/02/360_F_635340259_X69zuZlLKcX0RbPyjdNOpLPBFPeib3IN.jpg",
                            FullName = "Chloe Turner",
                            Contact = "+201122666661",
                            Age = 24,
                            YearsOfExperience = 0
                        }
                     });
                    context.SaveChanges();
                }
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

                    context.SaveChanges();
                }

            }
        }
    }
}
