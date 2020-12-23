using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    [Serializable] //User类允许被反序列化
    public class User
    {
        public int Index { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public Authority Level { get; set; }
    }

    public enum Authority
    { 
        Admin,
        Eng,
        Op,
    }

    public class UserHepler
    {
        private string filePath = string.Empty;

        public UserHepler(string path)
        {
            filePath = path;
        }

        /// <summary>
        /// 序列化到文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="listUser"></param>
        /// <returns></returns>
        public bool SerializedUser(string path, List<User> listUser)
        {
            if (listUser == null)
            {
                return false;
            }
            BinaryFormatter format = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                format.Serialize(fs, listUser);
                return true;
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<User> DeSerializedUser(string path)
        {
            BinaryFormatter format = new BinaryFormatter();
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    object o = format.Deserialize(fs);
                    return o as List<User>;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 创建超级用户
        /// </summary>
        /// <param name="path"></param>
        /// <param name="listUser"></param>
        public void CheckSupperUser(string path, List<User> listUser)
        {
            if (!File.Exists(path))
            {
                User user = new User()
                { Index = 0, UserName = "Admin", PassWord = "Admin", Level = Authority.Admin };
                listUser.Add(user);
                SerializedUser(path, listUser);
            }
        }

        public bool CheckContainUser(List<User> listUser, string userName)
        {
            var user = from item in listUser
                       where item.UserName == userName
                       select item;
            if (user.Count() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="path"></param>
        /// <param name="listUser"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(string path, List<User> listUser, User user)
        {
            if (user == null) return false;
            if (CheckContainUser(listUser, user.UserName)) return false;
            listUser.Add(user);
            SerializedUser(path, listUser);
            return true;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="path"></param>
        /// <param name="listUser"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool DeleteUser(string path, List<User> listUser, string userName)
        {
            if (listUser == null) return false;
            int index = 0;
            foreach (var item in listUser)
            {
                if (item.UserName == userName)
                {
                    break;
                }
                index++;
            }
            if (index == 0) return false;
            listUser.RemoveAt(index);
            SerializedUser(path, listUser);
            return true;
        }

        public bool EditUser(string path, List<User> listUser, User user)
        {
            if (listUser == null) return false;
            foreach (var item in listUser)
            {
                if (item.UserName == user.UserName)
                {
                    item.PassWord = user.PassWord;
                    item.Level = user.Level;
                    SerializedUser(path, listUser);
                    return true;
                }
            }
            return false;
        }
    }

}
