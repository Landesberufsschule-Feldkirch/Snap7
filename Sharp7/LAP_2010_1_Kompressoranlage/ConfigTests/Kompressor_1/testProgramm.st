FUNCTION_BLOCK DeleteMe

VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    StoerungAktiv: BOOL;
    KompressorEin: BOOL;
	SternDreieckAnlauf: TON;
END_VAR


(*  *********************************************************** *)
(*  LAP 2010/1 Kompressoranlage                                 *)
(*  *********************************************************** *)

IF (NOT StoerungAktiv) AND SimDI.S2 THEN
    KompressorEin := TRUE;
END_IF;

IF StoerungAktiv OR (NOT SimDI.S1) OR (NOT SimDI.B2) OR (NOT SimDI.F1) THEN
    KompressorEin := FALSE;
END_IF;

IF KompressorEin AND SimDI.B1 THEN
    SimDA.Q1 := TRUE;
END_IF;

IF NOT KompressorEin OR NOT SimDI.B1 THEN
    SimDA.Q1 := FALSE;
END_IF;

SternDreieckAnlauf(IN := SimDA.Q1,                    PT := T#2S);

SimDA.Q2 := SimDA.Q1 AND (SternDreieckAnlauf.ET < T#1S);
SimDA.Q3 := SimDA.Q1 AND (SternDreieckAnlauf.ET > T#1S500MS);

IF SimDI.B2 AND SimDI.F1 AND (NOT  SimDI.S1) THEN
    StoerungAktiv := FALSE;
END_IF;

IF (NOT SimDI.B2) OR (NOT SimDI.F1) THEN
    StoerungAktiv := TRUE;
END_IF;

SimDA.P1 := (NOT SimDI.B2) OR (NOT SimDI.F1) OR (Blinker.Clock_1_25Hz AND StoerungAktiv);
SimDA.P2 := NOT StoerungAktiv AND NOT KompressorEin;
