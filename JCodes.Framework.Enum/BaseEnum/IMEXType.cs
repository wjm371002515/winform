
namespace JCodes.Framework.jCodesenum.BaseEnum
{
    /// <summary>
    /// IMEX 三种模式。
    /// IMEX是用来告诉驱动程序使用Excel文件的模式，其值有0、1、2三种，分别代表导出、导入、混合模式。
    /// </summary>
    public enum IMEXType
    {
        ExportMode = 0, ImportMode = 1, LinkedMode = 2
    }
}
