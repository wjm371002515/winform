/*==============================================================*/
/* Table: TB_CITY                                               */
/*==============================================================*/
create table TB_CITY  (
   ID                   number                          not null,
   CITYNAME             NVARCHAR2(50),
   ZIPCODE              NVARCHAR2(50),
   PROVINCEID           number,
   constraint PK_TB_CITY primary key (ID)
);

comment on table TB_CITY is
'全国城市表';

comment on column TB_CITY.CITYNAME is
'城市名称';

comment on column TB_CITY.ZIPCODE is
'邮政编码';

comment on column TB_CITY.PROVINCEID is
'省份ID';

/*==============================================================*/
/* Table: TB_DICTDATA                                           */
/*==============================================================*/
create table TB_DICTDATA  (
   ID                   NVARCHAR2(50)                   not null,
   DICTTYPE_ID          NVARCHAR2(50),
   NAME                 NVARCHAR2(50),
   VALUE                NVARCHAR2(50),
   REMARK               NVARCHAR2(255),
   SEQ                  NVARCHAR2(50),
   EDITOR               NVARCHAR2(50),
   LASTUPDATED          DATE                           default sysdate,
   constraint PK_TB_DICTDATA primary key (ID)
);

comment on table TB_DICTDATA is
'通用字典明细项目信息';

comment on column TB_DICTDATA.ID is
'编号';

comment on column TB_DICTDATA.DICTTYPE_ID is
'字典大类';

comment on column TB_DICTDATA.NAME is
'字典名称';

comment on column TB_DICTDATA.VALUE is
'字典值';

comment on column TB_DICTDATA.REMARK is
'备注';

comment on column TB_DICTDATA.SEQ is
'排序';

comment on column TB_DICTDATA.EDITOR is
'编辑者';

comment on column TB_DICTDATA.LASTUPDATED is
'编辑时间';


/*==============================================================*/
/* Table: TB_DICTTYPE                                           */
/*==============================================================*/
create table TB_DICTTYPE  (
   ID                   NVARCHAR2(50)                   not null,
   NAME                 NVARCHAR2(50),
   REMARK               NVARCHAR2(255),
   SEQ                  NVARCHAR2(50),
   EDITOR               NVARCHAR2(50),
   LASTUPDATED          DATE                           default sysdate,
   PID                  NVARCHAR2(50),
   constraint "PK_TB_DictType" primary key (ID)
);

comment on table TB_DICTTYPE is
'通用字典大类信息';

comment on column TB_DICTTYPE.NAME is
'类型名称';

comment on column TB_DICTTYPE.REMARK is
'备注';

comment on column TB_DICTTYPE.SEQ is
'排序';

comment on column TB_DICTTYPE.EDITOR is
'编辑者';

comment on column TB_DICTTYPE.LASTUPDATED is
'编辑时间';

comment on column TB_DICTTYPE.PID is
'父ID';

/*==============================================================*/
/* Table: TB_USERPARAMETER                                      */
/*==============================================================*/
create table TB_USERPARAMETER  (
   ID                   NVARCHAR2(50)                   not null,
   NAME                 NVARCHAR2(255),
   CONTENT              CLOB,
   CREATOR              NVARCHAR2(50),
   CREATETIME           DATE                           default sysdate,
   constraint PK_TB_USERPARAMETER primary key (ID)
);

comment on table TB_USERPARAMETER is
'用户参数配置';

comment on column TB_USERPARAMETER.NAME is
'类型名称';

comment on column TB_USERPARAMETER.CONTENT is
'参数文本内容';

comment on column TB_USERPARAMETER.CREATOR is
'创建人';

comment on column TB_USERPARAMETER.CREATETIME is
'创建时间';


/*==============================================================*/
/* Table: TB_DISTRICT                                           */
/*==============================================================*/
create table TB_DISTRICT  (
   ID                   number                          not null,
   DISTRICTNAME         NVARCHAR2(50),
   CITYID               number,
   constraint PK_TB_DISTRICT primary key (ID)
);

comment on table TB_DISTRICT is
'城市行政区划';

comment on column TB_DISTRICT.DISTRICTNAME is
'行政区划';

comment on column TB_DISTRICT.CITYID is
'城市ID';

/*==============================================================*/
/* Table: TB_FILEUPLOAD                                         */
/*==============================================================*/
create table TB_FILEUPLOAD  (
   ID                   NVARCHAR2(50)                   not null,
   OWNER_ID             NVARCHAR2(50),
   ATTACHMENTGUID       NVARCHAR2(50),
   FILENAME             NVARCHAR2(255),
   BASEPATH             NVARCHAR2(255),
   SAVEPATH             NVARCHAR2(255),
   CATEGORY             NVARCHAR2(255),
   FILESIZE             INTEGER,
   FILEEXTEND           NVARCHAR2(10),
   EDITOR               NVARCHAR2(50),
   ADDTIME              DATE                           default sysdate,
   DELETEFLAG           INTEGER                        default 0,
   constraint PK_TB_FILEUPLOAD primary key (ID)
);

comment on table TB_FILEUPLOAD is
'上传附件信息';

comment on column TB_FILEUPLOAD.OWNER_ID is
'附件组所属的记录ID';

comment on column TB_FILEUPLOAD.ATTACHMENTGUID is
'附件组GUID';

comment on column TB_FILEUPLOAD.FILENAME is
'文件名';

comment on column TB_FILEUPLOAD.BASEPATH is
'基础路径';

comment on column TB_FILEUPLOAD.SAVEPATH is
'文件保存相对路径';

comment on column TB_FILEUPLOAD.CATEGORY is
'文件分类';

comment on column TB_FILEUPLOAD.FILESIZE is
'文件大小';

comment on column TB_FILEUPLOAD.FILEEXTEND is
'文件扩展名';

comment on column TB_FILEUPLOAD.EDITOR is
'所属用户';

comment on column TB_FILEUPLOAD.ADDTIME is
'添加时间';

comment on column TB_FILEUPLOAD.DELETEFLAG is
'删除标志，1为删除，0为正常';

/*==============================================================*/
/* Table: TB_PROVINCE                                           */
/*==============================================================*/
create table TB_PROVINCE  (
   ID                   number                          not null,
   PROVINCENAME         NVARCHAR2(50),
   constraint PK_TB_PROVINCE primary key (ID)
);

comment on table TB_PROVINCE is
'全国省份表';

comment on column TB_PROVINCE.PROVINCENAME is
'省份名称';

/*==============================================================*/
/* Table: T_ACL_BLACKIP                                         */
/*==============================================================*/
create table T_ACL_BLACKIP  (
   ID                   NVARCHAR2(50)                   not null,
   NAME                 NVARCHAR2(250),
   AUTHORIZETYPE        INTEGER                        default 0,
   FORBID               INTEGER                        default 0,
   IPSTART              NVARCHAR2(100),
   IPEND                NVARCHAR2(100),
   NOTE                 CLOB,
   CREATOR              NVARCHAR2(50),
   CREATOR_ID           NVARCHAR2(50),
   CREATETIME           DATE                           default sysdate,
   EDITOR               NVARCHAR2(50),
   EDITOR_ID            NVARCHAR2(50),
   EDITTIME             DATE                           default sysdate,
   constraint PK_T_ACL_BLACKIP primary key (ID)
);

comment on table T_ACL_BLACKIP is
'登陆系统的黑白名单列表';

comment on column T_ACL_BLACKIP.NAME is
'显示名称';

comment on column T_ACL_BLACKIP.AUTHORIZETYPE is
'授权类型';

comment on column T_ACL_BLACKIP.FORBID is
'是否禁用';

comment on column T_ACL_BLACKIP.IPSTART is
'IP起始地址';

comment on column T_ACL_BLACKIP.IPEND is
'IP结束地址';

comment on column T_ACL_BLACKIP.NOTE is
'备注';

comment on column T_ACL_BLACKIP.CREATOR is
'创建人';

comment on column T_ACL_BLACKIP.CREATOR_ID is
'创建人ID';

comment on column T_ACL_BLACKIP.CREATETIME is
'创建时间';

comment on column T_ACL_BLACKIP.EDITOR is
'编辑人';

comment on column T_ACL_BLACKIP.EDITOR_ID is
'编辑人ID';

comment on column T_ACL_BLACKIP.EDITTIME is
'编辑时间';

/*==============================================================*/
/* Table: T_ACL_BLACKIP_USER                                    */
/*==============================================================*/
create table T_ACL_BLACKIP_USER  (
   BLACKIP_ID           NVARCHAR2(50)                   not null,
   USER_ID              NUMBER(6)                       not null,
   constraint PK_T_ACL_BLACKIP_USER primary key (BLACKIP_ID, USER_ID)
);

comment on table T_ACL_BLACKIP_USER is
'系统黑白名单与用户关系表';

comment on column T_ACL_BLACKIP_USER.BLACKIP_ID is
'黑白名单ID';

comment on column T_ACL_BLACKIP_USER.USER_ID is
'用户ID';

/*==============================================================*/
/* Table: T_ACL_FUNCTION                                        */
/*==============================================================*/
create table T_ACL_FUNCTION  (
   ID                   NVARCHAR2(50)                   not null,
   PID                  NVARCHAR2(50)                  default '-1',
   NAME                 NVARCHAR2(100)                  not null,
   CONTROLID            NVARCHAR2(100)                  not null,
   SYSTEMTYPE_ID        NVARCHAR2(50)                   not null,
   SORTCODE             NVARCHAR2(50),
   constraint "PK_Function" primary key (ID)
);

comment on table T_ACL_FUNCTION is
'系统功能定义';

comment on column T_ACL_FUNCTION.PID is
'父ID';

comment on column T_ACL_FUNCTION.NAME is
'功能名称';

comment on column T_ACL_FUNCTION.CONTROLID is
'控制标识';

comment on column T_ACL_FUNCTION.SYSTEMTYPE_ID is
'系统编号';

comment on column T_ACL_FUNCTION.SORTCODE is
'排序码';

/*==============================================================*/
/* Table: T_ACL_LOGINLOG                                        */
/*==============================================================*/
create table T_ACL_LOGINLOG  (
   ID                   NUMBER(6)                       not null,
   USER_ID              NVARCHAR2(50),
   LOGINNAME            NVARCHAR2(50),
   FULLNAME             NVARCHAR2(50),
   COMPANY_ID           NVARCHAR2(50),
   COMPANYNAME          NVARCHAR2(255),
   NOTE                 NVARCHAR2(255),
   IPADDRESS            NVARCHAR2(255),
   MACADDRESS           NVARCHAR2(255),
   LASTUPDATED          DATE                           default sysdate,
   SYSTEMTYPE_ID        NVARCHAR2(50),
   constraint "PK_TB_LoginLog" primary key (ID)
);

comment on table T_ACL_LOGINLOG is
'用户登录日志信息';

comment on column T_ACL_LOGINLOG.USER_ID is
'登录用户ID';

comment on column T_ACL_LOGINLOG.LOGINNAME is
'登录名';

comment on column T_ACL_LOGINLOG.FULLNAME is
'真实名称';

comment on column T_ACL_LOGINLOG.COMPANY_ID is
'所属公司ID';

comment on column T_ACL_LOGINLOG.COMPANYNAME is
'所属公司名称';

comment on column T_ACL_LOGINLOG.NOTE is
'日志描述';

comment on column T_ACL_LOGINLOG.IPADDRESS is
'IP地址';

comment on column T_ACL_LOGINLOG.MACADDRESS is
'Mac地址';

comment on column T_ACL_LOGINLOG.LASTUPDATED is
'更新时间';

comment on column T_ACL_LOGINLOG.SYSTEMTYPE_ID is
'系统编号';

/*==============================================================*/
/* Table: T_ACL_MENU                                            */
/*==============================================================*/
create table T_ACL_MENU  (
   ID                   NVARCHAR2(50)                   not null,
   PID                  NVARCHAR2(50)                  default '-1',
   NAME                 NVARCHAR2(50),
   ICON                 NVARCHAR2(50),
   SEQ                  NVARCHAR2(50),
   FUNCTIONID           NVARCHAR2(50),
   VISIBLE              INTEGER                        default 1,
   WINFORMTYPE          NVARCHAR2(100),
   URL                  NVARCHAR2(100),
   WEBICON              NVARCHAR2(100),
   SYSTEMTYPE_ID        NVARCHAR2(50)                   not null,
   CREATOR              NVARCHAR2(50),
   CREATOR_ID           NVARCHAR2(50),
   CREATETIME           DATE                           default sysdate,
   EDITOR               NVARCHAR2(50),
   EDITOR_ID            NVARCHAR2(50),
   EDITTIME             DATE                           default sysdate,
   DELETED              INTEGER,
   constraint PK_T_ACL_MENU primary key (ID)
);

