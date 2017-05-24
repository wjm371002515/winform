using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;

namespace JCodes.Framework.IDAL
{
    /// <summary>
    /// 上传文件操作
    /// </summary>
	public interface IFileUploads : IBaseDAL<FileUploadInfo>
	{
        /// <summary>
        /// 获取指定用户的上传信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        List<FileUploadInfo> GetAllByUser(string userId);
                
        /// <summary>
        /// 获取指定用户的上传信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="category">附件分类：个人附件，业务附件</param>
        /// <param name="pagerInfo">分页信息</param>
        /// <returns></returns>
        List<FileUploadInfo> GetAllByUser(string userId, string category, PagerInfo pagerInfo);
                        
        /// <summary>
        /// 获取指定附件组GUID的附件信息
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <param name="pagerInfo">分页信息</param>
        /// <returns></returns>
        List<FileUploadInfo> GetByAttachGUID(string attachmentGUID, PagerInfo pagerInfo);
                        
        /// <summary>
        /// 获取指定附件组GUID的附件信息
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <returns></returns>
        List<FileUploadInfo> GetByAttachGUID(string attachmentGUID);

        /// <summary>
        /// 根据文件的相对路径，删除文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        bool DeleteByFilePath(string relativeFilePath, string userId);

        /// <summary>
        /// 根据附件组GUID获取对应的文件名列表，方便列出文件名
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <returns>返回ID和文件名的列表</returns>
        Dictionary<string, string> GetFileNames(string attachmentGUID);

        /// <summary>
        /// 标记为删除（不直接删除)
        /// </summary>
        /// <param name="id">文件的ID</param>
        /// <returns></returns>
        bool SetDeleteFlag(string id);

    }
}