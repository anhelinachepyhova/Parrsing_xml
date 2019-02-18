title load file
cls
@echo off

:start
if not exist .\in\%1.csv goto nofile

sqlldr PARRSING/PARRSING@ORACLE11 , control = T_SOURCE_TABLE.ctl, log = .\log\%1.log, bad = .\bad\%1.bad, discard=.\discard\%1.dis, data=.\in\%1.csv, SKIP=0, errors=0

:nofile
echo No such file

:end
