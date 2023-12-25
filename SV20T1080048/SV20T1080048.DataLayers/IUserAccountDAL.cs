using SV20T1080048.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1080048.DataLayers
{
    /// <summary>
    ///  Định nghĩa các phép sử lý liên quan đến tài khoản
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// Xác thực tài khoản đăng nhập của người dùng(employee, customer)
        /// Nếu xác thực thành công thì trả về thông tin tài khoản, ngược lại thì trả về null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string password);
        /// <summary>
        /// Thay đổi mật khẩu
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //bool ChangePassword(string userName, string newPassword);
    }
}
