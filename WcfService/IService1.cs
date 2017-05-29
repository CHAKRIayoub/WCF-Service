using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.OleDb;


namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1 {


        [OperationContract]
        Resrvation get_reservation(int id);

        [OperationContract]
        Utilisateur VoitureQuiL(int id);

        [OperationContract]
        List<Voiture> ListVoitureL();

        [OperationContract]
        List<Resrvation> ListLocationsByV(int id);

        [OperationContract]
        bool terminer(int id);

        [OperationContract]
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "Voiture")]
        List<Voiture> live_searche(string cle);

        [OperationContract]
        bool add_user(string adr, string cin, string log, string nom, string pas, string pre, string tel);

        [OperationContract]
        bool set_accepted(int id);

        [OperationContract]
        bool set_refused(int id);

        [OperationContract]
        List<Resrvation> ListLocations(int id);

        [OperationContract]
        bool save_location(int idv, string date_dd, string date_ff, int idc);

        [OperationContract]
        bool edit_voiture(int id, string mat, string mod, string mar, string pri, string ima, string des, string car, string pas, string vil, string opt, string edi);
    
        [OperationContract]
        bool del_voiture(int id);

        [OperationContract]
        bool add_voiture(string mat, string mod, string mar, string pri, string ima, string des, string car, string pas, string vil, string opt, string edi);

        [OperationContract]
        List<Voiture> ListVoiture();

        [OperationContract]
        bool user_exist(string login, string pass);

        [OperationContract]
        Utilisateur get_user(string login, string pass);

        [OperationContract]
        Utilisateur get_user_byId(int id);

        [OperationContract]
        Voiture get_voiture(int id);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }



    
    [DataContract][Table(Name = "Utilisateur")]
    public class Utilisateur
    {
       
        [DataMember][Column(Name = "Id", IsPrimaryKey = true)]
        public int Id;
       
        [DataMember][Column]
        public string Cin;
        
        [DataMember][Column]
        public string Nom;
        
        [DataMember][Column]
        public string Prenom;
        
        [DataMember][Column]
        public string Tel;
        
        [DataMember][Column]
        public string Adresse;

        [DataMember][Column]
        public string Role;

        [DataMember][Column] 
        public string Login;

        [DataMember][Column]
        public string Password;

    }


    [DataContract][Table(Name = "Voiture")]
    public class Voiture
    {
        [DataMember]
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int Id;

        [DataMember]
        [Column]
        public string Matricule;

        [DataMember]
        [Column]
        public string Marque;

        [DataMember]
        [Column]
        public string Modele;

        [DataMember]
        [Column]
        public Double Prix;

        [DataMember]
        [Column]
        public string Image;


        [DataMember]
        [Column]
        public string Description;

        [DataMember]
        [Column]
        public string Carburant;

        [DataMember]
        [Column]
        public int Passagers;

        [DataMember]
        [Column]
        public string Ville;

        [DataMember]
        [Column]
        public string Options;


        [DataMember]
        [Column]
        public int Id_Manager;


        [DataMember]
        [Column]
        public string Joignable;
    }

    [DataContract]
    [Table(Name = "Resrvation")]
    public class Resrvation
    {
        [DataMember]
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int Id;

        [DataMember]
        [Column]
        public DateTime Date_d;

        [DataMember]
        [Column]
        public DateTime Date_f;

        [DataMember]
        [Column]
        public int Id_v;

        [DataMember]
        [Column]
        public int Id_u;

        [DataMember]
        [Column]
        public string Etat;

    }
}
