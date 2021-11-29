namespace Pharmacy_IS.ViewModel.Service
{
    public class LoggedUser
    {
        public string UserName { get; set; }
        public UserRole Role { get; set; }

        public LoggedUser()
        {
        }

        public LoggedUser(string userName, UserRole role)
        {
            UserName = userName;
            Role = role;
        }

        public string ToString()
        {
            return this.UserName + " - " + Role.ToString();
        }
    }
}