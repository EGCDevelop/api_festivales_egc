using api_festivales_egc.Models;
using api_festivales_egc.Models.DTO;
using api_festivales_egc.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace api_festivales_egc.Middle
{
    public class InvitationMiddle
    {
        public static FestivalInvitationDTO EXEC_SP_FES_GET_VALIDATE_INVITATION(string connectionString, string invitationCode)
        {
            FestivalInvitationDTO festivalInvitationDTO = null;

            using SqlConnection connection = new(connectionString);
            connection.Open();

            using SqlCommand cmd = new("SP_FES_GET_VALIDATE_INVITATION", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@invitationCode", SqlDbType.NVarChar, 50).Value = invitationCode;

            using SqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                festivalInvitationDTO = new()
                {
                    FestivalId = Helpers.GetValue<int>(reader, "FestivalId"),
                    FestivalName = Helpers.GetValue<string>(reader, "FestivalName"),
                    FestivalUbication = Helpers.GetValue<string>(reader, "FestivalUbication"),
                    FestivalDate = Helpers.GetValue<DateTime>(reader, "FestivalDate"),
                    FestivalStartTime = Helpers.GetValue<TimeSpan>(reader, "FestivalStartTime"),
                    GestBandId = Helpers.GetValue<int>(reader, "GestBandId"),
                    GestBandName = Helpers.GetValue<string>(reader, "GestBandName"),
                    GestBandState = Helpers.GetValue<byte>(reader, "GestBandState"),
                    GestFestivalId = Helpers.GetValue<int>(reader, "GestFestivalId"),
                    GestFestivalInvitationKey = Helpers.GetValue<string>(reader, "GestFestivalInvitationKey"),
                    GestFestivalActive = Helpers.GetValue<byte>(reader, "GestFestivalActive"),
                    GestFestivalNumberEntries = Helpers.GetValue<int>(reader, "GestFestivalNumberEntries"),
                    GestFestivalParticipation = Helpers.GetValue<byte>(reader, "GestFestivalParticipation")
                };
            }

            return festivalInvitationDTO!;
        }

        public static void TRANSACTION_UPDATE_FESTIVAL_INVITATIONS(string connectionString, InvitationRequest invitationRequest)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            using SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                EXEC_SP_UPDATE_FESTIVAL_INVITATIONS(connection, transaction, invitationRequest);
                EXEC_SP_INSERT_FESTIVAL_INVITATIONS_DETAIL(connection, transaction, invitationRequest);

                transaction.Commit();
            } 
            catch(Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Fallo al aceptar la invitación, intente más tarde o comuniquese con la banda EGC: " + ex.Message);
            }
        }

        public static void TRANSACTION_INSERT_FESTIVAL_INVITATIONS_DETAIL_REJECT(string connectionString, InvitationRequest invitationRequest)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            using SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                EXEC_SP_UPDATE_FESTIVAL_INVITATIONS(connection, transaction, invitationRequest);
                EXEC_SP_INSERT_FESTIVAL_INVITATIONS_DETAIL_REJECT(connection, transaction, invitationRequest);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Fallo al aceptar la invitación, intente más tarde o comuniquese con la banda EGC: " + ex.Message);
            }
        }

        public static void EXEC_SP_UPDATE_FESTIVAL_INVITATIONS(SqlConnection connection, SqlTransaction  transaction,
            InvitationRequest invitationRequest)
        {
            using SqlCommand cmd = new("SP_UPDATE_FESTIVAL_INVITATIONS", connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@festivalId", SqlDbType.Int).Value = invitationRequest.FestivalId;
            cmd.Parameters.Add("@active", SqlDbType.TinyInt).Value = 0;
            cmd.Parameters.Add("@participation", SqlDbType.TinyInt).Value = invitationRequest.FestivalAccept;

            cmd.ExecuteNonQuery();
        }

        public static void EXEC_SP_INSERT_FESTIVAL_INVITATIONS_DETAIL(SqlConnection connection, SqlTransaction transaction,
            InvitationRequest invitationRequest)
        {
            using SqlCommand cmd = new("SP_INSERT_FESTIVAL_INVITATIONS_DETAIL", connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@correo", SqlDbType.NVarChar, 500).Value = invitationRequest.EstablishmentEmail;
            cmd.Parameters.Add("@nombreEstablecimiento", SqlDbType.NVarChar, 500).Value = invitationRequest.EstablishmentName;
            cmd.Parameters.Add("@nombreDirector", SqlDbType.NVarChar, 100).Value = invitationRequest.EstablishmentDirectorName;
            cmd.Parameters.Add("@correoEstablecimiento", SqlDbType.NVarChar, 500).Value = invitationRequest.EstablishmentEmail;
            cmd.Parameters.Add("@numeroEstablecimiento", SqlDbType.NVarChar, 50).Value = invitationRequest.EstablishmentPhoneNumber;
            cmd.Parameters.Add("@estiloBanda", SqlDbType.NVarChar, 100).Value = invitationRequest.BandStyle;
            cmd.Parameters.Add("@nombreComandanteGeneral", SqlDbType.NVarChar, 500).Value = invitationRequest.NameCommanderGeneral;
            cmd.Parameters.Add("@nombreSubcomandanteGeneral", SqlDbType.NVarChar, 500).Value = invitationRequest.NameSubComanderGeneral;
            cmd.Parameters.Add("@cantidadIntegrantes", SqlDbType.SmallInt).Value = invitationRequest.BandNumberMembers;
            cmd.Parameters.Add("@nombreInstructorBanda", SqlDbType.NVarChar, 500).Value = invitationRequest.NameGeneralInstructor;
            cmd.Parameters.Add("@numeroInstructorBanda", SqlDbType.NVarChar, 50).Value = invitationRequest.PhoneNumberGeneralInstructor;
            cmd.Parameters.Add("@correoInstructorBanda", SqlDbType.NVarChar, 500).Value = invitationRequest.EmailGeneralInstructor;
            cmd.Parameters.Add("@reseniaHistorica", SqlDbType.NVarChar, -1).Value = invitationRequest.HistoricalOverview;
            cmd.Parameters.Add("@festivalId", SqlDbType.Int).Value = invitationRequest.FestivalId;

            cmd.ExecuteNonQuery();
        }

        public static void EXEC_SP_INSERT_FESTIVAL_INVITATIONS_DETAIL_REJECT(SqlConnection connection, SqlTransaction transaction,
            InvitationRequest invitationRequest)
        {
            using SqlCommand cmd = new("SP_INSERT_FESTIVAL_INVITATIONS_DETAIL_REJECT", connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@commentReject", SqlDbType.NVarChar, -1).Value = invitationRequest.CancelDetail;
            cmd.Parameters.Add("@festivalId", SqlDbType.Int).Value = invitationRequest.FestivalId;

            cmd.ExecuteNonQuery();
        }
    }
}
