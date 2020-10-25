
namespace JCodes.Framework.jCodesenum
{

	/// <summary>
	/// 数据字典项: 100008
	/// 系统参数Id
	/// 1-系统参数,
	/// 99-其他
	/// </summary>
	public enum SysId
	{
		系统参数 = 1,

		其他 = 99,

	}

	/// <summary>
	/// 数据字典项: 100007
	/// 控件类型
	/// 1-文本框,
	/// 2-整数框,
	/// 3-下拉单选框,
	/// 4-下拉多选框,
	/// 5-勾选框,
	/// 6-日期,
	/// 7-密码,
	/// 8-小数框,
	/// 9-时间框
	/// </summary>
	public enum ControlType
	{
		文本框 = 1,

		整数框 = 2,

		下拉单选框 = 3,

		下拉多选框 = 4,

		勾选框 = 5,

		日期 = 6,

		密码 = 7,

		小数框 = 8,

		时间框 = 9,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否禁用
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsForbid
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100009
	/// 授权类型
	/// 1-黑名单,
	/// 2-白名单
	/// </summary>
	public enum AuthorizeType
	{
		黑名单 = 1,

		白名单 = 2,

	}

	/// <summary>
	/// 数据字典项: 200001
	/// 备件属类
	/// 1-国产标准件,
	/// 2-国产非标件,
	/// 3-进口标准件,
	/// 4-进口非标件
	/// </summary>
	public enum ItemBigtype
	{
		国产标准件 = 1,

		国产非标件 = 2,

		进口标准件 = 3,

		进口非标件 = 4,

	}

	/// <summary>
	/// 数据字典项: 200002
	/// 备件类别
	/// 1-日常备件,
	/// 2-大修件,
	/// 3-技改件,
	/// 4-修复件,
	/// 5-事故件,
	/// 6-油品,
	/// 7-连一铜板,
	/// 8-连二铜板,
	/// 9-连铸维修区,
	/// 10-胶管承包,
	/// 11-电机承包,
	/// 12-其他承包件
	/// </summary>
	public enum ItemType
	{
		日常备件 = 1,

		大修件 = 2,

		技改件 = 3,

		修复件 = 4,

		事故件 = 5,

		油品 = 6,

		连一铜板 = 7,

		连二铜板 = 8,

		连铸维修区 = 9,

		胶管承包 = 10,

		电机承包 = 11,

		其他承包件 = 12,

	}

	/// <summary>
	/// 数据字典项: 200003
	/// 备件单位
	/// 1-BA,
	/// 2-BAG,
	/// 3-BOT,
	/// 4-EA,
	/// 5-FU,
	/// 6-GE,
	/// 7-JIE,
	/// 8-KG,
	/// 9-KUA,
	/// 10-M1,
	/// 11-M2,
	/// 12-M3,
	/// 13-PA,
	/// 14-PC,
	/// 15-SHT,
	/// 16-SHU,
	/// 17-TAI,
	/// 18-TAO,
	/// 19-TIA,
	/// 20-TO,
	/// 21-ZHI,
	/// 22-ZI
	/// </summary>
	public enum ItemUnit
	{
		BA = 1,

		BAG = 2,

		BOT = 3,

		EA = 4,

		FU = 5,

		GE = 6,

		JIE = 7,

		KG = 8,

		KUA = 9,

		M1 = 10,

		M2 = 11,

		M3 = 12,

		PA = 13,

		PC = 14,

		SHT = 15,

		SHU = 16,

		TAI = 17,

		TAO = 18,

		TIA = 19,

		TO = 20,

		ZHI = 21,

		ZI = 22,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否可见
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsVisable
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否删除
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsDelete
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100010
	/// 操作类型
	/// 1-新增,
	/// 2-修改,
	/// 3-删除,
	/// 4-查询
	/// </summary>
	public enum OperationType
	{
		新增 = 1,

		修改 = 2,

		删除 = 3,

		查询 = 4,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否插入日志
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsInsertLog
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否更新日志
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsUpdateLog
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否删除日志
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsDeleteLog
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100012
	/// 机构分类
	/// 1-集团,
	/// 2-公司,
	/// 3-部门,
	/// 4-工作组
	/// </summary>
	public enum OuType
	{
		集团 = 1,

		公司 = 2,

		部门 = 3,

		工作组 = 4,

	}

	/// <summary>
	/// 数据字典项: 100011
	/// 角色分类
	/// 1-超级管理角色,
	/// 2-系统管理角色,
	/// 3-业务角色,
	/// 4-应用角色
	/// </summary>
	public enum RoleType
	{
		超级管理角色 = 1,

		系统管理角色 = 2,

