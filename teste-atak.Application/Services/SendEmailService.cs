using AutoMapper;
using System;
using System.Threading.Tasks;
using teste_atak.Application.DTOs;
using teste_atak.Domain.Contracts;

public class SendEmailService : ISendEmailUseCase
{
    private readonly IMailerRepository _mailerRepository;

    public SendEmailService(IMailerRepository mailerRepository)
    {
        _mailerRepository = mailerRepository;
    }

    public async Task Execute(EmailDTO emailDTO)
    {

        try
        {
            await _mailerRepository.Send(
                emailDTO.To,
                emailDTO.Subject,
                emailDTO.Body,
                emailDTO.AttachmentStream);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Erro ao enviar o e-mail.", ex);
        }
    }
}
