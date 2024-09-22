using AutoMapper;
using System;
using System.Threading.Tasks;
using teste_atak.Application.DTOs;
using teste_atak.Domain.Contracts;
using teste_atak.Domain.Entities;

public class CreateUserService : ICreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ICrypterRepository _crypterRepository;
    private readonly IMapper _mapper;

    public CreateUserService(IUserRepository userRepository, ICrypterRepository crypterRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _crypterRepository = crypterRepository;
        _mapper = mapper;
    }

    public async Task Execute(UserDTO userDTO)
    {
        var existingUser = await _userRepository.GetByEmail(userDTO.Email!);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Um usuário com esse e-mail já existe.");
        }

        userDTO.PasswordHash = await _crypterRepository.Encrypt(userDTO.PasswordHash!); 

        var newUser = _mapper.Map<User>(userDTO);

        await _userRepository.Insert(newUser);
    }
}