		业务角色 = 3,

		应用角色 = 4,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否过期
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsExpire
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100014
	/// 性别
	/// 1-男,
	/// 2-女,
	/// 2-保密
	/// </summary>
	public enum Gender
	{
		男 = 1,

		女 = 2,

		保密 = 3,

	}

	/// <summary>
	/// 数据字典项: 100013
	/// 审核状态
	/// 1-未审核,
	/// 2-已审核,
	/// 3-审核中
	/// </summary>
	public enum AuditStatus
	{
		未审核 = 1,

		已审核 = 2,

		审核中 = 3,

	}

	/// <summary>
	/// 数据字典项: 100002
	/// 日志级别
	/// 1-LOG_LEVEL_EMERG,
	/// 2-LOG_LEVEL_ALERT,
	/// 3-LOG_LEVEL_CRIT,
	/// 4-LOG_LEVEL_ERR,
	/// 5-LOG_LEVEL_WARN,
	/// 6-LOG_LEVEL_NOTICE,
	/// 7-LOG_LEVEL_INFO,
	/// 8-LOG_LEVEL_DEBUG,
	/// 9-LOG_LEVEL_SQL
	/// </summary>
	public enum LogLevel
	{
		LOG_LEVEL_EMERG = 1,

		LOG_LEVEL_ALERT = 2,

		LOG_LEVEL_CRIT = 3,

		LOG_LEVEL_ERR = 4,

		LOG_LEVEL_WARN = 5,

		LOG_LEVEL_NOTICE = 6,

		LOG_LEVEL_INFO = 7,

		LOG_LEVEL_DEBUG = 8,

		LOG_LEVEL_SQL = 9,

	}

	/// <summary>
	/// 数据字典项: 100003
	/// 日志记录方式
	/// 1-记录到actionlog表中,
	/// 2-记录到日志文件中
	/// </summary>
	public enum LogInsertType
	{
		记录到actionlog表中 = 1,

		记录到日志文件中 = 2,

	}

	/// <summary>
	/// 数据字典项: 100004
	/// 用户状态
	/// 1-正常,
	/// 2-冻结,
	/// 3-注销,
	/// 4-删除
	/// </summary>
	public enum UserStatus
	{
		正常 = 1,

		冻结 = 2,

		注销 = 3,

		删除 = 4,

	}

	/// <summary>
	/// 数据字典项: 100005
	/// 字段类型
	/// 1-NUM,
	/// 2-STRING,
	/// 3-TEXTAREA,
	/// 4-DATE,
	/// 5-DATETIME,
	/// 6-BOOL,
	/// 7-SELECT,
	/// 8-RADIO,
	/// 9-CHECKBOX,
	/// 10-EDITOR,
	/// 11-PICTURE,
	/// 12-FILE
	/// </summary>
	public enum FieldType
	{
		NUM = 1,

		STRING = 2,

		TEXTAREA = 3,

		DATE = 4,

		DATETIME = 5,

		BOOL = 6,

		SELECT = 7,

		RADIO = 8,

		CHECKBOX = 9,

		EDITOR = 10,

		PICTURE = 11,

		FILE = 12,

	}

	/// <summary>
	/// 数据字典项: 100006
	/// 处理状态
	/// 1-未处理,
	/// 2-待处理,
	/// 3-正在处理,
	/// 4-已处理
	/// </summary>
	public enum DealStatus
	{
		未处理 = 1,

		待处理 = 2,

		正在处理 = 3,

		已处理 = 4,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否继承IDXDataError
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsIDXDataError
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否继承BaseEntity
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsBaseEntity
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否展开
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsExpand
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否勾选
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsCheck
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100015
	/// 通讯录类型
	/// 1-个人,
	/// 2-公司,
	/// 99-其他
	/// </summary>
	public enum AddressType
	{
		个人 = 1,

		公司 = 2,

		其他 = 99,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 允许空
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsNull
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100016
	/// 约束类型
	/// 1-主键,
	/// 2-索引,
	/// 3-唯一索引
	/// </summary>
	public enum ConstraintType
	{
		主键 = 1,

		索引 = 2,

		唯一索引 = 3,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否基础数据
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsBasicData
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100017
	/// 登陆状态
	/// 1-验证通过,
	/// 2-匹配失败,
	/// 3-禁止登陆,
	/// 4-用户不存在
	/// </summary>
	public enum LoginStatus
	{
		验证通过 = 1,

		匹配失败 = 2,

		禁止登陆 = 3,

		用户不存在 = 4,

	}

	/// <summary>
	/// 数据字典项: 200004
	/// 采购进货退货方式
	/// 1-进货,
	/// 2-退货
	/// </summary>
	public enum PuchaseStatus
	{
		进货 = 1,

