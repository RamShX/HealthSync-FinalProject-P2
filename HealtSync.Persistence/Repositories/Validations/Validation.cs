using Microsoft.IdentityModel.Tokens;

namespace HealtSync.Persistence.Repositories.Validations
{
    public class Validation<TClass>
    {
       
        private readonly List<string> _errorsMessages = new List<string>();
        public IReadOnlyList<string> ErrorMessages => _errorsMessages;

        public void AddError(string message)
        {
            _errorsMessages.Add(message);
        }

        public bool IsValid => !_errorsMessages.Any();

        public void ValidateNotNull(TClass entity, string entityName)
        {
            if (entity == null)
                AddError($"{entityName} es requerido.");
        }

        public void ValidateNumber(int id, string fieldName)
        {
            if (id <= 0)
                AddError($"{fieldName} no es válido.");
        }

        public void ValidateNotNullOrEmpty(string value, string fieldName)
        {
            if (value.IsNullOrEmpty())
                AddError($"{value} es requerido.");
        }

        public void ValidateDate(DateTime date, string fieldName)
        {
            if (date == DateTime.MinValue)
                AddError($"{fieldName} es requerida.");
        }

        public void ValidateDate(DateTime? date, string fieldName)
        {
            if (date == DateTime.MinValue)
                AddError($"{fieldName} es requerida.");
        }


    }
}
