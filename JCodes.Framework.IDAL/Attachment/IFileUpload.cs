using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.IDAL
{
    /// <summary>
    /// 上传文件操作
    /// </summary>
    public interface IFileUpload : IBaseDAL<FileUploadInfo>
	{
        /// <summary>
        /// 获取指定用户的上传信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        List<FileUploadInfo> GetAllByUserId(Int32 userId, bool isSuperAdmin, IsDelete isDelete);

        /// <summary>
        /// 获取指定用户的上传信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="category">附件分类：个人附件，业务附件</param>
        /// <param name="pagerInfo">分页信息</param>
        /// <returns></returns>
        List<FileUploadInfo> GetAllByUserId(Int32 userId, AttachmentType attachmentType, PagerInfo pagerInfo);
                        
        /// <summary>
        /// 获取指定附件组GUID的附件信息
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <param name="pagerInfo">分页信息</param>
        /// <returns></returns>
        List<FileUploadInfo> GetByAttachGid(string attachmentGid, PagerInfo pagerInfo);
                        
        /// <summary>
        /// 获取指定附件组GUID的附件信息
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <returns></returns>
        List<FileUploadInfo> GetByAttachGid(string attachmentGid);

        /// <summary>
        /// 根据文件的相对路径，删除文件
        /// </summary>
        /// <param name="savePath"></param>
        /// <returns></returns>
        bool DeleteByFilePath(string savePath, Int32 userId);

        /// <summary>
        /// 根据附件组GUID获取对应的文件名列表，方便列出文件名
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <returns>返回ID和文件名的列表</returns>
        Dictionary<string, string> GetFileNames(string attachmentGid);

        /// <summary>
        /// 标记为删除（不直接删除)
        /// </summary>
        /// <param name="id">文件的ID</param>
        /// <returns></returns>
        bool SetDeleteFlag(string Id);

    }
}