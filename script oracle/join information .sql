select
      s.name_0,
      s.name_1,
      s.child_type_1,
      s.name_2,
      s.object_loading_2,
      w.flag_for_migration
from
(select upper(trim(s.name_0)) as name_0,
       upper(trim(s.name_1)) as name_1,
       upper(trim(s.child_type_1)) as child_type_1,
       upper(trim(s.name_2)) as name_2,
       s.object_loading_2
from PARRSING.T_SOURCE_TABLE s
where trim(s.object_loading_2) = 'extended')s

left join (select distinct upper(trim(w.object_type)) as object_type,
                  upper(trim(w.schema_name)) as schema_name,
                  upper(trim(w.object_name)) as object_name,
                  'Y' as flag_for_migration
           from PARRSING.T_FILE_FOR_WORK w) w
on s.child_type_1 = w.object_type  
   and w.schema_name = s.name_0
   and w.object_name =  s.name_2           

left join (select upper(trim(t.name_0)) as name_0,
                  upper(trim(t.mdl_applying_result_0)) as mdl_applying_result_0,
                  upper(trim(t.name_1)) as name_1,
                  trim(t.child_type_1) as child_type_1,
                  trim(t.name_2) as name_2,     
                  trim(t.mdl_applying_result_2) as mdl_applying_result_2,
                  upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 2))) as Object_1,
                  case 
                      when upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 3))) = 'PACKAGES' then 'PACKAGE'
                      when upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 3))) = 'SEQUENCES' then 'SEQUENCE'    
                      when upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 3))) = 'TABLES' then 'TABLE'  
                      when upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 3))) = 'VIEWS' then 'VIEW' 
                      when upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 3))) = 'SEQUENCES' then 'SEQUENCE'    
                          else upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 3)))                                                            
                  end as Object_2,
                  upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 4))) as Object_3,
                  upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 5))) as Object_4,
                  upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 6))) as Object_5     
           from PARRSING.T_TARGET_TABLE t
           where trim(t.name_0) != 'aws_oracle_ext'
