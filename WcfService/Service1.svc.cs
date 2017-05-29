using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        DataBase db = new DataBase();

        public List<Resrvation> ListLocations(int id)
        {
            return db.ListLocations(id);
        }

        public bool edit_voiture(int id, string mat, string mod, string mar, string pri, string ima, string des, string car, string pas, string vil, string opt, string edi)
        {

            return db.edit_voiture(id, mat, mod, mar, pri, ima, des, car, pas, vil, opt, edi);
        }

        public bool add_voiture(string mat, string mod, string mar, string pri, string ima, string des, string car, string pas, string vil, string opt, string edi)
        {

            return db.add_voiture(mat, mod, mar, pri, ima, des, car, pas, vil, opt, edi);
        }

        public bool del_voiture(int id)
        {
            return db.del_voiture(id);
        }

        public Voiture get_voiture(int id)
        {
            return db.get_voiture(id);
        }

        public List<Voiture> ListVoiture()
        {
            return db.ListVoiture();
        }


        public bool user_exist(string login, string pass)
        {
            return db.user_exist(login, pass);
        }


        public Utilisateur get_user(string l, string p)
        {
            return db.get_user(l, p);
        }
        public Utilisateur get_user_byId(int id)
        {
            return db.get_user_byId(id);
        }

        public bool save_location(int idv, string date_dd, string date_ff, int idc)
        {
            db.save_location(idv, date_dd, date_ff, idc);
            return true;
        }

        public bool set_accepted(int id)
        {
            return db.set_accepted(id);
        }

        public bool set_refused(int id)
        {
            return db.set_refused(id);
        }

        public bool add_user(string adr, string cin, string log, string nom, string pas, string pre, string tel)
        {
            return db.add_user(adr, cin, log, nom, pas, pre, tel);
        }

        public List<Voiture> live_searche(string cle)
        {
            return db.live_searche(cle);
        }
        public bool terminer(int id)
        {
            return db.terminer(id);
        }

        public List<Resrvation> ListLocationsByV(int id)
        {
            return db.ListLocationsByV(id);
        }

        public List<Voiture> ListVoitureL()
        {
            return db.ListVoitureL();

        }

        public Utilisateur VoitureQuiL(int id)
        {
            return db.VoitureQuiL(id);
        }

        public Resrvation get_reservation(int id)
        {
            return db.get_reservation(id);
        }
    }
}
