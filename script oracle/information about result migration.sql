select upper(trim(t.name_0)) as name_0,
       upper(trim(t.mdl_applying_result_0)) as mdl_applying_result_0,
       upper(trim(t.name_1)) as name_1,
       trim(t.child_type_1) as child_type_1,
       trim(t.name_2) as name_2,     
       trim(t.mdl_applying_result_2) as mdl_applying_result_2,
       upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 2))) as Object_1,
       upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 3))) as Object_2,
       upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 4))) as Object_3,
       upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 5))) as Object_4,
       upper(trim(REGEXP_SUBSTR( trim(t.artificial_src_full_name_2),  ' [^,]+', 1, 6))) as Object_5     
from PARRSING.T_TARGET_TABLE t
where trim(t.name_0) != 'aws_oracle_ext'
 
