using DocumentFormat.OpenXml.Drawing.Charts;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Enums;
using Mini_E_Commerce_Project.Exceptions;
using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Services.ADMIN.AdminImplementations;
using Mini_E_Commerce_Project.Services.ADMIN.AdminInterfaces;
using Mini_E_Commerce_Project.Services.AdminImplementations;
using Mini_E_Commerce_Project.Services.Implementations;
using Mini_E_Commerce_Project.Services.Interfaces;
using Mini_E_Commerce_Project.Utilities;
using Mini_E_Commerce_Project.Utilities.Validations;




bool systemProcess = true;
bool AdminSystem = true;
bool UserSystem = true;

AdminService adminService = new();
UserService userService = new UserService();
OrderDetailService orderDetailService = new OrderDetailService();
PaymentService paymentService = new PaymentService();
ProductService productService = new ProductService();
OrderService orderService = new OrderService();



while (systemProcess)
{
    Console.WriteLine("Welcome to the system!");
restartSystemMenu:
    Console.WriteLine("> > > MAIN MENU < < <");
    Console.WriteLine("[1] Create a new user");
    Console.WriteLine("[2] Log in");
    Console.WriteLine("[0] Exit");

    string command = Console.ReadLine();

    switch (command)
    {
        case "1":
            await CreateUser(userService);
            break;
        case "2":
            await LoginUser(userService, adminService);
            break;
        case "0":
            systemProcess = false;
            break;



    }
}



//ExportUsersToExcelAsync<Payment>(payments, @"C:\Users\Fatima\OneDrive\Desktop\EXCEL E-COMMERCE.xlsx", "Payments");
//static bool ExportUsersToExcelAsync<Payment>(List<Payment> payments, string filePath, string sheetName)
//{
//    bool exported = false;

//    using(IXLWorkbook workbook = new XLWorkbook())
//    {
//        workbook.AddWorksheet(sheetName).FirstCell().InsertTable<Payment>(payments, false);

//        workbook.SaveAs(filePath);
//        exported = true;
//    }

//    return exported;

//}

static async Task CreateUser(IUserService userService)
{
restartRegistrationProcess:
    try
    {
        bool isAdmin = false;
        Colored.Write("Are you an admin? (yes/no): ", ConsoleColor.Green);
        string adminCheck = Console.ReadLine().ToLower().Trim();
        if (adminCheck == "yes")
        {
            isAdmin = true;
        }

        Console.Write("Enter full name: ");
        string? fullName = Console.ReadLine().Trim();
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentNullException("You can not enter an empty space / null.");
        }
        UserValidations.ValidFullName(fullName);

        Console.Write("Enter home address: ");
        string address = Console.ReadLine().Trim();


        Console.Write("Enter email: ");
        string email = Console.ReadLine().Trim();
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentNullException("You can not enter an empty space / null.");
        }
        UserValidations.ValidEmail(email);

        Console.Write("Enter password: ");
        string password = Console.ReadLine().Trim();
        UserValidations.ValidPassword(password);


        CreateUserDTO newUser = new CreateUserDTO
        {
            isAdmin = isAdmin,
            FullName = fullName,
            Address = address,
            Email = email,
            Password = password
        };
        await userService.RegisterAsync(newUser);
        Colored.WriteLine($"Registration successful!\nHello, {fullName}!", ConsoleColor.Green);

    }
    catch (InvalidUserInformationException ex)
    {
        Colored.WriteLine(ex.Message, ConsoleColor.Red);
        goto restartRegistrationProcess;
    }
    catch (ArgumentNullException ex)
    {
        Colored.WriteLine(ex.Message, ConsoleColor.Red);
        goto restartRegistrationProcess;
    }
    catch (Exception ex)
    {
        Colored.WriteLine(ex.Message, ConsoleColor.Red);
        goto restartRegistrationProcess;
    }

}