comment on table T_ACL_MENU is
'功能菜单';

comment on column T_ACL_MENU.PID is
'父ID';

comment on column T_ACL_MENU.NAME is
'显示名称';

comment on column T_ACL_MENU.ICON is
'图标';

comment on column T_ACL_MENU.SEQ is
'排序';

comment on column T_ACL_MENU.FUNCTIONID is
'功能ID';

comment on column T_ACL_MENU.VISIBLE is
'是否可见';

comment on column T_ACL_MENU.WINFORMTYPE is
'Winform窗体类型';

comment on column T_ACL_MENU.URL is
'Web界面Url地址';

comment on column T_ACL_MENU.WEBICON is
'Web界面的菜单图标';

comment on column T_ACL_MENU.SYSTEMTYPE_ID is
'系统编号';

comment on column T_ACL_MENU.CREATOR is
'创建人';

comment on column T_ACL_MENU.CREATOR_ID is
'创建人ID';

comment on column T_ACL_MENU.CREATETIME is
'创建时间';

comment on column T_ACL_MENU.EDITOR is
'编辑人';

comment on column T_ACL_MENU.EDITOR_ID is
'编辑人ID';

comment on column T_ACL_MENU.EDITTIME is
'编辑时间';

comment on column T_ACL_MENU.DELETED is
'是否已删除';

/*==============================================================*/
/* Table: T_ACL_OPERATIONLOG                                    */
/*==============================================================*/
create table T_ACL_OPERATIONLOG  (
   ID                   NVARCHAR2(50)                   not null,
   USER_ID              NVARCHAR2(50),
   LOGINNAME            NVARCHAR2(50),
   FULLNAME             NVARCHAR2(50),
   COMPANY_ID           NVARCHAR2(50),
   COMPANYNAME          NVARCHAR2(255),
   TABLENAME            NVARCHAR2(50),
   OPERATIONTYPE        NVARCHAR2(50),
   NOTE                 CLOB,
   IPADDRESS            NVARCHAR2(255),
   MACADDRESS           NVARCHAR2(255),
   CREATETIME           DATE                           default sysdate,
   constraint PK_T_ACL_OPERATIONLOG primary key (ID)
);

comment on table T_ACL_OPERATIONLOG is
'用户关键操作记录';

comment on column T_ACL_OPERATIONLOG.USER_ID is
'登录用户ID';

comment on column T_ACL_OPERATIONLOG.LOGINNAME is
'登录名';

comment on column T_ACL_OPERATIONLOG.FULLNAME is
'真实名称';

comment on column T_ACL_OPERATIONLOG.COMPANY_ID is
'所属公司ID';

comment on column T_ACL_OPERATIONLOG.COMPANYNAME is
'所属公司名称';

comment on column T_ACL_OPERATIONLOG.TABLENAME is
'操作表名称';

comment on column T_ACL_OPERATIONLOG.OPERATIONTYPE is
'操作类型';

comment on column T_ACL_OPERATIONLOG.NOTE is
'日志描述';

comment on column T_ACL_OPERATIONLOG.IPADDRESS is
'IP地址';

comment on column T_ACL_OPERATIONLOG.MACADDRESS is
'Mac地址';

comment on column T_ACL_OPERATIONLOG.CREATETIME is
'创建时间';

/*==============================================================*/
/* Table: T_ACL_OPERATIONLOGSETTING                             */
/*==============================================================*/
create table T_ACL_OPERATIONLOGSETTING  (
   ID                   NVARCHAR2(50)                   not null,
   FORBID               INTEGER                        default 0,
   TABLENAME            NVARCHAR2(50),
   INSERTLOG            INTEGER,
   DELETELOG            INTEGER,
   UPDATELOG            INTEGER,
   NOTE                 CLOB,
   CREATOR              NVARCHAR2(50),
   CREATOR_ID           NVARCHAR2(50),
   CREATETIME           DATE                           default sysdate,
   EDITOR               NVARCHAR2(50),
   EDITOR_ID            NVARCHAR2(50),
   EDITTIME             DATE                           default sysdate,
   constraint PK_T_ACL_OPERATIONLOGSETTING primary key (ID)
);

comment on table T_ACL_OPERATIONLOGSETTING is
'记录操作日志的数据表配置';

comment on column T_ACL_OPERATIONLOGSETTING.FORBID is
'是否禁用';

comment on column T_ACL_OPERATIONLOGSETTING.TABLENAME is
'数据库表';

comment on column T_ACL_OPERATIONLOGSETTING.INSERTLOG is
'记录插入日志';

comment on column T_ACL_OPERATIONLOGSETTING.DELETELOG is
'记录删除日志';

comment on column T_ACL_OPERATIONLOGSETTING.UPDATELOG is
'记录更新日志';

comment on column T_ACL_OPERATIONLOGSETTING.NOTE is
'备注';

comment on column T_ACL_OPERATIONLOGSETTING.CREATOR is
'创建人';

comment on column T_ACL_OPERATIONLOGSETTING.CREATOR_ID is
'创建人ID';

comment on column T_ACL_OPERATIONLOGSETTING.CREATETIME is
'创建时间';

comment on column T_ACL_OPERATIONLOGSETTING.EDITOR is
'编辑人';

comment on column T_ACL_OPERATIONLOGSETTING.EDITOR_ID is
'编辑人ID';

comment on column T_ACL_OPERATIONLOGSETTING.EDITTIME is
'编辑时间';

/*==============================================================*/
/* Table: T_ACL_OU                                              */
/*==============================================================*/
create table T_ACL_OU  (
   ID                   NUMBER(6)                       not null,
   PID                  NUMBER(6)                      default -1,
   HANDNO               NVARCHAR2(50),
   NAME                 NVARCHAR2(100)                  not null,
   SORTCODE             NVARCHAR2(50),
   CATEGORY             NVARCHAR2(50),
   ADDRESS              NVARCHAR2(200),
   OUTERPHONE           NVARCHAR2(50),
   INNERPHONE           NVARCHAR2(50),
   NOTE                 CLOB,
   CREATOR              NVARCHAR2(50),
   CREATOR_ID           NVARCHAR2(50),
   CREATETIME           DATE                           default sysdate,
   EDITOR               NVARCHAR2(50),
   EDITOR_ID            NVARCHAR2(50),
   EDITTIME             DATE                           default sysdate,
   DELETED              NUMBER,
   ENABLED              NUMBER,
   COMPANY_ID           NVARCHAR2(50),
   COMPANYNAME          NVARCHAR2(255),
   constraint "PK_Department" primary key (ID)
);

comment on table T_ACL_OU is
'机构（部门）信息';

comment on column T_ACL_OU.PID is
'父ID';

comment on column T_ACL_OU.HANDNO is
'机构编码';

comment on column T_ACL_OU.NAME is
'机构名称';

comment on column T_ACL_OU.SORTCODE is
'排序码';

comment on column T_ACL_OU.CATEGORY is
'机构分类';

comment on column T_ACL_OU.ADDRESS is
'机构地址';

comment on column T_ACL_OU.OUTERPHONE is
'外线电话';

comment on column T_ACL_OU.INNERPHONE is
'内线电话';

comment on column T_ACL_OU.NOTE is
'备注';

comment on column T_ACL_OU.CREATOR is
'创建人';

comment on column T_ACL_OU.CREATOR_ID is
'创建人ID';

comment on column T_ACL_OU.CREATETIME is
'创建时间';

comment on column T_ACL_OU.EDITOR is
'编辑人';

comment on column T_ACL_OU.EDITOR_ID is
'编辑人ID';

comment on column T_ACL_OU.EDITTIME is
'编辑时间';

comment on column T_ACL_OU.DELETED is
'是否已删除';

comment on column T_ACL_OU.ENABLED is
'有效标志';

comment on column T_ACL_OU.COMPANY_ID is
'所属公司ID';

comment on column T_ACL_OU.COMPANYNAME is
'所属公司名称';

/*==============================================================*/
/* Table: T_ACL_OU_ROLE                                         */
/*==============================================================*/
create table T_ACL_OU_ROLE  (
   OU_ID                NUMBER(6)                       not null,
   ROLE_ID              NUMBER(6)                       not null,
   constraint "PK_Group_Role" primary key (OU_ID, ROLE_ID)
);

comment on table T_ACL_OU_ROLE is
'机构角色关联';

comment on column T_ACL_OU_ROLE.OU_ID is
'机构ID';

comment on column T_ACL_OU_ROLE.ROLE_ID is
'角色ID';

/*==============================================================*/
/* Table: T_ACL_OU_USER                                         */
/*==============================================================*/
create table T_ACL_OU_USER  (
   USER_ID              NUMBER(6)                       not null,
   OU_ID                NUMBER(6)                       not null,
   constraint "PK_Group_User" primary key (USER_ID, OU_ID)
);

comment on table T_ACL_OU_USER is
'机构用户关联';

comment on column T_ACL_OU_USER.USER_ID is
'用户ID';

comment on column T_ACL_OU_USER.OU_ID is
'机构ID';

/*==============================================================*/
/* Table: T_ACL_ROLE                                            */
/*==============================================================*/
create table T_ACL_ROLE  (
   ID                   NUMBER(6)                       not null,
   PID                  NUMBER                         default -1,
   HANDNO               NVARCHAR2(50),
   NAME                 NVARCHAR2(100)                  not null,
   NOTE                 CLOB,
   SORTCODE             NVARCHAR2(50),
   CATEGORY             NVARCHAR2(50),
   COMPANY_ID           NVARCHAR2(50),
   COMPANYNAME          NVARCHAR2(255),
   CREATOR              NVARCHAR2(50),
   CREATOR_ID           NVARCHAR2(50),
   CREATETIME           DATE                           default sysdate,
   EDITOR               NVARCHAR2(50),
   EDITOR_ID            NVARCHAR2(50),
   EDITTIME             DATE                           default sysdate,
   DELETED              INTEGER,
   ENABLED              INTEGER,
   constraint "PK_Role" primary key (ID)
);

comment on table T_ACL_ROLE is
'角色信息';

comment on column T_ACL_ROLE.PID is
'父ID';

comment on column T_ACL_ROLE.HANDNO is
'角色编码';

comment on column T_ACL_ROLE.NAME is
'角色名称';

comment on column T_ACL_ROLE.NOTE is
'备注';

comment on column T_ACL_ROLE.SORTCODE is
'排序码';

comment on column T_ACL_ROLE.CATEGORY is
'角色分类';

comment on column T_ACL_ROLE.COMPANY_ID is
'所属公司ID';

comment on column T_ACL_ROLE.COMPANYNAME is
'所属公司名称';

comment on column T_ACL_ROLE.CREATOR is
'创建人';

comment on column T_ACL_ROLE.CREATOR_ID is
'创建人ID';

comment on column T_ACL_ROLE.CREATETIME is
'创建时间';

comment on column T_ACL_ROLE.EDITOR is
'编辑人';

comment on column T_ACL_ROLE.EDITOR_ID is
'编辑人ID';

comment on column T_ACL_ROLE.EDITTIME is
'编辑时间';

comment on column T_ACL_ROLE.DELETED is
'是否已删除';

comment on column T_ACL_ROLE.ENABLED is
'有效标志';

/*==============================================================*/
/* Table: T_ACL_ROLEDATA                                        */
/*==============================================================*/
create table T_ACL_ROLEDATA  (
   ID                   NVARCHAR2(50)                   not null,
   ROLE_ID              INTEGER                         not null,
   BELONGCOMPANYS       NVARCHAR2(255),
   BELONGDEPTS          NVARCHAR2(255),
   EXCLUDEDEPTS         NVARCHAR2(255),
   NOTE                 CLOB,
   constraint PK_T_ACL_ROLEDATA primary key (ID)
);

comment on table T_ACL_ROLEDATA is
'角色的数据权限';

comment on column T_ACL_ROLEDATA.ROLE_ID is
'角色ID';

comment on column T_ACL_ROLEDATA.BELONGCOMPANYS is
'所属公司';

comment on column T_ACL_ROLEDATA.BELONGDEPTS is
'所属部门';

comment on column T_ACL_ROLEDATA.EXCLUDEDEPTS is
'排除部门';

comment on column T_ACL_ROLEDATA.NOTE is
'备注';

/*==============================================================*/
/* Table: T_ACL_ROLE_FUNCTION                                   */
/*==============================================================*/
create table T_ACL_ROLE_FUNCTION  (
   ROLE_ID              NUMBER(6)                       not null,
   FUNCTION_ID          NVARCHAR2(50)                   not null,
   constraint "PK_Role_Function" primary key (ROLE_ID, FUNCTION_ID)
);

comment on table T_ACL_ROLE_FUNCTION is
'角色功能关联';

comment on column T_ACL_ROLE_FUNCTION.ROLE_ID is
'角色ID';

