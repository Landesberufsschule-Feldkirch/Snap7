using Stateless;
using Stateless.Graph;
using System;

namespace Synchronisiereinrichtung.Model;

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

    private readonly StateMachine<State, Trigger> _stateMachine;
    private readonly StateAus _stateAus;
    private readonly StateHochfahren _stateHochfahren;
    private readonly StateSynchronisieren _stateSynchronisieren;
    private readonly StateMaschineTot _stateMaschineTot;
    private readonly StateBelasten _stateBelasten;
    private readonly StateLeistungsschalterAus _stateLeistungsschalterAus;
    private readonly StateReset _stateReset;

    public Statemachine(Kraftwerk.Model.Kraftwerk kw)
    {
        var kraftWerk = kw;

        _stateMachine = CreateStateMachine();

        _stateAus = new StateAus(kraftWerk);
        _stateHochfahren = new StateHochfahren(kraftWerk);
        _stateSynchronisieren = new StateSynchronisieren(kraftWerk);
        _stateMaschineTot = new StateMaschineTot(kraftWerk);
        _stateBelasten = new StateBelasten(kraftWerk);
        _stateLeistungsschalterAus = new StateLeistungsschalterAus(kraftWerk);
        _stateReset = new StateReset(kraftWerk);
    }

    public bool Fire(Trigger trigger)
    {
        if (!_stateMachine.CanFire(trigger))
            return false;

        _stateMachine.Fire(trigger);
        return true;
    }

    public string StatusAusgeben() => _stateMachine.State.ToString();

    private StateMachine<State, Trigger> CreateStateMachine()
    {
        var stateMachine = new StateMachine<State, Trigger>(State.Aus);

        stateMachine.Configure(State.Aus)
            .InternalTransition(Trigger.Aktualisieren, _ => _stateAus.Doing())
            .Permit(Trigger.Hochfahren, State.Hochfahren)
            .Permit(Trigger.Reset, State.Reset);

        stateMachine.Configure(State.Hochfahren)
            .InternalTransition(Trigger.Aktualisieren, _ => _stateHochfahren.Doing())
            .Permit(Trigger.Synchronisieren, State.Synchronisieren)
            .Permit(Trigger.MaschineTot, State.MaschineTot)
            .Permit(Trigger.VentilGeschlossen, State.Aus)
            .Permit(Trigger.Reset, State.Reset);

        stateMachine.Configure(State.Synchronisieren)
            .OnEntry(() => _stateSynchronisieren.OnEntry())
            .Permit(Trigger.MaschineTot, State.MaschineTot)
            .Permit(Trigger.Belasten, State.Belasten)
            .Permit(Trigger.Reset, State.Reset);

        stateMachine.Configure(State.MaschineTot)
            .OnEntry(() => _stateMaschineTot.OnEntry())
            .Permit(Trigger.Reset, State.Reset);

        stateMachine.Configure(State.Belasten)
            .OnEntry(() => _stateBelasten.OnEntry())
            .InternalTransition(Trigger.Aktualisieren, _ => _stateBelasten.Doing())
            .Permit(Trigger.MaschineTot, State.MaschineTot)
            .Permit(Trigger.LeistungsschalterAus, State.LeistungsschalterAus)
            .Permit(Trigger.Reset, State.Reset);

        stateMachine.Configure(State.LeistungsschalterAus)
            .InternalTransition(Trigger.Aktualisieren, _ => _stateLeistungsschalterAus.Doing())
            .Permit(Trigger.MaschineTot, State.MaschineTot)
            .Permit(Trigger.VentilGeschlossen, State.Aus)
            .Permit(Trigger.Reset, State.Reset);

        stateMachine.Configure(State.Reset)
            .OnEntry(() => _stateReset.OnEntry())
            .Permit(Trigger.Neustart, State.Aus);

        stateMachine.OnUnhandledTrigger((_, _) =>
        {
            Console.WriteLine("Unhandled:");
        });

        var graph = UmlDotGraph.Format(stateMachine.GetInfo());
        Console.Write("\n \n" + graph + "\n \n");

        return stateMachine;
    }
}