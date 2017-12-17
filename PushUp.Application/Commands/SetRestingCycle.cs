using ApplicationInterfaces;
using System;
using System.Collections.Generic;

namespace PushUp.Application.Commands
{
    public sealed class SetRestingCycle : ICommand
    {
        public SetRestingCycle(Guid id, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun)
        {
            Id = id;
            RestingCycle = new Dictionary<int, bool>
            {
                {1,mon },
                {2,tue },
                {3,wed },
                {4,thu },
                {5,fri },
                {6, sat },
                {7,sun }
            };
        }

        public Guid Id { get; }
        public IDictionary<int,bool> RestingCycle { get; }
    }
}
