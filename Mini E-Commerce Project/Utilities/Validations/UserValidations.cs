using Mini_E_Commerce_Project.Exceptions;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.Utilities.Validations
{
    public static class UserValidations
    {
        public static void ValidPassword(string password)
        {
            bool isUpper = false;
            bool isLower = false;
            bool isDigit = false;
            bool isPunct = false;

            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsUpper(password[i]))
                {
                    isUpper = true;
                }
                else if (char.IsLower(password[i]))
                {
                    isLower = true;
                }
                else if (char.IsDigit(password[i]))
                {
                    isDigit = true;
                }
                else if (char.IsPunctuation(password[i]))
                {
                    isPunct = true;
                }
                if (isUpper && isLower && isDigit || isPunct && password.Length >= 8)
                {
                    return;
                }

            }
            throw new InvalidPasswordException("Password should contain uppercase & lowercase letters and digits. Length of password should minimum be 8 characters.");
        }

        public static bool ValidFullName(string fullName)
        {
            foreach (char c in fullName)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    throw new InvalidFullNameException("Full Name should consist of letters and spaces only.");
                }
            }
            return true;
        }


        public static bool ValidEmail(string email)
        {

            if (email.Contains('@') && email.Contains('.'))
            {
                return true;
            }
            throw new InvalidEmailException("Email should consist of '@' and '.'");
        }
    }
}
