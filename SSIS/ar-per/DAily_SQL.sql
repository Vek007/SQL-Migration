select * from [ts].[dbo].[view_ar_sec_t] where lt_diff_per >= 50 and lt_diff_per <= 200 order by ex desc, Sector desc, ht_diff_per desc;

