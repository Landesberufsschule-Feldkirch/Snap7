using Stateless;
using System;

namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    public class Statemachine
    {
        public enum State
        {
            Aus,
            Hochfahren,
            Synchronisieren,
            Belasten,
            Herunterfahren,
            Reset
        }

        public enum Trigger
        {
            Aktualisieren,      // Kraftwerk ruft damit die Standardfunktion auf
            Hochfahren,
            Synchronisieren,
            Belasten,
            Herunterfahren,
            Ausschalten,
            Reset
        }

        private readonly StateMachine<State, Trigger> stateMachine;
        private readonly Kraftwerk kraftWerk;

        private readonly StateAus stateAus;
        private readonly StateHochfahren stateHochfahren;
        private readonly StateSynchronisieren stateSynchronisieren;
        private readonly StateBelasten stateBelasten;
        private readonly StateHerunterfahren stateHerunterfahren;
        private readonly StateReset stateReset;

        public Statemachine(Kraftwerk kw)
        {
            stateMachine = CreateStateMachine();
            kraftWerk = kw;

            stateAus = new StateAus(kraftWerk);
            stateHochfahren = new StateHochfahren(kraftWerk);
            stateSynchronisieren = new StateSynchronisieren(kraftWerk);
            stateBelasten = new StateBelasten(kraftWerk);
            stateHerunterfahren = new StateHerunterfahren(kraftWerk);
            stateReset = new StateReset(kraftWerk);
        }


        public bool Fire(Trigger trigger)
        {
            if (!stateMachine.CanFire(trigger))
                return false;

            stateMachine.Fire(trigger);
            return true;
        }

        public State Status
        {
            get { return stateMachine.State; }
        }


        private StateMachine<State, Trigger> CreateStateMachine()
        {
            StateMachine<State, Trigger> stateMachine = new StateMachine<State, Trigger>(State.Aus);

            stateMachine.Configure(State.Aus)
                .OnEntry(() => stateAus.OnEntry())
                .OnExit(() => stateAus.OnExit())
                .InternalTransition(Trigger.Aktualisieren, t => stateAus.Doing())
                .Permit(Trigger.Hochfahren, State.Hochfahren)
                .Permit(Trigger.Reset, State.Reset);

            stateMachine.Configure(State.Hochfahren)
                .OnEntry(() => stateHochfahren.OnEntry())
                .OnExit(() => stateHochfahren.OnExit())
                .InternalTransition(Trigger.Aktualisieren, t => stateHochfahren.Doing())
                .Permit(Trigger.Synchronisieren, State.Synchronisieren)
                .Permit(Trigger.Herunterfahren, State.Herunterfahren)
                .Permit(Trigger.Reset, State.Reset);

            stateMachine.Configure(State.Synchronisieren)
                .OnEntry(() => stateSynchronisieren.OnEntry())
                .OnExit(() => stateSynchronisieren.OnExit())
                .InternalTransition(Trigger.Aktualisieren, t => stateSynchronisieren.Doing())
                .Permit(Trigger.Belasten, State.Belasten)
                .Permit(Trigger.Reset, State.Reset);

            stateMachine.Configure(State.Belasten)
                .OnEntry(() => stateBelasten.OnEntry())
                .OnExit(() => stateBelasten.OnExit())
                .InternalTransition(Trigger.Aktualisieren, t => stateBelasten.Doing())
                .Permit(Trigger.Reset, State.Reset);

            stateMachine.Configure(State.Herunterfahren)
                .OnEntry(() => stateHerunterfahren.OnEntry())
                .OnExit(() => stateHerunterfahren.OnExit())
                .InternalTransition(Trigger.Aktualisieren, t => stateHerunterfahren.Doing())
                .Permit(Trigger.Reset, State.Reset);

            stateMachine.Configure(State.Reset)
                .OnEntry(() => stateReset.OnEntry())
                .OnExit(() => stateReset.OnExit())
                .InternalTransition(Trigger.Aktualisieren, t => stateReset.Doing())
                .Permit(Trigger.Ausschalten, State.Aus);


            stateMachine.OnUnhandledTrigger((state, trigger) =>
            {
                Console.WriteLine("Unhandled: '{0}' state, '{1}' trigger!");
            });

            return stateMachine;
        }
    }
}