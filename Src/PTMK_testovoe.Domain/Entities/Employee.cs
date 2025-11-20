using PTMK_testovoe.Domain.Base;


namespace PTMK_testovoe.Domain.Entities;

/// <summary>
/// Сотрудник
/// </summary>
public class Employee : BaseEntity
{
    /// <summary>
    /// ФИО
    /// </summary>
    public string FullName {  get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Пол. 0 - М, 1 - Ж
    /// </summary>
    public bool Gender { get; set; }
}
