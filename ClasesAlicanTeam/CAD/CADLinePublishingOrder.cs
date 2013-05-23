using ClasesAlicanTeam.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ClasesAlicanTeam.CAD
{
    public class CADLinePublishingOrder : ICAD
    {
        

        public CADLinePublishingOrder()
        {
            init();
        }

        public  Boolean insert(ENLinePublishingOrder linePublishingOrder)
        {

            try
            {
                connect();
                SqlCommand cmd = new SqlCommand("INSERT INTO Order_Lines_Publishing_Orders (idLines_POrders, idHouse_order, idNews, Quantity) VALUES (@idLines_POrders, @idHouse_order, @ideNews, @Quantity)", connection);

                cmd.Parameters.Add(new SqlParameter("@IdHouse_Orders", linePublishingOrder.IdLines_POrders));
                cmd.Parameters.Add(new SqlParameter("@idHouse_order", linePublishingOrder.PublishingOrder.IdHouse_Orders));
                // cmd.Parameters.Add(new SqlParameter("@idNews", linePublishingOrder.NewBook.IdNews)); falta implementar en ENNewBook
                cmd.Parameters.Add(new SqlParameter("@Quantity", linePublishingOrder.Quantity));
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

        public  Boolean delete(ENLinePublishingOrder linePublishingOrder)
        {

            try
            {
                connect();
                SqlCommand cmd = new SqlCommand("DELETE FROM Order_Lines_Publishing_Orders WHERE idLines_POrders = @idLines_POrders AND IdHouse_Orders=@IdHouse_Orders", connection);

                cmd.Parameters.Add(new SqlParameter("@IdHouse_Orders", linePublishingOrder.PublishingOrder.IdHouse_Orders));
                cmd.Parameters.Add(new SqlParameter("@idLines_POrders", linePublishingOrder.IdLines_POrders));
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
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

        public  Boolean update(ENLinePublishingOrder a)
        {
            return true;
        }

        public  Boolean read(int a)
        {
            return true;
        }

        public  List<CADPublishingOrder> readAll()
        {
            return null;
        }
    }
}

