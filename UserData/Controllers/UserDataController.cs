using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserData.Models;
using UserData.ViewModels;

namespace UserData.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserDataController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public void AddUser([FromQuery]UserViewModel userModel)
        {
            _dataContext.Add<Users>( new Users()
            {
                Id = Guid.NewGuid(),
                Name = userModel.Name,
                Surname = userModel.Surname,
                BirthDate = userModel.BirthDate
            });

            _dataContext.SaveChanges();
        }

        [HttpGet("[action]")]
        public ActionResult<ICollection<UserViewModel>> GetAllUsers()
        {
            var users = _dataContext.Set<Users>().Select(o => new UserViewModel()
            {
                Name = o.Name,
                Surname = o.Surname,
                BirthDate = o.BirthDate
            });

            return users.ToList();
        }
    }
}