comment on column T_ACL_ROLE_FUNCTION.FUNCTION_ID is
'功能ID';

/*==============================================================*/
/* Table: T_ACL_SYSTEMAUTHORIZE                                 */
/*==============================================================*/
create table T_ACL_SYSTEMAUTHORIZE  (
   ID                   NUMBER(6)                       not null,
   SYSTEMTYPE_OID       NVARCHAR2(50)                   not null,
   CONTENT              NVARCHAR2(100),
   constraint "PK_SystemAuthorize" primary key (ID)
);

comment on table T_ACL_SYSTEMAUTHORIZE is
'系统类型授权信息';

comment on column T_ACL_SYSTEMAUTHORIZE.SYSTEMTYPE_OID is
'系统标识ID';

comment on column T_ACL_SYSTEMAUTHORIZE.CONTENT is
'备注内容';

/*==============================================================*/
/* Table: T_ACL_SYSTEMTYPE                                      */
/*==============================================================*/
create table T_ACL_SYSTEMTYPE  (
   OID                  NVARCHAR2(50)                   not null,
   NAME                 NVARCHAR2(100)                  not null,
   CUSTOMID             NVARCHAR2(50),
   AUTHORIZE            NVARCHAR2(100),
   NOTE                 CLOB,
   constraint "PK_SystemType" primary key (OID)
);

comment on table T_ACL_SYSTEMTYPE is
'系统类型信息';

comment on column T_ACL_SYSTEMTYPE.OID is
'系统标识';

comment on column T_ACL_SYSTEMTYPE.NAME is
'系统名称';

comment on column T_ACL_SYSTEMTYPE.CUSTOMID is
'客户编码';

comment on column T_ACL_SYSTEMTYPE.AUTHORIZE is
'授权编码';

comment on column T_ACL_SYSTEMTYPE.NOTE is
'备注';

/*==============================================================*/
/* Table: T_ACL_USER                                            */
/*==============================================================*/
create table T_ACL_USER  (
   ID                   NUMBER(6)                       not null,
   PID                  NUMBER                         default -1,
   HANDNO               NVARCHAR2(50),
   NAME                 NVARCHAR2(50)                   not null,
   PASSWORD             NVARCHAR2(100)                  not null,
   FULLNAME             NVARCHAR2(100),
   NICKNAME             NVARCHAR2(100),
   ISEXPIRE             NUMBER                         default 0,
   TITLE                NVARCHAR2(100),
   IDENTITYCARD         NVARCHAR2(50),
   MOBILEPHONE          NVARCHAR2(100),
   OFFICEPHONE          NVARCHAR2(100),
   HOMEPHONE            NVARCHAR2(100),
   EMAIL                NVARCHAR2(100),
   ADDRESS              NVARCHAR2(255),
   WORKADDR             NVARCHAR2(255),
   GENDER               NVARCHAR2(50),
   BIRTHDAY             DATE,
   QQ                   NVARCHAR2(50),
   SIGNATURE            NVARCHAR2(255),
   AUDITSTATUS          NVARCHAR2(50),
   PORTRAIT             BLOB,
   NOTE                 CLOB,
   CUSTOMFIELD          CLOB,
   DEPT_ID              NVARCHAR2(50),
   DEPTNAME             NVARCHAR2(255),
   COMPANY_ID           NVARCHAR2(50),
   COMPANYNAME          NVARCHAR2(255),
   SORTCODE             NVARCHAR2(50),
   CREATOR              NVARCHAR2(50),
   CREATOR_ID           NVARCHAR2(50),
   CREATETIME           DATE                           default sysdate,
   EDITOR               NVARCHAR2(50),
   EDITOR_ID            NVARCHAR2(50),
   EDITTIME             DATE                           default sysdate,
   DELETED              INTEGER,
   QUESTION             NVARCHAR2(100),
   ANSWER               NVARCHAR2(100),
   LASTLOGINIP          NVARCHAR2(50),
   LASTLOGINTIME        DATE,
   LASTMACADDRESS       NVARCHAR2(100),
   CURRENTLOGINIP       NVARCHAR2(50),
   CURRENTLOGINTIME     DATE,
   CURRENTMACADDRESS    NVARCHAR2(100),
   LASTPASSWORDTIME     DATE,
   constraint "PK_User" primary key (ID)
);

comment on table T_ACL_USER is
'系统用户信息';

comment on column T_ACL_USER.PID is
'父ID';

comment on column T_ACL_USER.HANDNO is
'用户编码';

comment on column T_ACL_USER.NAME is
'用户名/登录名';

comment on column T_ACL_USER.PASSWORD is
'用户密码';

comment on column T_ACL_USER.FULLNAME is
'真实姓名';

comment on column T_ACL_USER.NICKNAME is
'用户呢称';

comment on column T_ACL_USER.ISEXPIRE is
'是否过期';

comment on column T_ACL_USER.TITLE is
'职务头衔';

comment on column T_ACL_USER.IDENTITYCARD is
'身份证号码';

comment on column T_ACL_USER.MOBILEPHONE is
'移动电话';

comment on column T_ACL_USER.OFFICEPHONE is
'办公电话';

comment on column T_ACL_USER.HOMEPHONE is
'家庭电话';

comment on column T_ACL_USER.EMAIL is
'邮件地址';

comment on column T_ACL_USER.ADDRESS is
'住址';

comment on column T_ACL_USER.WORKADDR is
'办公地址';

comment on column T_ACL_USER.GENDER is
'性别';

comment on column T_ACL_USER.BIRTHDAY is
'出生日期';

comment on column T_ACL_USER.QQ is
'QQ号码';

comment on column T_ACL_USER.SIGNATURE is
'个性签名';

comment on column T_ACL_USER.AUDITSTATUS is
'审核状态';

comment on column T_ACL_USER.PORTRAIT is
'个人图片';

comment on column T_ACL_USER.NOTE is
'备注';

comment on column T_ACL_USER.CUSTOMFIELD is
'自定义字段';

comment on column T_ACL_USER.DEPT_ID is
'默认部门ID';

comment on column T_ACL_USER.DEPTNAME is
'默认部门名称';

comment on column T_ACL_USER.COMPANY_ID is
'所属公司ID';

comment on column T_ACL_USER.COMPANYNAME is
'所属公司名称';

comment on column T_ACL_USER.SORTCODE is
'排序码';

comment on column T_ACL_USER.CREATOR is
'创建人';

comment on column T_ACL_USER.CREATOR_ID is
'创建人ID';

comment on column T_ACL_USER.CREATETIME is
'创建时间';

comment on column T_ACL_USER.EDITOR is
'编辑人';

comment on column T_ACL_USER.EDITOR_ID is
'编辑人ID';

comment on column T_ACL_USER.EDITTIME is
'编辑时间';

comment on column T_ACL_USER.DELETED is
'是否已删除';

comment on column T_ACL_USER.QUESTION is
'密保：提示问题';

comment on column T_ACL_USER.ANSWER is
'密保:问题答案';

comment on column T_ACL_USER.LASTLOGINIP is
'上次登录IP';

comment on column T_ACL_USER.LASTLOGINTIME is
'上次登录时间';

comment on column T_ACL_USER.LASTMACADDRESS is
'上次Mac地址';

comment on column T_ACL_USER.CURRENTLOGINIP is
'当前登录IP';

comment on column T_ACL_USER.CURRENTLOGINTIME is
'当前登录时间';

comment on column T_ACL_USER.CURRENTMACADDRESS is
'当前Mac地址';

comment on column T_ACL_USER.LASTPASSWORDTIME is
'最后修改密码日期';

/*==============================================================*/
/* Table: T_ACL_USER_ROLE                                       */
/*==============================================================*/
create table T_ACL_USER_ROLE  (
   USER_ID              NUMBER(6)                       not null,
   ROLE_ID              NUMBER(6)                       not null,
   constraint "PK_User_Role" primary key (USER_ID, ROLE_ID)
);

comment on table T_ACL_USER_ROLE is
'用户角色关联';

comment on column T_ACL_USER_ROLE.USER_ID is
'用户ID';

comment on column T_ACL_USER_ROLE.ROLE_ID is
'角色ID';

/*==============================================================*/
/* Table: TB_INFORMATION                                        */
/*==============================================================*/
create table TB_INFORMATION  (
   ID                   NVARCHAR2(50)                   not null,
   TITLE                NVARCHAR2(255),
   CONTENT              CLOB,
   ATTACHMENT_GUID      NVARCHAR2(50),
   CATEGORY             NVARCHAR2(255),
   SUBTYPE              NVARCHAR2(255),
   EDITOR               NVARCHAR2(50),
   EDITTIME             date,
   ISCHECKED            INTEGER,
   CHECKUSER            NVARCHAR2(50),
   CHECKTIME            DATE,
   FORCEEXPIRE          INTEGER,
   TIMEOUT              DATE,
   constraint PK_TB_INFORMATION primary key (ID)
);

comment on table TB_INFORMATION is
'政策法规公告动态';

comment on column TB_INFORMATION.TITLE is
'标题';

comment on column TB_INFORMATION.CONTENT is
'内容';

comment on column TB_INFORMATION.ATTACHMENT_GUID is
'附件GUID';

comment on column TB_INFORMATION.CATEGORY is
'大类名称';

comment on column TB_INFORMATION.SUBTYPE is
'子类名称';

comment on column TB_INFORMATION.EDITOR is
'编辑者';

comment on column TB_INFORMATION.EDITTIME is
'编辑时间';

comment on column TB_INFORMATION.ISCHECKED is
'是否审批通过';

comment on column TB_INFORMATION.CHECKUSER is
'审批者';

comment on column TB_INFORMATION.CHECKTIME is
'审批时间';

comment on column TB_INFORMATION.FORCEEXPIRE is
'是否强制过期';

comment on column TB_INFORMATION.TIMEOUT is
'过期截止时间';

/*==============================================================*/
/* Table: TB_INFORMATIONSTATUS                                  */
/*==============================================================*/
create table TB_INFORMATIONSTATUS  (
   ID                   NVARCHAR2(50)                   not null,
   CATEGORY             NVARCHAR2(50),
   INFORMATION_ID       NVARCHAR2(50),
   STATUS               INTEGER,
   USER_ID              NVARCHAR2(50),
   constraint PK_TB_INFORMATIONSTATUS primary key (ID)
);

comment on table TB_INFORMATIONSTATUS is
'用户对指定内容的操作状态记录';

comment on column TB_INFORMATIONSTATUS.CATEGORY is
'信息类型';

comment on column TB_INFORMATIONSTATUS.INFORMATION_ID is
'信息ID';

comment on column TB_INFORMATIONSTATUS.STATUS is
'阅读状态（0：未读，1：已读，其他待定）';

comment on column TB_INFORMATIONSTATUS.USER_ID is
'用户ID';


alter table T_ACL_OU_ROLE
   add constraint "FK_OU_Role_OU" foreign key (OU_ID)
      references T_ACL_OU (ID)
      on delete cascade;

alter table T_ACL_OU_ROLE
   add constraint "FK_OU_Role_Role" foreign key (ROLE_ID)
      references T_ACL_ROLE (ID)
      on delete cascade;

alter table T_ACL_OU_USER
   add constraint "FK_OU_User_OU" foreign key (OU_ID)
      references T_ACL_OU (ID)
      on delete cascade;

alter table T_ACL_OU_USER
   add constraint "FK_User_OU_User" foreign key (USER_ID)
      references T_ACL_USER (ID)
      on delete cascade;

alter table T_ACL_ROLEDATA
   add constraint FK_T_ACL_RO_FK_ROLE_D_T_ACL_RO foreign key (ROLE_ID)
      references T_ACL_ROLE (ID)
      on delete cascade;

alter table T_ACL_ROLE_FUNCTION
   add constraint "FK_Role_Function_Function" foreign key (FUNCTION_ID)
      references T_ACL_FUNCTION (ID)
      on delete cascade;

alter table T_ACL_ROLE_FUNCTION
   add constraint "FK_Role_Function_Role" foreign key (ROLE_ID)
      references T_ACL_ROLE (ID)
      on delete cascade;

alter table T_ACL_SYSTEMAUTHORIZE
   add constraint "FK_SystemAuthorize_SystemType" foreign key (SYSTEMTYPE_OID)
      references T_ACL_SYSTEMTYPE (OID)
      on delete cascade;

alter table T_ACL_USER_ROLE
   add constraint "FK_User_Role_Role" foreign key (ROLE_ID)
      references T_ACL_ROLE (ID)
      on delete cascade;

alter table T_ACL_USER_ROLE
   add constraint "FK_User_Role_User" foreign key (USER_ID)
      references T_ACL_USER (ID)
      on delete cascade;

commit;



