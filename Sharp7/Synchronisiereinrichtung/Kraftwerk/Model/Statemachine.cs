using Stateless;
using Stateless.Graph;
using System;

namespace Synchronisiereinrichtung.kraftwerk.Model
{
    public class Statemachine
    {
        public enum State
        {
            Aus,
            Hochfahren,
            Synchronisieren,
            MaschineTot,
            Belasten,
            LeistungsschalterAus,
            Reset
        }

        public enum Trigger
        {
            Aktualisieren,      // Kraftwerk ruft damit die Standardfunktion auf
            Hochfahren,
            Synchronisieren,
            MaschineTot,
            Belasten,
            LeistungsschalterAus,
            VentilGeschlossen,
            Reset,
            Neustart
        }

        private readonly StateMachine<State, Trigger> stateMachine;
        private readonly StateAus stateAus;
        private readonly StateHochfahren stateHochfahren;
        private readonly StateSynchronisieren stateSynchronisieren;
        private readonly StateMaschineTot stateMaschineTot;
        private readonly StateBelasten stateBelasten;
        private readonly StateLeistungsschalterAus stateLeistungsschalterAus;
        private readonly StateReset stateReset;

        public Statemachine(Kraftwerk kw)
        {
            Kraftwerk kraftWerk;
            kraftWerk = kw;

            stateMachine = CreateStateMachine();

            stateAus = new StateAus(kraftWerk);
            stateHochfahren = new StateHochfahren(kraftWerk);
            stateSynchronisieren = new StateSynchronisieren(kraftWerk);
            stateMaschineTot = new StateMaschineTot(kraftWerk);
            stateBelasten = new StateBelasten(kraftWerk);
            stateLeistungsschalterAus = new StateLeistungsschalterAus(kraftWerk);
            stateReset = new StateReset(kraftWerk);
        }


        public bool Fire(Trigger trigger)
        {
            if (!stateMachine.CanFire(trigger))
                return false;

            stateMachine.Fire(trigger);
            return true;
        }

        public State Status { get { return stateMachine.State; } }
        public string StatusAusgeben() => stateMachine.State.ToString();

        private StateMachine<State, Trigger> CreateStateMachine()
        {
            StateMachine<State, Trigger> _stateMachine = new StateMachine<State, Trigger>(State.Aus);

            _stateMachine.Configure(State.Aus)
                .InternalTransition(Trigger.Aktualisieren, t => stateAus.Doing())
                .Permit(Trigger.Hochfahren, State.Hochfahren)
                .Permit(Trigger.Reset, State.Reset);

            _stateMachine.Configure(State.Hochfahren)
                .InternalTransition(Trigger.Aktualisieren, t => stateHochfahren.Doing())
                .Permit(Trigger.Synchronisieren, State.Synchronisieren)
                .Permit(Trigger.MaschineTot, State.MaschineTot)
                .Permit(Trigger.VentilGeschlossen, State.Aus)
                .Permit(Trigger.Reset, State.Reset);

            _stateMachine.Configure(State.Synchronisieren)
                .OnEntry(() => stateSynchronisieren.OnEntry())
                .Permit(Trigger.MaschineTot, State.MaschineTot)
                .Permit(Trigger.Belasten, State.Belasten)
                .Permit(Trigger.Reset, State.Reset);

            _stateMachine.Configure(State.MaschineTot)
                .OnEntry(() => stateMaschineTot.OnEntry())
                .Permit(Trigger.Reset, State.Reset);

            _stateMachine.Configure(State.Belasten)
                .OnEntry(() => stateBelasten.OnEntry())
                .InternalTransition(Trigger.Aktualisieren, t => stateBelasten.Doing())
                .Permit(Trigger.MaschineTot, State.MaschineTot)
                .Permit(Trigger.LeistungsschalterAus, State.LeistungsschalterAus)
                .Permit(Trigger.Reset, State.Reset);

            _stateMachine.Configure(State.LeistungsschalterAus)
                .InternalTransition(Trigger.Aktualisieren, t => stateLeistungsschalterAus.Doing())
                .Permit(Trigger.MaschineTot, State.MaschineTot)
                .Permit(Trigger.VentilGeschlossen, State.Aus)
                .Permit(Trigger.Reset, State.Reset);

            _stateMachine.Configure(State.Reset)
                .OnEntry(() => stateReset.OnEntry())
                .Permit(Trigger.Neustart, State.Aus);


            _stateMachine.OnUnhandledTrigger((state, trigger) =>
            {
                Console.WriteLine("Unhandled: '{0}' state, '{1}' trigger!");
            });


            string graph = UmlDotGraph.Format(_stateMachine.GetInfo());
            Console.Write("\n \n" + graph + "\n \n");

            return _stateMachine;
        }
    }
}