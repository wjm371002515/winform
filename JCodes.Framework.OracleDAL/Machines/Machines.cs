using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Databases;
using System.Text;

namespace JCodes.Framework.OracleDAL
{
    /// <summary>
    /// 服务器列表
    /// </summary>
    public class Machines : BaseDALOracle<MachinesInfo>, IMachines
    {
        #region 对象实例及构造函数

        public static Machines Instance
        {
            get
            {
                return new Machines();
            }
        }
        public Machines()
            : base(OraclePortal.gc._zszqTablePre + "Machines", "ID")
        {
        }

        #endregion

        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// </summary>
        /// <param name="dr">有效的DataReader对象</param>
        /// <returns>实体类对象</returns>
        protected override MachinesInfo DataReaderToEntity(IDataReader dataReader)
        {
            MachinesInfo info = new MachinesInfo();
            SmartDataReader reader = new SmartDataReader(dataReader);

            info.ID = reader.GetInt32("ID");
            info.GBRQ = reader.GetString("GBRQ");
            info.GLY = reader.GetString("GLY");
            info.GWFWDK = reader.GetString("GWFWDK");
            info.GWIP = reader.GetString("GWIP");
            info.JGWZ = reader.GetString("JGWZ");
            info.JQRT = reader.GetString("JQRT");
            info.JQXH = reader.GetString("JQXH");
            info.NWFWDK = reader.GetString("NWFWDK");
            info.NWIP = reader.GetString("NWIP");
            info.TABTYPE = reader.GetString("TABTYPE");
            info.WJLY = reader.GetString("WJLY");
            info.WWFWDK = reader.GetString("WWFWDK");
            info.WWIP = reader.GetString("WWIP");
            info.XTBB = reader.GetString("XTBB");
            info.YJXLH = reader.GetString("YJXLH");
            info.YYBB = reader.GetString("YYBB");
            info.ZG = reader.GetString("ZG");
            info.MODIFYMARK = reader.GetString("MODIFYMARK");
          
            return info;
        }

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(MachinesInfo obj)
        {
            MachinesInfo info = obj as MachinesInfo;
            Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
            hash.Add("GBRQ", info.GBRQ);
            hash.Add("GLY", info.GLY);
            hash.Add("GWFWDK", info.GWFWDK);
            hash.Add("GWIP", info.GWIP);
            hash.Add("JGWZ", info.JGWZ);
            hash.Add("JQRT", info.JQRT);
            hash.Add("JQXH", info.JQXH);
            hash.Add("NWFWDK", info.NWFWDK);
            hash.Add("NWIP", info.NWIP);
            hash.Add("TABTYPE", info.TABTYPE);
            hash.Add("WJLY", info.WJLY);
            hash.Add("WWFWDK", info.WWFWDK);
            hash.Add("WWIP", info.WWIP);
            hash.Add("XTBB", info.XTBB);
            hash.Add("YJXLH", info.YJXLH);
            hash.Add("YYBB", info.YYBB);
            hash.Add("ZG", info.ZG);
            hash.Add("MODIFYMARK", info.MODIFYMARK);

            return hash;
        }

        private AddressType Convert(string strAddressType)
        {
            AddressType addressType = AddressType.个人;
            try
            {
                addressType = (AddressType)Enum.Parse(typeof(AddressType), strAddressType);
            }
            catch
            {
            }
            return addressType;
        }

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetColumnNameAlias()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            #region 添加别名解析
            dict.Add("ID", "编号");
            dict.Add("GBRQ", "过保日期");
            dict.Add("GLY", "管理人");
            dict.Add("GWFWDK", "公网端口");
            dict.Add("GWIP", "公网IP");
            dict.Add("JGWZ", "机柜位置");
            dict.Add("JQRT", "机器用途");
            dict.Add("JQXH", "机器型号");
            dict.Add("NWFWDK", "内网开放端口");
            dict.Add("NWIP", "内网IP");
            dict.Add("TABTYPE", "表格类型");
            dict.Add("WJLY", "数据来源");
            dict.Add("WWFWDK", "外网开放端口");
            dict.Add("WWIP", "外网IP");
            dict.Add("XTBB", "系统版本");
            dict.Add("YJXLH", "硬件序列号");
            dict.Add("YYBB", "应用版本");
            dict.Add("ZG", "主管");
            dict.Add("MODIFYMARK", "修改备注");
            #endregion

            return dict;
        }

        public DataTable GetIntranet(string key)
        {
            string sql = string.Format("select nwip from (select distinct trim(nwip) as nwip from {0}machines t where trim(nwip) is not null and t.nwip like '%{1}%') where rownum <= 10 order by nwip", OraclePortal.gc._zszqTablePre, key);
            DataTable dt = base.GetDataTableBySql(sql);
            return dt;
        }

        public DataTable GetWWIP(string key)
        {
            string sql = string.Format("select wwip from (select distinct trim(wwip) as wwip from {0}machines t where trim(wwip) is not null and t.wwip like '%{1}%') where rownum <= 10 order by wwip", OraclePortal.gc._zszqTablePre, key);
            DataTable dt = base.GetDataTableBySql(sql);
            return dt;
        }

        public DataTable GetJQRT(string key)
        {
            string sql = string.Format("select jqrt from (select distinct trim(jqrt) as jqrt from {0}machines t where trim(jqrt) is not null and t.jqrt like '%{1}%') where rownum <= 10 order by jqrt", OraclePortal.gc._zszqTablePre, key);
            DataTable dt = base.GetDataTableBySql(sql);
            return dt;
        }