--仓库管理系统的表
/*==============================================================*/
/* Table: WM_ITEMDETAIL                                         */
/*==============================================================*/
create table WM_ITEMDETAIL  (
   ID                   NUMBER(6)                       not null,
   ITEMNO               NVARCHAR2(50),
   ITEMNAME             NVARCHAR2(50),
   MANUFACTURE          NVARCHAR2(50),
   MAPNO                NVARCHAR2(50),
   SPECIFICATION        NVARCHAR2(50),
   MATERIAL             NVARCHAR2(50),
   ITEMBIGTYPE          NVARCHAR2(50),
   ITEMTYPE             NVARCHAR2(50),
   UNIT                 NVARCHAR2(50),
   PRICE                NUMBER(8,2),
   SOURCE               NVARCHAR2(255),
   STORAGEPOS           NVARCHAR2(255),
   USAGEPOS             NVARCHAR2(255),
   NOTE                 NVARCHAR2(255),
   WAREHOUSE            NVARCHAR2(50),
   DEPT                 NVARCHAR2(50),
   constraint "PK_TB_ItemDetail" primary key (ID)
);

comment on table WM_ITEMDETAIL is
'备件信息';

comment on column WM_ITEMDETAIL.ITEMNO is
'备件编号';

comment on column WM_ITEMDETAIL.ITEMNAME is
'备件名称';

comment on column WM_ITEMDETAIL.MANUFACTURE is
'供货商';

comment on column WM_ITEMDETAIL.MAPNO is
'图号';

comment on column WM_ITEMDETAIL.SPECIFICATION is
'规格型号';

comment on column WM_ITEMDETAIL.MATERIAL is
'材质';

comment on column WM_ITEMDETAIL.ITEMBIGTYPE is
'备件属类';

comment on column WM_ITEMDETAIL.ITEMTYPE is
'备件类别';

comment on column WM_ITEMDETAIL.UNIT is
'单位';

comment on column WM_ITEMDETAIL.PRICE is
'单价';

comment on column WM_ITEMDETAIL.SOURCE is
'来源';

comment on column WM_ITEMDETAIL.STORAGEPOS is
'库位';

comment on column WM_ITEMDETAIL.USAGEPOS is
'使用位置';

comment on column WM_ITEMDETAIL.NOTE is
'备注';

comment on column WM_ITEMDETAIL.WAREHOUSE is
'所属库房';

comment on column WM_ITEMDETAIL.DEPT is
'所属部门';

/*==============================================================*/
/* Table: WM_PURCHASEDETAIL                                     */
/*==============================================================*/
create table WM_PURCHASEDETAIL  (
   ID                   NUMBER(6)                       not null,
   PURCHASEHEAD_ID      INTEGER,
   OPERATIONTYPE        NVARCHAR2(50),
   ITEMNO               NVARCHAR2(50),
   ITEMNAME             NVARCHAR2(50),
   MAPNO                NVARCHAR2(50),
   SPECIFICATION        NVARCHAR2(50),
   MATERIAL             NVARCHAR2(50),
   ITEMBIGTYPE          NVARCHAR2(50),
   ITEMTYPE             NVARCHAR2(50),
   UNIT                 NVARCHAR2(50),
   PRICE                DECIMAL,
   QUANTITY             DECIMAL,
   AMOUNT               DECIMAL,
   SOURCE               NVARCHAR2(255),
   STORAGEPOS           NVARCHAR2(255),
   USAGEPOS             NVARCHAR2(255),
   WAREHOUSE            NVARCHAR2(50),
   DEPT                 NVARCHAR2(50),
   constraint "PK_TB_PurchaseDetail" primary key (ID)
);

comment on table WM_PURCHASEDETAIL is
'入库出库明细';

comment on column WM_PURCHASEDETAIL.PURCHASEHEAD_ID is
'进货表单头';

comment on column WM_PURCHASEDETAIL.OPERATIONTYPE is
'操作类型(进货还是退货)';

comment on column WM_PURCHASEDETAIL.ITEMNO is
'备件编号';

comment on column WM_PURCHASEDETAIL.ITEMNAME is
'备件名称';

comment on column WM_PURCHASEDETAIL.MAPNO is
'图号';

comment on column WM_PURCHASEDETAIL.SPECIFICATION is
'规格型号';

comment on column WM_PURCHASEDETAIL.MATERIAL is
'材质';

comment on column WM_PURCHASEDETAIL.ITEMBIGTYPE is
'备件属类';

comment on column WM_PURCHASEDETAIL.ITEMTYPE is
'备件类别';

comment on column WM_PURCHASEDETAIL.UNIT is
'单位';

comment on column WM_PURCHASEDETAIL.PRICE is
'单价';

comment on column WM_PURCHASEDETAIL.QUANTITY is
'数量';

comment on column WM_PURCHASEDETAIL.AMOUNT is
'金额';

comment on column WM_PURCHASEDETAIL.SOURCE is
'来源';

comment on column WM_PURCHASEDETAIL.STORAGEPOS is
'库位';

comment on column WM_PURCHASEDETAIL.USAGEPOS is
'使用位置';

comment on column WM_PURCHASEDETAIL.WAREHOUSE is
'所属库房';

comment on column WM_PURCHASEDETAIL.DEPT is
'所属部门';

/*==============================================================*/
/* Table: WM_PURCHASEHEADER                                     */
/*==============================================================*/
create table WM_PURCHASEHEADER  (
   ID                   NUMBER(6)                       not null,
   HANDNO               NVARCHAR2(50),
   OPERATIONTYPE        CHAR(10),
   MANUFACTURE          NVARCHAR2(50),
   WAREHOUSE            NVARCHAR2(50),
   COSTCENTER           NVARCHAR2(50),
   NOTE                 NVARCHAR2(255),
   CREATEDATE           DATE,
   CREATOR              NVARCHAR2(50),
   CREATEYEAR           INTEGER,
   CREATEMONTH          INTEGER,
   PICKINGPEOPLE        NVARCHAR2(50),
   constraint "PK_TB_PurchaseHeader" primary key (ID)
);

comment on table WM_PURCHASEHEADER is
'入库出库表头';

comment on column WM_PURCHASEHEADER.HANDNO is
'进货编号';

comment on column WM_PURCHASEHEADER.OPERATIONTYPE is
'操作类型（进货还是退货）';

comment on column WM_PURCHASEHEADER.MANUFACTURE is
'供应商';

comment on column WM_PURCHASEHEADER.WAREHOUSE is
'库房编号';

comment on column WM_PURCHASEHEADER.COSTCENTER is
'成本中心';

comment on column WM_PURCHASEHEADER.NOTE is
'备注';

comment on column WM_PURCHASEHEADER.CREATEDATE is
'创建日期';

comment on column WM_PURCHASEHEADER.CREATOR is
'操作员';

comment on column WM_PURCHASEHEADER.CREATEYEAR is
'记录年';

comment on column WM_PURCHASEHEADER.CREATEMONTH is
'记录月';

comment on column WM_PURCHASEHEADER.PICKINGPEOPLE is
'领料人';

/*==============================================================*/
/* Table: WM_REPORTANNUALCOSTDETAIL                             */
/*==============================================================*/
create table WM_REPORTANNUALCOSTDETAIL  (
   ID                   NUMBER(6)                       not null,
   HEADER_ID            INTEGER,
   REPORTYEAR           INTEGER,
   ITEMTYPE             NVARCHAR2(50),
   COSTCENTERORDEPT     NVARCHAR2(50),
   ONE                  NUMBER(8,2),
   TWO                  NUMBER(8,2),
   THREE                NUMBER(8,2),
   FOUR                 NUMBER(8,2),
   FIVE                 NUMBER(8,2),
   SIX                  NUMBER(8,2),
   SEVEN                NUMBER(8,2),
   EIGHT                NUMBER(8,2),
   NINE                 NUMBER(8,2),
   TEN                  NUMBER(8,2),
   ELEVEN               NUMBER(8,2),
   TWELVE               NUMBER(8,2),
   TOTAL                NUMBER(8,2),
   REPORTCODE           NVARCHAR2(50),
   constraint PK_WM_REPORTANNUALCOSTDETAIL primary key (ID)
);

comment on table WM_REPORTANNUALCOSTDETAIL is
'年度成本报表详细信息';

comment on column WM_REPORTANNUALCOSTDETAIL.HEADER_ID is
'报表头ID';

comment on column WM_REPORTANNUALCOSTDETAIL.REPORTYEAR is
'报表年份';

comment on column WM_REPORTANNUALCOSTDETAIL.ITEMTYPE is
'备件类别';

comment on column WM_REPORTANNUALCOSTDETAIL.COSTCENTERORDEPT is
'成本中心或部门';

comment on column WM_REPORTANNUALCOSTDETAIL.ONE is
'总金额';

comment on column WM_REPORTANNUALCOSTDETAIL.REPORTCODE is
'额外的数据分类码';

/*==============================================================*/
/* Table: WM_REPORTANNUALCOSTHEADER                             */
/*==============================================================*/
create table WM_REPORTANNUALCOSTHEADER  (
   ID                   NUMBER(6)                       not null,
   REPORTTYPE           INTEGER,
   REPORTTITLE          NVARCHAR2(50),
   REPORTYEAR           INTEGER,
   CREATEDATE           DATE,
   CREATOR              NVARCHAR2(50),
   NOTE                 NVARCHAR2(255),
   constraint "PK_TB_ReportAnnualCostHeader" primary key (ID)
);

comment on table WM_REPORTANNUALCOSTHEADER is
'年度成本报表头信息';

comment on column WM_REPORTANNUALCOSTHEADER.REPORTTYPE is
'报表类型：1为全年费用汇总报表';

comment on column WM_REPORTANNUALCOSTHEADER.REPORTTITLE is
'报表标题';

comment on column WM_REPORTANNUALCOSTHEADER.REPORTYEAR is
'报表年份';

comment on column WM_REPORTANNUALCOSTHEADER.CREATEDATE is
'报表创建日期';

comment on column WM_REPORTANNUALCOSTHEADER.CREATOR is
'报表创建人员';

comment on column WM_REPORTANNUALCOSTHEADER.NOTE is
'备注';

/*==============================================================*/
/* Table: WM_REPORTDEPTCOST                                     */
/*==============================================================*/
create table WM_REPORTDEPTCOST  (
   ID                   NUMBER(6)                       not null,
   REPORTTITLE          NVARCHAR2(50),
   REPORTYEAR           INTEGER,
   REPORTMONTH          INTEGER,
   YEARMONTH            NVARCHAR2(50),
   DEPTNAME             NVARCHAR2(50),
   ITEMTYPE             NVARCHAR2(50),
   TOTALMONEY           DECIMAL,
   CREATEDATE           DATE,
   CREATOR              NVARCHAR2(50),
   NOTE                 NVARCHAR2(255),
   constraint "PK_TB_ReportDeptCost" primary key (ID)
);

comment on table WM_REPORTDEPTCOST is
'部门成本报表';

comment on column WM_REPORTDEPTCOST.REPORTTITLE is
'报表标题';

comment on column WM_REPORTDEPTCOST.REPORTYEAR is
'报表年份';

comment on column WM_REPORTDEPTCOST.REPORTMONTH is
'报表月份';

comment on column WM_REPORTDEPTCOST.YEARMONTH is
'报表年月';

comment on column WM_REPORTDEPTCOST.DEPTNAME is
'部门项目';

comment on column WM_REPORTDEPTCOST.ITEMTYPE is
'备件类别';

comment on column WM_REPORTDEPTCOST.TOTALMONEY is
'总金额';

comment on column WM_REPORTDEPTCOST.CREATEDATE is
'报表创建日期';

comment on column WM_REPORTDEPTCOST.CREATOR is
'创建人';

comment on column WM_REPORTDEPTCOST.NOTE is
'备注';

/*==============================================================*/
/* Table: WM_REPORTMONTHCHECKOUT                                */
/*==============================================================*/
create table WM_REPORTMONTHCHECKOUT  (
   ID                   NUMBER(6)                       not null,
   REPORTTYPE           INTEGER,
   REPORTTITLE          NVARCHAR2(50),
   REPORTYEAR           INTEGER,
   REPORTMONTH          INTEGER,
   YEARMONTH            NVARCHAR2(50),
   ITEMNAME             NVARCHAR2(50),
   LASTCOUNT            INTEGER,
   LASTMONEY            NUMBER(8,2),
   CURRENTINCOUNT       INTEGER,
   CURRENTINMONEY       NUMBER(8,2),
   CURRENTOUTCOUNT      INTEGER,
   CURRENTOUTMONEY      NUMBER(8,2),
   CURRENTCOUNT         INTEGER,
   CURRENTMONEY         NUMBER(8,2),
   CREATEDATE           DATE,
   CREATOR              NVARCHAR2(50),
   NOTE                 NVARCHAR2(255),
   constraint "PK_TB_ReportMonthCheckOut" primary key (ID)
);

