using FurnitureEmporim.Models.Entities;
namespace FurnitureEmporim.Models.Interface
{
    public interface IUserRepository
    {
        public List<Userss> GetAllUsers();
        public Userss GetUserByEmail(string email);
        public bool AddUser(Userss user);
        public void UpdateUser(Userss user);
        public void DeleteUser(int id);
        public bool isUSerExist(string email, string passwd);


    }
}
