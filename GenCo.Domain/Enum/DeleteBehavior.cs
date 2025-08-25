using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain.Enum
{
    public enum DeleteBehavior
    {
        Cascade,   // Xóa cha -> xóa con
        Restrict,  // Không cho xóa cha nếu còn con
        SetNull,   // Xóa cha -> con chuyển FK = null
        NoAction   // SQL hỗ trợ, khác Restrict (Deferred checking)
    }
}
