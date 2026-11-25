using EventManager.Api.DTOs.Answer;
using EventManager.Api.DTOs.ContestChoice;
using EventManager.Api.DTOs.ContestWork;
using EventManager.Api.DTOs.Event;
using EventManager.Api.DTOs.EventBranch;
using EventManager.Api.DTOs.Question;
using EventManager.Api.DTOs.Registration;
using EventManager.Api.DTOs.User;
using EventManager.Api.DTOs.UserAnswer;
using EventManager.Api.Models;
using Mapster;

namespace EventManager.Api.Mappings;

public static class MappingConfig
{
    public static void Register()
    {
        // Event
        TypeAdapterConfig<CreateEvent, Event>
            .NewConfig()
            .Ignore(dest => dest.Id);

        TypeAdapterConfig<Event, EventResponse>
            .NewConfig()
            .Map(dest => dest.EventType, src => src.EventType.Type)
            .Map(dest => dest.Status, src => src.Status.Status);

        // User
        TypeAdapterConfig<CreateUser, User>
            .NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.PasswordHash)
            .Ignore(dest => dest.CreatedAt);

        TypeAdapterConfig<User, UserResponse>
            .NewConfig()
            .Map(dest => dest.Branch, src => src.Branch.Name);

        // Registration
        TypeAdapterConfig<CreateRegistration, Registration>
            .NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.StatusId)
            .Ignore(dest => dest.RegistratedAt);

        TypeAdapterConfig<Registration, RegistrationResponse>
            .NewConfig()
            .Map(dest => dest.User, src => src.User.FullName)
            .Map(dest => dest.Event, src => src.Event.Title)
            .Map(dest => dest.Status, src => src.Status.Status);

        // UserAnswer
        TypeAdapterConfig<CreateUserAnswer, UserAnswer>
            .NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Point)
            .Ignore(dest => dest.AnsweredAt);

        TypeAdapterConfig<UserAnswer, UserAnswerResponse>
            .NewConfig()
            .Map(dest => dest.Registration, src => src.Registration.Id.ToString())
            .Map(dest => dest.Answer, src => src.Answer != null ? src.Answer.Text : string.Empty)
            .Map(dest => dest.Question, src => src.Question.Text);

        // Question
        TypeAdapterConfig<CreateQuestion, Question>
            .NewConfig()
            .Ignore(dest => dest.Id);

        TypeAdapterConfig<Question, QuestionResponse>
            .NewConfig()
            .Map(dest => dest.Event, src => src.Event.Title)
            .Map(dest => dest.Type, src => src.Type.Type);

        // Answer
        TypeAdapterConfig<CreateAnswer, Answer>
            .NewConfig()
            .Ignore(dest => dest.Id);

        TypeAdapterConfig<Answer, AnswerResponse>
            .NewConfig()
            .Map(dest => dest.Question, src => src.Question.Text);

        // ContestWork
        TypeAdapterConfig<CreateContestWork, ContestWork>
            .NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.CreatedAt);

        TypeAdapterConfig<ContestWork, ContestWorkResponse>
            .NewConfig()
            .Map(dest => dest.Registration, src => src.Registration.Id.ToString());

        // ContestChoice
        TypeAdapterConfig<CreateContestChoice, ContestChoice>
            .NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.ChoiceAt);

        TypeAdapterConfig<ContestChoice, ContestChoiceResponse>
            .NewConfig()
            .Map(dest => dest.Work, src => src.Work.Title)
            .Map(dest => dest.User, src => src.User.FullName);

        // EventBranch
        TypeAdapterConfig<CreateEventBranch, EventBranch>
            .NewConfig();

        TypeAdapterConfig<EventBranch, EventBranchResponse>
            .NewConfig()
            .Map(dest => dest.Event, src => src.Event.Title)
            .Map(dest => dest.Branch, src => src.Branch.Name);
    }
}