comment on table WM_REPORTMONTHCHECKOUT is
'库房结存月报表';

comment on column WM_REPORTMONTHCHECKOUT.REPORTTYPE is
'报表类型：1为库房部门结存，2库房结存，3为备件属类的库房结存，4为备件类别的库房结存';

comment on column WM_REPORTMONTHCHECKOUT.REPORTTITLE is
'报表标题';

comment on column WM_REPORTMONTHCHECKOUT.REPORTYEAR is
'报表年份';

comment on column WM_REPORTMONTHCHECKOUT.REPORTMONTH is
'报表月份';

comment on column WM_REPORTMONTHCHECKOUT.YEARMONTH is
'报表年月';

comment on column WM_REPORTMONTHCHECKOUT.ITEMNAME is
'项目名称';

comment on column WM_REPORTMONTHCHECKOUT.LASTCOUNT is
'上月结存数量';

comment on column WM_REPORTMONTHCHECKOUT.LASTMONEY is
'上月结存金额';

comment on column WM_REPORTMONTHCHECKOUT.CURRENTINCOUNT is
'本月入库数量';

comment on column WM_REPORTMONTHCHECKOUT.CURRENTINMONEY is
'本月入库金额';

comment on column WM_REPORTMONTHCHECKOUT.CURRENTOUTCOUNT is
'本月出库数量';

comment on column WM_REPORTMONTHCHECKOUT.CURRENTOUTMONEY is
'本月出库金额';

comment on column WM_REPORTMONTHCHECKOUT.CURRENTCOUNT is
'本月结存数量';

comment on column WM_REPORTMONTHCHECKOUT.CURRENTMONEY is
'本月结存金额';

comment on column WM_REPORTMONTHCHECKOUT.CREATEDATE is
'报表创建日期';

comment on column WM_REPORTMONTHCHECKOUT.CREATOR is
'报表创建人员';

comment on column WM_REPORTMONTHCHECKOUT.NOTE is
'备注';

/*==============================================================*/
/* Table: WM_REPORTMONTHLYCOSTDETAIL                            */
/*==============================================================*/
create table WM_REPORTMONTHLYCOSTDETAIL  (
   ID                   NUMBER(6)                       not null,
   HEADER_ID            INTEGER,
   REPORTYEAR           INTEGER,
   REPORTMONTH          INTEGER,
   YEARMONTH            NVARCHAR2(50),
   DEPTNAME             NVARCHAR2(50),
   ITEMTYPE             NVARCHAR2(50),
   TOTALMONEY           NUMBER(8,2),
   REPORTCODE           NVARCHAR2(50),
   constraint "PK_TB_ReportMonthlyCostDetail" primary key (ID)
);

comment on table WM_REPORTMONTHLYCOSTDETAIL is
'月报表成本明细';

comment on column WM_REPORTMONTHLYCOSTDETAIL.HEADER_ID is
'报表头ID';

comment on column WM_REPORTMONTHLYCOSTDETAIL.REPORTYEAR is
'报表年份';

comment on column WM_REPORTMONTHLYCOSTDETAIL.REPORTMONTH is
'报表月份';

comment on column WM_REPORTMONTHLYCOSTDETAIL.YEARMONTH is
'报表年月';

comment on column WM_REPORTMONTHLYCOSTDETAIL.DEPTNAME is
'部门项目';

comment on column WM_REPORTMONTHLYCOSTDETAIL.ITEMTYPE is
'备件类别';

comment on column WM_REPORTMONTHLYCOSTDETAIL.TOTALMONEY is
'总金额';

comment on column WM_REPORTMONTHLYCOSTDETAIL.REPORTCODE is
'额外的数据分类码';

create cluster  C_WM_REPORTMONTHLYDETAIL (
   ID		NUMBER
);

/*==============================================================*/
/* Table: WM_REPORTMONTHLYDETAIL                                */
/*==============================================================*/
create table WM_REPORTMONTHLYDETAIL  (
   ID                   NUMBER(6)                       not null,
   HEADER_ID            INTEGER,
   REPORTYEAR           INTEGER,
   REPORTMONTH          INTEGER,
   YEARMONTH            NVARCHAR2(50),
   ITEMNAME             NVARCHAR2(50),
   LASTCOUNT            INTEGER,
   LASTMONEY            NUMBER(8,2),
   CURRENTINCOUNT       INTEGER,
   CURRENTINMONEY       NUMBER(8,2),
   CURRENTOUTCOUNT      INTEGER,
   CURRENTOUTMONEY      NUMBER(8,2),
   CURRENTCOUNT         INTEGER,
   CURRENTMONEY         NUMBER(8,2),
   REPORTCODE           NVARCHAR2(50),
   constraint "PK_TB_ReportMonthlyDetail" primary key (ID)
);

comment on table WM_REPORTMONTHLYDETAIL is
'月报表详细信息';

comment on column WM_REPORTMONTHLYDETAIL.HEADER_ID is
'报表头ID';

comment on column WM_REPORTMONTHLYDETAIL.REPORTYEAR is
'报表年份';

comment on column WM_REPORTMONTHLYDETAIL.REPORTMONTH is
'报表月份';

comment on column WM_REPORTMONTHLYDETAIL.YEARMONTH is
'报表年月';

comment on column WM_REPORTMONTHLYDETAIL.ITEMNAME is
'项目名称';

comment on column WM_REPORTMONTHLYDETAIL.LASTCOUNT is
'上月结存数量';

comment on column WM_REPORTMONTHLYDETAIL.LASTMONEY is
'上月结存金额';

comment on column WM_REPORTMONTHLYDETAIL.CURRENTINCOUNT is
'本月入库数量';

comment on column WM_REPORTMONTHLYDETAIL.CURRENTINMONEY is
'本月入库金额';

comment on column WM_REPORTMONTHLYDETAIL.CURRENTOUTCOUNT is
'本月出库数量';

comment on column WM_REPORTMONTHLYDETAIL.CURRENTOUTMONEY is
'本月出库金额';

comment on column WM_REPORTMONTHLYDETAIL.CURRENTCOUNT is
'本月结存数量';

comment on column WM_REPORTMONTHLYDETAIL.CURRENTMONEY is
'本月结存金额';

comment on column WM_REPORTMONTHLYDETAIL.REPORTCODE is
'额外的数据分类码';

/*==============================================================*/
/* Table: WM_REPORTMONTHLYHEADER                                */
/*==============================================================*/
create table WM_REPORTMONTHLYHEADER  (
   ID                   NUMBER(6)                       not null,
   REPORTTYPE           INTEGER,
   REPORTTITLE          NVARCHAR2(50),
   REPORTYEAR           INTEGER,
   REPORTMONTH          INTEGER,
   YEARMONTH            NVARCHAR2(50),
   CREATEDATE           DATE,
   CREATOR              NVARCHAR2(50),
   NOTE                 NVARCHAR2(255),
   constraint "PK_TB_ReportMonthlyHeader" primary key (ID)
);

comment on table WM_REPORTMONTHLYHEADER is
'月报表头信息';

comment on column WM_REPORTMONTHLYHEADER.REPORTTYPE is
'报表类型：1为库房部门结存，2库房结存，3为所有库房结存报表（包括备件属类，备件类型），4为车间成本月报表';

comment on column WM_REPORTMONTHLYHEADER.REPORTTITLE is
'报表标题';

comment on column WM_REPORTMONTHLYHEADER.REPORTYEAR is
'报表年份';

comment on column WM_REPORTMONTHLYHEADER.REPORTMONTH is
'报表月份';

comment on column WM_REPORTMONTHLYHEADER.YEARMONTH is
'报表年月';

comment on column WM_REPORTMONTHLYHEADER.CREATEDATE is
'报表创建日期';

comment on column WM_REPORTMONTHLYHEADER.CREATOR is
'报表创建人员';

comment on column WM_REPORTMONTHLYHEADER.NOTE is
'备注';

/*==============================================================*/
/* Table: WM_STOCK                                              */
/*==============================================================*/
create table WM_STOCK  (
   ID                   NUMBER(6)                       not null,
   ITEMNO               NVARCHAR2(50),
   ITEMNAME             NVARCHAR2(50),
   ITEMBIGTYPE          NVARCHAR2(50),
   ITEMTYPE             NVARCHAR2(50),
   STOCKQUANTITY        INTEGER,
   STOCKMONEY           NVARCHAR2(50),
   LOWWARNING           INTEGER,
   HIGHWARNING          INTEGER,
   WAREHOUSE            NVARCHAR2(50),
   NOTE                 NVARCHAR2(255),
   constraint "PK_TB_Stock" primary key (ID)
);

comment on table WM_STOCK is
'库存信息';

comment on column WM_STOCK.ITEMNO is
'备件编号';

comment on column WM_STOCK.ITEMNAME is
'备件名称';

comment on column WM_STOCK.ITEMBIGTYPE is
'备件属类';

comment on column WM_STOCK.ITEMTYPE is
'备件类别';

comment on column WM_STOCK.STOCKQUANTITY is
'库存量';

comment on column WM_STOCK.STOCKMONEY is
'库存金额';

comment on column WM_STOCK.LOWWARNING is
'低储预警';

comment on column WM_STOCK.HIGHWARNING is
'超储预警';

comment on column WM_STOCK.WAREHOUSE is
'所属库房';

comment on column WM_STOCK.NOTE is
'备注';

/*==============================================================*/
/* Table: WM_WAREHOUSE                                          */
/*==============================================================*/
create table WM_WAREHOUSE  (
   ID                   NUMBER(6)                       not null,
   NAME                 NVARCHAR2(50),
   MANAGER              NVARCHAR2(255),
   PHONE                NVARCHAR2(50),
   ADDRESS              NVARCHAR2(255),
   NOTE                 NVARCHAR2(255),
   RESERVED             NUMBER,
   constraint "PK_TB_WareHouse" primary key (ID)
);

comment on table WM_WAREHOUSE is
'库房表';

comment on column WM_WAREHOUSE.NAME is
'仓库名称';

comment on column WM_WAREHOUSE.MANAGER is
'仓库负责人';

comment on column WM_WAREHOUSE.PHONE is
'联系电话';

comment on column WM_WAREHOUSE.ADDRESS is
'仓库地址';

comment on column WM_WAREHOUSE.NOTE is
'备注';

comment on column WM_WAREHOUSE.RESERVED is
'是否保留';



--其他模块数据库表
/*==============================================================*/
/* Table: MPS_MAILATTACH                                        */
/*==============================================================*/
create table MPS_MAILATTACH  (
   ID                   NUMBER(6)                       not null,
   COMPANY_ID           INTEGER,
   USER_ID              INTEGER,
   DOCTYPE              NVARCHAR2(10),
   CREATETIME           DATE,
   DOC_ID               INTEGER,
   FILENAME             NVARCHAR2(100),
   REALFILENAME         NVARCHAR2(100),
   FILESIZE             INTEGER,
   constraint PK_MPS_MAILATTACH primary key (ID)
);

comment on table MPS_MAILATTACH is
'发送和接受的邮件附件表
(该表不再使用，使用TB_FileUpload表进行代替)';

comment on column MPS_MAILATTACH.ID is
'编号';

comment on column MPS_MAILATTACH.COMPANY_ID is
'企业标识';

comment on column MPS_MAILATTACH.USER_ID is
'员工标识';

comment on column MPS_MAILATTACH.DOCTYPE is
'附件类型:发送邮件为mail，收取为mailrcv';

comment on column MPS_MAILATTACH.CREATETIME is
'产生日期';

comment on column MPS_MAILATTACH.DOC_ID is
'对应的邮件ID,即mps_MailSend中的ID';

comment on column MPS_MAILATTACH.FILENAME is
'友好显示文件名，不包括路径';

comment on column MPS_MAILATTACH.REALFILENAME is
'物理存储的实际文件名，不含路径';

comment on column MPS_MAILATTACH.FILESIZE is
'文件大小(byte)';

/*==============================================================*/
/* Table: MPS_MAILCONFIG                                        */
/*==============================================================*/
create table MPS_MAILCONFIG  (
   ID                   NUMBER(6)                       not null,
   COMPANY_ID           INTEGER,
   USER_ID              INTEGER,
   EMAIL                NVARCHAR2(50),
   POP3SERVER           NVARCHAR2(50),
   POP3PORT             INTEGER                        default 110,
   SMTPSERVER           NVARCHAR2(50),
   SMTPPORT             INTEGER                        default 25,
   LOGINID              NVARCHAR2(50),
   PASSWORD             NVARCHAR2(50),
   USESSL               INTEGER                        default 0,
   ISDEFAULT            INTEGER                        default 0,
   CHECKOUT             INTEGER                        default 0,
   CREATETIME           DATE,
   constraint PK_MPS_MAILCONFIG primary key (ID)
);

