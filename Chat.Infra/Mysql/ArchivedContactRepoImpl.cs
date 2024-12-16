using System.Diagnostics.Contracts;
using Chat.Domain.AggregatesModel.ArchiveAggregate;
using MySql.Data.MySqlClient;

namespace Chat.Infra.Mysql
{
    public class ArchivedContactRepoImpl : IArchivedContact
    {
        private readonly string _connectionString;

        public ArchivedContactRepoImpl(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public Guid ArchiveContact(Contact contact)
        {
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = "UPDATE Contacts SET IsArchived = @IsArchived WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@IsArchived", true);
            command.Parameters.AddWithValue("@Id", contact.Id);

            command.ExecuteNonQuery();
            return contact.Id;
        }

        public List<Guid> ArchiveContacts(List<Contact> contacts)
        {
            if (contacts == null || contacts.Count == 0)
                throw new ArgumentException("Contacts list is null or empty.", nameof(contacts));

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            foreach (var contact in contacts)
            {
                var query = "UPDATE Contacts SET IsArchived = @IsArchived WHERE Id = @Id";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@IsArchived", true);
                command.Parameters.AddWithValue("@Id", contact.Id);

                command.ExecuteNonQuery();
            }

            return contacts.ConvertAll(c => c.Id);
        }

        public Guid UnArchiveContact(Contact contact)
        {
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = "UPDATE Contacts SET IsArchived = @IsArchived WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@IsArchived", false);
            command.Parameters.AddWithValue("@Id", contact.Id);

            command.ExecuteNonQuery();
            return contact.Id;
        }

        public List<Guid> UnArchiveContacts(List<Contact> contacts)
        {
            if (contacts == null || contacts.Count == 0)
                throw new ArgumentException("Contacts list is null or empty.", nameof(contacts));

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            foreach (var contact in contacts)
            {
                var query = "UPDATE Contacts SET IsArchived = @IsArchived WHERE Id = @Id";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@IsArchived", false);
                command.Parameters.AddWithValue("@Id", contact.Id);

                command.ExecuteNonQuery();
            }

            return contacts.ConvertAll(c => c.Id);
        }

        public Contact GetArchivedContacts(Guid userId)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = "SELECT * FROM Contacts WHERE UserId = @UserId AND IsArchived = @IsArchived";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@IsArchived", true);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Contact
                {
                    Id = reader.GetGuid("Id"),
                };
            }

            return null;
        }
    }
}
