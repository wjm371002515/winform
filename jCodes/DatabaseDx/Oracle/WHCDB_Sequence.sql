--权限管理模块
create sequence SEQ_T_ACL_USER
start with 1000
increment by 1;

create sequence SEQ_T_ACL_ROLE
start with 100
increment by 1;

create sequence SEQ_T_ACL_OU
start with 100
increment by 1;

create sequence SEQ_T_ACL_LOGINLOG
start with 1000
increment by 1;

create sequence SEQ_T_ACL_SYSTEMAUTHORIZE
start with 1
increment by 1;

commit;



--仓库管理系统
create sequence SEQ_WM_WAREHOUSE
start with 100
increment by 1;

create sequence SEQ_WM_ITEMDETAIL
start with 100
increment by 1;

create sequence SEQ_WM_STOCK
start with 100
increment by 1;

create sequence SEQ_WM_PURCHASEHEADER
start with 100
increment by 1;

create sequence SEQ_WM_PURCHASEDETAIL
start with 100
increment by 1;

create sequence SEQ_WM_REPORTMONTHLYHEADER
start with 100
increment by 1;

create sequence SEQ_WM_REPORTMONTHLYDETAIL
start with 100
increment by 1;

create sequence SEQ_WM_REPORTANNUALCOSTHEADER
start with 100
increment by 1;

create sequence SEQ_WM_REPORTANNUALCOSTDETAIL
start with 100
increment by 1;

create sequence SEQ_WM_REPORTDEPTCOST
start with 100
increment by 1;

create sequence SEQ_WM_REPORTMONTHLYCOSTDETAIL
start with 100
increment by 1;

create sequence SEQ_WM_REPORTMONTHCHECKOUT
start with 100
increment by 1;

commit;



--邮件代收代发模块
create sequence SEQ_MPS_MAILCONFIG
start with 1
increment by 1;

create sequence SEQ_MPS_MAILSEND
start with 1
increment by 1;

create sequence SEQ_MPS_MAILUNIFIEDCONFIG
start with 1
increment by 1;

create sequence SEQ_MPS_MAILRECEIVE
start with 1
increment by 1;

create sequence SEQ_MPS_MAILSENDHISTORY
start with 1
increment by 1;

create sequence SEQ_MPS_MAILRECEIVETASK
start with 1
increment by 1;

create sequence SEQ_MPS_MAILATTACH
start with 1
increment by 1;

create sequence SEQ_MPS_MAILDETAIL
start with 1
increment by 1;

commit;