comment on table MPS_MAILCONFIG is
'员工帐号表';

comment on column MPS_MAILCONFIG.ID is
'自动编号';

comment on column MPS_MAILCONFIG.COMPANY_ID is
'企业标识';

comment on column MPS_MAILCONFIG.USER_ID is
'员工标识';

comment on column MPS_MAILCONFIG.EMAIL is
'邮件帐号';

comment on column MPS_MAILCONFIG.POP3SERVER is
'POP3服务器';

comment on column MPS_MAILCONFIG.POP3PORT is
'POP3端口';

comment on column MPS_MAILCONFIG.SMTPSERVER is
'SMTP服务器';

comment on column MPS_MAILCONFIG.SMTPPORT is
'SMTP端口';

comment on column MPS_MAILCONFIG.LOGINID is
'登录账号';

comment on column MPS_MAILCONFIG.PASSWORD is
'登录密码';

comment on column MPS_MAILCONFIG.USESSL is
'是否使用SSL验证:0不用，1需要SSL';

comment on column MPS_MAILCONFIG.ISDEFAULT is
'缺省帐号， 0表示false, 1表示true';

comment on column MPS_MAILCONFIG.CHECKOUT is
'是否签出，执行收取操作的时候签出1,默认0为正常状态';

comment on column MPS_MAILCONFIG.CREATETIME is
'创建时间';

/*==============================================================*/
/* Table: MPS_MAILDETAIL                                        */
/*==============================================================*/
create table MPS_MAILDETAIL  (
   ID                   NUMBER(6)                       not null,
   USER_ID              INTEGER,
   COMPANY_ID           INTEGER,
   CATEGORY             NVARCHAR2(200),
   TITLE                NVARCHAR2(200),
   MAILBODY             CLOB,
   CREATETIME           DATE,
   constraint PK_MPS_MAILDETAIL primary key (ID)
);

comment on table MPS_MAILDETAIL is
'邮件详细内容';

comment on column MPS_MAILDETAIL.USER_ID is
'员工标识';

comment on column MPS_MAILDETAIL.COMPANY_ID is
'企业标识';

comment on column MPS_MAILDETAIL.CATEGORY is
'邮件分类';

comment on column MPS_MAILDETAIL.TITLE is
'邮件标题';

comment on column MPS_MAILDETAIL.MAILBODY is
'邮件内容';

comment on column MPS_MAILDETAIL.CREATETIME is
'邮件日期';

/*==============================================================*/
/* Table: MPS_MAILRECEIVE                                       */
/*==============================================================*/
create table MPS_MAILRECEIVE  (
   ID                   NUMBER(6)                       not null,
   COMPANY_ID           INTEGER,
   USER_ID              INTEGER,
   MAILCONFIG_ID        INTEGER,
   EMAIL                NVARCHAR2(50),
   MAILUID              NVARCHAR2(50),
   SENDDATE             DATE,
   TITLE                NVARCHAR2(200),
   MAILFROM             NVARCHAR2(200),
   SENDERS              NVARCHAR2(200),
   CARBONCOPY           NVARCHAR2(200),
   MAILBODY             CLOB,
   RECEIVEDDATE         DATE,
   ISDELETED            INTEGER                        default 0,
   STATUS               INTEGER                        default 0,
   DELETETIME           DATE,
   TRYCOUNT             INTEGER                        default 0,
   CHECKOUT             INTEGER,
   constraint PK_MPS_MAILRECEIVE primary key (ID)
);

comment on table MPS_MAILRECEIVE is
'邮件接收表';

comment on column MPS_MAILRECEIVE.ID is
'编号';

comment on column MPS_MAILRECEIVE.COMPANY_ID is
'企业标识';

comment on column MPS_MAILRECEIVE.USER_ID is
'员工标识';

comment on column MPS_MAILRECEIVE.MAILCONFIG_ID is
'邮箱账号ID';

comment on column MPS_MAILRECEIVE.EMAIL is
'邮箱账号';

comment on column MPS_MAILRECEIVE.MAILUID is
'邮件唯一标识';

comment on column MPS_MAILRECEIVE.SENDDATE is
'邮件发送日期';

comment on column MPS_MAILRECEIVE.TITLE is
'邮件标题';

comment on column MPS_MAILRECEIVE.MAILFROM is
'发送地址';

comment on column MPS_MAILRECEIVE.SENDERS is
'接收地址';

comment on column MPS_MAILRECEIVE.CARBONCOPY is
'抄送';

comment on column MPS_MAILRECEIVE.MAILBODY is
'邮件内容';

comment on column MPS_MAILRECEIVE.RECEIVEDDATE is
'邮件接收到本地时间';

comment on column MPS_MAILRECEIVE.ISDELETED is
'删除标记，默认为0不删除，如1为删除服务器的邮件，服务程序处理后，设置删除状态。';

comment on column MPS_MAILRECEIVE.STATUS is
'服务器删除状态：0=正常 1=成功 -1=失败';

comment on column MPS_MAILRECEIVE.DELETETIME is
'最后删除操作的时间';

comment on column MPS_MAILRECEIVE.TRYCOUNT is
'执行删除尝试的次数';

comment on column MPS_MAILRECEIVE.CHECKOUT is
'是否签出，执行删除操作的时候签出1,默认0为正常状态';

/*==============================================================*/
/* Table: MPS_MAILRECEIVETASK                                   */
/*==============================================================*/
create table MPS_MAILRECEIVETASK  (
   ID                   NUMBER(6)                       not null,
   COMPANY_ID           INTEGER,
   USER_ID              INTEGER,
   MAILCONFIG_ID        INTEGER,
   constraint PK_MPS_MAILRECEIVETASK primary key (ID)
);

comment on table MPS_MAILRECEIVETASK is
'邮件接收任务表';

comment on column MPS_MAILRECEIVETASK.ID is
'编号';

comment on column MPS_MAILRECEIVETASK.COMPANY_ID is
'企业标识';

comment on column MPS_MAILRECEIVETASK.USER_ID is
'员工标识';

comment on column MPS_MAILRECEIVETASK.MAILCONFIG_ID is
'邮箱账号ID';

/*==============================================================*/
/* Table: MPS_MAILSEND                                          */
/*==============================================================*/
create table MPS_MAILSEND  (
   ID                   NUMBER(6)                       not null,
   COMPANY_ID           INTEGER,
   USER_ID              INTEGER,
   MAILDETAIL_ID        INTEGER,
   CREATETIME           DATE,
   RECEIVERS            NVARCHAR2(200),
   CARBONCOPY           NVARCHAR2(200),
   USEMULTISENDJOB      INTEGER                        default 0,
   STATUS               INTEGER,
   REASON               NVARCHAR2(200),
   SENDTIME             DATE,
   TRYCOUNT             INTEGER                        default 0,
   CHECKOUT             INTEGER,
   MAILCONFIG_ID        NVARCHAR2(50),
   constraint PK_MPS_MAILSEND primary key (ID)
);

comment on table MPS_MAILSEND is
'邮件发送表';

comment on column MPS_MAILSEND.ID is
'编号';

comment on column MPS_MAILSEND.COMPANY_ID is
'企业标识';

comment on column MPS_MAILSEND.USER_ID is
'员工标识';

comment on column MPS_MAILSEND.MAILDETAIL_ID is
'邮件文档ID';

comment on column MPS_MAILSEND.CREATETIME is
'邮件产生日期';

comment on column MPS_MAILSEND.RECEIVERS is
'接收者名单，名单间用 ;分隔';

comment on column MPS_MAILSEND.CARBONCOPY is
'抄送名单，名单间用 ;分隔';

comment on column MPS_MAILSEND.USEMULTISENDJOB is
'使用多账户发送任务0为默认账户发送 1为多账户发送';

comment on column MPS_MAILSEND.STATUS is
'状态：1=待发 0=成功 -1=失败';

comment on column MPS_MAILSEND.REASON is
'失败原因';

comment on column MPS_MAILSEND.SENDTIME is
'最后发送时间';

comment on column MPS_MAILSEND.TRYCOUNT is
'尝试的次数';

comment on column MPS_MAILSEND.CHECKOUT is
'是否签出，执行发送操作的时候签出1,默认0为正常状态';

comment on column MPS_MAILSEND.MAILCONFIG_ID is
'发送的账号配置ID，为空则为默认账户';

/*==============================================================*/
/* Table: MPS_MAILSENDHISTORY                                   */
/*==============================================================*/
create table MPS_MAILSENDHISTORY  (
   ID                   INTEGER                         not null,
   COMPANY_ID           INTEGER,
   USER_ID              INTEGER,
   MAILDETAIL_ID        INTEGER,
   CREATETIME           DATE,
   RECEIVERS            NVARCHAR2(200),
   CARBONCOPY           NVARCHAR2(200),
   USEMULTISENDJOB      INTEGER                        default 0,
   STATUS               INTEGER,
   REASON               NVARCHAR2(200),
   SENDTIME             DATE,
   TRYCOUNT             INTEGER                        default 0,
   CHECKOUT             INTEGER,
   constraint PK_MPS_MAILSENDHISTORY primary key (ID)
);

comment on table MPS_MAILSENDHISTORY is
'邮件发送表历史（成功记录）';

comment on column MPS_MAILSENDHISTORY.ID is
'编号';

comment on column MPS_MAILSENDHISTORY.COMPANY_ID is
'企业标识';

comment on column MPS_MAILSENDHISTORY.USER_ID is
'员工标识';

comment on column MPS_MAILSENDHISTORY.MAILDETAIL_ID is
'邮件文档ID';

comment on column MPS_MAILSENDHISTORY.CREATETIME is
'邮件产生日期';

comment on column MPS_MAILSENDHISTORY.RECEIVERS is
'接收者名单，名单间用;分隔';

comment on column MPS_MAILSENDHISTORY.CARBONCOPY is
'抄送名单，名单间用 ;分隔';

comment on column MPS_MAILSENDHISTORY.USEMULTISENDJOB is
'使用多账户发送任务0为默认账户发送 1为多账户发送';

comment on column MPS_MAILSENDHISTORY.STATUS is
'状态：1=待发 0=成功 -1=失败';

comment on column MPS_MAILSENDHISTORY.REASON is
'失败原因';

comment on column MPS_MAILSENDHISTORY.SENDTIME is
'最后发送时间';

comment on column MPS_MAILSENDHISTORY.TRYCOUNT is
'尝试的次数';

comment on column MPS_MAILSENDHISTORY.CHECKOUT is
'是否签出，执行发送操作的时候签出1,默认0为正常状态';

/*==============================================================*/
/* Table: MPS_MAILUNIFIEDCONFIG                                 */
/*==============================================================*/
create table MPS_MAILUNIFIEDCONFIG  (
   ID                   NUMBER(6)                       not null,
   COMPANY_ID           INTEGER,
   USER_ID              INTEGER,
   UNIFIEDREPLYTO       NVARCHAR2(100),
   NOFITYEMAIL          NVARCHAR2(100),
   UNIFIEDDISPLAYNAME   NVARCHAR2(100),
   EMAILTAIL            NVARCHAR2(255),
   HTMLALTERNATETEXT    NVARCHAR2(255),
   constraint PK_MPS_MAILUNIFIEDCONFIG primary key (ID)
);

comment on table MPS_MAILUNIFIEDCONFIG is
'邮件统一发送的配置信息';

comment on column MPS_MAILUNIFIEDCONFIG.ID is
'编号';

comment on column MPS_MAILUNIFIEDCONFIG.COMPANY_ID is
'企业标识';

comment on column MPS_MAILUNIFIEDCONFIG.USER_ID is
'员工标识';

comment on column MPS_MAILUNIFIEDCONFIG.UNIFIEDREPLYTO is
'统一回复地址';

comment on column MPS_MAILUNIFIEDCONFIG.NOFITYEMAIL is
'回执消息通知邮件地址';

comment on column MPS_MAILUNIFIEDCONFIG.UNIFIEDDISPLAYNAME is
'账户发送显示名称';

comment on column MPS_MAILUNIFIEDCONFIG.EMAILTAIL is
'邮件尾部信息';

comment on column MPS_MAILUNIFIEDCONFIG.HTMLALTERNATETEXT is
'HTML邮件的文本说明';



