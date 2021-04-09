FUNCTION_BLOCK TestProgramm
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


#PumpeHandEin := "DigInput".S1 AND (NOT #StoerungAktiv);

#Ueberschwappen(IN := "DigInput".B1,
                PT := T#1s);

IF (#PumpeAutomatikEin AND #Ueberschwappen.Q) OR #StoerungAktiv OR (NOT "DigInput".S2) THEN
    #PumpeAutomatikEin := FALSE;
END_IF;

IF (NOT #PumpeAutomatikEin) AND (NOT "DigInput".B2) AND "DigInput".S2 AND (NOT #StoerungAktiv) THEN
    #PumpeAutomatikEin := TRUE;
END_IF;


"DigOutput".Q1 := #PumpeHandEin OR #PumpeAutomatikEin;
"DigOutput".P1 := "DigOutput".Q1;

IF NOT "DigInput".F1 THEN
    #StoerungAktiv := TRUE;
END_IF;

IF "DigInput".F1 AND "DigInput".S3 THEN
    #StoerungAktiv := FALSE;
END_IF;

"DigOutput".P2 := (NOT "DigInput".F1) OR (#StoerungAktiv AND "Clock_1Hz");