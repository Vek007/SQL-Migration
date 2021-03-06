select * from [ts].[dbo].[view_ar_sec_t] where lt_diff_per >= 50 and lt_diff_per <= 200 order by ex desc, Sector desc, ht_diff_per desc;

SELECT p.rt, t.name, t.sector, t.Sub_Sector, p.ex, p.per_dayly_change d_per, p.vol, 
p.price, p.week_52_h,p.week_52_l  
FROM [ts].[dbo].[pr] p 
inner join t_s t on p.rt = t.root__ticker where p.per_dayly_change < -5 or p.per_dayly_change > 5
order by ex, sector, d_per 