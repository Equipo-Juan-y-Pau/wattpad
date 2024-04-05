using UsersandProfile.Models;

namespace UsersandProfile.Datos
{
	public static class ProfileStore
	{
        public static List<Profile> profileList = new List<Profile>
        {
            new Profile { Id = 1, Nombre = "Juan", Avatar ="Ospina.jpg"},
            new Profile { Id = 2, Nombre = "Paula", Avatar ="Misas.jpg"}

        };

    }
}