static async Task LoginUser(UserService userService, AdminService adminService)
{
restartLoginProcess:
    try
    {
        Console.Write("Enter email: ");
        string email = Console.ReadLine().ToLower().Trim();
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentNullException("You can not enter an empty space / null.");
        }

        Console.Write("Enter password: ");
        string password = Console.ReadLine().Trim();
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException("You can not enter an empty space / null.");
        }

        User loginUser = new User
        {
            Email = email,
            Password = password
        };

        User loggedInUser = await userService.LoginAsync(loginUser);

        Colored.WriteLine($"Welcome back, {loggedInUser.FullName}", ConsoleColor.Green);

        if (loggedInUser.isAdmin)
        {
            await AdminMenu(adminService, loggedInUser);

        }
        //else
        //{
        //   await UserMenu(userService, loggedInUser);
        //}

    }
    catch (InvalidUserInformationException ex)
    {
        Colored.WriteLine($"Login failed: {ex.Message}", ConsoleColor.Red);
        goto restartLoginProcess;
    }
    catch (ArgumentNullException ex)
    {
        Colored.WriteLine(ex.Message, ConsoleColor.Red);
        goto restartLoginProcess;
    }
    catch (Exception ex)
    {
        Colored.WriteLine(ex.Message, ConsoleColor.Red);
        goto restartLoginProcess;
    }



}

