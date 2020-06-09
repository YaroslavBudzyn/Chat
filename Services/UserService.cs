using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatTest.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResultHelper _resultHelper;

        // Constructor
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IResultHelper resultHelper,
            IOptions<JwtOptions> getJwtOptions)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _resultHelper = resultHelper;
        }
        public async Task<ObjectResult> GetAll()
        {
            var dbUsers = _unitOfWork.Users.GetAll();
            var total = _unitOfWork.Users.Count();
            var usersToMap = new List<UserCreateModel>();

            foreach (var dbUser in dbUsers)
            {
                usersToMap.Add(_mapper.Map<UserCreateModel>(dbUser));
                var user = usersToMap.SingleOrDefault(m => m.Id == dbUser.Id);

                if (null == user) continue;
                user.Role = dbUser.Role?.Name;
            }

            return await _resultHelper.Response(HttpStatusCode.OK, new
            {
                Data = usersToMap,
                Total = total
            });
        }

        public async Task<ObjectResult> GetById(int id)
        {
            var dbUser = _unitOfWork.Users.Get(id);

            if (dbUser == null)
            {
                return await _resultHelper.Response(HttpStatusCode.NotFound, new MessageHelper { Message = MessageEnums.NotFound });
            }

            var user = _mapper.Map<UserCreateModel>(dbUser);
            user.Role = dbUser.Role?.Name;

            return await _resultHelper.Response(HttpStatusCode.OK, user);
        }

        public async Task<ObjectResult> Create(UserCreateModel model)
        {
            if (null != _unitOfWork.Users.GetByEmail(model.Email))
            {
                return await _resultHelper.Response(HttpStatusCode.Conflict, new MessageHelper { Message = MessageEnums.EmailUsed });
            }

            var user = _mapper.Map<Entities.User>(model);
            user.CreatedAt = DateTime.Now;
            user.RoleId = model.RoleId;
            user.Name = model.Name;
            user.Phone = model.Phone;

            _unitOfWork.Users.Create(user);

            if (!_unitOfWork.Save())
            {
                return await _resultHelper.Response(HttpStatusCode.BadRequest, new MessageHelper { Message = MessageEnums.NotUpdated });
            }

            return await _resultHelper.Response(HttpStatusCode.Created, new MessageHelper { Message = MessageEnums.Created });
        }

        public async Task<ObjectResult> Update(UserUpdateModel model)
        {
            var user = _unitOfWork.Users.Get(model.Id);

            if (null == user)
            {
                return await _resultHelper.Response(HttpStatusCode.NotFound);
            }

            user.Name = model.Name;
            user.Email = model.Email;
            user.Phone = model.Phone;

            _unitOfWork.Users.Update(user);

            return _unitOfWork.Save()
                ? await _resultHelper.Response(HttpStatusCode.OK, new MessageHelper { Message = MessageEnums.Ok })
                : await _resultHelper.Response(HttpStatusCode.BadRequest, new MessageHelper { Message = MessageEnums.NotUpdated });
        }

        public async Task<ObjectResult> SearchWithPaging(UserSearchModel model)
        {
            var searchResult = _unitOfWork.Users.Search(model);

            var enumerable = searchResult.ToList();
            var total = enumerable.Count;
            var users = enumerable.ToList();

            var usersToMap = new List<UserViewModel>();

            foreach (var dbUser in users)
            {
                usersToMap.Add(_mapper.Map<UserViewModel>(dbUser));

                var user = usersToMap.SingleOrDefault(m => m.Id == dbUser.Id);

                if (null == user) continue;
                user.Role = dbUser.Role?.Name;
            }

            return await _resultHelper.Response(HttpStatusCode.OK, new
            {
                Data = usersToMap,
                Total = total
            });
        }
    }
}
