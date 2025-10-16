namespace GenCo.Domain.Enum;

public enum ConstraintType
{
    /// <summary>
    /// Primary Key (PK) – định danh duy nhất cho mỗi bản ghi.
    /// </summary>
    PrimaryKey = 0,

    /// <summary>
    /// Foreign Key (FK) – tham chiếu đến Primary Key ở entity khác.
    /// </summary>
    ForeignKey = 1,

    /// <summary>
    /// Unique Key – đảm bảo các giá trị trong cột (hoặc nhóm cột) là duy nhất.
    /// </summary>
    UniqueKey = 2,

    /// <summary>
    /// Check Constraint – đảm bảo dữ liệu thỏa mãn điều kiện logic cụ thể.
    /// </summary>
    Check = 3,

    /// <summary>
    /// Index – chỉ mục giúp tăng tốc truy vấn.
    /// </summary>
    Index = 4,
}
