using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSIS.Entity
{
    public static class ExtensionMethods
    {
        public static bool PopulateFromCSVLine(this t_x tx, string csvLine, char deli=',')
        {
            string[] strs = csvLine.Split(deli);

            if (strs.Length < 62)
            {
                return false;
            }
            int i = 0;

            tx.Co_ID = strs[i].Trim(); i++;
            tx.PO_ID = strs[i].Trim(); i++;
            tx.Exchange = strs[i].Trim(); i++;
            tx.Name = strs[i].Trim(); i++;
            tx.Root_Ticker = strs[i].Trim(); i++;
            tx.qmv = strs[i].Trim(); i++;
            tx.OS_Shares = strs[i].Trim(); i++;
            tx.tech_sub_sec = strs[i].Trim(); i++;
            tx.cps = strs[i].Trim(); i++;
            tx.re = strs[i].Trim(); i++;
            tx.Sector = strs[i].Trim(); i++;
            tx.Sub_Sector = strs[i].Trim(); i++;
            tx.type_of_listing = strs[i].Trim(); i++;
            tx.date_of_listing = strs[i].Trim(); i++;
            tx.QT_Date = strs[i].Trim(); i++;
            tx.RTO_Date = strs[i].Trim(); i++;
            tx.Date_of_amalgamation = strs[i].Trim(); i++;
            tx.Interlisted = strs[i].Trim(); i++;
            tx.formar_cpc_or_cpc = strs[i].Trim(); i++;
            tx.Ven_50_2018 = strs[i].Trim(); i++;
            tx.Index = strs[i].Trim(); i++;
            tx.hq_location = strs[i].Trim(); i++;
            tx.hq_region = strs[i].Trim(); i++;
            tx.usa_city = strs[i].Trim(); i++;
            tx.USA_State = strs[i].Trim(); i++;
            tx.Asia_Region = strs[i].Trim(); i++;
            tx.clean_tech_primary = strs[i].Trim(); i++;
            tx.clean_tech_sub_sector = strs[i].Trim(); i++;
            tx.Section = strs[i].Trim(); i++;
            tx.trust = strs[i].Trim(); i++;
            tx.volume_ytd = strs[i].Trim(); i++;
            tx.value__ytd = strs[i].Trim(); i++;
            tx.num_traded_share_ytd = strs[i].Trim(); i++;
            tx.num_of_month_trading_data = strs[i].Trim(); i++;
            tx.AFRICA = strs[i].Trim(); i++;
            tx.AUS_NZ_PNG = strs[i].Trim(); i++;
            tx.CANADA = strs[i].Trim(); i++;
            tx.CHINA__ASIA = strs[i].Trim(); i++;
            tx.LATIN_AMERICA = strs[i].Trim(); i++;
            tx.OTHER = strs[i].Trim(); i++;
            tx.UK_EUROPE = strs[i].Trim(); i++;
            tx.USA = strs[i].Trim(); i++;
            tx.Oil_Gas = strs[i].Trim(); i++;
            tx.Gold = strs[i].Trim(); i++;
            tx.Silver = strs[i].Trim(); i++;
            tx.Copper = strs[i].Trim(); i++;
            tx.Nickel = strs[i].Trim(); i++;
            tx.Diamond = strs[i].Trim(); i++;
            tx.Molybdenum = strs[i].Trim(); i++;
            tx.Platinum_PGM = strs[i].Trim(); i++;
            tx.Iron = strs[i].Trim(); i++;
            tx.Lead = strs[i].Trim(); i++;
            tx.Zinc = strs[i].Trim(); i++;
            tx.Rare_Earths = strs[i].Trim(); i++;
            tx.Potash = strs[i].Trim(); i++;
            tx.Lithium = strs[i].Trim(); i++;
            tx.Uranium = strs[i].Trim(); i++;
            tx.Coal = strs[i].Trim(); i++;
            tx.Tungsten = strs[i].Trim(); i++;
            tx.base__precious_metals = strs[i].Trim(); i++;
            tx.mineral_properties = strs[i].Trim(); i++;
            if (i<strs.Length)
                tx.other_properties = strs[i].Trim(); i++;


            return true;
        }

        public static bool PopulateArFromFile(this tsEntities db, string fileName)
        {
            try
            {
                string[] lines = File.ReadAllLines(fileName);

                Debug.WriteLine(fileName);
                for (int i = 0; i < lines.Length; i++)
                {
                    Debug.WriteLine(i.ToString() + lines[i]);
                    if (lines[i].Trim().Contains("-"))
                    {
                        ar a = new ar();
                        string[] sy = lines[i].Split('-');
                        a.rt = sy[0].Trim();

                        if (a.rt.Trim() == string.Empty)
                        {
                            int lll = 0;
                        }
                        a.ex = sy[1].Trim();
                        i++; i++;

                        double pr = 0;
                        if (!double.TryParse(lines[i], out pr))
                        {
                            pr = 0;
                        }
                        i++;

                        pr p = new pr();
                        p.rt = a.rt;
                        p.ex = a.ex;
                        p.price = pr;

                        try
                        {
                            pr pd = db.prs.SingleOrDefault(p1 => (p1.rt.Trim() == p.rt.Trim() && p1.ex.Trim() == p.ex.Trim()));
                            if (pd == null)
                            {
                                db.prs.Add(p);
                                db.SaveChanges();
                                db.RefreshDatabase(p);
                            }
                            else
                            {
                                pd.price = p.price;
                                db.SaveChanges();
                                db.RefreshDatabase(pd);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                        }

                        double ht = 0;
                        if (!double.TryParse(lines[i], out ht))
                        {
                            ht = 0;
                        }
                        i++;
                        a.ht = ht;

                        double lt = 0;
                        if (!double.TryParse(lines[i], out lt))
                        {
                            lt = 0;
                        }
                        i++;
                        a.lt = lt;

                        double met = 0;
                        if (!double.TryParse(lines[i], out met))
                        {
                            met = 0;
                        }
                        i++;
                        a.met = met;

                        double mdt = 0;
                        if (!double.TryParse(lines[i], out mdt))
                        {
                            mdt = 0;
                        }

                        a.mdt = mdt;
                        try
                        {
                            ar ad = db.ars.SingleOrDefault(p1 => (p1.rt.Trim() == p.rt.Trim() && p1.ex.Trim() == p.ex.Trim()));
                            if (ad == null)
                            {
                                db.ars.Add(a);
                                db.SaveChanges();
                                db.RefreshDatabase(a);
                            }
                            else
                            {
                                ad.ht = a.ht;
                                ad.lt = a.lt;
                                ad.met = a.met;
                                ad.mdt = a.mdt;
                                db.SaveChanges();
                                db.RefreshDatabase(ad);
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }



            return true;
        }

        public static bool PopulatePerFromFile(this tsEntities db, string fileName)
        {
            try
            {
                string[] lines = File.ReadAllLines(fileName);

                Debug.WriteLine(fileName);
                for (int i = 0; i < lines.Length; i++)
                {
                    Debug.WriteLine(i.ToString() + lines[i]);
                    if (lines[i].Trim().Contains("-"))
                    {
                        per a = new per();
                        string[] sy = lines[i].Split('-');
                        a.rt = sy[0].Trim();
                        a.ex = sy[1].Trim();
                        i++; i++;

                        if (a.rt.Trim().ToLower() == "")
                        {
                            int iii = 0;
                        }


                        double pr = 0;
                        if (!double.TryParse(lines[i], out pr))
                        {
                            pr = 0;
                        }
                        i++;

                        pr p = new pr();
                        p.rt = a.rt;
                        p.ex = a.ex;
                        p.price = pr;

                        try
                        {
                            pr pd = db.prs.SingleOrDefault(p1 => (p1.rt.Trim() == p.rt.Trim() && p1.ex.Trim() == p.ex.Trim()));
                            if (pd == null)
                            {
                                db.prs.Add(p);
                                db.SaveChanges();
                                db.RefreshDatabase(p);
                            }
                            else
                            {
                                pd.price = p.price;
                                db.SaveChanges();
                                db.RefreshDatabase(pd);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                        }

                        double ht = 0;
                        lines[i]=lines[i].Replace("%", "0");
                        if (!double.TryParse(lines[i], out ht))
                        {
                            ht = 0;
                        }
                        i++;
                        a.ytd = ht;

                        double lt = 0;
                        lines[i] = lines[i].Replace("%", "0");
                        if (!double.TryParse(lines[i], out lt))
                        {
                            lt = 0;
                        }
                        i++;
                        a.five_day = lt;

                        double oneM = 0;
                        lines[i] = lines[i].Replace("%", "0");
                        if (!double.TryParse(lines[i], out oneM))
                        {
                            oneM = 0;
                        }
                        i++;
                        a.one_month = oneM;

                        double threeM = 0;
                        lines[i] = lines[i].Replace("%", "0");
                        if (!double.TryParse(lines[i], out threeM))
                        {
                            threeM = 0;
                        }
                        i++;
                        a.three_month = threeM;

                        double oneY = 0;
                        lines[i] = lines[i].Replace("%", "0");
                        if (!double.TryParse(lines[i], out oneY))
                        {
                            oneY = 0;
                        }
                        a.one_year = oneY;

                        try
                        {
                            per ad = db.pers.SingleOrDefault(p1 => (p1.rt.Trim() == p.rt.Trim() && p1.ex.Trim() == p.ex.Trim()));
                            if (ad == null)
                            {
                                db.pers.Add(a);
                                db.SaveChanges();
                                db.RefreshDatabase(a);
                            }
                            else
                            {
                                ad.ytd = a.ytd;
                                ad.five_day = a.five_day;
                                ad.one_month = a.one_month;
                                ad.three_month = a.three_month;
                                ad.one_year = a.one_year;
                                db.SaveChanges();
                                db.RefreshDatabase(ad);
                            }

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return true;
        }

        public static void DumpTsxvSym(this tsEntities db, string fileName)
        {
            foreach (var r in db.t_x.OrderBy(a => a.Root_Ticker))
            {
                if(r.Name.Trim()!=string.Empty)
                    File.AppendAllText(fileName, r.Root_Ticker+"-X,");
            }
        }
        public static void DumpTsxSym(this tsEntities db, string fileName)
        {
            foreach (var r in db.t_s.OrderBy(a => a.root__ticker))
            {
                if (r.Name.Trim() != string.Empty)
                    File.AppendAllText(fileName, r.root__ticker + "-T,");
            }
        }

        public static void RefreshDatabase(this tsEntities db, Object entity)
        {
            ((IObjectContextAdapter)db)
                .ObjectContext
                .Refresh(RefreshMode.StoreWins, entity);
        }

    }
}
