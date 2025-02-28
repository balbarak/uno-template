using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoAppTemplate.ViewModels;

public class FormViewModel : BaseViewModel
{
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
}
