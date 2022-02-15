using DataAccessLayer.Models.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation.FluentValidation
{
   public class HobbyValidation:AbstractValidator<HobbyDTO>
    {
        public HobbyValidation()
        {
            RuleFor(x => x.MyHobby).NotEmpty().WithMessage("Hobi Boş Geçilemez");
         
        }
    }
}