		退货 = 2,

	}

	/// <summary>
	/// 数据字典项: 200005
	/// 收入支出类型
	/// 1-收入,
	/// 2-支出
	/// </summary>
	public enum IncomeType
	{
		收入 = 1,

		支出 = 2,

	}

	/// <summary>
	/// 数据字典项: 200006
	/// 报表类型
	/// 1-库房部门结存,
	/// 2-库房结存,
	/// 3-所有库房结存,
	/// 4-车间成本月报表,
	/// 100-全年费用汇总表
	/// </summary>
	public enum MonthlyReportType
	{
		库房部门结存 = 1,

		库房结存 = 2,

		所有库房结存 = 3,

		车间成本月报表 = 4,

		全年费用汇总表 = 100,

	}

	/// <summary>
	/// 数据字典项: 100018
	/// 生肖
	/// 1-鼠,
	/// 2-牛,
	/// 3-虎,
	/// 4-兔,
	/// 5-龙,
	/// 6-蛇,
	/// 7-马,
	/// 8-羊,
	/// 9-猴,
	/// 10-鸡,
	/// 11-狗,
	/// 12-猪
	/// </summary>
	public enum ChineseZodiac
	{
		鼠 = 1,

		牛 = 2,

		虎 = 3,

		兔 = 4,

		龙 = 5,

		蛇 = 6,

		马 = 7,

		羊 = 8,

		猴 = 9,

		鸡 = 10,

		狗 = 11,

		猪 = 12,

	}

	/// <summary>
	/// 数据字典项: 100019
	/// 星座
	/// 1-白羊座,
	/// 2-金牛座,
	/// 3-双子座,
	/// 4-巨蟹座,
	/// 5-狮子座,
	/// 6-处女座,
	/// 7-天秤座,
	/// 8-天蝎座,
	/// 9-射手座,
	/// 10-魔羯座,
	/// 11-水瓶座,
	/// 12-双鱼座
	/// </summary>
	public enum Constellation
	{
		白羊座 = 1,

		金牛座 = 2,

		双子座 = 3,

		巨蟹座 = 4,

		狮子座 = 5,

		处女座 = 6,

		天秤座 = 7,

		天蝎座 = 8,

		射手座 = 9,

		魔羯座 = 10,

		水瓶座 = 11,

		双鱼座 = 12,

	}

	/// <summary>
	/// 数据字典项: 100020
	/// 婚姻状态
	/// 1-未婚,
	/// 2-已婚,
	/// 3-丧偶,
	/// 4-离婚
	/// </summary>
	public enum MarriageStatus
	{
		未婚 = 1,

		已婚 = 2,

		丧偶 = 3,

		离婚 = 4,

	}

	/// <summary>
	/// 数据字典项: 100021
	/// 健康状态
	/// 1-健康,
	/// 2-亚健康,
	/// 3-疾病
	/// </summary>
	public enum HealthStatus
	{
		健康 = 1,

		亚健康 = 2,

		疾病 = 3,

	}

	/// <summary>
	/// 数据字典项: 100022
	/// 重要级别
	/// 1-重要紧急,
	/// 2-重要不紧急,
	/// 3-不重要紧急,
	/// 4-不重要不紧急
	/// </summary>
	public enum ImportanceLevel
	{
		重要紧急 = 1,

		重要不紧急 = 2,

		不重要紧急 = 3,

		不重要不紧急 = 4,

	}

	/// <summary>
	/// 数据字典项: 100023
	/// 认可程度级别
	/// 1-极为认可,
	/// 2-较认可,
	/// 3-一般,
	/// 4-较不认可,
	/// 5-非常不认可
	/// </summary>
	public enum RecognitionLevel
	{
		极为认可 = 1,

		较认可 = 2,

		一般 = 3,

		较不认可 = 4,

		非常不认可 = 5,

	}

	/// <summary>
	/// 数据字典项: 100024
	/// 体型
	/// 1-体型纤细,
	/// 2-不胖不瘦,
	/// 3-体型丰满,
	/// 4-体型超重
	/// </summary>
	public enum BodyType
	{
		体型纤细 = 1,

		不胖不瘦 = 2,

		体型丰满 = 3,

		体型超重 = 4,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否吸烟
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsSmoking
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否喝酒
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsDrink
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100025
	/// 客户类别
	/// 1-潜在客户,
	/// 2-正式客户,
	/// 3-合作伙伴,
	/// 4-流动商
	/// </summary>
	public enum CustomerType
	{
		潜在客户 = 1,

		正式客户 = 2,

