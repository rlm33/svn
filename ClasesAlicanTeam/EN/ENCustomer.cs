using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ClasesAlicanTeam.CAD;

namespace ClasesAlicanTeam.EN
{
    public class ENCustomer : ENUser
    {
        private int idUser;
        private string name;
        private string surname;
        private int telephone;
        private string adress;
        private List<ENAlarm> alarms;
        private bool alarmsLoaded;
        private List<ENCustomerOrder> orders;
        private bool ordersLoaded;
        private List<ENAdvertisement> advertisements;
        private bool advertisementsLoaded;

        #region //Getters & Setters

        /// <summary>
        /// Devuelve y establece el Id de usuario.
        /// </summary>
        public int IdUser
        {
            get { return idUser; }
            set { idUser = value; }
        }

        /// <summary>
        /// Devuelve y establece el nombre de cliente.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Devuelve y establece el apellido del cliente.
        /// </summary>
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        /// <summary>
        /// Devuelve y establece el telefono del cliente.
        /// </summary>
        public int Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        /// <summary>
        /// Devuelve y establece la dirección del cliente.
        /// </summary>
        public string Adress
        {
            get { return adress; }
            set { adress = value; }
        }
        /*
        /// <summary>
        /// Devuelve las alarmas del cliente.
        /// </summary>
        public List<ENAlarm> Alarms
        {
            get
            {
                this.LoadAlarms();
                return alarms;
            }
        }
        
        /// <summary>
        /// Devuelve los pedidos del cliente.
        /// </summary>
        public List<ENCustomerOrder> Orders
        {
            get
            {
                this.LoadOrders();
                return this.orders;
            }
        }
        
        /// <summary>
        /// Devuelve los anuncios realizados por el cliente actual.
        /// </summary>
        public List<ENAdvertisement> Advertisements
        {
            get
            {
                this.LoadAdvertisements();
                return this.advertisements;
            }
        }
        */
        #endregion

        #region //Private Methods

        private void LoadAlarms()
        {
            if (!alarmsLoaded)
            {
                DataTable dTable = new CADAlarms_Has_Customers().GetAlarmsFrom(this.id);
                foreach (DataRow rows in dTable.Rows)
                {
                    alarms.Add(new ENAlarm().Read((int)rows["idAlarms"]));
                }
                alarmsLoaded = true;
            }
        }

        private void LoadOrders()
        {
            if (!ordersLoaded)
            {
                this.orders = (new ENCustomerOrder()).Filter("idCustomers = " + this.id);
                ordersLoaded = true;
            }
        }

        private void LoadAdvertisements()
        {
            if (!advertisementsLoaded)
            {
                this.advertisements = new ENAdvertisement().ReadFromCustomer(this.id);
                advertisementsLoaded = true;
            }
        }

        protected override DataRow ToDataRow
        {
            get
            {
                DataRow ret = cad.GetVoidRow;
                ret["ID"] = this.id;
                ret["idUser"] = this.idUser;
                ret["Name"] = this.name;
                ret["Surname"] = this.surname;
                ret["Telephone"] = this.telephone;
                ret["Address"] = this.adress;
                return ret;
            }
        }

        protected override void FromRow(DataRow Row)
        {
            ENUser u = base.Read((int)Row["idUser"]);
            this.Account = u.Account;
            this.Password = u.Password;
            this.id = (int)Row["ID"];
            this.idUser = (int)Row["idUser"];
            this.name = (string)Row["Name"];
            this.surname = (string)Row["Surname"];
            this.telephone = (int)Row["Telephone"];
            this.adress = (string)Row["Address"];
        }

        #endregion

        #region //Public Methods

        /// <summary>
        /// Constructor por defecto que inicializa el ojbeto con sus campos vacíos.
        /// </summary>
        public ENCustomer() 
            : base()
        {
            cad = new CADCustomer();
            this.idUser = 0;
            this.name = "";
            this.surname = "";
            this.telephone = 0;
            this.adress = "";
            alarms = new List<ENAlarm>();
            alarmsLoaded = false;
            orders = new List<ENCustomerOrder>();
            ordersLoaded = false;
            advertisements = new List<ENAdvertisement>();
            advertisementsLoaded = false;

        }

