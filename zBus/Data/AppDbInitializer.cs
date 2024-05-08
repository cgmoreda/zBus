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
            
                
                    //context.SaveChanges();
                }

            }
        }
    }