----通讯录人员管理脚本
/*==============================================================*/
/* Table: TB_ADDRESS                                            */
/*==============================================================*/
create table TB_ADDRESS  (
   ID                   NVARCHAR2(50)                   not null,
   ADDRESSTYPE          NVARCHAR2(50)                  default '0',
   NAME                 NVARCHAR2(255),
   SEX                  NVARCHAR2(50),
   BIRTHDATE            DATE,
   MOBILE               NVARCHAR2(255),
   EMAIL                NVARCHAR2(50),
   QQ                   NVARCHAR2(50),
   HOMETELEPHONE        NVARCHAR2(255),
   OFFICETELEPHONE      NVARCHAR2(255),
   HOMEADDRESS          NVARCHAR2(255),
   OFFICEADDRESS        NVARCHAR2(255),
   FAX                  NVARCHAR2(50),
   COMPANY              NVARCHAR2(255),
   DEPT                 NVARCHAR2(255),
   OTHER                CLOB,
   NOTE                 CLOB,
   CREATOR              NVARCHAR2(50),
   CREATETIME           DATE,
   EDITOR               NVARCHAR2(50),
   EDITTIME             DATE,
   DEPT_ID              NVARCHAR2(50),
   COMPANY_ID           NVARCHAR2(50),
   constraint PK_TB_ADDRESS primary key (ID)
);

comment on table TB_ADDRESS is
'通讯录联系人';

comment on column TB_ADDRESS.ADDRESSTYPE is
'通讯录类型[个人,公司]';

comment on column TB_ADDRESS.NAME is
'姓名';

comment on column TB_ADDRESS.SEX is
'性别';

comment on column TB_ADDRESS.BIRTHDATE is
'出生日期';

comment on column TB_ADDRESS.MOBILE is
'手机';

comment on column TB_ADDRESS.EMAIL is
'电子邮箱';

comment on column TB_ADDRESS.QQ is
'QQ';

comment on column TB_ADDRESS.HOMETELEPHONE is
'家庭电话';

comment on column TB_ADDRESS.OFFICETELEPHONE is
'办公电话';

comment on column TB_ADDRESS.HOMEADDRESS is
'家庭住址';

comment on column TB_ADDRESS.OFFICEADDRESS is
'办公地址';

comment on column TB_ADDRESS.FAX is
'传真号码';

comment on column TB_ADDRESS.COMPANY is
'公司单位';

comment on column TB_ADDRESS.DEPT is
'部门';

comment on column TB_ADDRESS.OTHER is
'其他';

comment on column TB_ADDRESS.NOTE is
'备注';

comment on column TB_ADDRESS.CREATOR is
'创建人';

comment on column TB_ADDRESS.CREATETIME is
'创建时间';

comment on column TB_ADDRESS.EDITOR is
'编辑人';

comment on column TB_ADDRESS.EDITTIME is
'编辑时间';

comment on column TB_ADDRESS.DEPT_ID is
'所属部门';

comment on column TB_ADDRESS.COMPANY_ID is
'所属公司';

/*==============================================================*/
/* Table: TB_ADDRESSGROUP                                       */
/*==============================================================*/
create table TB_ADDRESSGROUP  (
   ID                   NVARCHAR2(50)                   not null,
   PID                  NVARCHAR2(50),
   ADDRESSTYPE          NVARCHAR2(50)                  default '0',
   NAME                 NVARCHAR2(255),
   NOTE                 CLOB,
   SEQ                  NVARCHAR2(50),
   CREATOR              NVARCHAR2(50),
   CREATETIME           DATE,
   EDITOR               NVARCHAR2(50),
   EDITTIME             DATE,
   DEPT_ID              NVARCHAR2(50),
   COMPANY_ID           NVARCHAR2(50),
   constraint PK_TB_ADDRESSGROUP primary key (ID)
);

comment on table TB_ADDRESSGROUP is
'通讯录分组';

comment on column TB_ADDRESSGROUP.PID is
'父ID';

comment on column TB_ADDRESSGROUP.ADDRESSTYPE is
'通讯录类型[个人,公司]';

comment on column TB_ADDRESSGROUP.NAME is
'分组名称';

comment on column TB_ADDRESSGROUP.NOTE is
'备注';

comment on column TB_ADDRESSGROUP.SEQ is
'排序序号';

comment on column TB_ADDRESSGROUP.CREATOR is
'创建人';

comment on column TB_ADDRESSGROUP.CREATETIME is
'创建时间';

comment on column TB_ADDRESSGROUP.EDITOR is
'编辑人';

comment on column TB_ADDRESSGROUP.EDITTIME is
'编辑时间';

comment on column TB_ADDRESSGROUP.DEPT_ID is
'所属部门';

comment on column TB_ADDRESSGROUP.COMPANY_ID is
'所属公司';

/*==============================================================*/
/* Table: TB_ADDRESSGROUP_ADDRESS                               */
/*==============================================================*/
create table TB_ADDRESSGROUP_ADDRESS  (
   GROUP_ID             NVARCHAR2(50)                   not null,
   ADDRESS_ID           NVARCHAR2(50)                   not null,
   constraint PK_TB_ADDRESSGROUP_ADDRESS primary key (GROUP_ID, ADDRESS_ID)
);

comment on table TB_ADDRESSGROUP_ADDRESS is
'通讯录分组和联系人关联表';


----人员管理表脚本
/*==============================================================*/
/* Table: TB_STAFF                                              */
/*==============================================================*/
create table TB_STAFF  (
   ID                   NVARCHAR2(50)                   not null,
   NAME                 NVARCHAR2(50),
   SEX                  NVARCHAR2(50),
   BIRTHDATE            DATE,
   POLITICAL            NVARCHAR2(50),
   PARTYDATE            DATE,
   NATIONALITY          NVARCHAR2(50),
   NATIVEPLACE          NVARCHAR2(150),
   OFFICIALRANK         NVARCHAR2(50),
   SERVINGDATE          NVARCHAR2(50),
   WORKINGDATE          NVARCHAR2(50),
   HIGHESTEDUCATION     NVARCHAR2(50),
   EDUCATIONDATE        NVARCHAR2(50),
   HIGHESTDEGREE        NVARCHAR2(50),
   DEGREEDATE           NVARCHAR2(50),
   MARRIAGESTATUS       NVARCHAR2(50),
   TITLES               NVARCHAR2(50),
   TITLESDATE           NVARCHAR2(50),
   CHILDSTATUS          NVARCHAR2(50),
   USERIDENTITY         NVARCHAR2(50),
   EMAIL                NVARCHAR2(50),
   MOBILE               NVARCHAR2(50),
   OFFICEPHONE          NVARCHAR2(50),
   HOMEPHONE            NVARCHAR2(50),
   ACADEMIC             CLOB,
   RESEARCH             CLOB,
   INTRODUCE            CLOB,
   NOTE                 CLOB,
   PORTRAINT            CLOB,
   ATTACHGUID           NVARCHAR2(50),
   CHECKUSER            NVARCHAR2(50),
   CREATOR              NVARCHAR2(50),
   CREATETIME           DATE,
   EDITOR               NVARCHAR2(50),
   EDITTIME             DATE,
   DEPT_ID              NVARCHAR2(50),
   COMPANY_ID           NVARCHAR2(50),
   constraint PK_TB_STAFF primary key (ID)
);

comment on table TB_STAFF is
'人员基本信息';

comment on column TB_STAFF.NAME is
'姓名';

comment on column TB_STAFF.SEX is
'性别';

comment on column TB_STAFF.BIRTHDATE is
'出生时间';

comment on column TB_STAFF.POLITICAL is
'政治面貌';

comment on column TB_STAFF.PARTYDATE is
'党团时间';

comment on column TB_STAFF.NATIONALITY is
'民族';

comment on column TB_STAFF.NATIVEPLACE is
'籍贯';

comment on column TB_STAFF.OFFICIALRANK is
'职务';

comment on column TB_STAFF.SERVINGDATE is
'任职时间';

comment on column TB_STAFF.WORKINGDATE is
'工作时间';

comment on column TB_STAFF.HIGHESTEDUCATION is
'最高学历';

comment on column TB_STAFF.EDUCATIONDATE is
'获学历时间';

comment on column TB_STAFF.HIGHESTDEGREE is
'最高学位';

comment on column TB_STAFF.DEGREEDATE is
'获学位时间';

comment on column TB_STAFF.MARRIAGESTATUS is
'婚否';

comment on column TB_STAFF.TITLES is
'职称';

comment on column TB_STAFF.TITLESDATE is
'职称时间';

comment on column TB_STAFF.CHILDSTATUS is
'是否独生子女';

comment on column TB_STAFF.USERIDENTITY is
'身份';

comment on column TB_STAFF.EMAIL is
'电子邮箱';

comment on column TB_STAFF.MOBILE is
'手机';

comment on column TB_STAFF.OFFICEPHONE is
'办公电话';

comment on column TB_STAFF.HOMEPHONE is
'家庭电话';

comment on column TB_STAFF.ACADEMIC is
'学术任职';

comment on column TB_STAFF.RESEARCH is
'研究方向';

comment on column TB_STAFF.INTRODUCE is
'个人介绍';

comment on column TB_STAFF.NOTE is
'备注信息';

comment on column TB_STAFF.PORTRAINT is
'个人照片';

comment on column TB_STAFF.ATTACHGUID is
'个人资料';

comment on column TB_STAFF.CHECKUSER is
'资料核对';

comment on column TB_STAFF.CREATOR is
'创建人';

comment on column TB_STAFF.CREATETIME is
'创建时间';

comment on column TB_STAFF.EDITOR is
'编辑人';

comment on column TB_STAFF.EDITTIME is
'编辑时间';

comment on column TB_STAFF.DEPT_ID is
'所属部门';

comment on column TB_STAFF.COMPANY_ID is
'所属公司';

/*==============================================================*/
/* Table: TB_STAFFABROAD                                        */
/*==============================================================*/
create table TB_STAFFABROAD  (
   ID                   NVARCHAR2(50)                   not null,
   STAFF_ID             NVARCHAR2(50),
   STARTDATE            NVARCHAR2(50),
   ENDDATE              NVARCHAR2(50),
   COUNTRY              NVARCHAR2(255),
   SERVEUNIT            NVARCHAR2(255),
   ABROADTYPE           NVARCHAR2(50),
   SEQ                  INTEGER,
   constraint PK_TB_STAFFABROAD primary key (ID)
);

comment on table TB_STAFFABROAD is
'人员出国情况';

comment on column TB_STAFFABROAD.STAFF_ID is
'人员ID';

comment on column TB_STAFFABROAD.STARTDATE is
'起始年月';

comment on column TB_STAFFABROAD.ENDDATE is
'截止年月';

comment on column TB_STAFFABROAD.COUNTRY is
'国别';

comment on column TB_STAFFABROAD.SERVEUNIT is
'工作（学习）单位';

comment on column TB_STAFFABROAD.ABROADTYPE is
'出国类型';

comment on column TB_STAFFABROAD.SEQ is
'序号';

/*==============================================================*/
/* Table: TB_STAFFAWARD                                         */
/*==============================================================*/
create table TB_STAFFAWARD  (
   ID                   NVARCHAR2(50)                   not null,
   STAFF_ID             NVARCHAR2(50),
   NOTE                 BLOB,
   ATTACHGUID           NVARCHAR2(50),
   SEQ                  INTEGER,
   constraint PK_TB_STAFFAWARD primary key (ID)
);

comment on table TB_STAFFAWARD is
'人员受奖情况';

comment on column TB_STAFFAWARD.STAFF_ID is
'人员ID';

comment on column TB_STAFFAWARD.NOTE is
'受奖情况';

comment on column TB_STAFFAWARD.ATTACHGUID is
'相关资料';

comment on column TB_STAFFAWARD.SEQ is
'序号';

/*==============================================================*/
/* Table: TB_STAFFBUSINESSTRIP                                  */
/*==============================================================*/
create table TB_STAFFBUSINESSTRIP  (
   ID                   NVARCHAR2(50)                   not null,
   STAFF_ID             NVARCHAR2(50)                   not null,
   STARTDATE            NVARCHAR2(50),
   ENDDATE              NVARCHAR2(50),
   DESTINATION          NVARCHAR2(255),
   COUNTRY              NVARCHAR2(255),
   REASON               NVARCHAR2(255),
   NOTE                 CLOB,
   SEQ                  INTEGER,
   constraint PK_TB_STAFFBUSINESSTRIP primary key (ID)
);

comment on table TB_STAFFBUSINESSTRIP is
'人员出差情况';

comment on column TB_STAFFBUSINESSTRIP.STAFF_ID is
'人员ID';

comment on column TB_STAFFBUSINESSTRIP.STARTDATE is
'起始年月';

comment on column TB_STAFFBUSINESSTRIP.ENDDATE is
'截止年月';

comment on column TB_STAFFBUSINESSTRIP.DESTINATION is
'出差目的地';

comment on column TB_STAFFBUSINESSTRIP.COUNTRY is
'国别';

comment on column TB_STAFFBUSINESSTRIP.REASON is
'出差原因';

