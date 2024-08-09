select 
  males.sex as sex_first,
  males.name as name_first,
  females.sex as sex_second,
  females.name as name_second
from (
  select 
    row_number() over (partition by pm.sex) as LINENUM,
    pm.sex, 
    pm.name
  from company_test_persons pm
  where pm.sex = 'М'
) males
full join (
  select 
    row_number() over (partition by pf.sex) as LINENUM,
    pf.sex, 
    pf.name
  from company_test_persons pf
  where pf.sex = 'Ж'
) females on males.LINENUM = females.LINENUM;
