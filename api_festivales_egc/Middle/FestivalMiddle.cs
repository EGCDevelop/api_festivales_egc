using api_festivales_egc.Models;
using api_festivales_egc.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace api_festivales_egc.Middle
{
    public class FestivalMiddle
    {
        public static List<Festival> EXEC_SP_GET_FESTIVAL_BY_FILTERS(string connectionString, string festivalName, int festivalYear)
        {
            List<Festival> dataList = [];

            using SqlConnection conn = new(connectionString);
            conn.Open();

            using SqlCommand cmd = new("SP_GET_FESTIVAL_BY_FILTERS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@festivalName", SqlDbType.NVarChar, -1).Value = festivalName;
            cmd.Parameters.Add("@festivalYear", SqlDbType.BigInt).Value = festivalYear;

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Festival festival = new()
                {
                    Id = Helpers.GetValue<int>(reader, "Id"),
                    Nombres = Helpers.GetValue<string>(reader, "Nombres"),
                    Anio = Helpers.GetValue<int>(reader, "Anio"),
                    Ubicacion = Helpers.GetValue<string>(reader, "Ubicacion"),
                    Fecha = Helpers.GetValue<DateOnly>(reader, "Fecha"),
                    HoraInicio = Helpers.GetValue<TimeOnly>(reader, "HoraInicio"),
                    HoraFin = Helpers.GetValue<TimeOnly>(reader, "HoraFin"),
                    PrecioEntrada = Helpers.GetValue<decimal>(reader, "PrecioEntrada"),
                };

                dataList.Add(festival);
            }

            return dataList;

        }

        public static List<BandFestival> EXEC_SP_GET_BANDS_INVITED_TO_FESTIVAL(string connectionString, string bandName, byte bandState)
        {
            List<BandFestival> dataList = [];

            using SqlConnection conn = new(connectionString);
            conn.Open();

            using SqlCommand cmd = new("SP_GET_BANDS_INVITED_TO_FESTIVAL", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@bandName", SqlDbType.NVarChar, -1).Value = bandName;
            cmd.Parameters.Add("@bandState", SqlDbType.TinyInt).Value = bandState;

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                BandFestival festival = new()
                {
                    Id = Helpers.GetValue<int>(reader, "Id"),
                    NombreBanda = Helpers.GetValue<string>(reader, "NombreBanda"),
                    Estado = Helpers.GetValue<byte>(reader, "Estado"),
                };

                dataList.Add(festival);
            }

            return dataList;

        }

        public static List<FestivalInvitation> EXEC_SP_GET_INVITATIONS_FESTIVAL(string connectionString, byte isActive, byte participation)
        {
            List<FestivalInvitation> dataList = [];

            using SqlConnection conn = new(connectionString);
            conn.Open();

            using SqlCommand cmd = new("SP_GET_INVITATIONS_FESTIVAL", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@isActive", SqlDbType.TinyInt).Value = isActive;
            cmd.Parameters.Add("@participation", SqlDbType.TinyInt).Value = participation;

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                FestivalInvitation festival = new()
                {
                    Id = Helpers.GetValue<int>(reader, "Id"),
                    NombreBanda = Helpers.GetValue<string>(reader, "NombreBanda"),
                    Estado = Helpers.GetValue<byte>(reader, "Estado"),
                    CantidadEntradas = Helpers.GetValue<int>(reader, "CantidadEntradas"),
                    Participacion = Helpers.GetValue<byte>(reader, "Participacion"),
                    FESId = Helpers.GetValue<int>(reader, "FESId"),
                    BIFId = Helpers.GetValue<int>(reader, "BIFId"),
                };

                dataList.Add(festival);
            }

            return dataList;
        }

        public static List<DetailInvitationsFestival> EXEC_SP_GET_DETAIL_INVITATIONS_FESTIVAL(string connectionString, int festivalInvitation,
            int detailId)
        {
            List<DetailInvitationsFestival> dataList = [];

            using SqlConnection conn = new(connectionString);
            conn.Open();

            using SqlCommand cmd = new("SP_GET_DETAIL_INVITATIONS_FESTIVAL", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@festivalInvitation", SqlDbType.Int).Value = festivalInvitation;
            cmd.Parameters.Add("@detailId", SqlDbType.Int).Value = detailId;

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DetailInvitationsFestival detailFestival = new()
                {
                    Id = Helpers.GetValue<int>(reader, "Id"),
                    Correo = Helpers.GetValueNull<string>(reader, "Correo"),
                    NombreEstablecimiento = Helpers.GetValueNull<string>(reader, "NombreEstablecimiento"),
                    NombreDirector = Helpers.GetValueNull<string>(reader, "NombreDirector"),
                    CorreoEstablecimiento = Helpers.GetValueNull<string>(reader, "CorreoEstablecimiento"),
                    NumeroEstablecimiento = Helpers.GetValueNull<string>(reader, "NumeroEstablecimiento"),
                    EstiloBanda = Helpers.GetValueNull<string>(reader, "EstiloBanda"),
                    NombreComandanteGeneral = Helpers.GetValueNull<string>(reader, "NombreComandanteGeneral"),
                    NombreSubcomandanteGeneral = Helpers.GetValueNull<string>(reader, "NombreSubcomandanteGeneral"),
                    CantidadIntegrantes = Helpers.GetValueNull<int>(reader, "CantidadIntegrantes"),
                    NombreInstructorBanda = Helpers.GetValueNull<string>(reader, "NombreInstructorBanda"),
                    NumeroInstructorBanda = Helpers.GetValueNull<string>(reader, "NumeroInstructorBanda"),
                    CorreoInstructorBanda = Helpers.GetValueNull<string>(reader, "CorreoInstructorBanda"),
                    ReseniaHistorica = Helpers.GetValueNull<string>(reader, "ReseniaHistorica"),
                    IFId = Helpers.GetValueNull<int>(reader, "IFId"),
                    ComentarioRechazo = Helpers.GetValueNull<string>(reader, "ComentarioRechazo"),
                };

                dataList.Add(detailFestival);
            }

            return dataList;
        }

        public static void EXEC_SP_INSERT_FESTIVAL(string connectionString, string name, short year, string ubication, DateTime date, 
            TimeSpan startDate, TimeSpan endDate, decimal entrancePrice)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();

            using SqlCommand cmd = new("SP_INSERT_FESTIVAL", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 1000).Value = name;
            cmd.Parameters.Add("@year", SqlDbType.SmallInt).Value = year;
            cmd.Parameters.Add("@ubication", SqlDbType.NVarChar, 1000).Value = ubication;
            cmd.Parameters.Add("@date", SqlDbType.Date).Value = date.Date;
            cmd.Parameters.Add("@startDate", SqlDbType.Time).Value = startDate;
            cmd.Parameters.Add("@endDate", SqlDbType.Time).Value = endDate;
            cmd.Parameters.Add("@entrancePrice", SqlDbType.Decimal).Value = entrancePrice;

            cmd.ExecuteNonQuery();
        }

        public static void EXEC_SP_INSERT_FESTIVAL_INVITATION_BAND(string connectionString, string bandName, byte state)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();

            using SqlCommand cmd = new("SP_INSERT_FESTIVAL_INVITATION_BAND", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@bandName", SqlDbType.NVarChar, 1000).Value = bandName;
            cmd.Parameters.Add("@state", SqlDbType.TinyInt).Value = state;

            cmd.ExecuteNonQuery();
        }

        public static void EXEC_SP_INSERT_FESTIVAL_INVITATION(string connectionString, string claveInvitacion, byte activa, 
            short cantidadEntradas, byte participacion, int fesId, int bifId)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();

            using SqlCommand cmd = new("SP_INSERT_FESTIVAL_INVITATION", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@claveInvitacion", SqlDbType.NVarChar, 50).Value = claveInvitacion;
            cmd.Parameters.Add("@activa", SqlDbType.TinyInt).Value = activa;
            cmd.Parameters.Add("@CantidadEntradas", SqlDbType.SmallInt).Value = cantidadEntradas;
            cmd.Parameters.Add("@Participacion", SqlDbType.TinyInt).Value = participacion;
            cmd.Parameters.Add("@FESId", SqlDbType.Int).Value = fesId;
            cmd.Parameters.Add("@BIFId", SqlDbType.Int).Value = bifId;

            cmd.ExecuteNonQuery();
        }

    }
}