comment on column TB_STAFFBUSINESSTRIP.NOTE is
'备注';

comment on column TB_STAFFBUSINESSTRIP.SEQ is
'序号';

/*==============================================================*/
/* Table: TB_STAFFFAMILY                                        */
/*==============================================================*/
create table TB_STAFFFAMILY  (
   ID                   NVARCHAR2(50)                   not null,
   STAFF_ID             NVARCHAR2(50),
   NAME                 NVARCHAR2(50),
   RELATION             NVARCHAR2(50),
   SEX                  NVARCHAR2(50),
   BIRTHDATE            DATE,
   POLITICAL            NVARCHAR2(50),
   SERVEUNIT            NVARCHAR2(255),
   SEQ                  INTEGER,
   constraint PK_TB_STAFFFAMILY primary key (ID)
);

comment on table TB_STAFFFAMILY is
'人员家庭情况';

comment on column TB_STAFFFAMILY.STAFF_ID is
'人员ID';

comment on column TB_STAFFFAMILY.NAME is
'姓名';

comment on column TB_STAFFFAMILY.RELATION is
'关系';

comment on column TB_STAFFFAMILY.SEX is
'性别';

comment on column TB_STAFFFAMILY.BIRTHDATE is
'出生时间';

comment on column TB_STAFFFAMILY.POLITICAL is
'政治面貌';

comment on column TB_STAFFFAMILY.SERVEUNIT is
'工作（学习）单位';

comment on column TB_STAFFFAMILY.SEQ is
'序号';

/*==============================================================*/
/* Table: TB_STAFFPICTURE                                       */
/*==============================================================*/
create table TB_STAFFPICTURE  (
   ID                   NVARCHAR2(50)                   not null,
   STAFF_ID             NVARCHAR2(50),
   CATEGORY             NVARCHAR2(50),
   NOTE                 CLOB,
   ATTACHGUID           NVARCHAR2(50),
   constraint PK_TB_STAFFPICTURE primary key (ID)
);

comment on table TB_STAFFPICTURE is
'人员图片信息';

comment on column TB_STAFFPICTURE.STAFF_ID is
'人员ID';

comment on column TB_STAFFPICTURE.CATEGORY is
'类别';

comment on column TB_STAFFPICTURE.NOTE is
'说明信息';

comment on column TB_STAFFPICTURE.ATTACHGUID is
'相关资料';

/*==============================================================*/
/* Table: TB_STAFFRESEARCH                                      */
/*==============================================================*/
create table TB_STAFFRESEARCH  (
   ID                   NVARCHAR2(50)                   not null,
   STAFF_ID             NVARCHAR2(50),
   CATEGORY             NVARCHAR2(50),
   NOTE                 CLOB,
   ATTACHGUID           NVARCHAR2(50),
   SEQ                  INTEGER,
   constraint PK_TB_STAFFRESEARCH primary key (ID)
);

comment on table TB_STAFFRESEARCH is
'人员科研情况';

comment on column TB_STAFFRESEARCH.STAFF_ID is
'人员ID';

comment on column TB_STAFFRESEARCH.CATEGORY is
'类别（获奖、课题、专利、文章）';

comment on column TB_STAFFRESEARCH.NOTE is
'科研情况';

comment on column TB_STAFFRESEARCH.ATTACHGUID is
'相关资料';

comment on column TB_STAFFRESEARCH.SEQ is
'序号';

/*==============================================================*/
/* Table: TB_STAFFRESUME                                        */
/*==============================================================*/
create table TB_STAFFRESUME  (
   ID                   NVARCHAR2(50)                   not null,
   STAFF_ID             NVARCHAR2(50),
   STARTDATE            NVARCHAR2(50),
   ENDDATE              NVARCHAR2(50),
   SERVECOMPANY         NVARCHAR2(255),
   OFFICERANK           NVARCHAR2(50),
   SEQ                  INTEGER,
   constraint PK_TB_STAFFRESUME primary key (ID)
);

comment on table TB_STAFFRESUME is
'人员履历情况';

comment on column TB_STAFFRESUME.STAFF_ID is
'人员ID';

comment on column TB_STAFFRESUME.STARTDATE is
'起始年月';

comment on column TB_STAFFRESUME.ENDDATE is
'毕业年月';

comment on column TB_STAFFRESUME.SERVECOMPANY is
'工作单位';

comment on column TB_STAFFRESUME.OFFICERANK is
'职务';

comment on column TB_STAFFRESUME.SEQ is
'序号';

/*==============================================================*/
/* Table: TB_STAFFROTATION                                      */
/*==============================================================*/
create table TB_STAFFROTATION  (
   ID                   NVARCHAR2(50)                   not null,
   STAFF_ID             NVARCHAR2(50)                   not null,
   STARTDATE            NVARCHAR2(50),
   ENDDATE              NVARCHAR2(50),
   DEPARTMENT           NVARCHAR2(255),
   SUBSPECIALTY         NVARCHAR2(255),
   WITNESS              NVARCHAR2(50),
   WITNESSPHONE         NVARCHAR2(50),
   NOTE                 CLOB,
   SEQ                  INTEGER,
   constraint PK_TB_STAFFROTATION primary key (ID)
);

comment on table TB_STAFFROTATION is
'人员轮转况';

comment on column TB_STAFFROTATION.STAFF_ID is
'人员ID';

comment on column TB_STAFFROTATION.STARTDATE is
'起始年月';

comment on column TB_STAFFROTATION.ENDDATE is
'截止年月';

comment on column TB_STAFFROTATION.DEPARTMENT is
'轮转科室';

comment on column TB_STAFFROTATION.SUBSPECIALTY is
'轮转亚专业';

comment on column TB_STAFFROTATION.WITNESS is
'证明人';

comment on column TB_STAFFROTATION.WITNESSPHONE is
'证明人电话';

comment on column TB_STAFFROTATION.NOTE is
'备注';

comment on column TB_STAFFROTATION.SEQ is
'序号';

/*==============================================================*/
/* Table: TB_STAFFSTUDY                                         */
/*==============================================================*/
create table TB_STAFFSTUDY  (
   ID                   NVARCHAR2(50)                   not null,
   STAFF_ID             NVARCHAR2(50),
   STARTDATE            NVARCHAR2(50),
   ENDDATE              NVARCHAR2(50),
   SCHOOL               NVARCHAR2(255),
   SPECIALTY            NVARCHAR2(50),
   EDUCATION            NVARCHAR2(50),
   DEGREE               NVARCHAR2(50),
   ISFULLTIME           NVARCHAR2(50),
   SEQ                  INTEGER,
   constraint PK_TB_STAFFSTUDY primary key (ID)
);

comment on table TB_STAFFSTUDY is
'人员学习情况';

comment on column TB_STAFFSTUDY.STAFF_ID is
'人员ID';

comment on column TB_STAFFSTUDY.STARTDATE is
'起始年月';

comment on column TB_STAFFSTUDY.ENDDATE is
'截止年月';

comment on column TB_STAFFSTUDY.SCHOOL is
'毕业院校';

comment on column TB_STAFFSTUDY.SPECIALTY is
'所学专业';

comment on column TB_STAFFSTUDY.EDUCATION is
'学历';

comment on column TB_STAFFSTUDY.DEGREE is
'学位';

comment on column TB_STAFFSTUDY.ISFULLTIME is
'是否全日制';

comment on column TB_STAFFSTUDY.SEQ is
'序号';

/*==============================================================*/
/* Table: TB_STAFFTITLES                                        */
/*==============================================================*/
create table TB_STAFFTITLES  (
   ID                   NVARCHAR2(50)                   not null,
   STAFF_ID             NVARCHAR2(50)                   not null,
   TITLES               NVARCHAR2(255),
   OBTAINDATE           NVARCHAR2(50),
   APPOINTDATE          NVARCHAR2(50),
   SEQ                  INTEGER,
   constraint PK_TB_STAFFTITLES primary key (ID)
);

comment on table TB_STAFFTITLES is
'人员职称情况';

comment on column TB_STAFFTITLES.STAFF_ID is
'人员ID';

comment on column TB_STAFFTITLES.TITLES is
'职称';

comment on column TB_STAFFTITLES.OBTAINDATE is
'取得资格年月';

comment on column TB_STAFFTITLES.APPOINTDATE is
'任命年月';

comment on column TB_STAFFTITLES.SEQ is
'序号';

/*==============================================================*/
/* Table: TB_STAFFVACATION                                      */
/*==============================================================*/
create table TB_STAFFVACATION  (
   ID                   NVARCHAR2(50)                   not null,
   STAFF_ID             NVARCHAR2(50),
   STARTDATE            NVARCHAR2(50),
   ENDDATE              NVARCHAR2(50),
   VACATIONLOCATION     NVARCHAR2(255),
   COUNTRY              NVARCHAR2(255),
   EMERGENCYPHONE       NVARCHAR2(50),
   NOTE                 CLOB,
   SEQ                  INTEGER,
   constraint PK_TB_STAFFVACATION primary key (ID)
);

comment on table TB_STAFFVACATION is
'人员休假情况';

comment on column TB_STAFFVACATION.STAFF_ID is
'人员ID';

comment on column TB_STAFFVACATION.STARTDATE is
'起始年月';

comment on column TB_STAFFVACATION.ENDDATE is
'截止年月';

comment on column TB_STAFFVACATION.VACATIONLOCATION is
'休假地点';

comment on column TB_STAFFVACATION.COUNTRY is
'国别';

comment on column TB_STAFFVACATION.EMERGENCYPHONE is
'紧急联系电话';

comment on column TB_STAFFVACATION.NOTE is
'备注';

comment on column TB_STAFFVACATION.SEQ is
'序号';


/*==============================================================*/
/* Table: T_Customer                                          */
/*==============================================================*/
create table T_CUSTOMER  (
   ID                   NVARCHAR2(50)                   not null,
   Name               NVARCHAR2(50),
   Age                INTEGER,
   Creator            NVARCHAR2(50),
   CreateTime         DATE,
   constraint PK_T_CUSTOMER primary key (ID)
);

comment on table T_CUSTOMER is
'客户信息';

comment on column T_CUSTOMER.ID is
'编号';

comment on column T_CUSTOMER.NAME is
'姓名';

comment on column T_CUSTOMER.AGE is
'年龄';

comment on column T_CUSTOMER.CREATOR is
'创建人';

comment on column T_CUSTOMER.CREATETIME is
'创建时间';



alter table TB_ADDRESSGROUP_ADDRESS
   add constraint FK_CONTACT_REF_GROUP foreign key (GROUP_ID)
      references TB_ADDRESSGROUP (ID);

alter table TB_ADDRESSGROUP_ADDRESS
   add constraint FK_TB_ADDRE_REFERENCE_TB_ADDRE foreign key (ADDRESS_ID)
      references TB_ADDRESS (ID);

alter table TB_STAFFABROAD
   add constraint "FK_TB_STAFF_REF_TB_STAFFAbroad" foreign key (STAFF_ID)
      references TB_STAFF (ID);

alter table TB_STAFFAWARD
   add constraint "FK_TB_STAFF_REF_TB_STAFFAward" foreign key (STAFF_ID)
      references TB_STAFF (ID);

alter table TB_STAFFBUSINESSTRIP
   add constraint "FK_TB_STAFFTrip_REF_TB_STAFF" foreign key (STAFF_ID)
      references TB_STAFF (ID);

alter table TB_STAFFFAMILY
   add constraint "FK_TB_STAFF_REF_TB_STAFFFamily" foreign key (STAFF_ID)
      references TB_STAFF (ID);

alter table TB_STAFFPICTURE
   add constraint FK_TB_STAFF_REF_STAFFPICTURE foreign key (STAFF_ID)
      references TB_STAFF (ID);

alter table TB_STAFFRESEARCH
   add constraint "FK_TB_STAFF_REF_STAFFResearch" foreign key (STAFF_ID)
      references TB_STAFF (ID);

alter table TB_STAFFRESUME
   add constraint "FK_TB_STAFF_REF_STAFFResume" foreign key (STAFF_ID)
      references TB_STAFF (ID);

alter table TB_STAFFROTATION
   add constraint "FK_TB_STAFFRotation_REF_STAFF" foreign key (STAFF_ID)
      references TB_STAFF (ID);

alter table TB_STAFFSTUDY
   add constraint "FK_TB_STAFF_REF_STAFFStudy" foreign key (STAFF_ID)
      references TB_STAFF (ID);

alter table TB_STAFFTITLES
   add constraint "FK_TB_STAFF_REF_STAFFTitles" foreign key (STAFF_ID)
      references TB_STAFF (ID);

alter table TB_STAFFVACATION
   add constraint "FK_TB_STAFFVacation_REF_STAFF" foreign key (STAFF_ID)
      references TB_STAFF (ID);
