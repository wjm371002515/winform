create tablespace whc_tbs datafile 'C:\app\Administrator\oradata\whcdb\whc.dbf' size 100M;
--DROP TABLESPACE whc_tbs INCLUDING CONTENTS AND DATAFILES CASCADE CONSTRAINTS;
create user whc identified by whc default tablespace whc_tbs;

grant connect,resource to whc; 
grant dba to whc;
--Revoke dba from whc;