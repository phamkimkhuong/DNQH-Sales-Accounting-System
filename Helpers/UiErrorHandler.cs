using System;
using System.Windows.Forms;

namespace DNQH_KeToanBanHang.Helpers
{
    public static class UiErrorHandler
    {
        public static void Show(
            IWin32Window owner,
            string operation,
            string userMessage,
            Exception exception)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string userId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "System";
            string role = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "None";

            AppLogger.Error(
                operation,
                userMessage,
                exception,
                correlationId,
                userId,
                0,
                null,
                null,
                role);

            MessageBox.Show(
                owner,
                string.Format("{0}\nMã tra cứu: {1}", userMessage, correlationId),
                "Lỗi ứng dụng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
