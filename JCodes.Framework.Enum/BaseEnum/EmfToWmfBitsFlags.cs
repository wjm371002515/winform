
namespace JCodes.Framework.jCodesenum.BaseEnum
{
    public enum EmfToWmfBitsFlags
    {

        // Use the default conversion
        EmfToWmfBitsFlagsDefault = 0x00000000,

        // Embedded the source of the EMF metafiel within the resulting WMF
        // metafile
        EmfToWmfBitsFlagsEmbedEmf = 0x00000001,

        // Place a 22-byte header in the resulting WMF file.  The header is
        // required for the metafile to be considered placeable.
        EmfToWmfBitsFlagsIncludePlaceable = 0x00000002,

        // Don't simulate clipping by using the XOR operator.
        EmfToWmfBitsFlagsNoXORClip = 0x00000004
    };
}
