using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using User.Service.CustomExceptions;
using User.Service.CustomExtensionMethods;
using User.Service.Entities;
using User.Service.Repositories;

namespace User.Service.Services
{
    public class UserService : IEntityService<UserEntity>
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Add(UserEntity user)
        {
            try
            {
                user.ValidateModelState();
                var addedUser = await _userRepository.Add(user);

                return new ObjectResult(addedUser) { StatusCode = 201 };
            }
            catch (ModelStateErrorException ex)
            {
                return new BadRequestObjectResult(ex.ValidationResults);
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (Guid.Empty == id)
                {
                    throw new NotValidOrEmptyIdException();
                }

                await _userRepository.Delete(id);

                return new NoContentResult();
            }
            catch (NotValidOrEmptyIdException ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
            catch (NotFoundIdException ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }

        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAll();

            return new OkObjectResult(users);
        }

        public async Task<IActionResult> GetSingleById(Guid id)
        {
            try
            {
                if (Guid.Empty == id)
                {
                    throw new NotValidOrEmptyIdException();
                }

                var user = await _userRepository.GetSingleById(id);

                return new OkObjectResult(user);
            }
            catch (NotValidOrEmptyIdException ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
            catch (NotFoundIdException ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }

        public async Task<IActionResult> Update(UserEntity user)
        {
            try
            {
                user.ValidateModelState();

                if (Guid.Empty == user.Id)
                {
                    throw new NotValidOrEmptyIdException();
                }

                var updatedUser = await _userRepository.Update(user);

                return new OkObjectResult(updatedUser);
            }
            catch (ModelStateErrorException ex)
            {
                return new BadRequestObjectResult(ex.ValidationResults);
            }
            catch (NotValidOrEmptyIdException ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
            catch (NotFoundIdException ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }
    }
}