        public DataTable GetGLY(string key)
        {
            string sql = string.Format("select gly from (select distinct trim(gly) as gly from {0}machines t where trim(gly) is not null and t.gly like '%{1}%') where rownum <= 10 order by gly", OraclePortal.gc._zszqTablePre, key);
            DataTable dt = base.GetDataTableBySql(sql);
            return dt;
        }

        public DataTable GetZG(string key)
        {
            string sql = string.Format("select zg from (select distinct trim(zg) as zg from {0}machines t where trim(zg) is not null and t.zg like '%{1}%') where rownum <= 10 order by zg", OraclePortal.gc._zszqTablePre, key);
            DataTable dt = base.GetDataTableBySql(sql);
            return dt;
        }

        public List<MachinesInfo> GetMachines(string NWIP, string WWIP, string JQRT, string GLY, string ZG, PagerInfo info)
        {
            StringBuilder sb = new StringBuilder();
            string sql = string.Format("select ID,GBRQ,GLY,GWFWDK,GWIP,JGWZ,JQRT,JQXH,NWFWDK,NWIP,TABTYPE,WJLY,WWFWDK,WWIP,t2.value as XTBB,YJXLH,YYBB,ZG,MODIFYMARK from {0}MACHINES t, {1}DictData t2 where t.xtbb = t2.gid ", OraclePortal.gc._zszqTablePre, OraclePortal.gc._basicTablePre);
            sb.Append(sql);
            if (!string.IsNullOrEmpty(NWIP)) sb.Append(string.Format(" AND NWIP like '%{0}%'", NWIP));
            if (!string.IsNullOrEmpty(WWIP)) sb.Append(string.Format(" AND WWIP like '%{0}%'", WWIP));
            if (!string.IsNullOrEmpty(JQRT)) sb.Append(string.Format(" AND JQRT like '%{0}%'", JQRT));
            if (!string.IsNullOrEmpty(GLY)) sb.Append(string.Format(" AND GLY like '%{0}%'", GLY));
            if (!string.IsNullOrEmpty(ZG)) sb.Append(string.Format(" AND ZG like '%{0}%'", ZG));
            return base.GetListWithPager(sb.ToString(), info);
        }

        public Int32 GetMachinesCount(string NWIP, string WWIP, string JQRT, string GLY, string ZG)
        {
            StringBuilder sb = new StringBuilder();
            string sql = string.Format("select Count(t.ID) as RowCount from {0}MACHINES t, {1}DictData t2 where t.xtbb = t2.gid ", OraclePortal.gc._zszqTablePre, OraclePortal.gc._basicTablePre);
            sb.Append(sql);
            if (!string.IsNullOrEmpty(NWIP)) sb.Append(string.Format(" AND NWIP like '%{0}%'", NWIP));
            if (!string.IsNullOrEmpty(WWIP)) sb.Append(string.Format(" AND WWIP like '%{0}%'", WWIP));
            if (!string.IsNullOrEmpty(JQRT)) sb.Append(string.Format(" AND JQRT like '%{0}%'", JQRT));
            if (!string.IsNullOrEmpty(GLY)) sb.Append(string.Format(" AND GLY like '%{0}%'", GLY));
            if (!string.IsNullOrEmpty(ZG)) sb.Append(string.Format(" AND ZG like '%{0}%'", ZG));

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sb.ToString());

            return base.GetExecuteScalarValue(db, command);
        }

        public Int32 AddMachine(MachinesInfo machine)
        {
            string sql = string.Format("insert into {0}MACHINES(ID ,GBRQ ,GLY ,GWFWDK ,GWIP, JGWZ ,JQRT ,JQXH ,NWFWDK ,NWIP, TABTYPE, WJLY ,WWFWDK ,WWIP ,XTBB, YJXLH ,YYBB ,ZG ,MODIFYMARK) values(seq_MACHINES.nextval, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}','') ", OraclePortal.gc._zszqTablePre, machine.GBRQ, machine.GLY, machine.GWFWDK, machine.GWIP, machine.JGWZ, machine.JQRT, machine.JQXH, machine.NWFWDK, machine.NWIP, machine.TABTYPE, machine.WJLY, machine.WWFWDK, machine.WWIP, machine.XTBB, machine.YJXLH, machine.YYBB, machine.ZG);

            return base.SqlExecute(sql);
        }

        public Int32 ModMachine(MachinesInfo machine)
        {
            string sql = string.Format("update {0}MACHINES set GBRQ = '{1}',GLY = '{2}',GWFWDK = '{3}',GWIP = '{4}', JGWZ = '{5}',JQRT = '{6}',JQXH = '{7}',NWFWDK = '{8}',NWIP = '{9}', TABTYPE = '{10}', WJLY = '{11}',WWFWDK = '{12}',WWIP = '{13}',XTBB = '{14}', YJXLH = '{15}',YYBB = '{16}',ZG = '{17}',MODIFYMARK = '{18}' where ID = {19} ", OraclePortal.gc._zszqTablePre, machine.GBRQ, machine.GLY, machine.GWFWDK, machine.GWIP, machine.JGWZ, machine.JQRT, machine.JQXH, machine.NWFWDK, machine.NWIP, machine.TABTYPE, machine.WJLY, machine.WWFWDK, machine.WWIP, machine.XTBB, machine.YJXLH, machine.YYBB, machine.ZG, machine.MODIFYMARK, machine.ID);
            
            return base.SqlExecute(sql);
        }
    }
}