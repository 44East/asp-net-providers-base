using ProvidersBase.Model.Models;

namespace ProvidersBase.Model.DataAccessLayer
{
    public static class DBInitializer
    {
        public static void GenerateTestData(ProvidersContext context)
        {
            if (context.Providers.Any())
            {
                return; // DB has been seeded
            }

            // Создание тестовых данных для ProviderCompany
            var company1 = new ProviderCompany
            {
                CompanyTitle = "Company 1",
                INN = "1234567890",
                Email = "company1@example.com",
                Address = "Address 1"
            };

            var company2 = new ProviderCompany
            {
                CompanyTitle = "Company 2",
                INN = "9876543210",
                Email = "company2@example.com",
                Address = "Address 2"
            };

            context.Providers.AddRange(company1, company2);
            context.SaveChanges();

            // Создание тестовых данных для ProviderProduct
            var product1 = new ProviderProduct
            {
                ProviderId = company1.Id,
                Provider = company1,
                Title = "Product 1",
                Description = "Description 1",
                Price = 10.99m,
                InPacking = 5
            };

            var product2 = new ProviderProduct
            {
                ProviderId = company1.Id,
                Provider = company1,
                Title = "Product 2",
                Description = "Description 2",
                Price = 19.99m,
                InPacking = 3
            };

            var product3 = new ProviderProduct
            {
                ProviderId = company2.Id,
                Provider = company2,
                Title = "Product 3",
                Description = "Description 3",
                Price = 15.99m,
                InPacking = 4
            };

            context.Products.AddRange(product1, product2, product3);
            context.SaveChanges();

            // Создание тестовых данных для ProviderUser
            var user1 = new ProviderUser
            {
                ProviderId = company1.Id,
                Provider = company1,
                Name = "User 1",
                UserName = "user1",
                Email = "user1@example.com",
                PhoneNumber = "1234567890"
            };

            var user2 = new ProviderUser
            {
                ProviderId = company1.Id,
                Provider = company1,
                Name = "User 2",
                UserName = "user2",
                Email = "user2@example.com",
                PhoneNumber = "9876543210"
            };

            var user3 = new ProviderUser
            {
                ProviderId = company2.Id,
                Provider = company2,
                Name = "User 3",
                UserName = "user3",
                Email = "user3@example.com",
                PhoneNumber = "5555555555"
            };

            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges();

        }
    }
}
