﻿using System;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IAnswerChoiceService>().To<AnswerChoiceService>();
            Bind<IAnswerChoicePictureService>().To<AnswerChoicePictureService>();
            Bind<IAnswerStepService>().To<AnswerStepService>();
            Bind<IAnswerStepPictureService>().To<AnswerStepPictureService>();
            Bind<IExamService>().To<ExamService>();
            Bind<IExamProblemService>().To<ExamProblemService>();
            Bind<IProblemService>().To<ProblemService>();
            Bind<IProblemPictureService>().To<ProblemPictureService>();
            Bind<IProblemTypeService>().To<ProblemTypeService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<ITestingAreaService>().To<TestingAreaService>();
            Bind<IUserService>().To<UserService>();
            Bind<IUserAnswerService>().To<UserAnswerService>();
            Bind<IUserRoleService>().To<UserRoleService>();
        }
    }
}