static async Task AdminMenu(AdminService adminService, User currentUser)
{
    bool adminSystem = true;


    if (currentUser.isAdmin == true)
        while (adminSystem)
        {
            Colored.WriteLine("> > > ADMIN MENU < < <", ConsoleColor.Blue);
        restartAdminMenu:
            Console.WriteLine("[1] User Management");
            Console.WriteLine("[2] Product Management");
            Console.WriteLine("[3] Order Management");
            Console.WriteLine("[4] Order Detail Management");
            Console.WriteLine("[5] Payment Management");
            Console.WriteLine("[0] Exit");

            string commandAdmin = Console.ReadLine();

            switch (commandAdmin)
            {
                case "1":
                restartAdminsUserMenu:
                    Colored.WriteLine(" - - - USER MANAGEMENT - - - ", ConsoleColor.Gray);
                    Console.WriteLine("[1] Get all users");
                    Console.WriteLine("[2] Get user by ID");
                    Console.WriteLine("[3] Delete user");
                    Console.WriteLine("[0] Go back to main admin menu");

                    string userCommand = Console.ReadLine();
                    switch (userCommand)
                    {
                        case "1":
                            try
                            {
                                var users = await adminService.GetAllUsersAsync(currentUser);
                                Colored.WriteLine("Here are all the users in the system:", ConsoleColor.DarkGray);
                                foreach (var user in users)
                                {
                                    Console.WriteLine($"[{user.Id}] - {user.FullName} - {user.Email} - Admin?: {user.isAdmin}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminsUserMenu;
                            }
                            break;
                        case "2":
                        restartUserId:
                            try
                            {
                                var users = await adminService.GetAllUsersAsync(currentUser);
                                Colored.WriteLine("Here are all the users in the system's database:", ConsoleColor.DarkGray);
                                foreach (var user in users)
                                {
                                    Console.WriteLine($"[{user.Id}] - {user.FullName} - {user.Email} - Admin?: {user.isAdmin}");
                                }


                                Colored.Write("Enter the user ID that you want to update: ", ConsoleColor.DarkYellow);
                                int userId = int.Parse(Console.ReadLine());
                                var userById = await adminService.GetUserByIdAsync(userId, currentUser);
                                Console.WriteLine($"{userById.Id} - {userById.FullName} - {userById.Email} - Admin: {userById.isAdmin}");
                                break;
                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartUserId;
                            }
                        case "3":
                        restartDeleteProcess:
                            try
                            {
                                Colored.Write("Enter user ID to delete: ", ConsoleColor.DarkYellow);
                                int deleteUserId = int.Parse(Console.ReadLine());
                                await adminService.DeleteUserAsync(deleteUserId, currentUser);
                                Colored.WriteLine($"User with ID -{deleteUserId}- has been deleted successfully.", ConsoleColor.Green);
                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartDeleteProcess;
                            }
                            break;
                        case "0":
                            goto restartAdminMenu;
                    }
                    break;

                case "2":
                    Colored.WriteLine(" - - - PRODUCT MANAGEMENT - - - ", ConsoleColor.Gray);
                    Console.WriteLine("[1] Create a product");
                    Console.WriteLine("[2] Update a product by ID");
                    Console.WriteLine("[3] Delete a product");
                    Console.WriteLine("[4] Get product by ID");
                    Console.WriteLine("[5] Get all products");
                    Console.WriteLine("[0] Go back to main admin menu");

                    string productCommand = Console.ReadLine();

                    switch (productCommand)
                    {
                        case "1":
                        restartProductCreation:
                            try
                            {
                                Colored.WriteLine("Please enter the details below.", ConsoleColor.DarkYellow);

                                Console.Write("Product name: ");
                                string productName = Console.ReadLine();
                                if (string.IsNullOrEmpty(productName))
                                {
                                    throw new ArgumentNullException("You can not enter an empty space / null.");
                                }

                                Console.Write("Product price: ");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal productPrice))
                                {
                                    throw new ArgumentNullException("Invalid amount.");
                                }
                                if (productPrice < 0)
                                {
                                    throw new InvalidProductException("Product price can not be less than 0.");
                                }

                                Console.Write("Product description: ");
                                string productDescription = Console.ReadLine();
                                if (string.IsNullOrEmpty(productDescription))
                                {
                                    throw new ArgumentNullException("You can not enter an empty space / null.");
                                }

                                Console.Write("Product stock: ");
                                if (!int.TryParse(Console.ReadLine(), out int productStock))
                                {
                                    throw new InvalidProductException("Invalid number.");
                                }
                                if (productStock < 0)
                                {
                                    throw new InvalidProductException("Product stock can not be less than 0.");
                                }

                                CreateProductDTO createProduct = new CreateProductDTO()
                                {
                                    Name = productName,
                                    Price = productPrice,
                                    Description = productDescription,
                                    Stock = productStock
                                };

                                await adminService.CreateProductAsync(createProduct, currentUser);
                                Colored.WriteLine($"Product with the name {productName} was successfully created!", ConsoleColor.Green);
                            }
                            catch (ArgumentNullException ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartProductCreation;
                            }
                            catch (InvalidProductException ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartProductCreation;
                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartProductCreation;
                            }
                            break;
                        case "2":
                            try
                            {
                                var products = await adminService.GetAllProductsAsync(currentUser);

                                Colored.WriteLine("Here are all the products in the system:", ConsoleColor.DarkGray);
                                foreach (var product in products)
                                {
                                    Console.WriteLine($"[{product.Id}] - {product.Name} - ${product.Price} - Stock: {product.Stock}");
                                    Console.WriteLine($"{product.Description}");
                                    Console.WriteLine($"Created: {product.CreatedDate} | Updated: {product.UpdatedDate}");
                                }

                            restartIdInput:
                                Console.Write("Enter the product ID that you want to update: ");
                                int updateProduct = int.Parse(Console.ReadLine());
                                if (!int.TryParse(Console.ReadLine(), out updateProduct) || updateProduct <= 0)
                                {
                                    Colored.WriteLine("Invalid input.", ConsoleColor.Red);
                                    goto restartIdInput;
                                }

                                var productToUpdate = await adminService.GetProductByIdAsync(updateProduct, currentUser);

                            restartNameInput:
                                Console.Write("Enter new name: ");
                                string newName = Console.ReadLine().Trim();
                                if (string.IsNullOrEmpty(newName))
                                {
                                    Colored.WriteLine("Product name cannot be empty.", ConsoleColor.Red);
                                    goto restartNameInput;
                                }

                            restartPriceInput:
                                Console.Write("Enter new price: ");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice) || newPrice < 0)
                                {
                                    Colored.WriteLine("Invalid price.", ConsoleColor.Red);
                                    goto restartPriceInput;
                                }

                            restartStockInput:
                                Console.Write("Enter new stock: ");
                                if (!int.TryParse(Console.ReadLine(), out int newStock) || newStock < 0)
                                {
                                    Colored.WriteLine("Invalid stock. Please enter a positive integer value.", ConsoleColor.Red);
                                    goto restartStockInput;
                                }

                            restartDescriptionInput:
                                Console.Write("Enter new description: ");
                                string newDescription = Console.ReadLine().Trim();
                                if (string.IsNullOrEmpty(newDescription))
                                {
                                    Colored.WriteLine("Description cannot be empty.", ConsoleColor.Red);
                                    goto restartDescriptionInput;
                                }


                                InsertProductDTO updateProductDTO = new InsertProductDTO()
                                {
                                    Name = newName,
                                    Price = newPrice,
                                    Stock = newStock,
                                    Description = newDescription
                                };

                                var updatedProduct = await adminService.UpdateProductAsync(updateProductDTO, currentUser);
                                Colored.WriteLine($"Product '{updatedProduct.Name}' updated successfully!", ConsoleColor.Green);

                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            break;
                        case "3":
                            try
                            {
                                var products = await adminService.GetAllProductsAsync(currentUser);

                                Colored.WriteLine("Here are all the products in the system:", ConsoleColor.DarkGray);
                                foreach (var product in products)
                                {
                                    Console.WriteLine($"[{product.Id}] - {product.Name} - ${product.Price} - Stock: {product.Stock}");
                                    Console.WriteLine($"{product.Description}");
                                    Console.WriteLine($"Created: {product.CreatedDate} | Updated: {product.UpdatedDate}");
                                }

                            restartIdInput:
                                Colored.Write("Enter the product ID: ", ConsoleColor.DarkYellow);
                                if (!int.TryParse(Console.ReadLine(), out int deleteProductId) || deleteProductId <= 0)
                                {
                                    Colored.WriteLine("Invalid input.", ConsoleColor.Red);
                                    goto restartIdInput;
                                }

                                await adminService.DeleteProductAsync(deleteProductId, currentUser);
                                Colored.WriteLine($"Product with the ID -{deleteProductId}- was successfully deleted!", ConsoleColor.Green);
                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            break;
                        case "4":
                            try
                            {
                                var allProducts = await adminService.GetAllProductsAsync(currentUser);
                                Colored.WriteLine("Here are all the products in the system:", ConsoleColor.DarkGray);
                                foreach (var allproduct in allProducts)
                                {
                                    Console.WriteLine($"[{allproduct.Id}] - {allproduct.Name}");
                                }


                            restartIdInput:
                                Colored.Write("Enter the product ID: ", ConsoleColor.DarkYellow);
                                if (!int.TryParse(Console.ReadLine(), out int productId) || productId <= 0)
                                {
                                    Colored.WriteLine("Invalid input.", ConsoleColor.Red);
                                    goto restartIdInput;
                                }
                                
                                

                                var product = await adminService.GetProductByIdAsync(productId, currentUser);
                                Console.WriteLine($"[{product.Id}] - {product.Name} - ${product.Price} - Stock: {product.Stock}");
                                Console.WriteLine($"{product.Description}");
                                Console.WriteLine($"Created: {product.CreatedDate} | Updated: {product.UpdatedDate}");
                                

                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            break;
                        case "5":
                            try
                            {
                                var products = await adminService.GetAllProductsAsync(currentUser);
                                Colored.WriteLine("Here are all the products in the system:", ConsoleColor.DarkGray);
                                foreach (var product in products)
                                {
                                    Console.WriteLine($"[{product.Id}] - {product.Name} - ${product.Price} - Stock: {product.Stock}");
                                    Console.WriteLine($"{product.Description}");
                                    Console.WriteLine($"Created: {product.CreatedDate} | Updated: {product.UpdatedDate}\n");

                                }
                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            break;
                        case "0":
                            goto restartAdminMenu;
                    }

                    break;
                case "3":
                    Colored.WriteLine(" - - - ORDER MANAGEMENT - - - ", ConsoleColor.Gray);
                    Console.WriteLine("[1] Get all orders");
                    Console.WriteLine("[2] Get order by ID");
                    Console.WriteLine("[3] Update order status");
                    Console.WriteLine("[4] Complete an order");
                    Console.WriteLine("[5] Delete an order");
                    Console.WriteLine("[0] Go back to main admin menu");

                    string orderCommand = Console.ReadLine();
                    switch (orderCommand)
                    {
                        case "1":
                            try
                            {
                                var orders = await adminService.GetAllOrdersAsync(currentUser);
                                Colored.WriteLine("Here are all the orders in the system:", ConsoleColor.DarkGray);
                                foreach (var order in orders)
                                {
                                    Console.WriteLine($"Order ID: [{order.Id}] - User: {order.UsersName} - Total: {order.TotalAmount} - Status: {order.Status}\nOrder Date: {order.OrderDate}\n");
                                }
                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                            }
                            break;
                        case "2":
                            try
                            {
                            restartIdInput:
                                Console.Write("Enter the order ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int orderId) || orderId <= 0)
                                {
                                    Colored.WriteLine("Invalid input.", ConsoleColor.Red);
                                    goto restartIdInput;
                                }

                                var order = await adminService.GetOrderByIdAsync(orderId, currentUser);
                                Console.WriteLine($"Order ID: {order.Id} - User: {order.UsersName} - Total: {order.TotalAmount} - Status: {order.Status}\nOrder Date: {order.OrderDate}\n\nOrder Details:");
                                foreach (var detail in order.OrderDetails)
                                {
                                    Console.WriteLine($"Product ID: {detail.ProductId} - Quantity: {detail.Quantity} - Price per item: {detail.PricePerItem}");
                                }

                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            break;
                        case "3":
                            try
                            {
                                var orders = await adminService.GetAllOrdersAsync(currentUser);

                                Colored.WriteLine("Here are all the orders in the system:", ConsoleColor.Gray);
                                foreach (var order in orders)
                                {
                                    Console.WriteLine($"Order ID: {order.Id} - User: {order.UsersName} - Total: {order.TotalAmount} - Status: {order.Status}\nOrder Date: {order.OrderDate}\n\nOrder Details:");
                                    foreach (var detail in order.OrderDetails)
                                    {
                                        Console.WriteLine($"Order ID: [{detail.OrderId}] - Order Name : {detail.Orders} - Products: {detail.Product} - Quantity: {detail.Quantity} - Price per item: {detail.PricePerItem}");
                                    }
                                }

                            restartIdInput:
                                Colored.Write("Enter the order ID to update:", ConsoleColor.DarkYellow);
                                if (!int.TryParse(Console.ReadLine(), out int orderId) || orderId <= 0)
                                {
                                    Colored.WriteLine("Invalid input.", ConsoleColor.Red);
                                    goto restartIdInput;
                                }
                                var orderToUpdate = await adminService.GetOrderByIdAsync(orderId, currentUser);

                            restartChooseStatusEnum:
                                Colored.WriteLine("Select the new order status from the options down below:", ConsoleColor.Cyan);
                                Console.WriteLine("Pending");
                                Console.WriteLine("Completed");
                                Console.WriteLine("Canceled");
                                if (Enum.TryParse<StatusEnum>(Console.ReadLine().Trim().ToLower(), out StatusEnum status))
                                {
                                    if (orderToUpdate.Status == status)
                                    {
                                        Colored.WriteLine($"Order is already in '{status}' status.", ConsoleColor.Red);
                                        goto restartChooseStatusEnum;
                                    }
                                    await adminService.UpdateOrderStatusAsync(orderId, status, currentUser);
                                    Colored.WriteLine("Order status updated successfully!", ConsoleColor.Green);
                                }
                                else
                                {
                                    Colored.WriteLine("Invalid status.", ConsoleColor.Red);
                                    goto restartChooseStatusEnum;
                                }
                            }
                            catch (NotFoundException ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);

                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            break;
                        case "4":
                            try
                            {
                                var orders = await adminService.GetAllOrdersAsync(currentUser);

                                Colored.WriteLine("Here are all the orders in the system:", ConsoleColor.Gray);
                                foreach (var order in orders)
                                {
                                    Console.WriteLine($"Order ID: {order.Id} - User: {order.UsersName} - Total: {order.TotalAmount} - Status: {order.Status}\nOrder Date: {order.OrderDate}\n\nOrder Details:");
                                    foreach (var detail in order.OrderDetails)
                                    {
                                        Console.WriteLine($"Order ID: [{detail.OrderId}] - Order Name : {detail.Orders} - Products: {detail.Product} - Quantity: {detail.Quantity} - Price per item: {detail.PricePerItem}");
                                    }
                                }

                            restartIdInput:
                                Console.Write("Enter the order ID to complete: ");
                                if (!int.TryParse(Console.ReadLine(), out int orderId) || orderId <= 0)
                                {
                                    Colored.WriteLine("Invalid input.", ConsoleColor.Red);
                                    goto restartIdInput;
                                }

                                var orderToComplete = await adminService.GetOrderByIdAsync(orderId, currentUser);
                                if (orderToComplete.Status == StatusEnum.Completed)
                                {
                                    Colored.WriteLine("The order is already completed.", ConsoleColor.Red);
                                    break;
                                }
                                if (orderToComplete.Status == StatusEnum.Canceled)
                                {
                                    Colored.WriteLine("The order is already canceled.", ConsoleColor.Red);
                                    break;
                                }

                                await adminService.CompleteOrderAsync(orderId, currentUser);
                                Colored.WriteLine("Order completed successfully!", ConsoleColor.Green);
                            }
                            catch (NotFoundException ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            break;
                        case "5":
                            try
                            {
                                var orders = await adminService.GetAllOrdersAsync(currentUser);

                                Colored.WriteLine("Here are all the orders in the system:", ConsoleColor.Gray);
                                foreach (var order in orders)
                                {
                                    Console.WriteLine($"Order ID: {order.Id} - User: {order.UsersName} - Total: {order.TotalAmount} - Status: {order.Status}\nOrder Date: {order.OrderDate}\n\nOrder Details:");
                                    foreach (var detail in order.OrderDetails)
                                    {
                                        Console.WriteLine($"Order ID: [{detail.OrderId}] - Order Name : {detail.Orders} - Products: {detail.Product} - Quantity: {detail.Quantity} - Price per item: {detail.PricePerItem}");
                                    }
                                }

                            restartIdInput:
                                Colored.Write("Enter the order ID to delete: ", ConsoleColor.DarkYellow);
                                if (!int.TryParse(Console.ReadLine(), out int orderId) || orderId <= 0)
                                {
                                    Colored.WriteLine("Invalid input.", ConsoleColor.Red);
                                    goto restartIdInput;
                                }


                                var orderToDelete = await adminService.GetOrderByIdAsync(orderId, currentUser);

                                if (orderToDelete.Status == StatusEnum.Completed)
                                {
                                    Colored.WriteLine("Cannot delete a completed order.", ConsoleColor.Red);
                                    break;
                                }

                                if (orderToDelete.Status == StatusEnum.Canceled)
                                {
                                    Colored.WriteLine("Order is already canceled.", ConsoleColor.Red);
                                    break;
                                }
                                await adminService.DeleteOrderAsync(orderId, currentUser);
                                Colored.WriteLine("Order deleted successfully!", ConsoleColor.Green);

                            }
                            catch (UnauthorizedAccessException ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            catch (NotFoundException ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            break;
                        case "0":
                            goto restartAdminMenu;

                        default:
                            Colored.WriteLine("Invalid command. Please choose a valid option.", ConsoleColor.Red);
                            break;
                    }
                    break;
                case "4":
                    Colored.WriteLine(" - - - ORDER DETAIL MANAGEMENT - - - ", ConsoleColor.Gray);
                    Console.WriteLine("[1] View order details by order ID");
                    Console.WriteLine("[2] Update order detail");
                    Console.WriteLine("[3] Delete order detail");
                    Console.WriteLine("[0] Go back to main admin menu");

                    string orderDetailCommand = Console.ReadLine();
                    switch (orderDetailCommand)
                    {
                        case "1":
                            try
                            {
                                var orders = await adminService.GetAllOrdersAsync(currentUser);

                                Colored.WriteLine("Here are all the orders in the system:", ConsoleColor.Gray);
                                foreach (var order in orders)
                                {
                                    Console.WriteLine($"Order ID: {order.Id} - User: {order.UsersName} - Total: {order.TotalAmount} - Status: {order.Status}\nOrder Date: {order.OrderDate}");
                                }


                            restartIdInput:
                                Console.Write("Enter the order ID to view details: ");
                                if (!int.TryParse(Console.ReadLine(), out int orderId) || orderId <= 0)
                                {
                                    Colored.WriteLine("Invalid input.", ConsoleColor.Red);
                                    goto restartIdInput;
                                }

                                var orderDetails = await adminService.GetOrderDetailsByOrderIdAsync(orderId, currentUser);
                                if (orderDetails == null || !orderDetails.Any())
                                {
                                    Colored.WriteLine("No order details found for the given order ID.", ConsoleColor.Red);
                                    break;
                                }

                                foreach (var detail in orderDetails)
                                {
                                    Console.WriteLine($"Product ID: [{detail.ProductId}] - Quantity: {detail.Quantity} - Price per item: {detail.PricePerItem}");
                                }
                            }
                            catch (UnauthorizedAccessException ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            catch (NotFoundException ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);

                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            break;
                        case "2":
                            try
                            {
                                var orders = await adminService.GetAllOrdersAsync(currentUser);

                                Colored.WriteLine("Here are all the orders in the system:", ConsoleColor.Gray);
                                foreach (var order in orders)
                                {
                                    Console.WriteLine($"Order ID: {order.Id} - User: {order.UsersName} - Total: {order.TotalAmount} - Status: {order.Status}\nOrder Date: {order.OrderDate}\n\nOrder Details:");
                                    foreach (var detail in order.OrderDetails)
                                    {
                                        Console.WriteLine($"Product ID: [{detail.ProductId}] - Quantity: {detail.Quantity} - Price per item: {detail.PricePerItem}");
                                    }
                                }


                            restartIdInput:
                                Console.Write("Enter the product ID that you want to update: ");
                                if (!int.TryParse(Console.ReadLine(), out int updateOrder) || updateOrder <= 0)
                                {
                                    Colored.WriteLine("Invalid input.", ConsoleColor.Red);
                                    goto restartIdInput;
                                }

                                var orderToUpdate = await adminService.GetProductByIdAsync(updateOrder, currentUser);

                            restartQuantityInput:
                                Console.Write("Enter new quantity: ");
                                if (!int.TryParse(Console.ReadLine(), out int newQuantity) || newQuantity < 0)
                                {
                                    Colored.WriteLine("Invalid quantity.", ConsoleColor.Red);
                                    goto restartQuantityInput;
                                }

                                //restartPriceInput:
                                //    Console.Write("Enter new price: ");
                                //    if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice) || newPrice < 0)
                                //    {
                                //        Colored.WriteLine("Invalid price.", ConsoleColor.Red);
                                //        goto restartPriceInput;
                                //    }

                                InsertOrderDetailDTO updateOdDTO = new InsertOrderDetailDTO()
                                {
                                    Quantity = newQuantity,
                                    //PricePerItem = newPricepItem
                                };

                                var updatedOrderDetail = await adminService.UpdateOrderDetailByIdAsync(updateOdDTO, currentUser);
                                Colored.WriteLine($"Quantity '{updateOdDTO.Quantity}' in order detail updated successfully!", ConsoleColor.Green);


                            }
                            catch (NotFoundException ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                            }
                            catch (Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            break;
                        case "3":
                            try
                            {
                                var orders = await adminService.GetAllOrdersAsync(currentUser);

                                Colored.WriteLine("Here are all the orders in the system:", ConsoleColor.Gray);
                                foreach (var order in orders)
                                {
                                    Console.WriteLine($"Order ID: {order.Id} - User: {order.UsersName} - Total: {order.TotalAmount} - Status: {order.Status}\nOrder Date: {order.OrderDate}\n\nOrder Details:");
                                    foreach (var detail in order.OrderDetails)
                                    {
                                        Console.WriteLine($"Product ID: [{detail.ProductId}] - Quantity: {detail.Quantity} - Price per item: {detail.PricePerItem}");
                                    }
                                }

                            restartIdInput:
                                Console.Write("Enter the order detail that you want to delete: ");

                                if (!int.TryParse(Console.ReadLine(), out int orderDetailId) || orderDetailId <= 0)
                                {
                                    Colored.WriteLine("Invalid input.", ConsoleColor.Red);
                                    goto restartIdInput;
                                }


                                var orderDetailToDelete = await adminService.GetOrderByIdAsync(orderDetailId, currentUser);

                                if (orderDetailToDelete.Status == StatusEnum.Completed)
                                {
                                    Colored.WriteLine("Cannot delete a completed order.", ConsoleColor.Red);
                                    break;
                                }

                                if (orderDetailToDelete.Status == StatusEnum.Canceled)
                                {
                                    Colored.WriteLine("Order is already canceled.", ConsoleColor.Red);
                                    break;
                                }


                                await adminService.DeleteOrderDetailAsync(orderDetailId, currentUser);
                                Colored.WriteLine("Order deleted successfully!", ConsoleColor.Green);



                            }
                            catch(NotFoundException ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                            catch(Exception ex)
                            {
                                Colored.WriteLine(ex.Message, ConsoleColor.Red);
                                goto restartAdminMenu;
                            }
                                break;
                        case "0":
                            goto restartAdminMenu;
                        default:
                            Colored.WriteLine("Invalid command. Please choose a valid option.", ConsoleColor.Red);
                            break;

                    }
                    break;
                case "5":
                    Colored.WriteLine(" - - - PAYMENT MANAGEMENT - - - ",ConsoleColor.Gray);
                    Console.WriteLine("[1] Get all payments");
                    Console.WriteLine("[2] Refund payment");
                    Console.WriteLine("[0] Go back to admin menu");

                    string paymentCommand = Console.ReadLine();
                    switch(paymentCommand)
                    {
                        case "1":

                            break;
                    }
                    break;
            }

        }
}
//static async Task

















