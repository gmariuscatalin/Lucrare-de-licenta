using System;

namespace NewBank2.ViewModel
{
    /* This class, StoreUserViewModel, is a static class that stores the user's data
     during the application session. It acts as a temporary storage for the user's
     information, allowing other ViewModel classes to easily access and modify it.*/
    internal static class StoreUserViewModel
    {
        public static Guid Id { get; set; }

        public static string Username { get; set; }

        public static string Password { get; set; }

        public static string Name { get; set; }

        public static string LastName { get; set; }

        public static string ProfilePicture { get; set; }

    }
}
