using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using System.Drawing;
using JCodes.Framework.IDAL;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.Other.Images;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Common.Format;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// �ϴ��ļ���Ϣ
    /// </summary>
	public class FileUpload : BaseBLL<FileUploadInfo>
    {
        private IFileUpload dal = null;

        public FileUpload() : base()
        {
            if (isMultiDatabase)
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, dicmultiDatabase[this.GetType().Name].ToString());
            }
            else
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            }

            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//�����Ҫ��¼������־����ʵ������¼�

            dal = baseDal as IFileUpload;
        }

        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="info">�ļ���Ϣ�����������ݣ�</param>
        /// <returns></returns>
        public ReturnResult Upload(FileUploadInfo info)
        {
            ReturnResult result = new ReturnResult();

            try
            {
                #region ȷ�����Ŀ¼��Ȼ���ϴ��ļ�

                string relativeSavePath = "";

                //����ϴ���ʱ�� ��ָ���˻���·������ô�Ͳ����޸�
                if (string.IsNullOrEmpty(info.BasePath))
                {
                    string AttachmentBasePath = config.AppConfigGet("AttachmentBasePath");//���õĻ���·��
                    if (string.IsNullOrEmpty(AttachmentBasePath))
                    {
                        //Ĭ���Ը�Ŀ¼�µ�UploadFilesĿ¼Ϊ�ϴ�Ŀ¼�� ����"C:\SPDTPatientMisService\UploadFiles";
                        info.BasePath = "UploadFiles";
                    }
                    else
                    {
                        info.BasePath = AttachmentBasePath;
                    }

                     //���ûָ������·��,�ͱ����ļ����ϴ�
                    relativeSavePath = UploadFile(info);
                }
                else
                {
                    //���ָ���˻���·������ô����Winform���س��������ӣ�����Ҫ�ļ��ϴ�,���·�������ļ���
                    relativeSavePath = info.Name;
                }                

                #endregion

                if (!string.IsNullOrEmpty(relativeSavePath))
                {
                    info.SavePath = relativeSavePath.Trim('\\');
                    info.LastUpdateTime = DateTimeHelper.GetServerDateTime2();

                    try
                    {
                        info.Gid = Guid.NewGuid().ToString();
                        info.IsDelete = (short)IsDelete.��;

                        bool success = base.Insert(info);
                        if (success)
                        {
                            result.ErrorCode = 0;
                        }
                        else
                        {
                            result.ErrorMessage = "����д�����ݿ������";
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FileUpload));
                        result.ErrorMessage = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FileUpload));
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// ��ȡ��һ���ļ����ݣ������ļ��ֽ����ݣ�
        /// </summary>
        /// <param name="id">������¼��ID</param>
        /// <returns></returns>
        public FileUploadInfo Download(string id)
        {
            FileUploadInfo info = FindById(id);
            if (info != null && !string.IsNullOrEmpty(info.SavePath))
            {
                string serverRealPath = Path.Combine(info.BasePath, info.SavePath.Trim('\\'));
                if (!Path.IsPathRooted(serverRealPath))
                {
                    //��������Ŀ¼�����ϵ�ǰ�����Ŀ¼���ܶ�λ�ļ���ַ
                    serverRealPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, serverRealPath);
                }

                if (File.Exists(serverRealPath))
                {
                    // ���⴦��
                    info.FileData = FileUtil.FileToBytes(serverRealPath);
                }
            }
            return info;
        }

        /// <summary>
        /// ��ȡ��һ���ļ����ݣ������ļ��ֽ����ݣ�
        /// </summary>
        /// <param name="id">������¼��ID</param>
        /// <returns></returns>
        public FileUploadInfo Download(string Id, int width, int height)
        {
            //����ͼƬ�����ߴ�
            width = width > 1024 ?  1024 : width;
            height = height > 768 ? 768 : height;

            FileUploadInfo info = FindById(Id);
            if (info != null && !string.IsNullOrEmpty(info.SavePath))
            {
                string serverRealPath = Path.Combine(info.BasePath, info.SavePath.Trim('\\'));
                if (!Path.IsPathRooted(serverRealPath))
                {
                    //��������Ŀ¼�����ϵ�ǰ�����Ŀ¼���ܶ�λ�ļ���ַ
                    serverRealPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, serverRealPath);
                }

                if (File.Exists(serverRealPath))
                {
                    byte[] bytes = FileUtil.FileToBytes(serverRealPath);
                    Image image = ImageHelper.BitmapFromBytes(bytes);
                    Image smallImage = ImageHelper.ChangeImageSize(image, width, height);
                    info.FileData = ImageHelper.ImageToBytes(smallImage);
                }
            }
            return info;
        }

        /// <summary>
        /// ��ȡָ���û����ϴ���Ϣ
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetAllByUserId(Int32 userId, bool isSuperAdmin, IsDelete isDelete = IsDelete.��)
        {
            return dal.GetAllByUserId(userId, isSuperAdmin, isDelete);
        }
               
        /// <summary>
        /// ��ȡָ���û����ϴ���Ϣ
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="category">�������ࣺ���˸�����ҵ�񸽼�</param>
        /// <param name="pagerInfo">��ҳ��Ϣ</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetAllByUserId(Int32 userId, AttachmentType attachmentType, PagerInfo pagerInfo)
        {
            return dal.GetAllByUserId(userId, attachmentType, pagerInfo);
        }

        /// <summary>
        /// ��ȡָ��������GUID�ĸ�����Ϣ
        /// </summary>
        /// <param name="attachmentGid">������GUID</param>
        /// <param name="pagerInfo">��ҳ��Ϣ</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByAttachGid(string attachmentGid, PagerInfo pagerInfo)
        {
            return dal.GetByAttachGid(attachmentGid, pagerInfo);
        }
                        
        /// <summary>
        /// ��ȡָ��������GUID�ĸ�����Ϣ
        /// </summary>
        /// <param name="attachmentGid">������GUID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByAttachGid(string attachmentGid)
        {
            return dal.GetByAttachGid(attachmentGid);
        }

        /// <summary>
        /// �����ļ������·����ɾ���ļ�
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        public bool DeleteByFilePath(string savePath, Int32 userId)
        {
            return dal.DeleteByFilePath(savePath, userId);
        }

        /// <summary>
        /// ����Owner��ȡ��Ӧ�ĸ����б�
        /// </summary>
        /// <param name="creatorId">ӵ����ID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByCreatorId(Int32 creatorId)
        {
            string condition = string.Format("CreatorId = {0} ", creatorId);
            return base.Find(condition);
        }

        /// <summary>
        /// ����Owner��ȡ��Ӧ�ĸ����б�
        /// </summary>
        /// <param name="creatorId">ӵ����ID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByCreatorId(Int32 creatorId, PagerInfo pagerInfo)
        {
            string condition = string.Format("CreatorId = {0} ", creatorId);
            return base.FindWithPager(condition, pagerInfo);
        }

        /// <summary>
        /// ����Owner��ȡ��Ӧ�ĸ����б�
        /// </summary>
        /// <param name="creatorId">ӵ����ID</param>
        /// <param name="attachmentGid">������GUID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByOwnerAndAttachGid(Int32 creatorId, string attachmentGid)
        {
            string condition = string.Format("CreatorId ={0} AND AttachmentGid='{1}' ", creatorId, attachmentGid);
            return base.Find(condition);
        }

        /// <summary>
        /// ����Owner��ȡ��Ӧ�ĸ����б�
        /// </summary>
        /// <param name="creatorId">ӵ����ID</param>
        /// <param name="attachmentGid">������GUID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByOwnerAndAttachGid(Int32 creatorId, string attachmentGid, PagerInfo pagerInfo)
        {
            string condition = string.Format("CreatorId ={0} AND attachmentGid='{1}' ", creatorId, attachmentGid);
            return base.FindWithPager(condition, pagerInfo);
        }

        /// <summary>
        /// ���ݸ�����GUID��ȡ��Ӧ���ļ����б��������г��ļ���
        /// </summary>
        /// <param name="attachmentGid">������GUID</param>
        /// <returns>����ID���ļ������б�</returns>
        public Dictionary<string, string> GetFileNames(string attachmentGid)
        {
            return dal.GetFileNames(attachmentGid);
        }

        /// <summary>
        /// ���Ϊɾ������ֱ��ɾ��)
        /// </summary>
        /// <param name="id">�ļ���ID</param>
        /// <returns></returns>
        public bool SetDeleteFlag(string Id)
        {
            return dal.SetDeleteFlag(Id);
        }

        /// <summary>
        /// ���ļ����浽ָ��Ŀ¼,��������Ի���Ŀ¼��·��
        /// </summary>
        /// <param name="info">�ļ��ϴ���Ϣ</param>
        /// <returns>�ɹ�������Ի���Ŀ¼��·�������򷵻ؿ��ַ�</returns>
        private string UploadFile(FileUploadInfo info)
        {
            //������뼰���·��
            string filePath = GetFilePath(info);
            string relativeSavePath = filePath.Replace(info.BasePath, "").Trim('\\');//�滻����ʼĿ¼��Ϊ���·��

            string serverRealPath = filePath;
            if (!Path.IsPathRooted(filePath))
            {
                serverRealPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, filePath);
            }

            //ͨ��ʵ���ļ���ȥ���Ҷ�Ӧ���ļ�����
            serverRealPath = GetRightFileName(serverRealPath, 1);

            //���ļ��Ѵ��ڣ�����������ʱ���޸�Filename��relativeSavePath
            relativeSavePath = relativeSavePath.Substring(0, relativeSavePath.LastIndexOf(info.Name)) +   FileUtil.GetFileName(serverRealPath);
            info.Name = FileUtil.GetFileName(serverRealPath);
            
            //����ʵ���ļ��������ļ�
            FileUtil.CreateFile(serverRealPath, info.FileData);

            bool success = FileUtil.IsExistFile(serverRealPath);
            if (success)
            {
                return relativeSavePath;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// ������뼰���·��
        /// </summary>
        /// <param name="info">�ϴ��ļ���Ϣ</param>
        /// <returns></returns>
        public string GetFilePath(FileUploadInfo info)
        {
            string fileName = info.Name;
            FileUploadType fileUploadType = (FileUploadType)info.FileUploadType;

            if (0 == (short)fileUploadType )
            {
                fileUploadType = FileUploadType.ͼƬ���;
            }

            //��������Ŀ¼����
            string uploadFolder = Path.Combine(info.BasePath, EnumHelper.GetMemberName<FileUploadType>(fileUploadType));
            string realFolderPath = uploadFolder;

            //���Ŀ¼Ϊ���Ŀ¼����ôת��Ϊʵ��Ŀ¼��������
            if (!Path.IsPathRooted(uploadFolder))
            {
                realFolderPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, uploadFolder);
            }
            if (!Directory.Exists(realFolderPath))
            {
                Directory.CreateDirectory(realFolderPath);
            }

            //�������Ŀ¼
            string filePath = Path.Combine(uploadFolder, fileName);
            return filePath;
        }

        /// <summary>
        /// ����attachmentGid�Ĳ�����ȡ��Ӧ�ĵ�һ���ļ�·��
        /// </summary>
        /// <param name="attachmentGid">������attachmentGid</param>
        /// <returns></returns>
        public string GetFirstFilePath(string attachmentGid)
        {
            string serverRealPath = "UploadFiles";
            if (!string.IsNullOrEmpty(attachmentGid))
            {
                List<FileUploadInfo> fileList = BLLFactory<FileUpload>.Instance.GetByAttachGid(attachmentGid);
                if (fileList != null && fileList.Count > 0)
                {
                    FileUploadInfo fileInfo = fileList[0];
                    if (fileInfo != null)
                    {
                        serverRealPath = Path.Combine(fileInfo.BasePath, fileInfo.SavePath.Trim('\\'));
                        if (!Path.IsPathRooted(serverRealPath))
                        {
                            //��������Ŀ¼�����ϵ�ǰ�����Ŀ¼���ܶ�λ�ļ���ַ
                            serverRealPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, serverRealPath);
                        }
                    }
                }
            }
            return serverRealPath;
        }

        /// <summary>
        /// �����ļ�����������������ļ��������(i)��i��1��ʼ����
        /// </summary>
        /// <param name="originalFileName">ԭ�ļ���</param>
        /// <param name="i">����ֵ</param>
        /// <returns></returns>
        private string GetRightFileName(string originalFilePath, int i)
        {
            bool fileExist = FileUtil.IsExistFile(originalFilePath);
            if (fileExist)
            {
                string onlyFileName = FileUtil.GetFileName(originalFilePath, true);
                int idx = originalFilePath.LastIndexOf(onlyFileName);
                string firstPath = originalFilePath.Substring(0, idx);
                string onlyExt = FileUtil.GetExtension(originalFilePath);
                string newFileName = string.Format("{0}{1}({2}){3}", firstPath, onlyFileName, i, onlyExt);
                if (FileUtil.IsExistFile(newFileName))
                {
                    i++;
                    return GetRightFileName(originalFilePath, i);
                }
                else
                {
                    return newFileName;
                }
            }
            else
            {
                return originalFilePath;
            }
        }

        /// <summary>
        /// ɾ��ָ����ID��¼����������Ŀ¼���ļ����Ƴ��ļ���DeletedFiles�ļ�������
        /// </summary>
        /// <param name="key">��¼ID</param>
        /// <returns></returns>
        public override bool DeleteByUser(object key, Int32 userId, DbTransaction trans = null)
        {
            //ɾ����¼ǰ����Ҫ���ļ��ƶ���ɾ��Ŀ¼����
            FileUploadInfo info = FindById(key, trans);
            if (info != null && !string.IsNullOrEmpty(info.SavePath))
            {
                string serverRealPath = Path.Combine(info.BasePath, info.SavePath.Trim('\\'));
                if (!Path.IsPathRooted(serverRealPath))
                {
                    //��������Ŀ¼�����ϵ�ǰ�����Ŀ¼���ܶ�λ�ļ���ַ
                    serverRealPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, serverRealPath);

                    //��������Ŀ¼�ģ��ƶ���ɾ��Ŀ¼����
                    if (File.Exists(serverRealPath))
                    {
                        try
                        {
                            string deletedPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Path.Combine(info.BasePath, "DeletedFiles"));
                            DirectoryUtil.AssertDirExist(deletedPath);

                            string newFilePath = Path.Combine(deletedPath, info.Name);
                            newFilePath = GetRightFileName(newFilePath, 1);
                            File.Move(serverRealPath, newFilePath);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FileUpload));
                        }
                    }
                }
            }

            return base.DeleteByUser(key, userId, trans);
        }

        /// <summary>
        /// ɾ��ָ��creatorId�����ݼ�¼
        /// </summary>
        /// <param name="owerID">�����ߵ�ID</param>
        /// <returns></returns>
        public bool DeleteByCreatorId(Int32 creatorId, Int32 userId)
        {
            string condition = string.Format("CreatorId ={0} ", creatorId);
            List<FileUploadInfo> list = base.Find(condition);
            foreach (FileUploadInfo info in list)
            {
                DeleteByUser(info.Gid, userId);
            }
            return true;
        }

        /// <summary>
        /// ɾ��ָ��attachmentGid�����ݼ�¼
        /// </summary>
        /// <param name="attachmentGid">�����ߵ�ID</param>
        /// <returns></returns>
        public bool DeleteByAttachGid(string attachmentGid, Int32 userId)
        {
            string condition = string.Format("attachmentGid ='{0}' ", attachmentGid);
            List<FileUploadInfo> list = base.Find(condition);
            foreach (FileUploadInfo info in list)
            {
                DeleteByUser(info.Gid, userId);
            }
            return true;
        }
    }
}