FUNCTION_BLOCK DeleteMe

VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    StoerungAktiv: BOOL;
    PumpeHandEin: BOOL;
    PumpeAutomatikEin: BOOL;
	Ueberschwappen: TON;
END_VAR


(*  *********************************************************** *)
(*  LAP 2010/5 Pumpensteuerung                                  *)
(*  *********************************************************** *)


PumpeHandEin := SimDI.S1 AND (NOT StoerungAktiv);

Ueberschwappen(IN := SimDI.B1, PT := T#1S);

IF (PumpeAutomatikEin AND Ueberschwappen.Q) OR StoerungAktiv OR (NOT SimDI.S2) THEN
    PumpeAutomatikEin := FALSE;
END_IF;

IF (NOT PumpeAutomatikEin) AND (NOT SimDI.B2) AND SimDI.S2 AND (NOT StoerungAktiv) THEN
    PumpeAutomatikEin := TRUE;
END_IF;

SimDA.Q1 := PumpeHandEin OR PumpeAutomatikEin;
SimDA.P1 := SimDA.Q1;

IF NOT SimDI.F1           THEN    StoerungAktiv := TRUE;     END_IF;
IF SimDI.F1 AND SimDI.S3  THEN    StoerungAktiv := FALSE;    END_IF;

SimDA.P2 := (NOT SimDI.F1) OR (StoerungAktiv AND Blinker.Clock_1Hz);