using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WcfService
{
    public class DataBase
    {
        public IEnumerable<Utilisateur> utilisateurs;
        public IEnumerable<Voiture> voitures;
        public IEnumerable<Resrvation> resrvations;
        private string conString;
        public DataContext db;

        public DataBase()
        {
            var xml = XDocument.Load(@"C:\locvfiles\Chemin.xml");
            var query = from c in xml.Root.Descendants("chemin")
                        select c.Element("c").Value;
            // c.Element("sgbd").Value;
            foreach (string c in query)
            {
                conString = c;
            }
            var query1 = from c in xml.Root.Descendants("chemin")
                         select c.Element("sgbd").Value;    
            db = new DataContext(conString);
            Table<Utilisateur> tblusers = db.GetTable<Utilisateur>();
            utilisateurs = tblusers;
            Table<Voiture> tblcars = db.GetTable<Voiture>();
            voitures = tblcars;
            Table<Resrvation> tblres = db.GetTable<Resrvation>();
            resrvations = tblres;
        }

        //tester l'existence d'un utilisateur
        public bool user_exist(string l, string p)
        {

            int nbr = 0;
            var requete = from user in utilisateurs
                          where user.Login == l
                          where user.Password == p
                          select user;
            foreach (Utilisateur obj in requete)
            {
                nbr++;
            }
            if (nbr == 0) return false;
            else return true;

        }

        //retourner un utilisateur
        public Utilisateur get_user(string l, string p)
        {

            if (user_exist(l, p))
            {
                var userr = (from user in utilisateurs
                             where user.Login == l
                             where user.Password == p
                             select user).FirstOrDefault();
                return userr;
            }
            else return new Utilisateur();
        }
        public Utilisateur get_user_byId(int id)
        {
            var userr = (from user in utilisateurs
                         where user.Id == id
                         select user).FirstOrDefault();
            return userr;
        }

        public List<Voiture> ListVoiture()
        {
            List<Voiture> list = new List<Voiture>();
            foreach (Voiture obj in voitures)
            {
                if (obj.Joignable.Equals("oui"))
                {
                    list.Add(obj);

                }
            }
            return list;

        }

        public Voiture get_voiture(int id)
        {

            Voiture v = new Voiture();
            v = (from user in voitures
                         where user.Id == id
                         select user).FirstOrDefault();
            return v;

        }

        public bool del_voiture(int id)
        {

           Voiture v = new Voiture();
            v = (from user in voitures
                 where user.Id == id
                 select user).FirstOrDefault();
            db.GetTable<Voiture>().DeleteOnSubmit(v);
            db.SubmitChanges();
            
            return true;


        }

        public bool add_voiture(string mat, string mod, string mar, string pri, string ima, string des, string car, string pas, string vil, string opt, string edi)
        {
            Voiture v = new Voiture() {
                Id = db.GetTable<Voiture>().Count() + 1,
                Matricule = mat,
                Carburant = car,
                Description = des,
                Image = ima,
                Marque = mar,
                Modele = mod,
                Options = opt,
                Passagers = Convert.ToInt32(pas),
                Prix = Convert.ToDouble(pri),
                Ville = vil,
                Joignable = "oui",
                Id_Manager = Convert.ToInt32(edi)
            };
            db.GetTable<Voiture>().InsertOnSubmit(v);
            db.SubmitChanges();
            return true;
        }

        public bool edit_voiture(int id, string mat, string mod, string mar, string pri, string ima, string des, string car, string pas, string vil, string opt, string edi)
        {
            Voiture v = new Voiture();
            v = (from user in voitures
                 where user.Id == id
                 select user).FirstOrDefault();

            v.Matricule = mat;
            v.Carburant = car;
            v.Description = des;
            v.Image = ima;
            v.Marque = mar;
            v.Modele = mod;
            v.Options = opt;
            v.Passagers = Convert.ToInt32(pas);
            v.Prix = Convert.ToDouble(pri);
            v.Ville = vil;
            v.Id_Manager = Convert.ToInt32(edi);

            db.SubmitChanges();
            return true;
        }

        public void save_location(int idv, string date_dd, string date_ff, int idc)
        {
            Resrvation r = new Resrvation()
            {
                Id = db.GetTable<Resrvation>().Count() + 1,
                Date_d = DateTime.Parse(date_dd),
                Date_f = DateTime.Parse(date_ff),
                Id_v = idv,
                Id_u = idc,
                Etat = "initiale"
                
            };
            db.GetTable<Resrvation>().InsertOnSubmit(r);
            db.SubmitChanges();
        }

        public List<Resrvation> ListLocations(int id)
        {
            List<Resrvation> list = new List<Resrvation>();

            Utilisateur a = new Utilisateur();
            a = (from user in utilisateurs
                 where user.Id == id
                 select user).FirstOrDefault();
                

            if (a.Role.Equals("Manager"))
            {
                foreach (Voiture v in voitures)
                {
                    foreach(Resrvation r in resrvations)
                    {
                        if(v.Id == r.Id_v)
                        {
                            if(v.Id_Manager == id)
                            {
                                if(! r.Etat.Equals("terminee"))
                                    list.Add(r);
                            }
                        }
                    }
                }



                return list;


            }
            else
            {
                var r = from res in resrvations
                        where res.Id_u == id
                        select res;
                foreach (Resrvation obj in r)
                {
                    if (!obj.Etat.Equals("terminee"))
                        list.Add(obj);
                }

                return list;
            }
           
        }

        public bool set_accepted(int id)
        {
           Resrvation r = (from user in resrvations
                           where user.Id == id
                           select user).FirstOrDefault();
            r.Etat = "confirmee";
            Voiture v = (from voi in voitures
                         where voi.Id == r.Id_v
                         select voi
                         ).FirstOrDefault();
            v.Joignable = "non";
            db.SubmitChanges();
            return true;

        }

        public bool set_refused(int id)
        {
            Resrvation r = (from user in resrvations
                            where user.Id == id
                            select user).FirstOrDefault();
            db.GetTable<Resrvation>().DeleteOnSubmit(r);

            Voiture v = (from voi in voitures
                         where voi.Id == r.Id_v
                         select voi
                        ).FirstOrDefault();
            v.Joignable = "oui";

            db.SubmitChanges();
            return true;

        }

        public bool add_user(string adr, string cin, string log, string nom, string pas, string pre, string tel)
        {
            Utilisateur u = new Utilisateur()
            {
                Id = db.GetTable<Utilisateur>().Count() + 1,
                Adresse = adr,
                Cin = cin,
                Login = log,
                Nom = nom,
                Password = pas,
                Prenom = pre,
                Role = "Client",
                Tel = tel
                              
            };
            db.GetTable<Utilisateur>().InsertOnSubmit(u);
            db.SubmitChanges();
            return true;
        }

        public List<Voiture> live_searche(string cle)
        {

            List<Voiture> list = new List<Voiture>();
            var ls = from voi in voitures
                     where voi.Marque.Contains(cle) || voi.Modele.Contains( cle ) || voi.Ville.Contains(cle )
                     select voi;
            foreach( Voiture v in ls)
            {
                list.Add(v);
            }
            return list;

        }


        public bool terminer(int id)
        {

            Resrvation r = (from user in resrvations
                            where user.Id == id
                            select user).FirstOrDefault();
            r.Etat = "terminee";
            Voiture v = (from voi in voitures
                         where voi.Id == r.Id_v
                         select voi
                        ).FirstOrDefault();
            v.Joignable = "oui";

            db.SubmitChanges();

            return true;
        }

        public List<Resrvation> ListLocationsByV(int id)
        {
            List<Resrvation> list = new List<Resrvation>();

            var rr = from res in resrvations
                    where res.Id_v == id
                    select res;
             
            foreach (Resrvation r in rr)
            {
                if(r.Etat.Equals("confirmee"))
                    list.Add(r);
            }
            foreach (Resrvation r in rr)
            {
                if ( r.Etat.Equals("terminee") )
                    list.Add(r);
            }
            return list;

            }

        public List<Voiture> ListVoitureL()
        {
            List<Voiture> list = new List<Voiture>();
            foreach (Voiture obj in voitures)
            {
                if (obj.Joignable.Equals("non"))
                {
                    list.Add(obj);

                }
            }
            return list;

        }

        public Utilisateur VoitureQuiL(int id)
        {
            Utilisateur a = (from voi in voitures
                    join res in resrvations
                    on voi.Id equals res.Id_v
                    join user in utilisateurs
                    on res.Id_u equals user.Id
                    where res.Etat.Equals("confirmee")
                    select user).FirstOrDefault();

            return a;
        }

        public Resrvation get_reservation(int id)
        {
            Resrvation r = (from res in resrvations
                           where res.Id == id
                           select res).FirstOrDefault();
            return r;
        }
    }

    

}



