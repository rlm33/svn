using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClasesAlicanTeam.EN;
using ClasesAlicanTeam.CAD;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ClasesAlicanTeam.CAD
{
    public class CADNew : CADBook
    {

        public CADNew() 
        {
            init();
        }

        public  Boolean insert(ENNew book)
        {
            
            try
            {
                connect();
                SqlCommand cmd = new SqlCommand("INSERT INTO News (idnews) VALUES(@idnews)", connection);
                cmd.Parameters.Add(new SqlParameter("@id", book.IDBook));
                

                
                if (cmd.ExecuteNonQuery() == 1)//este comando sirve para ejecutar la sentencia
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

        public  Boolean modify(ENNew book)
        {
            //Método innecesario
            return true;
        }

        public  Boolean delete(ENNew book)
        {
            

            try
            {
                connect();

                SqlCommand cmd = new SqlCommand("DELETE FROM News WHERE idnews=@idnews", connection);
                cmd.Parameters.Add(new SqlParameter("@idnews", book.IDBook));
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            finally
            {
                disconnect();
            }
        }

        
        public  ENNew read(String id)
        {

            try
            {

                connect();


                SqlCommand cmd = new SqlCommand("SELECT * FROM News WHERE idnews=@id)", connection);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                ENBook book = base.read(id);

                ENNew nuevo = new ENNew();
                
                nuevo.IDBook = dr["idnews"].ToString(); //devuelve un objeto EN que tendra todos sus datos
                nuevo.Quantity = int.Parse(dr["quantity"].ToString());
                nuevo.CIF = book.CIF;
                nuevo.Course = book.Course;
                nuevo.Description = book.Description;
                nuevo.Name = book.Name;
                nuevo.Subject = book.Subject;
                nuevo.Years = book.Years;
                

                dr.Close();

                return nuevo;
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

        public  List<ENNew> readAllNewBooks()
        {
            List<ENNew> news = new List<ENNew>();
            List<ENBook> books = new List<ENBook>();
            List<ENNew> resultado = new List<ENNew>();
            SqlConnection connection = null;
            try
            {
                connect();
                DataTable dt = new DataTable();
                SqlDataAdapter adaptador;
                DataSet ds = new DataSet();

                adaptador = new SqlDataAdapter("SELECT * FROM  News ", connection);
                adaptador.Fill(ds, "News");


                dt = ds.Tables["News"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ENNew book = new ENNew();
                    book.IDBook = dt.Rows[i][0].ToString();
                    book.Quantity = int.Parse(dt.Rows[i][1].ToString());

                    news.Add(book);
                }

                books = base.readAll();
                int k = 0;
                for (int i = 0; i < books.Capacity; i++)
                {
                    for(int j = 0; j < news.Capacity; j++)
                    {
                        if (books[i].IDBook == news[j].IDBook)
                        {

                            resultado.Add(news[j]);
                            resultado[k].CIF = books[j].CIF;
                            resultado[k].Course = books[j].Course;
                            resultado[k].Description = books[j].Description;
                            resultado[k].Name = books[j].Name;
                            resultado[k].Subject = books[j].Subject;
                            resultado[k].Years = books[j].Years;
                            k++;
                        }
                    }
                    
                }

                return resultado;
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
    }
}
