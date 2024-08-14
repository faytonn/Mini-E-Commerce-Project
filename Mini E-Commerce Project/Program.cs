using Microsoft.IdentityModel.Tokens;
using Mini_E_Commerce_Project.Exceptions;
using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Services.ADMIN.AdminImplementations;
using Mini_E_Commerce_Project.Services.AdminImplementations;
using Mini_E_Commerce_Project.Services.Implementations;
using Mini_E_Commerce_Project.Utilities;
using Mini_E_Commerce_Project.Utilities.Validations;
using System.Drawing;

bool systemProcess = true;
bool AdminSystem = true;
bool UserSystem = true;

AdminService adminService = new AdminService();
UserService userService = new UserService();
OrderDetailService orderDetailService = new OrderDetailService();
PaymentService paymentService = new PaymentService();
ProductService productService = new ProductService();
OrderService orderService = new OrderService();

User[] prospectiveUser = new User[0];
User loggedInUser = new("", "", "", "", "", "");

while (systemProcess)
{
    Console.WriteLine("Welcome to the system!");
restartSystemMenu:
    Console.WriteLine("> > > MAIN MENU < < <");
    Console.WriteLine("[1] Create a new user");
    Console.WriteLine("[2] Log in");
    Console.WriteLine("[3] Exit");

    string command = Console.ReadLine();

    switch (command)
    {
        case "1":
            

    }
}






static void CreateUser(UserService userService, User[] prospectiveUser)
{
    restartRegistrationProcess:
    try
    {
        Colored.WriteLine("Are you an admin? (yes/no): ", ConsoleColor.Green);
        string adminCheck = Console.ReadLine().ToLower().Trim();
        bool isAdmin = adminCheck == "yes";

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


        User user = new User
        {
            isAdmin = isAdmin,
            FullName = fullName,
            Address = address,
            Email = email,
            Password = password
        };

    }
    catch(ArgumentNullException ex)
    {

    }

}

















