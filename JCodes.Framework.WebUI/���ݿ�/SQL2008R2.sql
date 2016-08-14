DROP TABLE tbBug;
GO

DROP TABLE tbButton;
GO

DROP TABLE tbDepartment;
GO

DROP TABLE tbLoginLog;
GO

DROP TABLE tbMenu;
GO

DROP TABLE tbMenuButton;
GO

DROP TABLE tbOrder;
GO

DROP TABLE tbRole;
GO

DROP TABLE tbRoleMenuButton;
GO

DROP TABLE tbUser;
GO

DROP TABlE tbUserDepartment;
GO

DROP TABLE tbUSerOperateLog;
GO

DROP TABLE tbUserRole;
GO

/****** Object:  Table [dbo].[tbUserRole]    Script Date: 03/29/2014 11:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbUserRole](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	CONSTRAINT [PK_tbUserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)
) ON [PRIMARY]
GO
INSERT [dbo].[tbUserRole] ([UserId], [RoleId]) VALUES (72, 1)
/****** Object:  Table [dbo].[tbUserOperateLog]    Script Date: 03/29/2014 11:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbUserOperateLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[UserIp] [nvarchar](50) NULL,
	[OperateInfo] [nvarchar](64) NULL,
	[Description] [nvarchar](max) NULL,
	[IfSuccess] [bit] NULL,
	[OperateDate] [datetime] NULL,
 CONSTRAINT [PK_tbUserOperateInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tbUserOperateLog] ON
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (1, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2E100C720FF AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (2, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2E100C72495 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (3, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E100C7263C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (4, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E100C728D6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (5, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2E100C72AAD AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (6, N'admin', N'::1', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E100C738E5 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (7, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E100C73AA3 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (8, N'admin', N'::1', N'添加角色', N'添加成功，角色主键：53', 1, CAST(0x0000A2E100C86879 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (9, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E100C868A4 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (10, N'admin', N'::1', N'查询我的信息', N'查询我的信息', 1, CAST(0x0000A2E100F1C3E3 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (11, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2E80143D4B9 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (12, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2E80143DB44 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (13, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2E80143DB69 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (14, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E80143E307 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (15, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E80143F171 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (16, N'admin', N'::1', N'设置用户角色', N'设置成功，用户主键：75 角色主键：53', 1, CAST(0x0000A2E801440C3D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (17, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E801440C67 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (18, N'admin', N'::1', N'设置用户角色', N'设置成功，用户主键：75 角色主键：53', 1, CAST(0x0000A2E80144251A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (19, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E801442552 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (20, N'admin', N'::1', N'查询角色用户', N'查询角色Id：53 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E801443090 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (21, N'admin', N'::1', N'角色授权', N'授权成功，菜单/按钮Id：5 1,5 3,5 4,5 5,5 11,5 12', 1, CAST(0x0000A2E801444C10 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (22, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E801457D7B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (23, N'admin', N'::1', N'修改用户', N'修改成功，用户主键：75', 1, CAST(0x0000A2E801458BE8 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (24, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E801458C09 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (25, N'admin', N'::1', N'删除用户', N'删除成功，用户主键：75', 1, CAST(0x0000A2E801459214 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (26, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E801459230 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (27, N'admin', N'::1', N'添加用户', N'添加成功，用户主键：76', 1, CAST(0x0000A2E80145A44D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (28, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E80145A480 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (29, N'admin', N'::1', N'查询我的信息', N'查询我的信息', 1, CAST(0x0000A2E80145D3D4 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (30, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E80145E2F3 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (31, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E80146433B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (32, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E80146450F AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (33, N'admin', N'::1', N'查询角色用户', N'查询角色Id：1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E801464C5C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (34, N'admin', N'::1', N'查询角色用户', N'查询角色Id：52 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E801464EA9 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (35, N'admin', N'::1', N'查询角色用户', N'查询角色Id：53 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E801464F3F AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (36, N'admin', N'::1', N'设置用户角色', N'设置成功，用户主键：76 角色主键：53', 1, CAST(0x0000A2E80146614A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (37, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E801466179 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (38, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2E801466559 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (39, N'test', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2E801467C52 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (40, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2E80146D2C5 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (41, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2E80146D7DA AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (42, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E80146DBA4 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (43, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E80146E26E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (44, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2E80146E340 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (45, N'admin', N'::1', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E80146E4D5 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (46, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E80146F7CC AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (47, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014711E2 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (48, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E801471294 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (49, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E80147132B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (50, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014713C1 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (51, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：RoleName asc 页码/每页大小：1 20', 1, CAST(0x0000A2E80147160B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (52, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：RoleName desc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014716AA AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (53, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：RoleName asc 页码/每页大小：1 20', 1, CAST(0x0000A2E801471745 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (54, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014717D1 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (55, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：ModifyDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E80147186C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (56, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：Description asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014718EA AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (57, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：Description asc 页码/每页大小：1 20', 1, CAST(0x0000A2E801471BE7 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (58, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014AED26 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (59, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014AF7A7 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (60, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014BF8DC AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (61, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014BFD11 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (62, N'admin', N'::1', N'查询角色用户', N'查询角色Id：1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014C04C6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (63, N'admin', N'::1', N'角色功能异常', N'输入字符串的格式不正确。', 0, CAST(0x0000A2E8014C0EC4 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (64, N'admin', N'::1', N'角色功能异常', N'输入字符串的格式不正确。', 0, CAST(0x0000A2E8014C155F AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (65, N'admin', N'::1', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014C67A9 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (66, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014C6A9F AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (67, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2E8014C7DC0 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (68, N'admin', N'::1', N'查询角色用户', N'查询角色Id：52 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014C8D5F AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (69, N'admin', N'::1', N'查询角色用户', N'查询角色Id：1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014C9993 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (70, N'admin', N'::1', N'查询角色用户', N'查询角色Id：53 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014C9A82 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (71, N'admin', N'::1', N'角色功能异常', N'输入字符串的格式不正确。', 0, CAST(0x0000A2E8014CAF68 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (72, N'admin', N'::1', N'角色授权', N'授权成功，菜单/按钮Id：7 1,7 3,7 4,7 5,3 1,3 3,3 4,3 5,3 10,4 1,4 3,4 4,4 5,4 8,4 7,5 1,5 3,5 4,5 5,5 11,5 12,8 1,8 6,9 1,10 1,10 3,10 4,10 6', 1, CAST(0x0000A2E8014CBCB0 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (73, N'admin', N'::1', N'查询角色用户', N'查询角色Id：1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014CC1ED AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (74, N'admin', N'::1', N'查询角色用户', N'查询角色Id：53 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014CE6FE AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (75, N'admin', N'::1', N'查询角色用户', N'查询角色Id：52 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014CE78E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (76, N'admin', N'::1', N'查询角色用户', N'查询角色Id：1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2E8014CE803 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (77, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2E8014CEDBB AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (78, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2EC0106E6E3 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (79, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC0106EA5C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (80, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC0106EC4A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (81, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC0106EE09 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (82, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2EC010984F0 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (83, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC010986FA AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (84, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC01098879 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (85, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC010988D7 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (86, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC01098A60 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (87, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC01098DF8 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (88, N'admin', N'::1', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC01098F9C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (89, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC010C3F7E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (90, N'admin', N'::1', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC010C4184 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (91, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2EC010C4324 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (92, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC010C4520 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (93, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC010C461D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (94, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC010C4682 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (95, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC010C4742 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (96, N'admin', N'::1', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC0110192B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (97, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC01101E28 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (98, N'admin', N'::1', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC01106A34 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (99, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC01107504 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (100, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC011187D5 AS DateTime))
GO
print 'Processed 100 total records'
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (101, N'admin', N'::1', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC0111B98D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (102, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC01135979 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (103, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC011596AC AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (104, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC01159B34 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (105, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC0115B701 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (106, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC0115C32F AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (107, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC011628DB AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (108, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC0117CFD7 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (109, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC0117DE31 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (110, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC0119DDC7 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (111, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC0119E497 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (112, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC011AA88B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (113, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC011B4DE1 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (114, N'admin', N'::1', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC011B912E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (115, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC011E3743 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (116, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC011F1B9C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (117, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC01213C5D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (118, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC012140B0 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (119, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC013913E7 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (120, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2EC013914BA AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (121, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EC013919E9 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (122, N'admin', N'::1', N'按钮功能异常', N'列名 ''Sort'' 无效。', 0, CAST(0x0000A2EF010FA14A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (123, N'admin', N'::1', N'按钮功能异常', N'列名 ''Name'' 无效。', 0, CAST(0x0000A2EF011147A8 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (124, N'admin', N'::1', N'按钮功能异常', N'列名 ''Sort'' 无效。', 0, CAST(0x0000A2EF011252D3 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (125, N'admin', N'::1', N'按钮功能异常', N'列名 ''Sort'' 无效。', 0, CAST(0x0000A2EF0112B671 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (126, N'admin', N'::1', N'按钮功能异常', N'列名 ''Icon'' 无效。', 0, CAST(0x0000A2EF0112B9C6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (127, N'admin', N'::1', N'按钮功能异常', N'列名 ''Code'' 无效。', 0, CAST(0x0000A2EF0112BA4E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (128, N'admin', N'::1', N'按钮功能异常', N'列名 ''Icon'' 无效。', 0, CAST(0x0000A2EF0112BB17 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (129, N'admin', N'::1', N'按钮功能异常', N'列名 ''Name'' 无效。', 0, CAST(0x0000A2EF0112BB70 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (130, N'admin', N'::1', N'按钮功能异常', N'列名 ''Sort'' 无效。', 0, CAST(0x0000A2EF01134BB6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (131, N'admin', N'::1', N'按钮功能异常', N'列名 ''Sort'' 无效。', 0, CAST(0x0000A2EF01173A0E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (132, N'admin', N'::1', N'按钮功能异常', N'列名 ''Sort'' 无效。', 0, CAST(0x0000A2EF0117716A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (133, N'admin', N'::1', N'按钮功能异常', N'列名 ''Sort'' 无效。', 0, CAST(0x0000A2EF011784CE AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (134, N'admin', N'::1', N'按钮功能异常', N'列名 ''Sort'' 无效。', 0, CAST(0x0000A2EF01179CF6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (135, N'admin', N'::1', N'按钮功能异常', N'列名 ''Sort'' 无效。', 0, CAST(0x0000A2EF0117B4AE AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (136, N'admin', N'::1', N'按钮功能异常', N'列名 ''Sort'' 无效。', 0, CAST(0x0000A2EF0117C022 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (137, N'admin', N'::1', N'按钮功能异常', N'列名 ''Sort'' 无效。', 0, CAST(0x0000A2EF0118CDF2 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (138, N'admin', N'::1', N'按钮功能异常', N'列名 ''Sort'' 无效。', 0, CAST(0x0000A2EF0118E720 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (139, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF011975AE AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (140, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01197DAD AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (141, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0119AB7E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (142, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF011A25D2 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (143, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF011AB4F9 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (144, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF011ADFD9 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (145, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF011AECAD AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (146, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF011AFDB3 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (147, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF011B08B4 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (148, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF012221A4 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (149, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF012306F9 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (150, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01287D29 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (151, N'admin', N'::1', N'按钮功能异常', N'输入字符串的格式不正确。', 0, CAST(0x0000A2EF0128B82A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (152, N'admin', N'::1', N'按钮功能异常', N'输入字符串的格式不正确。', 0, CAST(0x0000A2EF0128CACD AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (153, N'admin', N'::1', N'按钮功能异常', N'输入字符串的格式不正确。', 0, CAST(0x0000A2EF0128CF66 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (154, N'admin', N'::1', N'按钮功能异常', N'输入字符串的格式不正确。', 0, CAST(0x0000A2EF0128D31F AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (155, N'admin', N'::1', N'按钮功能异常', N'输入字符串的格式不正确。', 0, CAST(0x0000A2EF0128DE14 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (156, N'admin', N'::1', N'按钮功能异常', N'输入字符串的格式不正确。', 0, CAST(0x0000A2EF0128E63D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (157, N'admin', N'::1', N'按钮功能异常', N'不能将值 NULL 插入列 ''shopname''，表 ''ZGZY.dbo.tbOrder''；列不允许有 Null 值。INSERT 失败。\r\n语句已终止。', 0, CAST(0x0000A2EF0128EB09 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (158, N'admin', N'::1', N'按钮功能异常', N'不能将值 NULL 插入列 ''shopname''，表 ''ZGZY.dbo.tbOrder''；列不允许有 Null 值。INSERT 失败。\r\n语句已终止。', 0, CAST(0x0000A2EF0128F18A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (159, N'admin', N'::1', N'按钮功能异常', N'不能将值 NULL 插入列 ''shopname''，表 ''ZGZY.dbo.tbOrder''；列不允许有 Null 值。INSERT 失败。\r\n语句已终止。', 0, CAST(0x0000A2EF01294301 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (160, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF012A8A38 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (161, N'admin', N'::1', N'按钮功能异常', N'不能将值 NULL 插入列 ''shopname''，表 ''ZGZY.dbo.tbOrder''；列不允许有 Null 值。INSERT 失败。\r\n语句已终止。', 0, CAST(0x0000A2EF012AA547 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (162, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF012AD39D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (163, N'admin', N'::1', N'按钮功能异常', N'不能将值 NULL 插入列 ''shopname''，表 ''ZGZY.dbo.tbOrder''；列不允许有 Null 值。INSERT 失败。\r\n语句已终止。', 0, CAST(0x0000A2EF012AE98D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (164, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF012B535C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (165, N'admin', N'::1', N'按钮功能异常', N'对象不能从 DBNull 转换为其他类型。', 0, CAST(0x0000A2EF012B6C74 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (166, N'admin', N'::1', N'按钮功能异常', N'订单已存在', 0, CAST(0x0000A2EF012B7852 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (167, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF012B9493 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (168, N'admin', N'::1', N'按钮功能异常', N'对象不能从 DBNull 转换为其他类型。', 0, CAST(0x0000A2EF012BCD43 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (169, N'admin', N'::1', N'按钮功能异常', N'对象不能从 DBNull 转换为其他类型。', 0, CAST(0x0000A2EF012DE069 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (170, N'admin', N'::1', N'按钮功能异常', N'订单已存在', 0, CAST(0x0000A2EF012DEFD2 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (171, N'admin', N'::1', N'按钮功能异常', N'订单已存在', 0, CAST(0x0000A2EF012E0283 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (172, N'admin', N'::1', N'按钮功能异常', N'对象不能从 DBNull 转换为其他类型。', 0, CAST(0x0000A2EF012E1E2C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (173, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF012F70E9 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (174, N'admin', N'::1', N'按钮功能异常', N'在将 nnvarchar 值 ''df'' 转换成数据类型 smallint 时失败。', 0, CAST(0x0000A2EF012FA162 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (175, N'admin', N'::1', N'按钮功能异常', N'在将 nnvarchar 值 ''df'' 转换成数据类型 smallint 时失败。', 0, CAST(0x0000A2EF012FA8B1 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (176, N'admin', N'::1', N'按钮功能异常', N'订单已存在', 0, CAST(0x0000A2EF012FBABC AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (177, N'admin', N'::1', N'按钮功能异常', N'在将 nnvarchar 值 ''sdf'' 转换成数据类型 smallint 时失败。', 0, CAST(0x0000A2EF012FBFDC AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (178, N'admin', N'::1', N'按钮功能异常', N'在将 nnvarchar 值 ''sdf'' 转换成数据类型 smallint 时失败。', 0, CAST(0x0000A2EF012FC63C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (179, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF012FEB86 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (180, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2EF01305B5B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (181, N'admin', N'::1', N'添加部门', N'添加成功，部门主键：47', 1, CAST(0x0000A2EF01306DE7 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (182, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2EF01306DFA AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (183, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0130A929 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (184, N'admin', N'::1', N'添加角色', N'添加成功，角色主键：54', 1, CAST(0x0000A2EF0130B2FB AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (185, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0130B317 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (186, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0131451A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (187, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0131AFBB AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (188, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013A8F2D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (189, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013A9985 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (190, N'admin', N'::1', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013A9A37 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (191, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013B8997 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (192, N'admin', N'::1', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013C0DCB AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (193, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013C404B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (194, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013C5CE8 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (195, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013CC29B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (196, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013CCAAA AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (197, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013CD3D5 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (198, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013D48E4 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (199, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013D4B48 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (200, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013D89B6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (201, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013E3779 AS DateTime))
GO
print 'Processed 200 total records'
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (202, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013EBAEF AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (203, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013EBFCE AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (204, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013F006F AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (205, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013F0509 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (206, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013F4697 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (207, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013F4CBE AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (208, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013F9B8A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (209, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013FA14B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (210, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013FBF76 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (211, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF013FC428 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (212, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0140D7B2 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (213, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01410722 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (214, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014119A1 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (215, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0141771C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (216, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01418814 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (217, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01419533 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (218, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0141A48B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (219, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0141ACF2 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (220, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0142F161 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (221, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01432C9B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (222, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01434308 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (223, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01435B05 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (224, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0143A4CB AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (225, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014423F0 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (226, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014435F8 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (227, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0144F2B9 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (228, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01451B8B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (229, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014593C3 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (230, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01459E93 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (231, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0145A48C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (232, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0145DEF1 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (233, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01465A8E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (234, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01465ED7 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (235, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01467BD3 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (236, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01467EBA AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (237, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014704D7 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (238, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014707CE AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (239, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0147F986 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (240, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0147FC4E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (241, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0148AB24 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (242, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0148AE17 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (243, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0148B2DD AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (244, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0148C4D3 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (245, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0148C70E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (246, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0148D054 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (247, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01492D75 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (248, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014930E1 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (249, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0149379A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (250, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0149CCE0 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (251, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0149D5A6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (252, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0149E0B4 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (253, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014ABBC9 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (254, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 and type=''1'' 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014AC41E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (255, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 and type=''0'' and aliwangwang=''测试旺旺'' 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014ADD3D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (256, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 and type=''1'' and in_company=''测试旺旺'' 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014AE8F5 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (257, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 and type=''0'' 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014AED6C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (258, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 and type=''0'' and aliwangwang=''23'' 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014B6E09 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (259, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 and type=''0'' and aliwangwang=''23'' and in_company=''21'' and in_orderid=''113'' and id=''23'' 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014B85DF AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (260, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 and type=''0'' 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014B8D62 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (261, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 and type=''0'' and aliwangwang=''23'' and in_company=''23'' and in_orderid=''113'' and id=''23'' 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014BA17A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (262, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014BCFB9 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (263, N'admin', N'::1', N'添加用户', N'添加成功，用户主键：77', 1, CAST(0x0000A2EF014BFA43 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (264, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014BFA77 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (265, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014C0793 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (266, N'admin', N'::1', N'查询角色用户', N'查询角色Id：53 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014C0A4E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (267, N'admin', N'::1', N'查询角色用户', N'查询角色Id：54 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014C1CDB AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (268, N'admin', N'::1', N'角色授权', N'授权成功，菜单/按钮Id：12 1,12 3,12 4,12 5,12 6,12 13', 1, CAST(0x0000A2EF014C2B4F AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (269, N'admin', N'::1', N'设置用户角色', N'设置成功，用户主键：77 角色主键：54', 1, CAST(0x0000A2EF014C3DF4 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (270, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014C3E19 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (271, N'test2', N'::1', N'用户重置密码', N'重置密码成功', 1, CAST(0x0000A2EF014C8AA6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (272, N'test2', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014C8DAF AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (273, N'test2', N'::1', N'查询按钮', N'查询条件：1=1 and type=''0'' and aliwangwang=''323'' 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014C92B9 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (274, N'test2', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014E0C9C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (275, N'test2', N'::1', N'按钮功能异常', N'订单已存在', 0, CAST(0x0000A2EF014E2876 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (276, N'test2', N'::1', N'按钮功能异常', N'订单已存在', 0, CAST(0x0000A2EF014E2FF3 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (277, N'test2', N'::1', N'按钮功能异常', N'订单已存在', 0, CAST(0x0000A2EF014E329C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (278, N'test2', N'::1', N'查询按钮', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF014E38A6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (279, N'test2', N'::1', N'查询按钮', N'查询条件：1=1 排序：aliwangwang asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01595808 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (280, N'test2', N'::1', N'查询按钮', N'查询条件：1=1 排序：aliwangwang desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF015958ED AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (281, N'test2', N'::1', N'查询按钮', N'查询条件：1=1 排序：aliwangwang asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF015959B5 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (282, N'test2', N'::1', N'查询按钮', N'查询条件：1=1 排序：create_time asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01595AAB AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (283, N'test2', N'::1', N'查询按钮', N'查询条件：1=1 排序：create_time desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01595B58 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (284, N'test2', N'::1', N'查询按钮', N'查询条件：1=1 排序：create_time asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01595C54 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (285, N'test2', N'::1', N'查询按钮', N'查询条件：1=1 排序：create_time desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01595D0F AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (286, N'test2', N'::1', N'查询我的信息', N'查询我的信息', 1, CAST(0x0000A2EF01596CC3 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (287, N'admin', N'::1', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF015ED258 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (288, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF015EDB42 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (289, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：2 20', 1, CAST(0x0000A2EF015EE7A6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (290, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：3 20', 1, CAST(0x0000A2EF015EEEC6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (291, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：4 20', 1, CAST(0x0000A2EF015EEF2D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (292, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：5 20', 1, CAST(0x0000A2EF015EEF81 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (293, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：6 20', 1, CAST(0x0000A2EF015EEFC7 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (294, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：7 20', 1, CAST(0x0000A2EF015EF01C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (295, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：8 20', 1, CAST(0x0000A2EF015EF08D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (296, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：9 20', 1, CAST(0x0000A2EF015EF110 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (297, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：10 20', 1, CAST(0x0000A2EF015EF16E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (298, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：11 20', 1, CAST(0x0000A2EF015EF21B AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (299, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：12 20', 1, CAST(0x0000A2EF015EF2DA AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (300, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：13 20', 1, CAST(0x0000A2EF015EF37A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (301, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：14 20', 1, CAST(0x0000A2EF015EF41A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (302, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：15 20', 1, CAST(0x0000A2EF015EF4C2 AS DateTime))
GO
print 'Processed 300 total records'
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (303, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：16 20', 1, CAST(0x0000A2EF015EF517 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (304, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：15 20', 1, CAST(0x0000A2EF015EF7F3 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (305, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF015F05F8 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (306, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：2 20', 1, CAST(0x0000A2EF015F0C99 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (307, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：3 20', 1, CAST(0x0000A2EF015F128C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (308, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：4 20', 1, CAST(0x0000A2EF015F19B6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (309, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：5 20', 1, CAST(0x0000A2EF015F1D3E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (310, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：4 20', 1, CAST(0x0000A2EF015F1F96 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (311, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：3 20', 1, CAST(0x0000A2EF015F204C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (312, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：2 20', 1, CAST(0x0000A2EF015F20C1 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (313, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF015F21E2 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (314, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：2 20', 1, CAST(0x0000A2EF015F3212 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (315, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：3 20', 1, CAST(0x0000A2EF015F38F1 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (316, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：4 20', 1, CAST(0x0000A2EF015FCF56 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (317, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：5 20', 1, CAST(0x0000A2EF015FD25E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (318, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：6 20', 1, CAST(0x0000A2EF015FD632 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (319, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：7 20', 1, CAST(0x0000A2EF015FD717 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (320, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：8 20', 1, CAST(0x0000A2EF015FDE46 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (321, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：9 20', 1, CAST(0x0000A2EF015FE5A6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (322, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：10 20', 1, CAST(0x0000A2EF015FE9A2 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (323, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：11 20', 1, CAST(0x0000A2EF015FF075 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (324, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：12 20', 1, CAST(0x0000A2EF015FF723 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (325, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01604D91 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (326, N'admin', N'::1', N'订单功能异常', N'订单已存在', 0, CAST(0x0000A2EF016065F8 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (327, N'admin', N'::1', N'添加订单', N'添加订单，订单编号：111', 1, CAST(0x0000A2EF01606E27 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (328, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2EF01606E3E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (329, N'admin', N'::1', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2EF0160733C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (330, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2F100B7BAC3 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (331, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2F7011732B6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (332, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2F70117488A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (333, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2F7011749E6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (334, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2F701174FCA AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (335, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2F70126A4C8 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (336, N'admin', N'::1', N'查询用户', N'查询条件：1=1 and UserId like ''%test%'' 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2F70126B2DE AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (337, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2F7012B9B6D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (338, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2F7012B9D9C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (339, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2F7012B9FBA AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (340, N'admin', N'::1', N'查询用户', N'查询条件：1=1 and UserId like ''%test%'' 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2F7012BA793 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (341, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2F7012BAD1A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (342, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2F7012BB776 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (343, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2F7012BD123 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (344, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2F7012BD9F5 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (345, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2F7012BDD77 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (346, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2F7012D0576 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (347, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2F7012D07A2 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (348, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2F801388446 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (349, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2F80138870E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (350, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2F801388BD2 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (351, N'admin', N'::1', N'查询用户', N'查询条件：1=1 and UserId like ''%test%'' 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2F801389287 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (352, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2F801389752 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (353, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2F8013899EA AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (354, N'admin', N'::1', N'查询订单', N'查询条件：1=1 and type=''0'' 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2F801389F58 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (355, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D53242 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (356, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D5357D AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (357, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D53836 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (358, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D5419C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (359, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D5D509 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (360, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D870DF AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (361, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId desc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D87806 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (362, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D878B8 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (363, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：Sort asc,ParentId asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D8794A AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (364, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D8E2F6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (365, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D8E763 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (366, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2FA00D8E92F AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (367, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：create_time asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D8F3AC AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (368, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：create_time desc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D8F45E AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (369, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：create_time asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D8F50C AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (370, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：create_time desc 页码/每页大小：1 20', 1, CAST(0x0000A2FA00D8F5A6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (371, N'admin', N'::1', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA014FB1DC AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (372, N'admin', N'::1', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA014FB2D4 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (373, N'admin', N'::1', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, CAST(0x0000A2FA014FB3E9 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (374, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA014FB439 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (375, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2FA014FB4E6 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (376, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA014FB655 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (377, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA015157E4 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (378, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA015158D7 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (379, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA01515C98 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (380, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA015162D8 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (381, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA0151638F AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (382, N'admin', N'::1', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA0151968F AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (383, N'admin', N'::1', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, CAST(0x0000A2FA0151E196 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (384, N'admin', N'::1', N'查询部门', N'查询条件：1=1', 1, CAST(0x0000A2FA0151E758 AS DateTime))
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (385, N'admin', N'115.236.91.18', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (386, N'admin', N'115.236.91.18', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (387, N'admin', N'115.236.91.18', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (388, N'admin', N'115.236.91.18', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (389, N'admin', N'115.236.91.18', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (390, N'admin', N'115.236.91.18', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (391, N'admin', N'115.236.91.18', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (392, N'admin', N'115.236.91.18', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (393, N'admin', N'218.86.135.109', N'查询我的信息', N'查询我的信息', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (394, N'admin', N'218.86.135.109', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (395, N'admin', N'218.86.135.109', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (396, N'admin', N'218.86.135.109', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (397, N'admin', N'218.86.135.109', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (398, N'admin', N'218.86.135.109', N'查询角色用户', N'查询角色Id：1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (399, N'admin', N'218.86.135.109', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (400, N'admin', N'218.86.135.109', N'查询部门用户', N'查询部门Id：43,41 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (401, N'admin', N'218.86.135.109', N'查询部门用户', N'查询部门Id：43 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (402, N'admin', N'218.86.135.109', N'查询部门用户', N'查询部门Id：8,7,46,10,9,47,2 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (403, N'admin', N'218.86.135.109', N'查询部门用户', N'查询部门Id：8 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
GO
print 'Processed 400 total records'
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (404, N'admin', N'218.86.135.109', N'查询部门用户', N'查询部门Id：7 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (405, N'admin', N'218.86.135.109', N'查询部门用户', N'查询部门Id：47 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (406, N'admin', N'218.86.135.109', N'查询部门用户', N'查询部门Id：9 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (407, N'admin', N'218.86.135.109', N'查询部门用户', N'查询部门Id：10 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (408, N'admin', N'218.86.135.109', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (409, N'admin', N'218.86.135.109', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (410, N'admin', N'218.86.135.109', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (411, N'admin', N'218.86.135.109', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (412, N'admin', N'218.86.135.109', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (413, N'admin', N'218.86.135.109', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (414, N'admin', N'218.86.135.109', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (415, N'admin', N'218.86.135.109', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (416, N'admin', N'218.86.135.109', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (417, N'admin', N'218.86.135.109', N'查询操作日志', N'查询条件：1=1 and UserName like ''%wu%'' 排序：OperateDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (418, N'admin', N'218.86.135.109', N'查询操作日志', N'查询条件：1=1 and UserName like ''%a%'' 排序：OperateDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (419, N'admin', N'218.86.135.109', N'查询操作日志', N'查询条件：1=1 and UserName like ''%m%'' 排序：OperateDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (420, N'admin', N'218.86.135.109', N'查询操作日志', N'查询条件：1=1 and UserName like ''%m%'' and OperateInfo like ''%cha%'' 排序：OperateDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (421, N'admin', N'218.86.135.109', N'查询操作日志', N'查询条件：1=1 and UserName like ''%m%'' and OperateInfo like ''%查%'' 排序：OperateDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (422, N'admin', N'218.86.135.109', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (423, N'admin', N'218.86.135.109', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 100', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (424, N'admin', N'218.86.135.109', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (425, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (426, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (427, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (428, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (429, N'admin', N'115.195.177.224', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (430, N'admin', N'115.195.177.224', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (431, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (432, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (433, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (434, N'admin', N'115.195.177.224', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (435, N'admin', N'115.195.177.224', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (436, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (437, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (438, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (439, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (440, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (441, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (442, N'admin', N'115.195.177.224', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (443, N'admin', N'115.195.177.224', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (444, N'admin', N'221.234.128.191', N'订单功能异常', N'在将 nnvarchar 值 ''g67678'' 转换成数据类型 smallint 时失败。', 0, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (445, N'admin', N'221.234.128.191', N'订单功能异常', N'转换 nnvarchar 值''67678'' 时溢出了 INT2 列。请使用较大的整数列。', 0, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (446, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (447, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (448, N'admin', N'115.195.177.224', N'查询订单', N'查询条件：1=1 and type=''0'' and id=''1987'' 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (449, N'admin', N'115.195.177.224', N'查询订单', N'查询条件：1=1 and type=''1'' and id=''1987'' 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (450, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 and type=''0'' 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (451, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 and type=''1'' 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (452, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 and type=''1'' 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (453, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (454, N'admin', N'115.195.177.224', N'查询订单', N'查询条件：1=1 and type=''1'' 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (455, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 and type=''0'' 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (456, N'admin', N'115.195.177.224', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (457, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (458, N'admin', N'115.195.177.224', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (459, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (460, N'admin', N'115.195.177.224', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (461, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (462, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (463, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (464, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (465, N'admin', N'115.195.177.224', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (466, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (467, N'admin', N'221.234.128.191', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (468, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (469, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：8 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (470, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：4,15,5,17,16,1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (471, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：15,4 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (472, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：15 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (473, N'admin', N'115.195.177.224', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (474, N'admin', N'221.234.128.191', N'修改部门', N'修改成功，部门主键：15', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (475, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (476, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：8 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (477, N'admin', N'115.195.177.224', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (478, N'admin', N'115.195.177.224', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (479, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (480, N'admin', N'115.195.177.224', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (481, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (482, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (483, N'admin', N'115.195.177.224', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (484, N'admin', N'115.195.177.224', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (485, N'admin', N'115.195.177.224', N'查询角色用户', N'查询角色Id：54 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (486, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (487, N'admin', N'221.234.128.191', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (488, N'admin', N'221.234.128.191', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (489, N'admin', N'221.234.128.191', N'删除用户', N'删除成功，用户主键：74,77', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (490, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (491, N'admin', N'221.234.128.191', N'用户修改密码', N'修改成功，用户主键：72', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (492, N'admin', N'221.234.128.191', N'查询按钮', N'查询条件：1=1 排序：Sort asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (493, N'admin', N'221.234.128.191', N'查询我的信息', N'查询我的信息', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (494, N'admin', N'221.234.128.191', N'查询菜单', N'查询条件：1=1 排序：ParentId asc,Sort asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (495, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (496, N'admin', N'221.234.128.191', N'删除用户', N'删除成功，用户主键：76', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (497, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (498, N'admin', N'221.234.128.191', N'添加用户', N'添加成功，用户主键：78', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (499, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (500, N'admin', N'221.234.128.191', N'添加用户', N'添加成功，用户主键：79', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (501, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (502, N'admin', N'221.234.128.191', N'添加用户', N'添加成功，用户主键：80', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (503, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (504, N'admin', N'221.234.128.191', N'添加用户', N'添加成功，用户主键：81', 1, NULL)
GO
print 'Processed 500 total records'
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (505, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (506, N'admin', N'221.234.128.191', N'修改用户', N'修改成功，用户主键：78', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (507, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (508, N'admin', N'221.234.128.191', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (509, N'admin', N'221.234.128.191', N'查询角色用户', N'查询角色Id：53 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (510, N'admin', N'221.234.128.191', N'查询角色用户', N'查询角色Id：52 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (511, N'admin', N'221.234.128.191', N'删除角色', N'删除成功，角色主键：52', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (512, N'admin', N'221.234.128.191', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (513, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (514, N'admin', N'221.234.128.191', N'查询角色用户', N'查询角色Id：53 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (515, N'admin', N'221.234.128.191', N'查询角色用户', N'查询角色Id：54 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (516, N'admin', N'221.234.128.191', N'删除角色', N'删除成功，角色主键：54', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (517, N'admin', N'221.234.128.191', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (518, N'admin', N'221.234.128.191', N'查询角色用户', N'查询角色Id：53 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (519, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (520, N'admin', N'221.234.128.191', N'修改角色', N'修改成功，角色主键：53', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (521, N'admin', N'221.234.128.191', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (522, N'admin', N'221.234.128.191', N'添加角色', N'添加成功，角色主键：55', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (523, N'admin', N'221.234.128.191', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (524, N'admin', N'221.234.128.191', N'添加角色', N'添加成功，角色主键：56', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (525, N'admin', N'221.234.128.191', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (526, N'admin', N'221.234.128.191', N'添加角色', N'添加成功，角色主键：57', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (527, N'admin', N'221.234.128.191', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (528, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (529, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：8,7,46,10,9,47,2 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (530, N'admin', N'221.234.128.191', N'修改部门', N'修改成功，部门主键：2', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (531, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (532, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：8 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (533, N'admin', N'221.234.128.191', N'删除部门', N'删除成功，部门主键：8', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (534, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (535, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (536, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：46 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (537, N'admin', N'221.234.128.191', N'删除部门', N'删除成功，部门主键：46', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (538, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (539, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (540, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：7 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (541, N'admin', N'221.234.128.191', N'删除部门', N'删除成功，部门主键：7', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (542, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (543, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (544, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：10 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (545, N'admin', N'221.234.128.191', N'删除部门', N'删除成功，部门主键：10', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (546, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (547, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (548, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：9 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (549, N'admin', N'221.234.128.191', N'删除部门', N'删除成功，部门主键：9', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (550, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (551, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (552, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：47 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (553, N'admin', N'221.234.128.191', N'删除部门', N'删除成功，部门主键：47', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (554, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (555, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (556, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：12,11,3 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (557, N'admin', N'221.234.128.191', N'修改部门', N'修改成功，部门主键：3', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (558, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (559, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：12 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (560, N'admin', N'221.234.128.191', N'删除部门', N'删除成功，部门主键：12', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (561, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (562, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (563, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：11 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (564, N'admin', N'221.234.128.191', N'删除部门', N'删除成功，部门主键：11', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (565, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (566, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (567, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：4,15,5,17,16,1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (568, N'admin', N'221.234.128.191', N'修改部门', N'修改成功，部门主键：1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (569, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (570, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：15,4 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (571, N'admin', N'221.234.128.191', N'删除部门', N'删除成功，部门主键：15,4', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (572, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (573, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (574, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：17,16,5 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (575, N'admin', N'221.234.128.191', N'删除部门', N'删除成功，部门主键：17,16,5', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (576, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (577, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (578, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：43,41 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (579, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：43 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (580, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：43,41 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (581, N'admin', N'221.234.128.191', N'修改部门', N'修改成功，部门主键：41', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (582, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (583, N'admin', N'221.234.128.191', N'查询部门用户', N'查询部门Id：43 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (584, N'admin', N'221.234.128.191', N'删除部门', N'删除成功，部门主键：43', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (585, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (586, N'admin', N'221.234.128.191', N'查询用户', N'查询条件：1=1 排序：AddDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (587, N'admin', N'221.234.128.191', N'添加部门', N'添加成功，部门主键：48', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (588, N'admin', N'221.234.128.191', N'查询部门', N'查询条件：1=1', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (589, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (590, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (591, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (592, N'admin', N'221.234.128.191', N'查询登陆日志', N'查询条件：1=1 排序：LoginDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (593, N'admin', N'221.234.128.191', N'查询操作日志', N'查询条件：1=1 排序：OperateDate desc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (594, N'admin', N'221.234.128.191', N'查询订单', N'查询条件：1=1 排序：id asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (595, N'admin', N'115.195.177.224', N'查询角色', N'查询条件：1=1 排序：AddDate asc 页码/每页大小：1 20', 1, NULL)
INSERT [dbo].[tbUserOperateLog] ([Id], [UserName], [UserIp], [OperateInfo], [Description], [IfSuccess], [OperateDate]) VALUES (596, N'admin', N'115.195.177.224', N'查询部门', N'查询条件：1=1', 1, NULL)
SET IDENTITY_INSERT [dbo].[tbUserOperateLog] OFF
/****** Object:  Table [dbo].[tbUserDepartment]    Script Date: 03/29/2014 11:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbUserDepartment](
	[UserId] [int] NULL,
	[DepartmentId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbUser]    Script Date: 03/29/2014 11:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[UserPwd] [nvarchar](50) NULL,
	[IsAble] [bit] NULL,
	[IfChangePwd] [bit] NULL,
	[AddDate] [datetime] NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_tbUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tbUser] ON
INSERT [dbo].[tbUser] ([Id], [UserId], [UserName], [UserPwd], [IsAble], [IfChangePwd], [AddDate], [Description]) VALUES (72, N'admin', N'管理员', N'855065BBB9780BBEC87AA6930B70AF', 1, 1, CAST(0x0000A2AD00ADD91E AS DateTime), N'管理员账号')
INSERT [dbo].[tbUser] ([Id], [UserId], [UserName], [UserPwd], [IsAble], [IfChangePwd], [AddDate], [Description]) VALUES (78, N'漂泊', N'漂泊', N'202CB962AC59075B964B07152D234B', 1, 1, NULL, N'漂泊')
INSERT [dbo].[tbUser] ([Id], [UserId], [UserName], [UserPwd], [IsAble], [IfChangePwd], [AddDate], [Description]) VALUES (79, N'漂雪儿', N'漂雪儿', N'202CB962AC59075B964B07152D234B', 1, 1, NULL, N'漂雪儿')
INSERT [dbo].[tbUser] ([Id], [UserId], [UserName], [UserPwd], [IsAble], [IfChangePwd], [AddDate], [Description]) VALUES (80, N'漂柔儿', N'漂柔儿', N'202CB962AC59075B964B07152D234B', 1, 1, NULL, NULL)
INSERT [dbo].[tbUser] ([Id], [UserId], [UserName], [UserPwd], [IsAble], [IfChangePwd], [AddDate], [Description]) VALUES (81, N'小程', N'小程', N'202CB962AC59075B964B07152D234B', 1, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbUser] OFF
/****** Object:  Table [dbo].[tbRoleMenuButton]    Script Date: 03/29/2014 11:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbRoleMenuButton](
	[RoleId] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[ButtonId] [int] NOT NULL,
	 CONSTRAINT [PK_tbRoleMenuButton] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[MenuId] ASC,
	[ButtonId] ASC
)
) ON [PRIMARY]
GO
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 3, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 3, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 3, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 3, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 4, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 4, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 4, 7)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 4, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 4, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 5, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 5, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 5, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 5, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 6, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 6, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 6, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 6, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 7, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 7, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 7, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 7, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 8, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 4, 8)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 8, 6)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 9, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 6, 9)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 3, 10)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 10, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 5, 11)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 10, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 10, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 10, 6)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 1, 0)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 2, 0)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 5, 12)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 5, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 5, 12)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 5, 11)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 5, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 1, 0)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 5, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 5, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 4, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 4, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 3, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 8, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 2, 0)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 7, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 9, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 10, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 3, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 8, 6)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 4, 7)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 10, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 10, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 4, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 3, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 7, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 4, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 7, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 10, 6)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 7, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 3, 10)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 3, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (53, 4, 8)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 12, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 12, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 12, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 12, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 12, 6)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 12, 13)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 11, 0)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 13, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 13, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 13, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 13, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 13, 6)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 13, 13)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 14, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 14, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 14, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 14, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 14, 6)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 14, 13)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 15, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 15, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 15, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 15, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 15, 6)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 15, 13)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 16, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 16, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 16, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 16, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 16, 6)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 16, 13)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 18, 1)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 18, 3)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 18, 4)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 18, 5)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 18, 6)
GO
print 'Processed 100 total records'
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 18, 13)
INSERT [dbo].[tbRoleMenuButton] ([RoleId], [MenuId], [ButtonId]) VALUES (1, 17, 0)
/****** Object:  Table [dbo].[tbRole]    Script Date: 03/29/2014 11:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
	[Description] [nvarchar](100) NULL,
	[AddDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_tbRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tbRole] ON
INSERT [dbo].[tbRole] ([Id], [RoleName], [Description], [AddDate], [ModifyDate]) VALUES (1, N'超级管理员', N'拥有所有增删改查权限', CAST(0x0000A27301080F41 AS DateTime), CAST(0x0000A29800AAFBF4 AS DateTime))
INSERT [dbo].[tbRole] ([Id], [RoleName], [Description], [AddDate], [ModifyDate]) VALUES (53, N'仓库售后', N'仓库售后', CAST(0x0000A2E100C86879 AS DateTime), CAST(0x0000A2FC0173E0B1 AS DateTime))
INSERT [dbo].[tbRole] ([Id], [RoleName], [Description], [AddDate], [ModifyDate]) VALUES (55, N'客服售前', N'客服售前', NULL, NULL)
INSERT [dbo].[tbRole] ([Id], [RoleName], [Description], [AddDate], [ModifyDate]) VALUES (56, N'运营', N'运营', NULL, NULL)
INSERT [dbo].[tbRole] ([Id], [RoleName], [Description], [AddDate], [ModifyDate]) VALUES (57, N'美工', N'美工', NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbRole] OFF
/****** Object:  Table [dbo].[tbOrder]    Script Date: 03/29/2014 11:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbOrder](
	[type] [tinyint] NOT NULL,
	[shopname] [nvarchar](128) NOT NULL,
	[in_company] [nvarchar](64) NOT NULL,
	[in_orderid] [nvarchar](32) NOT NULL,
	[id] [nvarchar](32) NOT NULL,
	[in_color] [nvarchar](32) NULL,
	[in_size] [nvarchar](5) NULL,
	[in_num] [smallint] NOT NULL,
	[aliwangwang] [nvarchar](32) NOT NULL,
	[out_company] [nvarchar](64) NOT NULL,
	[out_orderid] [nvarchar](32) NOT NULL,
	[out_id] [nvarchar](32) NOT NULL,
	[out_color] [nvarchar](32) NULL,
	[out_size] [nvarchar](5) NULL,
	[out_num] [smallint] NOT NULL,
	[remark] [nvarchar](255) NULL,
	[create_time] [datetime] NOT NULL,
	[update_time] [datetime] NOT NULL,
	[username] [nvarchar](64) NOT NULL,
	CONSTRAINT [PK_tbOrder] PRIMARY KEY CLUSTERED 
(
	[in_orderid] ASC
)
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[tbOrder] ([type], [shopname], [in_company], [in_orderid], [id], [in_color], [in_size], [in_num], [aliwangwang], [out_company], [out_orderid], [out_id], [out_color], [out_size], [out_num], [remark], [create_time], [update_time], [username]) VALUES (0, N'32', N'23', N'113', 23, N'3', N'2', 2, N'23', N'2', N'22', 2, N'2', N'2', 2, N'吴建明测试', CAST(0x0000A2EF012B6C59 AS DateTime), CAST(0x0000A2EF012B6C59 AS DateTime), N'admin')
INSERT [dbo].[tbOrder] ([type], [shopname], [in_company], [in_orderid], [id], [in_color], [in_size], [in_num], [aliwangwang], [out_company], [out_orderid], [out_id], [out_color], [out_size], [out_num], [remark], [create_time], [update_time], [username]) VALUES (0, N'1', N'132', N'4321', 112, N'232', N'23', 2, N'23', N'23', N'11', 2, N'332', N'23', 11, N'sdf', CAST(0x0000A2EF012E1E2E AS DateTime), CAST(0x0000A2EF012E1E2E AS DateTime), N'admin')
--INSERT [dbo].[tbOrder] ([type], [shopname], [in_company], [in_orderid], [id], [in_color], [in_size], [in_num], [aliwangwang], [out_company], [out_orderid], [out_id], [out_color], [out_size], [out_num], [remark], [create_time], [update_time], [username]) VALUES (0, N'1', N'132', N'4321', 113, N'232', N'23', 2, N'23', N'23', N'11', 2, N'332', N'23', 11, N'sdf', CAST(0x0000A2EF00000000 AS DateTime), CAST(0x0000A2EF00000000 AS DateTime), N'admin')
--INSERT [dbo].[tbOrder] ([type], [shopname], [in_company], [in_orderid], [id], [in_color], [in_size], [in_num], [aliwangwang], [out_company], [out_orderid], [out_id], [out_color], [out_size], [out_num], [remark], [create_time], [update_time], [username]) VALUES (0, N'1', N'132', N'4321', 115, N'232', N'23', 2, N'23', N'23', N'11', 2, N'332', N'23', 11, N'sdf', CAST(0x0000A2EF00000000 AS DateTime), CAST(0x0000A2EF00000000 AS DateTime), N'admin')
--INSERT [dbo].[tbOrder] ([type], [shopname], [in_company], [in_orderid], [id], [in_color], [in_size], [in_num], [aliwangwang], [out_company], [out_orderid], [out_id], [out_color], [out_size], [out_num], [remark], [create_time], [update_time], [username]) VALUES (0, N'1', N'132', N'4321', 117, N'232', N'23', 2, N'23', N'23', N'11', 2, N'332', N'23', 11, N'sdf', CAST(0x0000A2EF00000000 AS DateTime), CAST(0x0000A2EF00000000 AS DateTime), N'admin')
INSERT [dbo].[tbOrder] ([type], [shopname], [in_company], [in_orderid], [id], [in_color], [in_size], [in_num], [aliwangwang], [out_company], [out_orderid], [out_id], [out_color], [out_size], [out_num], [remark], [create_time], [update_time], [username]) VALUES (0, N'兑付', N'13232', N'1323', 232, N'232', N'23', 23, N'兑付', N'23', N'23', 23, N'23', N'23', 44, N'dsf', CAST(0x0000A2EF00000000 AS DateTime), CAST(0x0000A2EF00000000 AS DateTime), N'admin')
INSERT [dbo].[tbOrder] ([type], [shopname], [in_company], [in_orderid], [id], [in_color], [in_size], [in_num], [aliwangwang], [out_company], [out_orderid], [out_id], [out_color], [out_size], [out_num], [remark], [create_time], [update_time], [username]) VALUES (0, N'1', N'132', N'432', 1223, N'232', N'23', 2, N'23', N'23', N'11', 2, N'332', N'23', 11, N'sdf', CAST(0x0000A2EF012DE06A AS DateTime), CAST(0x0000A2EF012DE06A AS DateTime), N'admin')
INSERT [dbo].[tbOrder] ([type], [shopname], [in_company], [in_orderid], [id], [in_color], [in_size], [in_num], [aliwangwang], [out_company], [out_orderid], [out_id], [out_color], [out_size], [out_num], [remark], [create_time], [update_time], [username]) VALUES (1, N'海漂旗舰店', N'顺丰', N'5023625864', 1986, N'红', N'S', 1, N'测试旺旺', N'申通', N'7778595685215', 1986, N'红', N'M', 1, N'吴建明测试', CAST(0x0000A2EE00000000 AS DateTime), CAST(0x0000A2EE00000000 AS DateTime), N'admin')
INSERT [dbo].[tbOrder] ([type], [shopname], [in_company], [in_orderid], [id], [in_color], [in_size], [in_num], [aliwangwang], [out_company], [out_orderid], [out_id], [out_color], [out_size], [out_num], [remark], [create_time], [update_time], [username]) VALUES (1, N'还敲', N'df ', N'123', 321, N'sd', N'23', 23, N'物价 ', N'11', N'df', 322, N'红色', N'M', 11, N'fa', CAST(0x0000A2EF012FCD3B AS DateTime), CAST(0x0000A2EF012FCD3B AS DateTime), N'admin')
INSERT [dbo].[tbOrder] ([type], [shopname], [in_company], [in_orderid], [id], [in_color], [in_size], [in_num], [aliwangwang], [out_company], [out_orderid], [out_id], [out_color], [out_size], [out_num], [remark], [create_time], [update_time], [username]) VALUES (1, N'海漂旗舰店', N'圆通', N'5023625865', 1987, N'红色', N'S', 2, N'测试旺旺2', N'圆通', N'5023625866', 2005, N'黑色', N'S', 22, N'吴建明测试内容', CAST(0x0000A2EF0131AFA1 AS DateTime), CAST(0x0000A2EF0131AFA1 AS DateTime), N'admin')
INSERT [dbo].[tbOrder] ([type], [shopname], [in_company], [in_orderid], [id], [in_color], [in_size], [in_num], [aliwangwang], [out_company], [out_orderid], [out_id], [out_color], [out_size], [out_num], [remark], [create_time], [update_time], [username]) VALUES (0, N'册', N'23', N'2311', 231, N'4423', N'23', 23, N'23', N'43', N'23', 23, N'23', N'23', 1, N'23而', CAST(0x0000A2EF014E3879 AS DateTime), CAST(0x0000A2EF014E3879 AS DateTime), N'test2')
INSERT [dbo].[tbOrder] ([type], [shopname], [in_company], [in_orderid], [id], [in_color], [in_size], [in_num], [aliwangwang], [out_company], [out_orderid], [out_id], [out_color], [out_size], [out_num], [remark], [create_time], [update_time], [username]) VALUES (1, N'2323', N'2332', N'12', 111, N'23', N'23', 23, N'23233', N'23', N'23', 23, N'23', N'23', 23, N'23', CAST(0x0000A2EF01606E1A AS DateTime), CAST(0x0000A2EF01606E1A AS DateTime), N'admin')
/****** Object:  Table [dbo].[tbMenuButton]    Script Date: 03/29/2014 11:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbMenuButton](
	[MenuId] [int] NOT NULL,
	[ButtonId] [int] NOT NULL,
	CONSTRAINT [PK_tbMenuButton] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC,
	[ButtonId] ASC
)
) ON [PRIMARY]
GO
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (3, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (3, 3)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (3, 4)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (3, 5)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (4, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (4, 7)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (4, 3)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (4, 4)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (4, 5)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (5, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (5, 3)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (5, 4)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (5, 5)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (6, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (6, 3)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (6, 4)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (6, 5)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (7, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (7, 3)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (7, 4)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (7, 5)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (8, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (4, 8)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (8, 6)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (9, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (6, 9)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (3, 10)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (10, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (5, 11)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (10, 3)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (10, 4)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (10, 6)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (5, 12)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (12, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (12, 3)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (12, 4)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (12, 5)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (12, 6)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (12, 13)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (13, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (13, 3)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (13, 4)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (13, 5)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (13, 6)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (13, 13)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (14, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (14, 3)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (14, 4)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (14, 5)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (14, 6)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (14, 13)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (15, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (15, 3)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (15, 4)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (15, 5)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (15, 6)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (15, 13)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (16, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (16, 3)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (16, 4)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (16, 5)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (16, 6)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (16, 13)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (18, 1)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (18, 3)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (18, 4)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (18, 5)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (18, 6)
INSERT [dbo].[tbMenuButton] ([MenuId], [ButtonId]) VALUES (18, 13)
/****** Object:  Table [dbo].[tbMenu]    Script Date: 03/29/2014 11:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ParentId] [int] NULL,
	[Code] [nvarchar](50) NULL,
	[LinkAddress] [nvarchar](100) NULL,
	[Icon] [nvarchar](50) NULL,
	[Sort] [int] NULL,
	[AddDate] [datetime] NULL,
 CONSTRAINT [PK_tbMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tbMenu] ON
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (1, N'系统设置', 0, NULL, NULL, N'icon-cog', 1, CAST(0x0000A24000EFB330 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (2, N'其他', 0, NULL, NULL, N'icon-tux', 4, CAST(0x0000A24000EFB330 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (3, N'菜单管理', 1, N'menu', N'html/ui_menu.html', N'icon-layout', 2, CAST(0x0000A24000EFB330 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (4, N'用户管理', 1, N'user', N'html/ui_user.html', N'icon-user_suit_black', 3, CAST(0x0000A24000EFB330 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (5, N'部门管理', 1, N'department', N'html/ui_department.html', N'icon-group', 5, CAST(0x0000A24000EFB330 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (6, N'角色管理', 1, N'role', N'html/ui_role.html', N'icon-key_go', 4, CAST(0x0000A24000EFB330 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (7, N'按钮管理', 1, N'button', N'html/ui_button.html', N'icon-button', 1, CAST(0x0000A24000EFB330 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (8, N'登录日志', 2, N'loginlog', N'html/ui_loginlog.html', N'icon-drive_user', 1, CAST(0x0000A24000EFB330 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (9, N'操作日志', 2, N'operatelog', N'html/ui_operatelog.html', N'icon-table', 2, CAST(0x0000A24000EFB330 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (10, N'Bug反馈', 2, N'bugs', N'html/ui_bugs.html', N'icon-bug', 3, CAST(0x0000A24000EFB330 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (11, N'海漂订单管理', 0, NULL, NULL, N'icon-tux', 2, CAST(0x0000A2E80147A4AD AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (12, N'海漂退换货管理', 11, N'orders', N'html/ui_orders.html', N'cart', 1, CAST(0x0000A2E8014958F1 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (13, N'老大店', 11, N'orders', N'html/ui_orders.html', N'cart', 2, CAST(0x0000A2FA00D613C6 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (14, N'神奇店', 11, N'orders', N'html/ui_orders.html', N'cart', 3, CAST(0x0000A2FA00D67F97 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (15, N'优优店', 11, N'orders', N'html/ui_orders.html', N'cart', 4, CAST(0x0000A2FA00D6AECB AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (16, N'海漂分销管理', 11, N'orders', N'html/ui_orders.html', N'cart', 5, CAST(0x0000A2FA00D6B8D5 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (17, N'日常事务', 0, NULL, NULL, N'icon-tux', 3, CAST(0x0000A2FA00D7E948 AS DateTime))
INSERT [dbo].[tbMenu] ([Id], [Name], [ParentId], [Code], [LinkAddress], [Icon], [Sort], [AddDate]) VALUES (18, N'待处理', 17, N'business', N'html/ui_business.html', N'cart', 1, CAST(0x0000A2FA00D83B1E AS DateTime))
SET IDENTITY_INSERT [dbo].[tbMenu] OFF
/****** Object:  Table [dbo].[tbLoginLog]    Script Date: 03/29/2014 11:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbLoginLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[UserIp] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Success] [bit] NULL,
	[LoginDate] [datetime] NULL,
 CONSTRAINT [PK_tbLoginInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tbLoginLog] ON
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (10, N'admin', N'::1', N'浙江杭州', 1, CAST(0x0000A2E801457856 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (11, N'test', N'::1', N'未知', 1, CAST(0x0000A2E801462D6C AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (12, N'admin', N'::1', N'浙江杭州', 1, CAST(0x0000A2E801463D71 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (13, N'test', N'::1', N'浙江杭州', 1, CAST(0x0000A2E801467A20 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (14, N'admin', N'::1', N'浙江杭州', 1, CAST(0x0000A2E80146D0C7 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (15, N'admin', N'::1', N'未知', 1, CAST(0x0000A2EC0106D934 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (16, N'admin', N'::1', N'未知', 1, CAST(0x0000A2EC01097C83 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (17, N'admin', N'::1', N'未知', 1, CAST(0x0000A2EC010A036D AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (18, N'admin', N'::1', N'未知', 1, CAST(0x0000A2EC0112902E AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (19, N'admin', N'::1', N'未知', 1, CAST(0x0000A2EC0117CA33 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (20, N'admin', N'::1', N'未知', 1, CAST(0x0000A2EC011D0C64 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (21, N'admin', N'::1', N'未知', 1, CAST(0x0000A2EC011E29CF AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (22, N'admin', N'::1', N'未知', 1, CAST(0x0000A2EC011F073F AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (23, N'admin', N'::1', N'未知', 1, CAST(0x0000A2EC01243A0A AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (24, N'admin', N'::1', N'浙江杭州', 1, CAST(0x0000A2EC0139118E AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (25, N'admin', N'::1', N'浙江杭州', 1, CAST(0x0000A2EC013E3E43 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (26, N'admin', N'::1', N'未知', 1, CAST(0x0000A2EF010F9934 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (27, N'admin', N'::1', N'未知', 1, CAST(0x0000A2EF0113BE23 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (28, N'admin2', N'::1', NULL, 0, CAST(0x0000A2EF0117237B AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (29, N'admin', N'::1', NULL, 0, CAST(0x0000A2EF0117308D AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (30, N'admin', N'::1', NULL, 1, CAST(0x0000A2EF0117376D AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (31, N'admin', N'::1', NULL, 1, CAST(0x0000A2EF0119A9C7 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (32, N'admin', N'::1', NULL, 1, CAST(0x0000A2EF0123050D AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (33, N'admin', N'::1', NULL, 1, CAST(0x0000A2EF01287B7E AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (34, N'admin', N'::1', NULL, 1, CAST(0x0000A2EF012AD1EA AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (35, N'admin', N'::1', NULL, 1, CAST(0x0000A2EF012B51BB AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (36, N'test2', N'::1', NULL, 0, CAST(0x0000A2EF014C4E2B AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (37, N'test2', N'::1', NULL, 1, CAST(0x0000A2EF014C8594 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (38, N'admin', N'::1', NULL, 1, CAST(0x0000A2EF015E447B AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (39, N'admin', N'::1', NULL, 1, CAST(0x0000A2EF01604B61 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (40, N'admin', N'::1', NULL, 1, CAST(0x0000A2F100B7A81D AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (41, N'admin', N'::1', NULL, 1, CAST(0x0000A2F701142F19 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (42, N'admin', N'::1', NULL, 1, CAST(0x0000A2F701143BFE AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (43, N'admin', N'::1', NULL, 1, CAST(0x0000A2F701147AC4 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (44, N'admin', N'::1', NULL, 1, CAST(0x0000A2F70114E623 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (45, N'admin', N'::1', NULL, 1, CAST(0x0000A2F701153CF3 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (46, N'admin', N'::1', NULL, 1, CAST(0x0000A2F701155D00 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (47, N'admin', N'::1', NULL, 1, CAST(0x0000A2F701160A79 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (48, N'admin', N'::1', NULL, 1, CAST(0x0000A2F7011628E9 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (49, N'admin', N'::1', NULL, 1, CAST(0x0000A2F70116B506 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (50, N'admin', N'::1', NULL, 1, CAST(0x0000A2F701172AA7 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (51, N'admin', N'::1', NULL, 1, CAST(0x0000A2F701174513 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (52, N'admin', N'::1', NULL, 1, CAST(0x0000A2F70126DE4F AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (53, N'admin', N'::1', NULL, 1, CAST(0x0000A2F701273905 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (54, N'admin', N'::1', NULL, 0, CAST(0x0000A2F7012B8914 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (55, N'admin', N'::1', NULL, 1, CAST(0x0000A2F7012B941D AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (56, N'admin', N'::1', NULL, 1, CAST(0x0000A2F7012BCE1B AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (57, N'admin', N'::1', NULL, 1, CAST(0x0000A2F7012D035A AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (58, N'admin', N'::1', NULL, 1, CAST(0x0000A2F8013874DC AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (59, N'admin', N'::1', NULL, 1, CAST(0x0000A2FA00D52BED AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (60, N'admin', N'::1', NULL, 1, CAST(0x0000A2FA00D5D235 AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (61, N'admin', N'::1', NULL, 1, CAST(0x0000A2FA00D8E09F AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (62, N'admin', N'::1', NULL, 1, CAST(0x0000A2FA014FA7FC AS DateTime))
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (63, N'admin', N'115.236.91.18', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (64, N'admin', N'218.86.135.109', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (65, N'admin', N'218.86.135.109', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (66, N'admin', N'218.86.135.109', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (67, N'admin', N'218.86.135.109', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (68, N'admin', N'221.234.128.191', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (69, N'admin', N'115.195.177.224', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (70, N'admin', N'115.195.177.224', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (71, N'admin', N'221.234.128.191', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (72, N'admin', N'221.234.128.191', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (73, N'admin', N'115.195.177.224', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (74, N'admin', N'115.195.177.224', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (75, N'admin', N'36.22.51.54', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (76, N'admin', N'221.234.128.191', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (77, N'admin', N'221.234.128.191', NULL, 1, NULL)
INSERT [dbo].[tbLoginLog] ([Id], [UserName], [UserIp], [City], [Success], [LoginDate]) VALUES (78, N'admin', N'115.195.177.224', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[tbLoginLog] OFF
/****** Object:  Table [dbo].[tbDepartment]    Script Date: 03/29/2014 11:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbDepartment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](50) NULL,
	[ParentId] [int] NULL,
	[Sort] [int] NULL,
	[AddDate] [datetime] NULL,
 CONSTRAINT [PK_tbDepartment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tbDepartment] ON
INSERT [dbo].[tbDepartment] ([Id], [DepartmentName], [ParentId], [Sort], [AddDate]) VALUES (1, N'售前部', 0, 3, CAST(0x0000A25801693D18 AS DateTime))
INSERT [dbo].[tbDepartment] ([Id], [DepartmentName], [ParentId], [Sort], [AddDate]) VALUES (2, N'运营部', 0, 1, CAST(0x0000A258016942B9 AS DateTime))
INSERT [dbo].[tbDepartment] ([Id], [DepartmentName], [ParentId], [Sort], [AddDate]) VALUES (3, N'售后部', 0, 2, CAST(0x0000A25801694C97 AS DateTime))
INSERT [dbo].[tbDepartment] ([Id], [DepartmentName], [ParentId], [Sort], [AddDate]) VALUES (41, N'美工部', 0, 4, CAST(0x0000A2AD00B7E540 AS DateTime))
INSERT [dbo].[tbDepartment] ([Id], [DepartmentName], [ParentId], [Sort], [AddDate]) VALUES (48, N'后勤部', 0, 5, NULL)
SET IDENTITY_INSERT [dbo].[tbDepartment] OFF
/****** Object:  Table [dbo].[tbButton]    Script Date: 03/29/2014 11:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbButton](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [nvarchar](50) NULL,
	[Icon] [nvarchar](50) NULL,
	[Sort] [int] NULL,
	[AddDate] [datetime] NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_tbButton] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tbButton] ON
INSERT [dbo].[tbButton] ([Id], [Name], [Code], [Icon], [Sort], [AddDate], [Description]) VALUES (1, N'浏览', N'browser', N'icon-eye', 1, CAST(0x0000A28A00C1ED72 AS DateTime), NULL)
INSERT [dbo].[tbButton] ([Id], [Name], [Code], [Icon], [Sort], [AddDate], [Description]) VALUES (3, N'添加', N'add', N'icon-add', 3, CAST(0x0000A28A00C1ED72 AS DateTime), NULL)
INSERT [dbo].[tbButton] ([Id], [Name], [Code], [Icon], [Sort], [AddDate], [Description]) VALUES (4, N'修改', N'edit', N'icon-application_edit', 4, CAST(0x0000A28A00C1ED72 AS DateTime), NULL)
INSERT [dbo].[tbButton] ([Id], [Name], [Code], [Icon], [Sort], [AddDate], [Description]) VALUES (5, N'删除', N'delete', N'icon-delete', 5, CAST(0x0000A28A00C1ED72 AS DateTime), NULL)
INSERT [dbo].[tbButton] ([Id], [Name], [Code], [Icon], [Sort], [AddDate], [Description]) VALUES (6, N'导出', N'export', N'icon-page_excel', 6, CAST(0x0000A28A00C1ED72 AS DateTime), NULL)
INSERT [dbo].[tbButton] ([Id], [Name], [Code], [Icon], [Sort], [AddDate], [Description]) VALUES (7, N'部门设置', N'setdepartment', N'icon-group', 8, CAST(0x0000A28A00C1ED72 AS DateTime), NULL)
INSERT [dbo].[tbButton] ([Id], [Name], [Code], [Icon], [Sort], [AddDate], [Description]) VALUES (8, N'角色设置', N'setrole', N'icon-user_key', 7, CAST(0x0000A28A00C1ED72 AS DateTime), NULL)
INSERT [dbo].[tbButton] ([Id], [Name], [Code], [Icon], [Sort], [AddDate], [Description]) VALUES (9, N'角色授权', N'authorize', N'icon-key', 9, CAST(0x0000A28A00C1ED72 AS DateTime), NULL)
INSERT [dbo].[tbButton] ([Id], [Name], [Code], [Icon], [Sort], [AddDate], [Description]) VALUES (10, N'分配按钮', N'setbutton', N'icon-link', 10, CAST(0x0000A2910097FDB5 AS DateTime), NULL)
INSERT [dbo].[tbButton] ([Id], [Name], [Code], [Icon], [Sort], [AddDate], [Description]) VALUES (11, N'全部展开', N'expandall', N'icon-expand', 11, CAST(0x0000A29100ACA955 AS DateTime), NULL)
INSERT [dbo].[tbButton] ([Id], [Name], [Code], [Icon], [Sort], [AddDate], [Description]) VALUES (12, N'全部隐藏', N'collapseall', N'icon-collapse', 12, CAST(0x0000A29100ACBC48 AS DateTime), NULL)
INSERT [dbo].[tbButton] ([Id], [Name], [Code], [Icon], [Sort], [AddDate], [Description]) VALUES (13, N'查找', N'search', N'icon-search', 13, CAST(0x0000A2E8014AA724 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[tbButton] OFF
/****** Object:  Table [dbo].[tbBug]    Script Date: 03/29/2014 11:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbBug](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[UserIp] [nvarchar](50) NULL,
	[BugInfo] [nvarchar](max) NULL,
	[BugReply] [nvarchar](max) NULL,
	[BugDate] [datetime] NULL,
	[IfShow] [bit] NULL,
	[IfSolve] [bit] NULL,
 CONSTRAINT [PK_tbBug] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO



-- 存储过程

/****** Object:  StoredProcedure [dbo].[sp_Pager]    Script Date: 03/29/2014 11:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Pager]
@tableName varchar(64),  --分页表名
@columns varchar(512),  --查询的字段
@order varchar(256),    --排序方式
@pageSize int,  --每页大小
@pageIndex int,  --当前页
@where varchar(1024) = '1=1',  --查询条件
@totalCount int output  --总记录数
as
declare @beginIndex int,@endIndex int,@sqlResult nvarchar(2000),@sqlGetCount nvarchar(2000)
set @beginIndex = (@pageIndex - 1) * @pageSize + 1  --开始
set @endIndex = (@pageIndex) * @pageSize  --结束
set @sqlresult = 'select '+@columns+' from (
select row_number() over(order by '+ @order +')
as Rownum,'+@columns+'
from '+@tableName+' where '+ @where +') as T
where T.Rownum between ' + CONVERT(varchar(max),@beginIndex) + ' and ' + CONVERT(varchar(max),@endIndex)
set @sqlGetCount = 'select @totalCount = count(*) from '+@tablename+' where ' + @where  --总数
--print @sqlresult
exec(@sqlresult)
exec sp_executesql @sqlGetCount,N'@totalCount int output',@totalCount output
--测试调用：
--declare @total int
--exec sp_Pager 'tbLoginInfo','Id,UserName,Success','LoginDate','desc',4,2,'1=1',@total output
--print @total
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAuthorityByUserId]    Script Date: 03/29/2014 11:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_GetAuthorityByUserId]  
@userId int  --用户主键id  
as    
declare @roleIds varchar(128)  --用户所有的角色集合  
declare @sql varchar(max)    
SELECT @roleIds=REPLACE(    
(select str(ur.RoleId)+',' from tbUser u    
join tbUserRole ur on u.Id = ur.UserId where u.Id = @userId  
for xml path('')),' ','')    
--print substring(@roleids,1,len(@roleids)-1)  --打印出用户所拥有的角色id  
set @sql=    
'select m.Id menuid,m.Name menuname,m.ParentId parentid,m.Icon menuicon,mb.ButtonId buttonid,b.Name buttonname,b.Icon buttonicon,rmb.RoleId roleid,  
case      
when isnull(rmb.ButtonId,0) = 0   
then ''false'' else ''true''    
end checked    
from tbMenu m  
left join tbMenuButton mb on m.Id=mb.MenuId  
left join tbButton b on mb.ButtonId=b.Id    
left join tbRoleMenuButton rmb on (mb.MenuId=rmb.MenuId and mb.ButtonId=rmb.ButtonId and rmb.RoleId in ('    
+    
isnull(substring(@roleIds,1,len(@roleIds)-1),0)    
+'))    
order by m.ParentId,m.Sort,b.Sort'  
--print @sql    
exec (@sql)
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckLogin]    Script Date: 03/29/2014 11:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_CheckLogin]  
@ip nvarchar(50),  
@lastErrorLoginTime datetime output  
as          
declare @errorLoginCount int  --错误次数  
select @errorLoginCount = Count(1) from tbLoginLog where Success = 0 and DATEADD(MI,30,LoginDate) > GETDATE() and UserIp = @ip  
if @errorLoginCount >= 5  
begin  
 select top 1 @lastErrorLoginTime = T.LoginDate from (select top 5 LoginDate from tbLoginLog where UserIp = @ip order by LoginDate desc ) T order by LoginDate asc  
end  
else  
begin  
 set @lastErrorLoginTime = null  
end
GO