        /// <summary>
        /// Constructor sobrecargado que inicializa el objeto con los datos pasados por parámetro.
        /// </summary>
        /// <param name="account">Nombre de la cuenta.</param>
        /// <param name="password">Contraseña de la cuenta.</param>
        /// <param name="name">Nombre del cliente.</param>
        /// <param name="surname">Apellido del cliente.</param>
        /// <param name="telephone">Telefono del cliente.</param>
        /// <param name="adress">Direccion del cliente.</param>
        public ENCustomer(string account, string password, string name, string surname, int telephone, string adress) 
            : base(account, password)
        {
            this.name = name;
            this.surname = surname;
            this.telephone = telephone;
            this.adress = adress;
            alarms = new List<ENAlarm>();
            alarmsLoaded = false;
            orders = new List<ENCustomerOrder>();
            ordersLoaded = false;
            advertisements = new List<ENAdvertisement>();
            advertisementsLoaded = false;
            cad = new CADCustomer();
        }

        /// <summary>
        /// Busca el cliente en la base de datos y lo devuelve. 
        /// </summary>
        /// <param name="id">Identificador del cliente a buscar.</param>
        /// <returns>ENCustomer encontrado en la base de datos.</returns>
        public ENCustomer Read(int id)
        {
            cad = new CADCustomer();
            ENCustomer ret = new ENCustomer();
            List<object> param = new List<object>();
            param.Add((object)id);
            ret.FromRow(cad.Select(param));
            return ret;
        }

        /// <summary>
        /// Devuelve todos los clientes en la base de datos.
        /// </summary>
        /// <returns>Lista de ENCustomers con todos los clientes de la base de datos.</returns>
        public List<ENCustomer> ReadAll()
        {
            List<ENCustomer> ret = new List<ENCustomer>();
            DataTable tabla = cad.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENCustomer nuevo = new ENCustomer();
                nuevo.FromRow(rows);
                ret.Add(nuevo);
            }
            return ret;
        }

        /// <summary>
        /// Se inscribe a una alarma añadiendola a la lista y la inserta en la base de datos.
        /// </summary>
        /// <param name="alarm">ENAlarm a la que se inscribe.</param>
        public void AddENAlarm(ENAlarm alarm)
        {
            this.LoadAlarms();
            this.alarms.Add(alarm);
            CADAlarms_Has_Customers ahc = new CADAlarms_Has_Customers();
            DataRow nueva = ahc.GetVoidRow;
            nueva["idCustomers"] = this.id;
            nueva["idAlarms"] = alarm.Id;
            ahc.Insert(nueva);
        }

        /// <summary>
        /// Añade un pedido de cliente a la lista y la inserta en la base de datos.
        /// </summary>
        /// <param name="order">ENCustomerOrder a añadir.</param>
        public void AddENCUsotmerOrder(ENCustomerOrder order)
        {
            this.LoadOrders();
            this.orders.Add(order);
            //falta tener hecho ENCustomerOrder
            throw new NotImplementedException();
        }

        /// <summary>
        /// Añade un anuncio a la lista y lo inserta en la base de datos.
        /// </summary>
        /// <param name="advertisement">Anuncio a insertar.</param>
        public void AddENAdvertisement(ENAdvertisement advertisement)
        {
            this.LoadAdvertisements();
            advertisement.Customer = this;
            this.advertisements.Add(advertisement);
            advertisement.Save();
        }

        public List<ENCustomer> Filter(String where)
        {
            List<ENCustomer> ret = new List<ENCustomer>();
            DataTable table = cad.SelectWhere(where);

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    ENCustomer course = new ENCustomer();
                    course.FromRow(row);
                    ret.Add(course);

                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override int Save()
        {
            ENUser us = new ENUser();
            us.Account = this.Account;
            us.Password = this.Password;
            us.Save();
            this.idUser = us.Id;
            if (id == 0)
            {
                return this.id = cad.Insert(ToDataRow);
            }
            else
            {
                cad.Update(ToDataRow);
                return 0;
            }
        }

        #endregion
    }
}