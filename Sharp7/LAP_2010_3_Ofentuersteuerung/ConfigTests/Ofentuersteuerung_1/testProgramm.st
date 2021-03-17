FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    ZyklusAktiv: BOOL;
	Zeit: TON;
END_VAR

(*  *********************************************************** *)
(*  Linearachse: Motorschaltungen 1 - Version 1                 *)
(*  *********************************************************** *)

IF "DigInput".S1 THEN
    #ZyklusAktiv := true;
END_IF;

IF "Zeit".Q THEN
    #ZyklusAktiv := false;
END_IF;

"Zeit".TON(IN := #ZyklusAktiv, PT := T#4s);
"DigOutput".P4 := #ZyklusAktiv;
"DigOutput".Q1 := FALSE;    // Die Ausgänge werden danach - eventuell - überschrieben
"DigOutput".Q2 := FALSE;

IF #ZyklusAktiv THEN
    
    IF "Zeit".ET < T#2s THEN
        "DigOutput".Q1 := TRUE;
    END_IF;
    IF "Zeit".ET > T#2s THEN
        "DigOutput".Q2 := TRUE;
    END_IF;
END_IF;
    