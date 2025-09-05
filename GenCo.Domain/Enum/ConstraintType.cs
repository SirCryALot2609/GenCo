namespace GenCo.Domain.Enum;

public enum ConstraintType
{
    PrimaryKey,   // Khóa chính
    UniqueKey,    // Khóa duy nhất (rename Unique -> UniqueKey cho rõ)
    Index,        // Index (non-unique hoặc composite)
    CheckConstraint // Rename Check -> CheckConstraint (tránh trùng keyword "CHECK")
}