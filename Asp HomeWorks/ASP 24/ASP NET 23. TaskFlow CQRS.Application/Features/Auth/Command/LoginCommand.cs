using ASP_NET_23._TaskFlow_CQRS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_NET_23._TaskFlow_CQRS.Application.Features.Auth.Command;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;