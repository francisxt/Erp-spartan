using Commons.Helpers;
using Models.Enums.HiAccounting;
using Models.Enums.HiLoans;
using Models.Models.HiAccounting.Debs;
using Models.Models.HiLoans;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Models.Models.HiAccounting
{
    /// <summary>
    /// Prestamo
    /// </summary>
    public class Loan : CommonsProperty
    {
        //User to Create loans
        public string UserId { get; set; }
        public User User { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public decimal InitialCapital { get; set; }
        [NotMapped]
        public string InitialCapitalFormated => StringHelper.FormatMoney(InitialCapital);


        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public decimal ActualCapital { get; set; }

        [NotMapped]
        public string ActualCapitalFormated => StringHelper.FormatMoney(ActualCapital);

        public AmortitationType AmortitationType { get; set; }
        public PaymentModality PaymentModality { get; set; }
        public RateType RateType { get; set; }

        [RequiredIfNot("AmortitationType", 0)]
        public int Shares { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public decimal Interest { get; set; }
        public Guid ClientUserId { get; set; }
        public ClientUser ClientUser { get; set; }
        public virtual IEnumerable<Deb> Debs { get; set; }
        public virtual IEnumerable<ReclosingHistory> ReclosingHistories { get; set; }
        public decimal AmountDeb { get; set; }

        [NotMapped]
        public string SharesStr { get; set; }

        [NotMapped]
        public decimal ReclosingAmount { get; set; }
        [NotMapped]
        public decimal ReclosingInitialAmount { get; set; }


        [NotMapped]
        public Guid IdLoanForReclosing { get; set; }

    
    }
    public class RequiredIfNotAttribute : ValidationAttribute, IClientValidatable
    {
        private String PropertyName { get; set; }
        private Object InvalidValue { get; set; }
        private readonly RequiredAttribute _innerAttribute;

        public RequiredIfNotAttribute(String propertyName, Object invalidValue)
        {
            PropertyName = propertyName;
            InvalidValue = invalidValue;
            _innerAttribute = new RequiredAttribute();
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var dependentValue = context.ObjectInstance.GetType().GetProperty(PropertyName).GetValue(context.ObjectInstance, null);

            if (dependentValue.ToString() != InvalidValue.ToString())
            {
                if (!_innerAttribute.IsValid(value))
                {
                    return new ValidationResult(FormatErrorMessage(context.DisplayName), new[] { context.MemberName });
                }
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessageString,
                ValidationType = "requiredifnot",
            };
            rule.ValidationParameters["dependentproperty"] = (context as ViewContext).ViewData.TemplateInfo.GetFullHtmlFieldId(PropertyName);
            rule.ValidationParameters["invalidvalue"] = InvalidValue is bool ? InvalidValue.ToString().ToLower() : InvalidValue;

            yield return rule;
        }
    }
    }
