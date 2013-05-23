using ClasesAlicanTeam.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace ClasesAlicanTeam.CAD
{
    public class CADPublishingOrder : ICAD
    {
        public CADPublishingOrder()
        {
            init();
        }

        public  Boolean insert(ENPublishingOrder publishingOrder)
        {

            try
            {

                connect();
                SqlCommand cmd = new SqlCommand("INSERT INTO Publishing_House_Orders(idPHouse_Orders, CIF, DataOrder) VALUES (@idPHouse_Orders, @CIF, @DataOrder)", connection);

                cmd.Parameters.Add(new SqlParameter("@idPHouse_Orders", publishingOrder.IdHouse_Orders));
                cmd.Parameters.Add(new SqlParameter("@CIF", publishingOrder.CIF));
                cmd.Parameters.Add(new SqlParameter("@DataOrder", publishingOrder.DataOrder));
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                disconnect();

            }
        }

        public  Boolean delete(ENPublishingOrder publishingOrder)
        {

            try
            {

                connect();
                SqlCommand cmd = new SqlCommand("DELETE FROM Publishing_House_Orders WHERE idPHouse_Orders=@idPHouse_Orders", connection);

                cmd.Parameters.Add(new SqlParameter("@idPHouse_Orders", publishingOrder.IdHouse_Orders));
                if (cmd.ExecuteNonQuery() == 1)
                {
                    SqlCommand cmdlines = new SqlCommand("DELETE FROM Order_Lines_Customers WHERE idPHouse_Orders=@idPHouse_Orders ", connection);
                    cmdlines.Parameters.Add(new SqlParameter("@idPHouse_Orders", publishingOrder.IdHouse_Orders));
                    if (cmdlines.ExecuteNonQuery() >= 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                disconnect();

            }
        }

        public  object update(object x)
        {
            return true;
        }

        public  object read(object x)
        {
            return true;
        }

        public  List<object> readAll()
        {
            return null;
        }
    }
}

