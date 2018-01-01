using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 图片相册
    /// </summary>
	public class PictureAlbum : BaseBLL<PictureAlbumInfo>
    {
        public PictureAlbum() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }
    }
}
