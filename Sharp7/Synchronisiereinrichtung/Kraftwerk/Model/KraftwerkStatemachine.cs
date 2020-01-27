using Stateless;
using System;

namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    public class KraftwerkStatemachine
    {

        public enum State
        {
            Aus,
            Hochfahren,
            Synchronisieren,
            Belasten,
            Ausschalten,
            Reset
        }

        public enum Trigger
        {
            Null,
            Starten,
            Synchronisieren,
            Ausschalten,
            Reset
        }

        private readonly StateMachine<State, Trigger> kraftwerkStateMachine;
        private readonly Kraftwerk kraftWerk;

        private readonly KraftwerkStateAus kraftwerkStateAus;
        private readonly KraftwerkStateHochfahren kraftwerkStateHochfahren;


        public KraftwerkStatemachine(Kraftwerk kw)
        {
            kraftwerkStateMachine = CreateStateMachine();
            kraftWerk = kw;

            kraftwerkStateAus = new KraftwerkStateAus(kraftWerk);
            kraftwerkStateHochfahren = new KraftwerkStateHochfahren(kraftWerk);
        }


        public void Aufrufen()
        {
            switch (Status)
            {
                case State.Aus:
                    Weiterschalten(kraftwerkStateAus.Aus());
                    break;

                case State.Hochfahren:
                    Weiterschalten(kraftwerkStateHochfahren.Hochfahren());
                    break;

                default:
                    break;


            }
        }

        public bool Weiterschalten(Trigger trigger)
        {
            if (!kraftwerkStateMachine.CanFire(trigger))
                return false;

            kraftwerkStateMachine.Fire(trigger);
            return true;
        }

        public State Status
        {
            get { return kraftwerkStateMachine.State; }
        }


        private StateMachine<State, Trigger> CreateStateMachine()
        {
            StateMachine<State, Trigger> stateMachine = new StateMachine<State, Trigger>(State.Aus);

            stateMachine.Configure(State.Aus)
                .OnEntry(() => kraftwerkStateAus.KraftwerkAusErsterDurchlauf())
                .Permit(Trigger.Starten, State.Hochfahren)
                .Permit(Trigger.Reset, State.Reset);

            stateMachine.Configure(State.Hochfahren)
                .OnEntry(() => kraftwerkStateHochfahren.KraftwerkHochfahrenErsterDurchlauf())
                .Permit(Trigger.Synchronisieren, State.Synchronisieren)
                .Permit(Trigger.Ausschalten, State.Ausschalten)
                .Permit(Trigger.Reset, State.Reset);

            stateMachine.Configure(State.Synchronisieren);

            stateMachine.Configure(State.Belasten);

            stateMachine.Configure(State.Ausschalten);

            stateMachine.Configure(State.Reset);


            stateMachine.OnUnhandledTrigger((state, trigger) =>
            {
                Console.WriteLine("Unhandled: '{0}' state, '{1}' trigger!");
            });


            return stateMachine;
        }




    }
}
