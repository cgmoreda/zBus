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
                            ProfilePicture ="https://assets-global.website-files.com/602a97fb7ff89c083c49cc06/64ae6f120f19a375e4fb8867_untitled-63.png",
                            FullName = "John Smith",
                            Contact = "+201120202020",
                            Age = 32,
                            YearsOfExperince = 5
                        },
                        new Driver()
                        {
                            ProfilePicture ="https://www.workitdaily.com/media-library/image.jpg?id=19298040&width=980&quality=85",
                            FullName = "Marcus Johnson",
                            Contact = "+201112345920",
                            Age = 36,
                            YearsOfExperince = 7
                        },
                        new Driver()
                        {
                            ProfilePicture ="https://t3.ftcdn.net/jpg/06/35/34/02/360_F_635340259_X69zuZlLKcX0RbPyjdNOpLPBFPeib3IN.jpg",
                            FullName = "Chloe Turner",
                            Contact = "+201122666661",
                            Age = 24,
                            YearsOfExperince = 0
                        }
                     }); ;
                    context.SaveChanges();
                }
            }
        }
    }
}
