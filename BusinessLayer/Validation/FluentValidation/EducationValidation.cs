using DataAccessLayer.Models.VMs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation.FluentValidation
{
  public  class EducationValidation:AbstractValidator<EducationVM>
    {
        public EducationValidation()
        {
            RuleFor(x => x.SchollName).NotEmpty().WithMessage("Okul Adı Boş Geçilemez");
            RuleFor(x => x.SchollName).MinimumLength(10).MaximumLength(80).WithMessage("Okul Adı min 10 max 80 karakter içermeli");
            RuleFor(x => x.Section).MinimumLength(10).MaximumLength(50).WithMessage("Bölüm min 10 max 50 karakter içermeli");
            RuleFor(x => x.Section).NotEmpty().WithMessage("Bölüm Boş Geçilemez");
            RuleFor(x => x.NoteAverage).NotEmpty().WithMessage("Genel Not Ortalaması Boş Geçilemez");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Mezuniyet Tarihi Boş Geçilemez");
            RuleFor(x => x.NoteAverage).MinimumLength(1).MaximumLength(4).WithMessage("1 ila 4 basamaklı olmalı");

        }
    }
}
