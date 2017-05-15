
namespace JCodes.Framework.jCodesenum.BaseEnum
{
    public enum OSProductType : byte
    {
        /// <summary>
        /// The system is a server
        /// </summary>
        VER_NT_SERVER = 3,

        /// <summary>
        /// The system is a domain controller
        /// </summary>
        VER_NT_DOMAIN_CONTROLLER = 2,

        /// <summary>
        /// Windows Professional
        /// </summary>
        VER_NT_WORKSTATION = 1,

        /// <summary>
        /// UnKnown
        /// </summary>
        VER_UNKNOWN = 0,
    }
}