		合作伙伴 = 3,

		流动商 = 4,

	}

	/// <summary>
	/// 数据字典项: 100026
	/// 客户级别
	/// 1-普通客户,
	/// 2-VIP客户,
	/// 3-高级VIP会员
	/// </summary>
	public enum CustomerLevel
	{
		普通客户 = 1,

		VIP客户 = 2,

		高级VIP会员 = 3,

	}

	/// <summary>
	/// 数据字典项: 100027
	/// 信用等级
	/// 1-优秀,
	/// 2-良好,
	/// 3-一般,
	/// 4-差
	/// </summary>
	public enum CreditStatus
	{
		优秀 = 1,

		良好 = 2,

		一般 = 3,

		差 = 4,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否公开
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsPublic
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100028
	/// 客户满意度级别
	/// 1-非常重要,
	/// 2-较为重要,
	/// 3-一般,
	/// 4-不重要
	/// </summary>
	public enum SatisfactionLevel
	{
		非常重要 = 1,

		较为重要 = 2,

		一般 = 3,

		不重要 = 4,

	}

	/// <summary>
	/// 数据字典项: 100029
	/// JsTree数据树状态
	/// 1-展开,
	/// 2-收回
	/// </summary>
	public enum JsTreeDataStatus
	{
		展开 = 1,

		收回 = 2,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否打开
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsOpened
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否选中
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsSelected
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否可用
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsDisabled
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100030
	/// Icon来源
	/// 1-Glyphicons,
	/// 2-FontAwesome,
	/// 3-SimpleLine
	/// </summary>
	public enum IconSourceType
	{
		Glyphicons = 1,

		FontAwesome = 2,

		SimpleLine = 3,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否强制过期
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsForceExpire
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100001
	/// 是否审批通过
	/// 1-是,
	/// 2-否
	/// </summary>
	public enum IsApprove
	{
		是 = 1,

		否 = 2,

	}

	/// <summary>
	/// 数据字典项: 100031
	/// 信息大类名称
	/// 1-通知公告,
	/// 2-行业动态,
	/// 3-政策法规
	/// </summary>
	public enum InformationCategory
	{
		通知公告 = 1,

		行业动态 = 2,

		政策法规 = 3,

	}

	/// <summary>
	/// 数据字典项: 100032
	/// 信息子类
	/// 1-法律法规,
	/// 99-其他
	/// </summary>
	public enum InformationSubType
	{
		法律法规 = 1,

		其他 = 99,

	}

	/// <summary>
	/// 数据字典项: 200007
	/// 统计值类型
	/// 1-本月结存数量,
	/// 2-本月入库数量,
	/// 3-本月出库数量,
	/// 4-上月结存数量,
	/// 5-本月入库金额,
	/// 6-本月结存金额,
	/// 7-本月出库金额,
	/// 8-上月结存金额
	/// </summary>
	public enum StatisticValueType
	{
		本月结存数量 = 1,

		本月入库数量 = 2,

		本月出库数量 = 3,

		上月结存数量 = 4,

		本月入库金额 = 5,

		本月结存金额 = 6,

		本月出库金额 = 7,

		上月结存金额 = 8,

	}

	/// <summary>
	/// 数据字典项: 200008
	/// 个人图片分类
	/// 1-个人肖像,
	/// 2-身份证照片1,
	/// 3-身份证照片2,
	/// 4-名片1,
	/// 5-名片2
	/// </summary>
	public enum UserImageType
	{
		个人肖像 = 1,

		身份证照片1 = 2,

		身份证照片2 = 3,

		名片1 = 4,

		名片2 = 5,

	}

	/// <summary>
	/// 数据字典项: 100033
	/// 附件分类
	/// 1-个人附件,
	/// 2-业务附件
	/// </summary>
	public enum AttachmentType
	{
		个人附件 = 1,

		业务附件 = 2,

	}

	/// <summary>
	/// 数据字典项: 100034
	/// 上传文件分类
	/// 1-动态信息,
	/// 2-多媒体文件,
	/// 3-数据导入文件,
	/// 4-图片相册,
	/// 5-行业动态,
	/// 6-政策法规
	/// </summary>
	public enum FileUploadType
	{
		动态信息 = 1,

		多媒体文件 = 2,

		数据导入文件 = 3,

		图片相册 = 4,

		行业动态 = 5,

		政策法规 = 6,

	}

    /// <summary>
    /// 数据字典项: 300005
    /// 启用状态
    /// 1-启用,
    /// 2-待启用,
    /// 3-作废
    /// </summary>
    public enum EnableStatus
    {
        启用 = 1,

        待启用 = 2,

        作废 = 3,

    }

}
