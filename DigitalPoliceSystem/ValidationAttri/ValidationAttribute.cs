﻿using System.ComponentModel.DataAnnotations;
using System;

namespace DigitalPoliceSystem.ValidationAttri
{
    public class ValidationOneYearAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = (DateTime)value;
            // This assumes inclusivity, i.e. exactly six years ago is okay
            if (DateTime.Now.AddYears(-2).CompareTo(value) <= 0 && DateTime.Now.CompareTo(value) >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date must be within the last two year!");
            }
        }
    }
}
