FUNCTION_BLOCK TestProgramm
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

IF (NOT #StoerungAktiv) AND "DigInput".S2 THEN
    #KompressorEin := TRUE;
END_IF;

IF #StoerungAktiv OR (NOT "DigInput".S1) OR (NOT "DigInput".B2) OR (NOT "DigInput".F1) THEN
    #KompressorEin := FALSE;
END_IF;

IF #KompressorEin AND "DigInput".B1 THEN
    "DigOutput".Q1 := true;
END_IF;

IF NOT #KompressorEin OR NOT "DigInput".B1 THEN
    "DigOutput".Q1 := false;
END_IF;

#SternDreieckAnlauf(IN := "DigOutput".Q1,
                    PT := T#2s);

"DigOutput".Q2 := "DigOutput".Q1 AND (#SternDreieckAnlauf.ET < T#1s);
"DigOutput".Q3 := "DigOutput".Q1 AND (#SternDreieckAnlauf.ET > T#1s500ms);

IF "DigInput".B2 AND "DigInput".F1 AND (NOT  "DigInput".S1) THEN
    #StoerungAktiv := FALSE;
END_IF;

IF (NOT "DigInput".B2) OR (NOT "DigInput".F1) THEN
    #StoerungAktiv := TRUE;
END_IF;

"DigOutput".P1 := (NOT "DigInput".B2) OR (NOT "DigInput".F1) OR ("Clock_1.25Hz" AND #StoerungAktiv);
"DigOutput".P2 := NOT #StoerungAktiv AND NOT "DigOutput".Q1;

