using AidManager.API.Services.Profiles.Application.Commands;
using AidManager.API.Services.Profiles.Application.DTOs;
using AidManager.API.Services.Profiles.Application.Interfaces;
using AidManager.API.Services.Profiles.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.API.Services.Profiles.Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // LOG DE DEPURACIÓN: Verificar entrada
            Console.WriteLine($"[DEBUG] Entrando a CreateUserHandler. Role recibido: {request.Role}. Email: {request.Email}");

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password, 
                ProfileImg = request.ProfileImg,
                Role = request.Role,
                
                CompanyName = request.CompanyName,
                CompanyEmail = request.CompanyEmail,
                CompanyCountry = request.CompanyCountry,
                TeamRegisterCode = request.TeamRegisterCode
            };

            if (request.Role == 0) 
            {
                Console.WriteLine("[DEBUG] El usuario ES Director (Role 0). Creando nueva compañía...");

                var generatedCode = string.IsNullOrEmpty(request.TeamRegisterCode) 
                                    ? Guid.NewGuid().ToString().Substring(0, 8).ToUpper() 
                                    : request.TeamRegisterCode;

                var newCompany = new Company
                {
                    CompanyName = request.CompanyName,
                    Country = request.CompanyCountry,
                    Email = request.CompanyEmail,
                    TeamRegisterCode = generatedCode,
                };

                await _unitOfWork.Companies.AddAsync(newCompany);
                await _unitOfWork.CompleteAsync(); // Guardamos para generar el ID

                Console.WriteLine($"[DEBUG] Compañía creada exitosamente. ID: {newCompany.Id}, Código: {newCompany.TeamRegisterCode}");

                user.CompanyId = newCompany.Id;
                user.TeamRegisterCode = generatedCode; 
                
                user.CompanyName = newCompany.CompanyName;
            }
            else 
            {
                Console.WriteLine($"[DEBUG] El usuario NO es Director (Role {request.Role}). Buscando compañía por código: {request.TeamRegisterCode}");

                var existingCompany = await _unitOfWork.Companies.GetByCodeAsync(request.TeamRegisterCode);
                
                if (existingCompany != null)
                {
                    Console.WriteLine($"[DEBUG] Compañía encontrada: {existingCompany.CompanyName} (ID: {existingCompany.Id})");
                    user.CompanyId = existingCompany.Id;
                    user.CompanyName = existingCompany.CompanyName;
                    user.CompanyEmail = existingCompany.Email;
                    user.CompanyCountry = existingCompany.Country;
                }
                else
                {
                    Console.WriteLine("[DEBUG] No se encontró ninguna compañía con ese código.");
                    user.CompanyId = 0; 
                }
            }

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync(); 

            Console.WriteLine($"[DEBUG] Usuario guardado exitosamente con ID: {user.Id} y CompanyID: {user.CompanyId}");

            return new UserDto(
                user.Id,
                $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}".Trim(),
                user.Age,
                user.Email ?? string.Empty,
                user.Phone ?? string.Empty,
                user.Password ?? string.Empty,
                user.ProfileImg ?? string.Empty,
				user.Occupation ?? string.Empty,
    			user.Bio ?? string.Empty,
                user.Role.ToString(),
                
                user.CompanyId,          
                user.CompanyName,        
                user.CompanyEmail,       
                user.CompanyCountry,
                user.TeamRegisterCode    
            );
        }
    }
}