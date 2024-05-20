using System.ComponentModel.DataAnnotations;
using Digital_assistant_backend.Data;

public class UniqueUserAttribute: ValidationAttribute
    {
        
        private readonly string _propertyName;
        public UniqueUserAttribute(string propertyName)
        {
            _propertyName=propertyName;
        }
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if(value==null){
            return ValidationResult.Success;
        }

        var _dbContext=(ManagementDbContext)validationContext.GetService(typeof(ManagementDbContext));
        var entity=_dbContext.Users.FirstOrDefault(x=>x.Email==value.ToString());
        if(entity!=null){
            return new ValidationResult(ErrorMessage);
        }
        return ValidationResult.Success;
    }
}
